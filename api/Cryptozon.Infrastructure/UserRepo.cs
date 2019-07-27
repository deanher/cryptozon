using System.Data;
using System.Threading.Tasks;
using Cryptozon.Domain.Users;
using Dapper;

namespace Cryptozon.Infrastructure
{
  public class UserRepo : IUserRepo
  {
    private readonly IDatabaseAdapter _database;

    public UserRepo(IDatabaseAdapter database)
    {
      _database = database;
    }

    public async Task<User> GetUserAsync(string username)
    {
      var param = new DynamicParameters();
      param.Add(nameof(username), username);

      var result = await _database.ExecuteAsync<dynamic>("sp_user_get", param);
      return User.Create(result.Username, result.PasswordSalt, result.PasswordHash, result.UserId);
    }
  }
}