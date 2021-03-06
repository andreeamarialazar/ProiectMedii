using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectMedii.Data;
using Microsoft.EntityFrameworkCore;
using ProiectMedii.Hubs;
using Microsoft.AspNetCore.Identity;

namespace ProiectMedii
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
            services.AddControllersWithViews();
            services.AddDbContext<LibraryContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSignalR();
            services.Configure<IdentityOptions>(options => {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan =
                TimeSpan.FromMinutes(5); 
                options.Lockout.MaxFailedAccessAttempts = 5; 
                options.Lockout.AllowedForNewUsers = true;
            });

            services.AddAuthorization(opts => {
                opts.AddPolicy("Admin", policy => {
                    policy.RequireRole("Admin");
                });
            });

            services.AddAuthorization(opts => {
                opts.AddPolicy("Employee", policy => {
                    policy.RequireRole("Employee");
                });
            });


            services.AddRazorPages();

            services.AddAuthorization(opts => {
                opts.AddPolicy("ResponsibleWithPublishers", policy => {
                    policy.RequireRole("Manager");
                    policy.RequireClaim("Department", "Sales");
                });
            });
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
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapRazorPages();

            });
        }
    }
}
