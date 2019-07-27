using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cryptozon.Domain
{
  public interface IPurchasesRepo
  {
    Task<PurchaseConfirmation> PurchaseAsync(string userId,
                                             IEnumerable<(int CoinId, decimal Quantity, decimal UnitPrice)> coins);
  }
}