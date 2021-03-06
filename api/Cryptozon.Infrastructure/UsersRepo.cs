using System.Linq;
using System.Threading.Tasks;
using Cryptozon.Domain.Users;
using Dapper;

namespace Cryptozon.Infrastructure
{
  public class UsersRepo : IUsersRepo
  {
    private readonly IDatabaseAdapter _database;

    public UsersRepo(IDatabaseAdapter database)
    {
      _database = database;
    }

    public async Task<User> GetUserAsync(string username)
    {
      var param = new DynamicParameters();
      param.Add(nameof(username), username);

      var result = await _database.ExecuteQueryAsync<User>("sp_user_get", param);
      return result.Single();
    }

    public async Task<User> RegisterUserAsync(string firstName, string surname, string username, string passwordSalt,
                                              string passwordHash)
    {
      var param = new DynamicParameters(new { firstName, surname, username, passwordSalt, passwordHash});

      await _database.ExecuteAsync("sp_user_add", param);

      return new User(firstName, surname, username, passwordSalt, passwordHash);
    }
  }
}