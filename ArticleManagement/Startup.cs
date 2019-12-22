using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AM.Api.Model;
using AM.Reopsitory;
using AM.Service;
using AM.Service.AutoMapperService;
using AM.Service.GoogleDriveService;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;

namespace ArticleManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;
            // Configure Databse 
            services.AddDbContextPool<ArticleManagementContext>(o =>
                o.UseSqlServer(Configuration.GetConnectionString("AppDb")));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddScoped<IArticleRepository, ArticleRepository>();

            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IAutoMapperService, AutoMapperService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddAuthDriveCredential();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                  name: "/",
                  template: "/{query?}",
                  defaults: new { controller = "Article", action = "Index" });


                routes.MapRoute(
                    name: "default",
                    template: "{controller=Article}/{action=Index}/{query?}");
            });
        }
    }
}
