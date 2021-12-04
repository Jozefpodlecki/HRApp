using HRApp.Common;
using HRApp.DAL;
using HRApp.DAL.Repositories;
using HRApp.Web.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace HRApp.Web
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var jwtConfiguration = Configuration
               .GetSection("Jwt")
               .Get<JwtConfiguration>();

            services.AddControllers()
               .AddJsonOptions(options => {});
            services.AddCors(options =>
            {
                options.AddPolicy("All", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "test CI API",
                    Version = "1.0",
                });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "TestCI.xml");

                if (File.Exists(filePath))
                {
                    options.IncludeXmlComments(filePath);
                }

                options.CustomSchemaIds(x => x.FullName);
            });

            services.AddSignalR();
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("database"));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddSystemClock();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
            });
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(pr =>
            {
                pr.MapControllers();
            });
            //app.UseSwagger();
            //app.UseSwaggerUI(options =>
            //{
            //    options.DocumentTitle = "HR App API";
            //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            //    options.RoutePrefix = string.Empty;
            //    options.DefaultModelsExpandDepth(-1);
            //    options.InjectStylesheet("/swagger-theme.css");
            //});

            if (env.IsDevelopment())
            {
                app.UseSpa(spa =>
                {                    
                    spa.Options.SourcePath = "ClientApp";
                    spa.UseProxyToSpaDevelopmentServer("https://localhost:9000");   
                });
            }
        }
    }
}
