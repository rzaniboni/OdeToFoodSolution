using System;
using System.Threading.Tasks;
using HealthChecks.UI.Core;
using HealthChecks.UI.SQLite.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using OdeToFood.Data;
namespace OdeToFood {
  public class Startup {
    public Startup (IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices (IServiceCollection services) {

      var connectionString = Configuration.GetConnectionString ("OdeToFoodDb");

      services.AddDbContextPool<OdeToFoodDbContext> (options => {
        options.UseSqlite (connectionString);
      });

      services.AddScoped<IRestaurantData, SqlRestaurantData> ();

      services.AddRazorPages ();
      services.AddControllers ();

      services.AddHealthChecks ().AddSqlite (connectionString)
        .AddCheck ("Test Health Check", () => HealthCheckResult.Healthy ("Server is healty"));

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment ()) {
        app.UseDeveloperExceptionPage ();
      } else {
        app.UseExceptionHandler ("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts ();
      }

      app.UseHttpsRedirection ();

      app.Use (SayHelloMiddleware);

      app.UseStaticFiles ();

      app.UseRouting ();

      app.UseAuthorization ();

      app.UseEndpoints (endpoints => {
        endpoints.MapRazorPages ();
        endpoints.MapControllers ();
        endpoints.MapHealthChecks ("/health");
      });

    }

    private RequestDelegate SayHelloMiddleware (RequestDelegate next) {
      return async ctx => {
        if (ctx.Request.Path.StartsWithSegments ("/hello")) {
          await ctx.Response.WriteAsync ("Hello, World!");
        } else {
          await next (ctx);
        }
      };
    }

  }
}