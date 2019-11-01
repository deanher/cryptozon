namespace Cryptozon.Api
{
  // note: this should move to some key vault instead of appSettings.json, i.e. Azure KeyVault or Hashicorp vault, etc.
  public class AppSettings
  {
    public ExternalService CoinMarketCap { get; set; } 
    public Logging Logging { get; set; }
    public string[] AllowedHosts { get; set; }
    public ConnectionStrings ConnectionStrings { get; set; }

    public string Secret { get; set; }
    public int TokenExpiry { get; set; }
  }

  public class ConnectionStrings
  {
    public string Sql { get; set; }
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