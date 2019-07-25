using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptozon.Domain.Products;

namespace Cryptozon.Test
{
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
}