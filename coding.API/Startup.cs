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
using Microsoft.OpenApi.Models;
using coding.API.Models.Products;
using coding.API.Helpers;
using coding.API.Data;

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

            // @Denis as� se inyecta el repositorio porque es gen�rico.
            services.AddTransient(typeof(Repository<>));

            // Data Context
            services.AddDbContext<DataContext>(x => x.UseMySql(Configuration.GetConnectionString("MySqlConnection")));
          

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "Coding in DFW API",
                    Version = "v1"
                });
            });
            // Add CORS support
            services.AddCors();
            //Cloudinary
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers().AddNewtonsoftJson(options =>
                  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );  /* 
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
            app.UseAuthentication();
            // Kestrel will look for index.html or other static files to use in webroot
            app.UseDefaultFiles();
            // Kestrel will use the static files found above
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            // app.UseSwagger();
            // app.UseSwaggerUI(c => {
            //     c.SwaggerEndpoint("/swagger/v1/swagger.json", "Coding in DfW API v1");
            // });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "coding-fallback",
                    pattern: " { controller = Fallback, action = Index }");
                endpoints.MapFallbackToController("Index", "Fallback");
            });
            app.UseMvc(
            //     routes =>  {
            //     routes.MapSpaFallbackRoute(
            //         name: "spa-fallback",
            //         defaults: new {
            //              controller = "Fallback", action = "Index"
            //         }
            //     );
            // }
            );

            app.UseHsts();
            app.UseHttpsRedirection();

            

        }
    }
}
