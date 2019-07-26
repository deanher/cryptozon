namespace Cryptozon.Api
{
  public class AppSettings
  {
    public ExternalService CoinMarketCap { get; set; } 
    public Logging Logging { get; set; }
    public string[] AllowedHosts { get; set; }
  }

  public class ExternalService
  {
    public string Key { get; set; }
    public string BaseUrl { get; set; }
  }

  public class Logging
  {
    public string Level { get; set; }
    public string OutputTemplate { get; set; }
    public string OutputPath { get; set; }
  }
}