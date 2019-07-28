using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Cryptozon.ApplicationService.Users;
using Cryptozon.Domain.Users;
using Microsoft.AspNetCore.Http;
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

  public class UserRequest
  {
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
  }
}