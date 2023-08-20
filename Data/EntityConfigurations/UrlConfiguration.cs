using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigurations;

public class UrlConfiguration : IEntityTypeConfiguration<Url>
{
    public void Configure(EntityTypeBuilder<Url> builder)
    {
        builder.HasKey(i => i.Id);
        builder.HasIndex(i => i.ShortAddress)
            .IsUnique();
        builder.HasIndex(i => i.FullAddress)
            .IsUnique();
        builder.HasOne<User>()
            .WithMany(u => u.Urls)
            .HasForeignKey(url => url.UserId);
    }
}