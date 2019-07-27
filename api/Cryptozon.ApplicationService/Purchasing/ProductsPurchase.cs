﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cryptozon.Domain;

namespace Cryptozon.ApplicationService.Purchasing
{
  public class ProductsPurchase
  {
    private readonly IPurchasesRepo _purchasesRepo;

    public ProductsPurchase(IPurchasesRepo purchasesRepo)
    {
      _purchasesRepo = purchasesRepo;
    }

    public async Task<PurchaseConfirmation> MakePurchaseAsync(string userId,
                                                              IEnumerable<(int CoinId, decimal Quantity, decimal
                                                                UnitPrice)> coins)
    {
      // record purchase
      // send notification - todo: Domain event
      return PurchaseConfirmation.Create(coins.Sum(i => i.Quantity * i.UnitPrice));
    }
  }
}