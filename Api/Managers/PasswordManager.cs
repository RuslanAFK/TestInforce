using Abstractions.Managers;
using BCrypt.Net;
using Domain.Exceptions;
using Domain.Models;

namespace Api.Managers;

public class PasswordManager : IPasswordManager
{
    public void SecureUser(User user)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Password = hashedPassword;
    }

    public void ThrowExceptionIfWrongPassword(string realPassword, string hashedPassword)
    {
        try
        {
            var passwordCorrect =
                BCrypt.Net.BCrypt.Verify(realPassword, hashedPassword);
            if (!passwordCorrect)
                throw new BaseException("You provided incorrect password.");
        }
        catch (SaltParseException)
        {
            throw new BaseException("Password is not parseable.");
        }
    }
}