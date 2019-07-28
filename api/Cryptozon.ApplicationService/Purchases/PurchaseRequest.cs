namespace Cryptozon.ApplicationService.Purchases
{
  public class PurchaseRequest
  {
    public int CoinId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
  }
}