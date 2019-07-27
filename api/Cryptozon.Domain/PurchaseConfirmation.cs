using System;

namespace Cryptozon.Domain
{
  public class PurchaseConfirmation
  {
    private PurchaseConfirmation(decimal totalAmount, string reference)
    {
      TotalAmount = totalAmount;
      Reference = reference;
    }

    public static PurchaseConfirmation Create(decimal totalAmount, string reference = null)
    {
      return new PurchaseConfirmation(totalAmount, reference ?? Guid.NewGuid().ToString("N"));
    }

    public string Reference { get; }
    public decimal TotalAmount { get; }
  }
}