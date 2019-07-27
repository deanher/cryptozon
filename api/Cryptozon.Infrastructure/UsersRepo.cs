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

      var result = await _database.ExecuteAsync<User>("sp_user_get", param);
      return new User(result.Username, result.PasswordSalt, result.PasswordHash, result.UserId);
    }
  }
}