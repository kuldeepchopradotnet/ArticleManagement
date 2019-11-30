﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AM.Api.Model;
#region Idenity Server 4 packages
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
#endregion
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;

namespace AM.Api
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
            // **** show detail exception infromation [PII : hidden] using Microsoft.IdentityModel.Logging;
            IdentityModelEventSource.ShowPII = true;
            // Configure Databse 
            services.AddDbContextPool<ArticleManagementContext>(o =>
                o.UseSqlServer("Server=(localdb)\\MyInstance;Database=ArticleManagement;Trusted_Connection=True;MultipleActiveResultSets=True;"));
            // Configure Identity
            services.AddIdentity<IdentityUser, IdentityRole>()
              .AddEntityFrameworkStores<ArticleManagementContext>()
              .AddDefaultTokenProviders();

            // **** other setting for identity server

            //for identity server 4 config. user to create identity server
            //services.AddIdentityServer()
            //.AddDeveloperSigningCredential(filename: "tempkey.rsa")
            //.AddInMemoryApiResources(IdentityConfig.GetApiResources())
            //.AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
            //.AddInMemoryClients(IdentityConfig.GetClients())
            ////.AddTestUsers(IdentityConfig.GetUsers())
            //.AddAspNetIdentity<IdentityUser>();


            // *** configure idenity server 4 with asp.net identity
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityConfig.GetApiResources())
                .AddInMemoryClients(IdentityConfig.GetClients())
                //.AddTestUsers(IdentityConfig.GetUsers())
                .AddAspNetIdentity<IdentityUser>()
                ;

            // *** Authentication Handler optional

            //services.AddAuthentication(options=> {
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //    .AddJwtBearer(options =>
            //    {
            //        options.Audience = "https://localhost:44315";
            //        options.Authority = "fiver_auth_api";
            //        options.RequireHttpsMetadata = false;
            //    });






            // *** Authentication Handler optional Idenity
            //services.AddAuthentication(
            // IdentityServerAuthenticationDefaults.AuthenticationScheme)
            //      .AddIdentityServerAuthentication(options =>
            //      {
            //          options.Authority = "https://localhost:44315"; // Auth Server  
            //          options.RequireHttpsMetadata = false; // only for development  
            //          options.ApiName = "fiver_auth_api"; // API Resource Id  
            //      });


            // *** Configure Claims in jwt token generated by Idenity server 4
            services.AddTransient<IProfileService, ProfileService>();
            // *** config MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // *** Jwt handler for authentical
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.Audience = "https://localhost:44315/resources";
                    options.Authority = "https://localhost:44315";
                    options.RequireHttpsMetadata = false;
                });

            // *** Config Cookies

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.AccessDeniedPath = "/Account/Login";
            //    options.LoginPath = "/Account/Denied";
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            //    options.Events.OnRedirectToLogin = context =>
            //    {
            //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //        return Task.CompletedTask;
            //    };
            //});


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // *** RUN ON DEVELOPMENT 
                app.UseDeveloperExceptionPage();
                // *** Middle ware of Stackify     localhost:2012       
                app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>();

            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // *** Cors MW

            // *** Static file MW


            // *** Middle ware Redirect
            app.UseHttpsRedirection();
            // *** Identity MW
            app.UseIdentityServer();
            // *** Auth MW
            app.UseAuthentication();
            // *** MVC MW
            app.UseMvc();
        }
    }
}
