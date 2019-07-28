using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptozon.Domain.Purchases;
using Cryptozon.Domain.Users;
using Serilog;

namespace Cryptozon.ApplicationService.Purchases
{
  public class ProductsPurchase : ApplicationServiceBase
  {
    private readonly IPurchasesRepo _purchasesRepo;
    private readonly IUsersRepo _usersRepo;

    public ProductsPurchase(IPurchasesRepo purchasesRepo, IUsersRepo usersRepo)
    {
      _purchasesRepo = purchasesRepo;
      _usersRepo = usersRepo;
    }

    public async Task<PurchaseConfirmation> MakePurchaseAsync(string username,
                                                              IEnumerable<(int CoinId, decimal Quantity, decimal UnitPrice)> coins)
    {
      try
      {
        var user = await _usersRepo.GetUserAsync(username);
        var purchaseConfirmation = await _purchasesRepo.PurchaseAsync(user.UserId, coins);
        // send notification - todo: Domain event
        return purchaseConfirmation;
      }
      catch (Exception ex)
      {
        ErrorMessage = "Your purchase chould not be completed at this time. Please try again later.";
        Log.Error(ex, ErrorMessage);
      }

      return null;
    }
  }
}