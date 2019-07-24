using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Cryptozon.Api
{
  public class Program
  {
    // Optional arguments:
    // --urls http://*:5000
    // --environment "Production"
    public static void Main(string[] args)
    {
      CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
  }
}
