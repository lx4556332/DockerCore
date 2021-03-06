using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DockerCore.Models;
using DockerCore.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DockerCore
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
            //services.AddTransient<IProductRepository, MockProductRepository>();
            services.AddTransient<IProductRepository, DataProductRepository>();
            var host = Configuration["DBHOST"] ?? "192.168.31.210";
            var port = Configuration["DBPORT"] ?? "3306";

            var password = Configuration["PASSWORD"] ?? "bb123456";

            var connectionStr = $"server={host};userId=root;pwd={password};"
                + $"port={port};database=products";

            services.AddDbContextPool<ProductDbContext>(options =>
            {
                options.UseMySql(connectionStr);
            });

            services.AddControllersWithViews();
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
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseDataInitializer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{id?}");
            });

            #region ×¢²áConsul

            this.Configuration.ConsulRegister();

            #endregion
        }
    }
}
