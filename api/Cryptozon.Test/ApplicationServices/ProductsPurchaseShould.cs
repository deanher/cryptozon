using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cryptozon.ApplicationService.Purchasing;
using Cryptozon.Domain;
using Moq;
using Xunit;

namespace Cryptozon.Test.ApplicationServices
{
  public class ProductsPurchaseShould
  {
    [Fact]
    public async Task MakePurchase()
    {
      //given
      var mockPurchaseRepo = new Mock<IPurchasesRepo>();

      var purchasedCoins = new List<(int CoinId, decimal Quantity, decimal UnitPrice)>
                           {
                             (1, 0.005m, 9574.94033318m),
                             (1027, 0.05m, 209.770552851m)
                           };

      var expected = PurchaseConfirmation.Create(purchasedCoins.Sum(i => i.Quantity * i.UnitPrice), Guid.NewGuid());
      mockPurchaseRepo.Setup(repo => repo.PurchaseAsync(It.IsAny<string>(),
                                                        It.IsAny<IEnumerable<(int CoinId, decimal Quantity, decimal UnitPrice)>>()))
                      .ReturnsAsync(expected);

      var productsPurchase = new ProductsPurchase(mockPurchaseRepo.Object);

      //when
      var purchaseConfirmation = await productsPurchase.MakePurchaseAsync("deanher@gmail.com",
                                                                          purchasedCoins);

      //then
      Assert.NotNull(purchaseConfirmation);
      Assert.IsType<Guid>(purchaseConfirmation.Reference); // Guids will be different so can't compare
      Assert.Equal(expected.TotalAmount, purchaseConfirmation.TotalAmount);
    }
  }
}
