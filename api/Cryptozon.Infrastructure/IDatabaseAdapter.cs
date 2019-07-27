using System.Threading.Tasks;
using Dapper;

namespace Cryptozon.Infrastructure
{
  public interface IDatabaseAdapter
  {
    Task ExecuteAsync(string sqlQuery, DynamicParameters parameters);
    Task<T> ExecuteAsync<T>(string sqlQuery, DynamicParameters parameters);
  }
}