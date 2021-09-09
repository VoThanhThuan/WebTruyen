using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using WebTruyen.UI.Admin.Data;
using WebTruyen.UI.Admin.Service.ChapterService;
using WebTruyen.UI.Admin.Service.ComicService;
using WebTruyen.UI.Admin.Service.GenreService;
using WebTruyen.UI.Admin.Service.ImageService;
using WebTruyen.UI.Admin.Service.PageService;
using WebTruyen.UI.Admin.Service.RoleService;
using WebTruyen.UI.Admin.Service.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using WebTruyen.UI.Admin.Service;

namespace WebTruyen.UI.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
            });
            services.AddSession(options => 
            { 
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            services.AddHttpContextAccessor();
            services.AddScoped<HttpContextBlazor>();

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Configuration["BaseAddress"]) });
            BaseAddressDefault.Address = new Uri(Configuration["BaseAddress"]);
            services.AddHttpContextAccessor();

            services.AddHttpClient();

            var issuer = Configuration.GetValue<string>("Tokens:Issuer");
            var signingKey = Configuration.GetValue<string>("Tokens:Key");
            var signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);
            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = issuer,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = System.TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                    };
                }).AddCookie();

            
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IComicApiClient, ComicApiClient>();
            services.AddTransient<IChapterService, ChapterService>();
            services.AddTransient<IPageService, PageService>();
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCookiePolicy();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
