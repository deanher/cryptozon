using System;

namespace Cryptozon.Domain.Users
{
  public class User
  {
    public User() { }

    public User(string username, string passwordSalt, string passwordHash, Guid? userId = null)
    {
      Username = username;
      PasswordSalt = passwordSalt;
      PasswordHash = passwordHash;
      UserId = userId.GetValueOrDefault();
    }

    public string Username { get; set; }
    public string PasswordSalt { get; set; }
    public string PasswordHash { get; set; }
    public Guid UserId { get; set; } = Guid.NewGuid();
  }
}