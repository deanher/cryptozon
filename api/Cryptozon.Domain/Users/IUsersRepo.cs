using System.Threading.Tasks;

namespace Cryptozon.Domain.Users
{
  public interface IUsersRepo
  {
    Task<User> GetUserAsync(string username);
    Task<User> RegisterUserAsync(string firstName, string surname, string username, string passwordSalt,
                                 string passwordHash);
  }
}