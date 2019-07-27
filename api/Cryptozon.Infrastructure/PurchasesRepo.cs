using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cryptozon.Domain.Purchases;
using Dapper;

namespace Cryptozon.Infrastructure
{
  public class PurchasesRepo : IPurchasesRepo
  {
    private readonly IDatabaseAdapter _database;

    public PurchasesRepo(IDatabaseAdapter database)
    {
      _database = database;
    }

    public async Task<PurchaseConfirmation> PurchaseAsync(Guid userId,
                                                          IEnumerable<(int CoinId, decimal Quantity, decimal UnitPrice)>
                                                            coins)
    {
      var reference = Guid.NewGuid();
      var coinList = coins.ToList();
      var allTasks = GetAllPurchaseTasks(reference, userId, coinList);

      await allTasks;

      if (allTasks.Exception != null)
        throw new ApplicationException(allTasks.Exception.Message, allTasks.Exception);

      return PurchaseConfirmation.Create(coinList.Sum(c => c.Quantity * c.UnitPrice), reference);
    }

    private Task PurchaseAsync(Guid reference, Guid userId, int coinId, decimal quantity, decimal unitPrice)
    {
      var param = new DynamicParameters(new {reference, userId, coinId, quantity, unitPrice});
      return _database.ExecuteAsync("sp_purchase_add", param);
    }

    private Task GetAllPurchaseTasks(Guid reference, Guid userId,
                                     IEnumerable<(int CoinId, decimal Quantity, decimal UnitPrice)> coinList)
    {
      var tasks = new List<Task>();
      foreach (var (coinId, quantity, unitPrice) in coinList)
      {
        tasks.Add(PurchaseAsync(reference, userId, coinId, quantity, unitPrice));
      }

      return Task.WhenAll(tasks);
    }
  }
}