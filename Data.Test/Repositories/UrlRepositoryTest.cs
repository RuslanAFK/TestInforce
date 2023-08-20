using Data.Repositories;
using Domain.Models;

namespace Data.Test.Repositories;

public class UrlRepositoryTest
{
    private AppDbContext dbContext;
    private UrlRepository repository;
    [SetUp]
    public void Setup()
    {
        var options = DataGenerator.CreateNewInMemoryDatabase();
        dbContext = new AppDbContext(options);
        repository = new UrlRepository(dbContext);
    }
    [TearDown]
    public void TearDown()
    {
        dbContext.Database.EnsureDeleted();
        dbContext.Dispose();
    }

    [Test]
    public async Task GetAllAsync_ReturnsAllItems()
    {
        var urls = await repository.GetAllAsync();
        Assert.That(urls.Count, Is.EqualTo(0));
    }

    [Test]
    public  async Task AddAsync_AddsUrl()
    {
        var url = new Url
        {
            CreatedDate = DateTime.Now, ShortAddress = "", FullAddress = "", Description = "",
            Id = 1
        };
        await repository.AddAsync(url);

        var foundUrl = await dbContext.Urls.FindAsync(url.Id);
        Assert.That(url.ShortAddress, Is.EqualTo(foundUrl?.ShortAddress));
    }
}