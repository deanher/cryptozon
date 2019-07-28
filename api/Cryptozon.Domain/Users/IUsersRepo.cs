using System.Threading.Tasks;

namespace Cryptozon.Domain.Users
{
  public interface IUsersRepo
  {
    Task<User> GetUserAsync(string username);
  }
}