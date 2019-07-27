using System.Collections.Generic;

namespace Cryptozon.Infrastructure
{
  public class Coin
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public long MaxSupply { get; set; }
    public long TotalSupply { get; set; }
    public Dictionary<string, Quote> Quote { get; set; } = new Dictionary<string, Quote>();
  }
}