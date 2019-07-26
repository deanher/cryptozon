using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;

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

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      loggerFactory.AddSerilog();
      app.UseCors("CorsPolicy");

      app.UseMvc();
    }

    public void ConfigureServices(IServiceCollection services)
    {
      var appSettings = Configuration.Get<AppSettings>();

      services.Configure<AppSettings>(Configuration);
      ConfigureLogging(appSettings.Logging, HostingEnvironment);

      services.AddCors(ConfigureCors(appSettings));
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    }

    private Action<CorsOptions> ConfigureCors(AppSettings appSettings)
    {
      return options => options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins(appSettings.AllowedHosts)
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials());
    }

    private void ConfigureLogging(Logging loggingSettings, IHostingEnvironment env)
    {
      Enum.TryParse(loggingSettings.Level, out LogEventLevel logEventLevel);

      var logConfig = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .MinimumLevel.ControlledBy(new LoggingLevelSwitch(logEventLevel))
        .WriteTo.RollingFile(loggingSettings.OutputPath, outputTemplate: loggingSettings.OutputTemplate);

      if (env.IsDevelopment())
        logConfig.WriteTo.Console(logEventLevel, loggingSettings.OutputTemplate);

      Log.Logger = logConfig.CreateLogger();
    }
  }
}
