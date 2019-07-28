using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cryptozon.Domain.Products;
using Serilog;

namespace Cryptozon.ApplicationService
{
  public class ProductsRetrieval : ApplicationServiceBase
  {
    private readonly IProductsRepo _productsRepo;

    public ProductsRetrieval(IProductsRepo productsRepo)
    {
      _productsRepo = productsRepo;
    }

    public async Task<IEnumerable<Product>> RetrieveProductsAsync()
    {
      try
      {
        return await _productsRepo.GetProductsAsync();
      }
      catch (Exception ex)
      {
        ErrorMessage = "Our apologies! We could not retrieve available cryptocurrencies at this time. Please try again.";
        Log.Error(ex, ErrorMessage);
      }

      return Enumerable.Empty<Product>();
    }
  }
}