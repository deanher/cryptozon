using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Cryptozon.Infrastructure
{
  public interface IDatabaseAdapter
  {
    Task ExecuteAsync(string sqlQuery, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

    Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sqlQuery, DynamicParameters parameters,
                                 CommandType commandType = CommandType.StoredProcedure) where T : new();
  }
}