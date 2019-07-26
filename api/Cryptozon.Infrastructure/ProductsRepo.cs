using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptozon.Domain.Products;

namespace Cryptozon.Infrastructure
{
  public class ProductsRepo : IProductsRepo
  {
    public ProductsRepo(string baseUrl, string key)
    {
      
    }

    public Task<IEnumerable<Product>> GetProductsAsync()
    {
      throw new System.NotImplementedException();
    }
  }
}