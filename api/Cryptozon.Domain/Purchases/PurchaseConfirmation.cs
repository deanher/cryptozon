using System;

namespace Cryptozon.Domain.Purchases
{
  public class PurchaseConfirmation
  {
    private PurchaseConfirmation(decimal totalAmount, Guid reference)
    {
      TotalAmount = totalAmount;
      Reference = reference;
    }

    public static PurchaseConfirmation Create(decimal totalAmount, Guid reference)
    {
      return new PurchaseConfirmation(totalAmount, reference);
    }

    public Guid Reference { get; }
    public decimal TotalAmount { get; }
  }
}