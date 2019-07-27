using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cryptozon.Domain;
using Cryptozon.Domain.Users;

namespace Cryptozon.ApplicationService.Purchasing
{
  public class ProductsPurchase
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

      var user = await _userRepo.GetUserAsync(username);
      var purchaseConfirmation = await _purchasesRepo.PurchaseAsync(user.UserId, coins);
      // send notification - todo: Domain event
      return purchaseConfirmation;
    }
  }
}