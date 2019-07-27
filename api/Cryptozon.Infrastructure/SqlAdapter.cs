using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Cryptozon.Infrastructure
{
  public class SqlAdapter : IDatabaseAdapter
  {
    private readonly string _connectionString;

    public SqlAdapter(string connectionString)
    {
      _connectionString = connectionString;
    }

    public async Task ExecuteAsync(string sqlQuery, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
    {
      using (var db = new SqlConnection(_connectionString))
      {
        await db.OpenAsync();
        await db.ExecuteAsync(sqlQuery, parameters, commandType: commandType);
        db.Close();
      }
    }

    public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sqlQuery, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure) where T : new()
    {
      using (var db = new SqlConnection(_connectionString))
      {
        await db.OpenAsync();
        var result = await db.QueryAsync<T>(sqlQuery, parameters, commandType: commandType);
        db.Close();
        return result;
      }
    }
  }
}