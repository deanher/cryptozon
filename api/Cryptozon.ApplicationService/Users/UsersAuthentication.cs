using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cryptozon.Domain.Users;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Cryptozon.ApplicationService.Users
{
  public class UsersAuthentication : ApplicationServiceBase
  {
    private readonly IUsersRepo _usersRepo;
    private readonly string _secret;
    private readonly int _tokenExpiry;

    public UsersAuthentication(IUsersRepo usersRepo, string secret, int tokenExpiry)
    {
      _usersRepo = usersRepo;
      _secret = secret;
      _tokenExpiry = tokenExpiry;
    }

    public async Task<AuthenticatedUser> AuthenticateAsync(string username, string password)
    {
      try
      {
        var user = await _usersRepo.GetUserAsync(username);
        if (user == null)
        {
          ErrorMessage = "Invalid username or password.";
          return null;
        }

        var validCredentials = VerifyPasswordHash(password, user.PasswordSalt, user.PasswordHash);
        if (!validCredentials)
        {
          ErrorMessage = "Invalid username or password.";
          return null;
        }

        var token = GenerateToken(user.Username, _secret, _tokenExpiry);

        return new AuthenticatedUser(user.Username, user.FirstName, user.Surname, token);
      }
      catch (Exception ex)
      {
        ErrorMessage = "Something went wrong while signing you in. Please try again.";
        Log.Error(ex, ErrorMessage);
      }

      return null;
    }

    private string GenerateToken(string userId, string secret, int tokenExpiry)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(secret);
      var tokenDescriptor = new SecurityTokenDescriptor
                            {
                              Subject = new ClaimsIdentity(new[]
                                                           {
                                                             new Claim(ClaimTypes.Name, userId)
                                                           }),
                              Expires = DateTime.UtcNow.AddDays(tokenExpiry),
                              SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                            };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      return tokenString;
    }

    private bool VerifyPasswordHash(string password, string passwordSalt, string passwordHash)
    {
      var saltBytes = Convert.FromBase64String(passwordSalt);
      var hashBytes = Convert.FromBase64String(passwordHash);

      using (var hmac = new System.Security.Cryptography.HMACSHA512(saltBytes))
      {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return !computedHash.Where((t, i) => t != hashBytes[i]).Any();
      }
    }
  }
}