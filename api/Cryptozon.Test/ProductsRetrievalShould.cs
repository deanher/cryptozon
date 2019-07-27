using System;
using System.Linq;
using System.Threading.Tasks;
using Cryptozon.ApplicationService;
using Cryptozon.Domain.Products;
using Moq;
using Xunit;

namespace Cryptozon.Test
{
  public class ProductsRetrievalShould
  {
    [Fact]
    public async Task RetrieveProducts()
    {
      //given
      var mock = new Mock<IProductsRepo>();

      var expected = new[] { Product.Create("Bitcoin", "BTC", 10087.3779318m, DateTime.Now) }.AsEnumerable();
      mock.Setup(repo => repo.GetProductsAsync())
          .ReturnsAsync(expected);

      var productRetrieval = new ProductsRetrieval(mock.Object);

      //when
      var products = await productRetrieval.RetrieveProductsAsync();

      //then
      Assert.NotNull(products);
      Assert.NotEmpty(products);
    }
  }
}
