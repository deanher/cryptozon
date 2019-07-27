using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cryptozon.Domain;
using Cryptozon.Domain.Purchases;
using Cryptozon.Domain.Users;
using Serilog;

namespace Cryptozon.ApplicationService.Purchasing
{
  public class ProductsPurchase : ApplicationServiceBase
  {
    private readonly IPurchasesRepo _purchasesRepo;
    private readonly IUserRepo _userRepo;

    public ProductsPurchase(IPurchasesRepo purchasesRepo, IUserRepo userRepo)
    {
      _purchasesRepo = purchasesRepo;
      _userRepo = userRepo;
    }

    public async Task<PurchaseConfirmation> MakePurchaseAsync(string username,
                                                              IEnumerable<(int CoinId, decimal Quantity, decimal UnitPrice)> coins)
    {

      try
      {
        var user = await _userRepo.GetUserAsync(username);
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