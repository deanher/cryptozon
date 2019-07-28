using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cryptozon.ApplicationService.Purchases;
using Cryptozon.Domain.Purchases;
using Cryptozon.Domain.Users;
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

      var expectedPurchaseConfirmation = PurchaseConfirmation.Create(purchasedCoins.Sum(i => i.Quantity * i.UnitPrice), Guid.NewGuid());
      mockPurchaseRepo.Setup(repo => repo.PurchaseAsync(It.IsAny<Guid>(),
                                                        It.IsAny<IEnumerable<(int CoinId, decimal Quantity, decimal UnitPrice)>>()))
                      .ReturnsAsync(expectedPurchaseConfirmation);

      var mockUserRepo = new Mock<IUsersRepo>();
      var expectedUser = new User("deanher@gmail.com", Guid.NewGuid().ToString("N"), Guid.NewGuid().ToString("N"), Guid.NewGuid());
      mockUserRepo.Setup(repo => repo.GetUserAsync(It.IsAny<string>()))
                  .ReturnsAsync(expectedUser); 
      var productsPurchase = new ProductsPurchase(mockPurchaseRepo.Object, mockUserRepo.Object);

      //when
      var purchaseConfirmation = await productsPurchase.MakePurchaseAsync("deanher@gmail.com",
                                                                          purchasedCoins);

      //then
      Assert.NotNull(purchaseConfirmation);
      Assert.IsType<Guid>(purchaseConfirmation.Reference); // Guids will be different so can't compare
      Assert.Equal(expectedPurchaseConfirmation.TotalAmount, purchaseConfirmation.TotalAmount);
    }
  }
}
