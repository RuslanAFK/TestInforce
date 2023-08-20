using System.Data;
using System;
using Data.Repositories;
using Domain.Exceptions;
using Domain.Models;

namespace Data.Test.Repositories;

public class UnitOfWorkTest
{
    private AppDbContext dbContext;
    private UnitOfWork unitOfWork;
    [SetUp]
    public void Setup()
    {
        var options = DataGenerator.CreateNewInMemoryDatabase();
        dbContext = new AppDbContext(options);
        unitOfWork = new UnitOfWork(dbContext);
    }
    [TearDown]
    public void TearDown()
    {
        dbContext.Database.EnsureDeleted();
        dbContext.Dispose();
    }
    [Test]
    public void CompleteOrThrowAsync_NothingChangedToDb_ThrowsException()
    {
        Assert.ThrowsAsync<BaseException>(async () =>
        {
            await unitOfWork.CompleteOrThrowAsync();
        });
    }
    [Test]
    public void CompleteOrThrowAsync_ChangedDbWithCorrectData_DoesNotThrow()
    {
        var user = new User()
        {
            Id = 0, IsAdmin = false, Password = "", Username = ""
        };
        Assert.DoesNotThrowAsync(async () =>
        {
            await dbContext.Users.AddAsync(user);
            await unitOfWork.CompleteOrThrowAsync();
        });
    }
    [Test]
    public void CompleteOrThrowAsync_ChangeDbWithWrongData_ThrowsException()
    {
        var dummyUser = A.Dummy<User>();
        Assert.ThrowsAsync<BaseException>(async () =>
        {
            await dbContext.AddAsync(dummyUser);
            await unitOfWork.CompleteOrThrowAsync();
        });
    }
}