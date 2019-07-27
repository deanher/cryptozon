using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cryptozon.Domain.Products
{
  public interface IProductsRepo
  {
    Task<IEnumerable<Product>> GetProductsAsync();
  }
}