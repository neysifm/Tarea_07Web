﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tarea_07Web.Models;
using Tarea_07Web.Repositorios.Base;

namespace Tarea_07Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //----------------------------------------------------------
            // Verifica cual conexion usar... produccion y desarrollo
            //-------------
            string connStr = Configuration.GetConnectionString("prodConn");
            if (Configuration.GetSection("AppSettings")["EnProduccion"].Equals("NO"))
                connStr = Configuration.GetConnectionString("devConn");



            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connStr),
            ServiceLifetime.Scoped);

            // services.AddDbContext<OldDbContext>(option => option.UseSqlServer(connStr),
            //ServiceLifetime.Scoped);


            //Inyectamos el repoWrapper
            services.AddScoped<IRepoWrapper, RepoWrapper>();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings")); //inyecta los datos constantes de la app.
            services.Configure<NSettings>(Configuration.GetSection("NSettings")); //inyecta los datos constantes de la app.
            services.Configure<GlobalSettings>(Configuration); //inyecta los datos constantes de la app.

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
