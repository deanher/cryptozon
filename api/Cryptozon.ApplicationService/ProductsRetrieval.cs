using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cryptozon.Domain.Products;
using Serilog;

namespace Cryptozon.ApplicationService
{
  public class ProductsRetrieval
  {
    private readonly IProductsRepo _productsRepo;

    public ProductsRetrieval(IProductsRepo productsRepo)
    {
      _productsRepo = productsRepo;
    }

    public bool HasError => string.IsNullOrWhiteSpace(ErrorMessage);
    public string ErrorMessage { get; private set; }

    public async Task<IEnumerable<Product>> RetrieveProductsAsync()
    {
      try
      {
        return await _productsRepo.GetProductsAsync();
      }
      catch (Exception ex)
      {
        ErrorMessage = ex.Message;
        Log.Error(ex, ErrorMessage);
      }

      return Enumerable.Empty<Product>();
    }
  }
}