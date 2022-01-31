using DemoBlazorAuthentication.Server.Models.Entities;
using DemoBlazorAuthentication.Server.Models.Enums;
using DemoBlazorAuthentication.Server.Models.Services.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace DemoBlazorAuthentication.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRazorPages();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo Blazor Authentication", Version = "v1" });
            });

            services.AddCors(o => o.AddPolicy("AllowAllCors", builder => 
            { 
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader(); 
            }));

            var identityBuilder = services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                // Criteri di validazione della password
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredUniqueChars = 4;

                // Conferma dell'account
                options.SignIn.RequireConfirmedAccount = true;

                // Blocco dell'account
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            });

            var persistence = Persistence.EfCore;
            switch (persistence)
            {
                case Persistence.EfCore:

                    //Imposta il MyCourseDbContext come servizio di persistenza per Identity
                    identityBuilder.AddEntityFrameworkStores<ApplicationDbContext>();

                    // Usando AddDbContextPool, il DbContext verrà implicitamente registrato con il ciclo di vita Scoped
                    services.AddDbContext<ApplicationDbContext>(optionsBuilder => {
                        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), options => 
                        {
                            // Abilito il connection resiliency - Info su: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
                            options.EnableRetryOnFailure(3);
                        });
                    });

                    break;
            }

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo Blazor Authentication v1"));
            }
            else
            {
                // app.UseExceptionHandler("/Error");
                // Breaking change .NET 5: https://docs.microsoft.com/en-us/dotnet/core/compatibility/aspnet-core/5.0/middleware-exception-handler-throws-original-exception
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandlingPath = "/Error",
                    AllowStatusCode404Response = true
                });
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("AllowAllCors");

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}