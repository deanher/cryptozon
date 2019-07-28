namespace Cryptozon.Api.Models
{
  public class PurchaseRequest
  {
    public int CoinId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
  }
}