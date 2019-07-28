using System.Net;
using System.Threading.Tasks;
using Cryptozon.ApplicationService;
using Cryptozon.Domain.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cryptozon.Api.Controllers
{
  [Authorize]
  [Route("api/v1/[controller]")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    private readonly IProductsRepo _productsRepo;

    public ProductsController(IProductsRepo productsRepo)
    {
      _productsRepo = productsRepo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var service = new ProductsRetrieval(_productsRepo);
      var result = await service.RetrieveProductsAsync();
      if (service.HasError)
        return new ObjectResult(service.ErrorMessage) { StatusCode = (int)HttpStatusCode.InternalServerError };

      return Ok(result);
    }
  }
}