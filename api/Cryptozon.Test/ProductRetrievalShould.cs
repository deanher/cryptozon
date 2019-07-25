using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Cryptozon.Test
{
  public class ProductRetrievalShould
  {
    [Fact]
    public async Task RetrieveProducts()
    {
      //given
      var productRetrieval = new ProductRetrieval();

      //when
      var products = await productRetrieval.RetrieveProductsAsync();

      //then
      Assert.NotNull(products);
      Assert.NotEmpty(products);
    }
  }

  public class ProductRetrieval
  {
    public async Task<IEnumerable<Product>> RetrieveProductsAsync()
    {
      return Enumerable.Empty<Product>();
    }
  }

  public class Product
  {
  }
}
