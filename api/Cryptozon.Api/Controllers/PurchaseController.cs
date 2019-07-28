using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Cryptozon.Api.Models;
using Cryptozon.ApplicationService.Purchases;
using Cryptozon.Domain.Purchases;
using Cryptozon.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cryptozon.Api.Controllers
{
  [Authorize]
  [Route("api/v1/[controller]")]
  [ApiController]
  public class PurchaseController : ControllerBase
  {
    private readonly IPurchasesRepo _purchasesRepo;
    private readonly IUsersRepo _usersRepo;

    public PurchaseController(IPurchasesRepo purchasesRepo, IUsersRepo usersRepo)
    {
      _purchasesRepo = purchasesRepo;
      _usersRepo = usersRepo;
    }

    [HttpPost]
    public async Task<IActionResult> Post(IEnumerable<PurchaseRequest> requestObj)
    {
      var username = "deanher@gmail.com";
      var productsPurchase = new ProductsPurchase(_purchasesRepo, _usersRepo);
      var purchaseConfirmation =
        await productsPurchase.MakePurchaseAsync(username, requestObj?.Select(r => (r.CoinId, r.Quantity, r.UnitPrice)));

      if (productsPurchase.HasError)
        return new ObjectResult(productsPurchase.ErrorMessage) {StatusCode = (int) HttpStatusCode.InternalServerError};

      return Ok(purchaseConfirmation);
    }
  }
}