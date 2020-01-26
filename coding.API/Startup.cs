using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using coding.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using coding.API.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;


namespace coding.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<DataContext>(x => x.UseMySql(Configuration.GetConnectionString("MySqlConnection")));
            //services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AzureSqlConnection")));
            //services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqliteConnection")));

            // Scoped services are created one per request but uses the same for scope of the request
            // with these you make the interfaces and its implementations available for Dependency Injection to the controllers
            services.AddScoped<IRepo, Repo>();
            services.AddScoped<IAuthRepo, AuthRepo>();

            // Add CORS support
            services.AddCors();

            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            /* 
            .AddJsonOptions( options => {
                options.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }) */
            services.AddAutoMapper(typeof(Startup));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:Token").Value)
                        ),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // if (!_env.IsDevelopment())
            // {
            //     services.AddHttpsRedirection(options =>
            //     {
            //         options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            //         options.HttpsPort = 5001;
            //     });
            // }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
            }
            else
            {
                try
                {
                    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<DataContext>()
                                .Database.Migrate();
                    }
                }
                catch { }
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();
            app.UseAuthentication();

            // Kestrel will look for index.html or other static files to use in webroot
            app.UseDefaultFiles();
            // Kestrel will use the static files found above
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "coding-fallback",
                    pattern: " { controller = Fallback, action = Index }");
                endpoints.MapFallbackToController("Index", "Fallback");
            });
            app.UseMvc();

            app.UseHsts();
            app.UseHttpsRedirection();


        }
    }
}
