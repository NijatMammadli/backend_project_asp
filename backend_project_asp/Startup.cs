using backend_project_asp.Areas.AdminPanel.Utils;
using backend_project_asp.DataAccessLayer;
using backend_project_asp.Models;
using FrontToBack_hw.DataAccessLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace backend_project_asp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        private readonly IWebHostEnvironment _environment;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;

                options.User.RequireUniqueEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 10;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true; 

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, builder =>
                {
                    builder.MigrationsAssembly(nameof(backend_project_asp));
                });
            });

            services.AddControllersWithViews();
            Constants.ImageFolderPath = Path.Combine(_environment.WebRootPath,  "img");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)    
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

            app.UseRouting();

           

            app.UseAuthentication();

            app.UseAuthorization(); 

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
          name: "areas",
          pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
        );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

         
        }
    }
}
