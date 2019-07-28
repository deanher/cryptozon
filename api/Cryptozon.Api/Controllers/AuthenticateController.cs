using System.Net;
using System.Threading.Tasks;
using Cryptozon.ApplicationService.Users;
using Cryptozon.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Cryptozon.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
      private readonly IUsersRepo _usersRepo;
      private readonly IOptions<AppSettings> _appSettings;

      public AuthenticateController(IUsersRepo usersRepo, IOptions<AppSettings> appSettings)
      {
        _usersRepo = usersRepo;
        _appSettings = appSettings;
      }

      [HttpPost]
      public async Task<IActionResult> Post(AuthenticationRequest requestObj)
      {
        var usersAuthentication = new UsersAuthentication(_usersRepo, _appSettings.Value.Secret, _appSettings.Value.TokenExpiry);

        var authenticatedUser = await usersAuthentication.AuthenticateAsync(requestObj.Username, requestObj.Password);
        if (usersAuthentication.HasError)
          return new ObjectResult(usersAuthentication.ErrorMessage)
                 {StatusCode = (int) HttpStatusCode.InternalServerError};

        return Ok(authenticatedUser);
      }
    }
}