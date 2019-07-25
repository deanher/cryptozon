using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
      var productRetrieval = new ProductsRetrieval();

      //when
      var products = await productRetrieval.RetrieveProductsAsync();

      //then
      Assert.NotNull(products);
      Assert.NotEmpty(products);
    }
  }

  public interface IProductsRepo
  {
  }

  public class ProductsRetrieval
  {
    public async Task<IEnumerable<Product>> RetrieveProductsAsync()
    {
      return new[] { new Product() };
    }
  }

  public class Product
  {
  }
}
