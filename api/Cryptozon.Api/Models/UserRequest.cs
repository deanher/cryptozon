using System.ComponentModel.DataAnnotations;

namespace Cryptozon.Api.Models
{
  public class UserRequest
  {
    [Required]
    [StringLength(250)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(250)]
    public string Surname { get; set; }

    [Required]
    [StringLength(100)]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
  }
}