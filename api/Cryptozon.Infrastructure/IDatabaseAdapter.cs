using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

  public class SqlAdapter : IDatabaseAdapter, IDisposable
  {
    private readonly string _connectionString;
    private readonly SqlConnection _db;

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
      }
    }

    public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sqlQuery, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure) where T:new()
    {
      using (var db = new SqlConnection(_connectionString))
      {
        await db.OpenAsync();
        return await db.QueryAsync<T>(sqlQuery, parameters, commandType: commandType);
      }
    }

    public void Dispose()
    {
      _db.Close();
      _db?.Dispose();
    }
  }
}