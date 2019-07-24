using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cryptozon.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
    {
      Configuration = configuration;
      HostingEnvironment = hostingEnvironment;
    }

    public IConfiguration Configuration { get; }
    public IHostingEnvironment HostingEnvironment { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      var appSettings = Configuration.Get<AppSettings>();

      services.Configure<AppSettings>(Configuration);
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }

  public class AppSettings
  {
    public Logging Logging { get; set; }
    public string AllowedHosts { get; set; }
  }

  public class Logging
  {
    public string Level { get; set; }
    public string OutputTemplate { get; set; }
    public string OutputPath { get; set; }
  }
}
