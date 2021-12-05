using HRApp.Common;
using HRApp.DAL;
using HRApp.DAL.Repositories;
using HRApp.Web.Configuration;
using HRApp.Web.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConfiguration.Issuer,
                        ValidAudience = jwtConfiguration.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey)),
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/hubs/chat")))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(op =>
            {
                op.AddPolicy(Policies.Authorized, policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                });
            });

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
            services.AddDbContext(Configuration.GetConnectionString("Default"));
            services.AddRepositories();
            services.AddSystemClock();
            services.AddTimer();
            services.AddScoped<TestDataGenerator>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                var serviceScopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
                using var serviceScope = serviceScopeFactory.CreateScope();
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                var testDataGenerator = serviceScope.ServiceProvider.GetRequiredService<TestDataGenerator>();
                var created = context.Database.EnsureCreatedAsync().GetAwaiter().GetResult();

                if (created)
                {
                    testDataGenerator.RunAsync().GetAwaiter().GetResult();
                }

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
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<HRAppHub>("/signalr");
                endpoints.MapControllers();
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
