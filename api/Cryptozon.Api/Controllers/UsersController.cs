using System.Net;
using System.Threading.Tasks;
using Cryptozon.Api.Models;
using Cryptozon.ApplicationService.Users;
using Cryptozon.Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace Cryptozon.Api.Controllers
{
  [Route("api/v1/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IUsersRepo _usersRepo;

    public UsersController(IUsersRepo usersRepo)
    {
      _usersRepo = usersRepo;
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserRequest requestObj)
    {
      var userRegistration = new UsersRegistration(_usersRepo);

      await userRegistration.RegisterAsync(requestObj.FirstName, requestObj.Surname, requestObj.Username,
                                           requestObj.Password);
      if (userRegistration.HasError)
        return new ObjectResult(userRegistration.ErrorMessage) {StatusCode = (int) HttpStatusCode.InternalServerError};

      return Ok();
    }
  }
}