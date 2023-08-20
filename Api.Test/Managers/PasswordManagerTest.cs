using Api.Managers;
using Domain.Exceptions;

namespace Api.Test.Managers;

public class PasswordManagerTest
{
    private PasswordManager passwordManager;
    [SetUp]
    public void Setup()
    {
        passwordManager = new PasswordManager();
    }
    [Test]
    public void ThrowExceptionIfWrongPassword_WithCorrectPassword_Passes()
    {
        var password = "password";
        var hashed = BCrypt.Net.BCrypt.HashPassword(password);
        passwordManager.ThrowExceptionIfWrongPassword(password, hashed);
        Assert.Pass();
    }
    [Test]
    public void ThrowExceptionIfWrongPassword_WithWrongPassword_ThrowsException()
    {
        var password = "password";
        var wrongPassword = "wrong";
        var hashed = BCrypt.Net.BCrypt.HashPassword(password);
        Assert.Throws<BaseException>(() =>
            passwordManager.ThrowExceptionIfWrongPassword(wrongPassword, hashed));
    }
    [Test]
    public void ThrowExceptionIfWrongPassword_WithNotHashedValue_ThrowsException()
    {
        var password = "password";
        var notHashedPassword = "not hashed at all";
        Assert.Throws<BaseException>(() =>
        {
            passwordManager.ThrowExceptionIfWrongPassword(password, notHashedPassword);
        });
    }
}