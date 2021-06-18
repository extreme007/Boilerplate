using AspNetCoreHero.Boilerplate.Application.Extensions;
using AspNetCoreHero.Boilerplate.Infrastructure.Extensions;
using AspNetCoreHero.Boilerplate.Web.Abstractions;
using AspNetCoreHero.Boilerplate.Web.Extensions;
using AspNetCoreHero.Boilerplate.Web.Permission;
using AspNetCoreHero.Boilerplate.Web.Services;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using AutoMapper;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddNotyf(o =>
            {
                o.DurationInSeconds = 10;
                o.IsDismissable = true;
                o.HasRippleEffect = true;
            });
            services.AddApplicationLayer();
            services.AddInfrastructure(_configuration);
            services.AddPersistenceContexts(_configuration);
            services.AddRepositories();
            services.AddSharedInfrastructure(_configuration);
            services.AddMultiLingualSupport();
            services.AddControllersWithViews().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //services.AddDistributedMemoryCache();

            // Register the RedisCache service
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = _configuration.GetSection("Redis")["ConnectionString"];
            });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(_configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            // services.AddHangfireServer();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
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
            app.UseNotyf();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMultiLingualFeature();
            app.UseRouting();
            app.UseAuthentication();
  
            app.UseAuthorization();
            app.UseHangfireDashboard();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                   name: "Admin",
                   areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapControllerRoute(
                // name: "areaRoute",
                //pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


                //endpoints.MapControllerRoute("category", "{slug}", new { Controllers = "Category", Actions = "Index" });
                //endpoints.MapControllerRoute("loadMore", "LoadMore/{slug}", new { Controllers = "Category", Actions = "GetDataArticle" });
               // endpoints.MapControllerRoute("article", "{categorySlug}/{slug}-{id}", new { Controllers = "Article", Actions = "Index" });


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
                endpoints.MapHangfireDashboard();

                endpoints.MapFallback(context => {
                    context.Response.Redirect("/Error");
                    return Task.CompletedTask;
                });
            });
        }
    }
}