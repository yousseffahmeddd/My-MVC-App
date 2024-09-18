using Demo.BLL;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using My_MVC_App.Helper;
using My_MVC_App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_MVC_App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); //register built in mvc services
            //services.AddScoped<AppDbContext>(); //DbContext=> Scoped ("per request")
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(" Server =.; Database = AppMVCDB04; Trusted_Connection = true; MultipleActiveResultSets = true ");
            });
            services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();// Repository => Scoped ("Per Request")
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddScoped<UserManager<ApplicationUser>>();
            //services.AddScoped<SignInManager<ApplicationUser>>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
            });
            //scoped ==> lifetime per request
            //transient ==> lifetime per operation
            //singleton ==> lifetime per app
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
            });
        }
    }
}
