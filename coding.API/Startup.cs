using System;
using System.Collections.Generic;
using System.Linq;
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

namespace coding.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration,  IWebHostEnvironment env)
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
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AzureSqlConnection")));
            //services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqliteConnection")));
            services.AddScoped<IRepo,Repo>();
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            /* 
            .AddJsonOptions( options => {
                options.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }) */

            if (!_env.IsDevelopment())
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                    options.HttpsPort = 5001;
                });
            }
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            //app.UseHsts();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
