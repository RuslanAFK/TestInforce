using Domain.Models;

namespace Abstractions.Managers;

public interface IPasswordManager
{
    void SecureUser(User userToCreate);
    void ThrowExceptionIfWrongPassword(string userPassword, string password);
}