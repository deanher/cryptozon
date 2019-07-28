using System;
using System.Text;
using System.Threading.Tasks;
using Cryptozon.Domain.Products;
using Cryptozon.Domain.Purchases;
using Cryptozon.Domain.Users;
using Cryptozon.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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

      app.UseAuthentication();

      app.UseMvc();
    }

    public void ConfigureServices(IServiceCollection services)
    {
      var appSettings = Configuration.Get<AppSettings>();

      services.Configure<AppSettings>(Configuration);
      ConfigureLogging(appSettings.Logging, HostingEnvironment);

      services.AddCors(ConfigureCors(appSettings));
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      ConfigureDependencies(services, appSettings);
      ConfigureAuthentication(services, appSettings);
    }

    private void ConfigureAuthentication(IServiceCollection services, AppSettings appSettings)
    {
      var key = Encoding.ASCII.GetBytes(appSettings.Secret);
      var tokenValidationParameters = new TokenValidationParameters
                                      {
                                        ValidateIssuerSigningKey = true,
                                        IssuerSigningKey = new SymmetricSecurityKey(key),
                                        ValidateIssuer = false,
                                        ValidateAudience = false
                                      };
      services.AddAuthentication(x =>
                                 {
                                   x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                   x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                 })
              .AddJwtBearer(x =>
                            {
                              x.Events = new JwtBearerEvents
                                         {
                                           OnTokenValidated = ValidateToken
                                         };
                              x.RequireHttpsMetadata = false;
                              x.SaveToken = true;
                              x.TokenValidationParameters = tokenValidationParameters;
                            });
    }

    private Task ValidateToken(TokenValidatedContext context)
    {
      var userService = context.HttpContext.RequestServices.GetRequiredService<IUsersRepo>();
      var username = context.Principal.Identity.Name;
      var user = userService.GetUserAsync(username);
      if (user == null)
      {
        // return unauthorized if user no longer exists
        context.Fail("Unauthorized");
      }

      return Task.CompletedTask;
    }

    private void ConfigureDependencies(IServiceCollection services, AppSettings appSettings)
    {
      services.AddTransient<IHttpRestClient, RestSharpClient>(provider => new RestSharpClient(appSettings.CoinMarketCap.BaseUrl));
      services.AddTransient<IDatabaseAdapter, SqlAdapter>(provider => new SqlAdapter(appSettings.ConnectionStrings.Sql));
      services.AddTransient<IProductsRepo, ProductsRepo>(provider => new ProductsRepo(provider.GetService<IHttpRestClient>(), appSettings.CoinMarketCap.Key));
      services.AddTransient<IUsersRepo, UsersRepo>();
      services.AddTransient<IPurchasesRepo, PurchasesRepo>();
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
