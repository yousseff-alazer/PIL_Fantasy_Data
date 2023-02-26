using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PIL_Fantasy_Data_Integration.API.Fantasy_Data.DAL.DB;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace PIL_Fantasy_Data_Integration.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {

            var builder = new ConfigurationBuilder()
      .SetBasePath(env.ContentRootPath)
      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
      .AddEnvironmentVariables();

            Configuration = builder.Build();
            Log.Logger = new LoggerConfiguration()
    .WriteTo.File(Configuration["Logging:LogPath"], rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 100000)
.MinimumLevel.Debug()
.CreateLogger();
            Log.Information("Inside Startup ctor");
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                Log.Information("ConfigureServices");
                services.Configure<ApiBehaviorOptions>(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });
                services.AddCors();
                Log.Information("dbConnectionString");
                var dbConnectionString = Configuration.GetValue<string>("DatabaseSettings:ConnectionString");
                Log.Information(dbConnectionString);
                services.AddDbContext<fantasy_dataContext>(opt =>
                    opt.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)));
                // Redis Configuration
                //services.AddStackExchangeRedisCache(options =>
                //{
                //    options.Configuration = Configuration.GetValue<string>("CacheSettings:ConnectionString");
                //});
                services.AddControllers();
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PIL_Fantasy_Data_Integration.API", Version = "v1" });
                    // Set the comments path for the Swagger JSON and UI.
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                });
                services.AddHealthChecks()
                    .AddMySql(Configuration["DatabaseSettings:ConnectionString"]);
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message + ex.StackTrace);
            }

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //Log.Information("Configure");
            try
            {

                app.UseStaticFiles();
                app.UseRouting();
                loggerFactory.AddSerilog();
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PIL_Fantasy_Data_Integration.API v1");
                    //c.RoutePrefix = "/api/fantasydata";
                });

                app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) // allow any origin
        .AllowCredentials()); // allow credentials

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHealthChecks("/hc", new HealthCheckOptions
                    {
                        Predicate = _ => true,
                        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                    });
                });

            }
            catch (Exception ex) 
            {
                Log.Information(ex.Message + ex.StackTrace);
            }
        }
    }

}
