using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CmnSoftwareBackend.Services.AutoMapper.Profiles;
using CmnSoftwareBackend.Services.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FrontEnd.Mvc
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
            //Api Copy
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //.AddRazorRuntimeCompilation tekrar derlemeye gerek kalmadan değişiklikleri sayfaya aktarıyor.
            //.AddControllerWithViews sen bir mvc projesisin demek

            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(opt =>
            {

                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

            services.AddAutoMapper(typeof(Startup), typeof(CategoryProfile), typeof(UserProfile), typeof(OperationClaimProfile), typeof(UserNotificationProfile), typeof(UserTokenProfile), typeof(ArticleProfile), typeof(CommentWithUserProfile), typeof(CommentWithoutUserProfile), typeof(ArticlePictureProfile));

            services.LoadMyServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //Böyle bir sayfa yoksa 404 not found sayfası gelecek.
                app.UseStatusCodePages();
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
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=index}/{id?}"
                    );
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

