using System.Linq;
using System.Threading.Tasks;
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

      var expected = Task.FromResult(new[] { new Product() }.AsEnumerable());
      mock.Setup(repo => repo.GetProductsAsync())
          .Returns(expected);

      var productRetrieval = new ProductsRetrieval(mock.Object);

      //when
      var products = await productRetrieval.RetrieveProductsAsync();

      //then
      Assert.NotNull(products);
      Assert.NotEmpty(products);
    }
  }
}
