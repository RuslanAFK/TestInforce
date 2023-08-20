using Microsoft.EntityFrameworkCore;

namespace Data.Test;

public static class DataGenerator
{
    public static DbContextOptions<AppDbContext> CreateNewInMemoryDatabase()
    {
        return new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
    }

}