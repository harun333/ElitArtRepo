using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ElitArt.Data;
using ElitArt.Data.Entities;
using ElitArt.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace ElitArt
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentity<StoreUser, IdentityRole>(cfg => {
                cfg.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ElitContext>();

            //services.AddAuthentication()
            //    .AddCookie()
            //    .AddJwtBearer(/*cfg =>*/
            //    //{
            //        //    cfg.TokenValidationParameters = new TokenValidationParameters()
            //        //    {
            //        //        ValidateIssuer=_config["Tokens:Issuer"],
            //        //        ValidateAudience=_config["Token:Audience"],
            //        //        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
            //        //    };
            //    /*}*/);

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(Options => {
            //    Options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //});

            services.AddDbContext<ElitContext>(cfg=> {

                cfg.UseSqlServer(_config.GetConnectionString("EliteConnectionString"));
            });

            services.AddAutoMapper();

            services.AddTransient<ElitSeeder>();


            services.AddScoped<IElitRepository, ElitRepository>();
            services.AddScoped<IMailService, NullMailService>();


            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

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
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();

            app.UseNodeModules(env);

            app.UseAuthentication();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc(cfg => {

                //cfg.MapRoute("Default",
                //    "/{controller}/{action}/{id?}",
                //    new { controller = "App", Action = "Index" });

                cfg.MapRoute(
                        name:"Default",
                        template:"{controller=App}/{action=Index}/{id?}"
                    );
            });

            if (env.IsDevelopment())
            {
                using (var scop = app.ApplicationServices.CreateScope())
                {
                    var seeder = scop.ServiceProvider.GetService<ElitSeeder>();
                    seeder.SeedAsync().Wait();
                }
            }
        }
    }
}
