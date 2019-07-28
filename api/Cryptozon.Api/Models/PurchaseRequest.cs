using System.ComponentModel.DataAnnotations;

namespace Cryptozon.Api.Models
{
  public class PurchaseRequest
  {
    [Required]
    public int CoinId { get; set; }

    [Required]
    public decimal Quantity { get; set; }

    [Required]
    public decimal UnitPrice { get; set; }
  }
}