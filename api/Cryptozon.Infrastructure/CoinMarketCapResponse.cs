using System.Collections.Generic;

namespace Cryptozon.Infrastructure
{
  public class CoinMarketCapResponse
  {
    public RequestStatus Status { get; set; }
    public IEnumerable<Coin> Data { get; set; }
  }
}