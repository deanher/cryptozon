using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cryptozon.Domain.Products;
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
      var mockProductsRepo = new Mock<IProductsRepo>();

      var expectedProducts = new List<Product>
                             {
                               Product.Create(1, "Bitcoin", "BTC", 9574.94033318m),
                               Product.Create(1027, "Ethereum", "ETH", 209.770552851m),
                             };
      mockProductsRepo.Setup(repo => repo.GetProductsAsync())
                      .ReturnsAsync(expectedProducts);

      var mockPurchaseRepo = new Mock<IPurchasesRepo>();

      var purchasedCoins = new List<(int CoinId, decimal Quantity, decimal UnitPrice)>
                           {
                             (1, 0.005m, 9574.94033318m),
                             (1027, 0.05m, 209.770552851m)
                           };

      var expected = PurchaseConfirmation.Create(purchasedCoins.Sum(i => i.Quantity * i.UnitPrice));
      mockPurchaseRepo.Setup(repo => repo.PurchaseAsync(It.IsAny<string>(),
                                                        It.IsAny<IEnumerable<(int CoinId, decimal Quantity, decimal UnitPrice)>>()));

      var productsPurchase = new ProductsPurchase(mockProductsRepo.Object, mockPurchaseRepo.Object);

      //when
      var purchaseConfirmation = await productsPurchase.MakePurchaseAsync("deanher@gmail.com",
                                                                          purchasedCoins);

      //then
      Assert.NotNull(purchaseConfirmation);
      Assert.NotNull(purchaseConfirmation.Reference); // Guids will be different so can't compare
      Assert.IsType<Guid>(Guid.Parse(purchaseConfirmation.Reference)); 
      Assert.Equal(expected.TotalAmount, purchaseConfirmation.TotalAmount);
    }
  }

  public class PurchaseConfirmation
  {
    private PurchaseConfirmation(decimal totalAmount, string reference)
    {
      TotalAmount = totalAmount;
      Reference = reference;
    }

    public static PurchaseConfirmation Create(decimal totalAmount, string reference = null)
    {
      return new PurchaseConfirmation(totalAmount, reference ?? Guid.NewGuid().ToString("N"));
    }

    public string Reference { get; }
    public decimal TotalAmount { get; }
  }

  public class ProductsPurchase
  {
    private readonly IProductsRepo _productsRepo;
    private readonly IPurchasesRepo _purchasesRepo;

    public ProductsPurchase(IProductsRepo productsRepo, IPurchasesRepo purchasesRepo)
    {
      _productsRepo = productsRepo;
      _purchasesRepo = purchasesRepo;
    }

    public async Task<PurchaseConfirmation> MakePurchaseAsync(string userId, IEnumerable<(int CoinId, decimal Quantity, decimal UnitPrice)> coins)
    {
      // record purchase
      // send notification - todo: Domain event
      return PurchaseConfirmation.Create(coins.Sum(i => i.Quantity * i.UnitPrice));
    }
  }

  public interface IPurchasesRepo
  {
    Task PurchaseAsync(string userId, IEnumerable<(int CoinId, decimal Quantity, decimal UnitPrice)> coins);
  }
}
