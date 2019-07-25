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

  public interface IProductsRepo
  {
    Task<IEnumerable<Product>> GetProductsAsync();
  }

  public class ProductsRetrieval
  {
    private readonly IProductsRepo _productsRepo;

    public ProductsRetrieval(IProductsRepo productsRepo)
    {
      _productsRepo = productsRepo;
    }

    public async Task<IEnumerable<Product>> RetrieveProductsAsync()
    {
      return await _productsRepo.GetProductsAsync();
    }
  }

  public class Product
  {
  }
}
