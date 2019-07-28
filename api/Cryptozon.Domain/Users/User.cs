using System;

namespace Cryptozon.Domain.Users
{
  public class User
  {
    public User()
    {
    }

    public User(string firstName, string surname, string username, string passwordSalt,
                string passwordHash,
                Guid? userId = null)
    {
      FirstName = firstName;
      Surname = surname;
      Username = username;
      PasswordSalt = passwordSalt;
      PasswordHash = passwordHash;
      UserId = userId.GetValueOrDefault();
    }

    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string PasswordSalt { get; set; }
    public string PasswordHash { get; set; }
    public Guid UserId { get; set; } = Guid.NewGuid();
  }
}