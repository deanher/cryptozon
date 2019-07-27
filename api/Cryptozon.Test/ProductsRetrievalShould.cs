using System.Collections.Generic;
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

      var expected = new List<Product> { Product.Create(1, "Bitcoin", "BTC", 10087.3779318m) };
      mock.Setup(repo => repo.GetProductsAsync())
          .ReturnsAsync(expected);

      var productRetrieval = new ProductsRetrieval(mock.Object);

      //when
      var products = (await productRetrieval.RetrieveProductsAsync()).ToList();

      //then
      Assert.NotNull(products);
      Assert.NotEmpty(products);
      Assert.Equal(expected.First(), products.First());
    }
  }
}
