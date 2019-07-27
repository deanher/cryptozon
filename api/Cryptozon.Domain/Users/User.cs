using System;

namespace Cryptozon.Domain.Users
{
  public class User
  {
    private User(string username, string passwordSalt, string passworHash, Guid userId)
    {
      Username = username;
      PasswordSalt = passwordSalt;
      PassworHash = passworHash;
      UserId = userId;
    }

    public string Username { get; }
    public string PasswordSalt { get; }
    public string PassworHash { get; }
    public Guid UserId { get; set; }

    public static User Create(string username, string passwordSalt, string passworHash, Guid? userId = null)
    {
      return new User(username, passwordSalt, passworHash, userId ?? Guid.NewGuid());
    }
  }
}