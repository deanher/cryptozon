using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Cryptozon.Domain.Users;
using Serilog;

namespace Cryptozon.ApplicationService.Users
{
  public class UsersRegistration : ApplicationServiceBase
  {
    private readonly IUsersRepo _usersRepo;

    public UsersRegistration(IUsersRepo usersRepo)
    {
      _usersRepo = usersRepo;
    }

    public async Task<User> RegisterAsync(string firstName, string surname, string username, string password)
    {
      try
      {
        var (salt, hash) = GeneratePasswordHash(password);
        var user = await _usersRepo.RegisterUserAsync(firstName, surname, username, salt, hash);
        return user;
      }
      catch (Exception ex)
      {
        ErrorMessage = "Oops! We were unable to register you at this time. Please try again.";
        Log.Error(ex, ErrorMessage);
      }

      return null;
    }

    private (string Salt, string Hash) GeneratePasswordHash(string password)
    {
      using (var hmac = new HMACSHA512())
      {
        var salt = hmac.Key;
        var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        return (Convert.ToBase64String(salt), Convert.ToBase64String(hash));
      }
    }
  }
}