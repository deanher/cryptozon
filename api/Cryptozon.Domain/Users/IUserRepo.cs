using System.Threading.Tasks;

namespace Cryptozon.Domain.Users
{
  public interface IUserRepo
  {
    Task<User> GetUserAsync(string username);
  }
}