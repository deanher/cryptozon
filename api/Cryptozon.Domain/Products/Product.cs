namespace Cryptozon.Domain.Products
{
  public class Product
  {
    public int Id { get; }
    public string Name { get; }
    public string Symbol { get; }
    public decimal Price { get; }

    private Product(int id, string name, string symbol, decimal price)
    {
      Id = id;
      Name = name;
      Symbol = symbol;
      Price = price;
    }

    public static Product Create(int id, string name, string symbol, decimal price)
    {
      return new Product(id, name, symbol, price);
    }
  }
}