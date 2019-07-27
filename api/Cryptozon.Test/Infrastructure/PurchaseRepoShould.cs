using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cryptozon.Domain;
using Cryptozon.Infrastructure;
using Dapper;
using Moq;
using Xunit;

namespace Cryptozon.Test.Infrastructure
{
  public class PurchaseRepoShould
  {
    [Fact]
    public async Task AddPurchase()
    {
      //given
      var mockDatabaseAdaper = new Mock<IDatabaseAdapter>();

      var purchasedCoins = new List<(int CoinId, decimal Quantity, decimal UnitPrice)>
                           {
                             (1, 0.005m, 9574.94033318m),
                             (1027, 0.05m, 209.770552851m)
                           };

      var expected = PurchaseConfirmation.Create(purchasedCoins.Sum(i => i.Quantity * i.UnitPrice), Guid.NewGuid());
      mockDatabaseAdaper.Setup(repo => repo.ExecuteAsync(It.IsAny<string>(),
                                                         It.IsAny<DynamicParameters>()));

      var purchasesRepo = new PurchasesRepo(mockDatabaseAdaper.Object);

      //when
      var purchaseConfirmation = await purchasesRepo.PurchaseAsync(Guid.NewGuid(),
                                                                      purchasedCoins);

      //then
      Assert.NotNull(purchaseConfirmation);
      Assert.IsType<Guid>(purchaseConfirmation.Reference);// Guids will be different so can't compare
      Assert.Equal(expected.TotalAmount, purchaseConfirmation.TotalAmount);
    }
  }
}