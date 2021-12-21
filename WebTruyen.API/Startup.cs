using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebTruyen.API.Repository.AnnouncementDI;
using WebTruyen.API.Service;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities;
using Microsoft.IdentityModel.Tokens;
using SixLabors.ImageSharp.Web.DependencyInjection;
using WebTruyen.API.Repository.BookmarkDI;
using WebTruyen.API.Repository.ComicDI;
using WebTruyen.API.Repository.ComicInGenreDI;
using WebTruyen.API.Repository.CommentDI;
using WebTruyen.API.Repository.ChapterDI;
using WebTruyen.API.Repository.GenreDI;
using WebTruyen.API.Repository.HistoryReadDI;
using WebTruyen.API.Repository.PageDI;
using WebTruyen.API.Repository.ReportDI;
using WebTruyen.API.Repository.RoleDI;
using WebTruyen.API.Repository.TranslationOfUserDI;
using WebTruyen.API.Repository.UserDI;

namespace WebTruyen.API
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

            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebTruyen.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Nhập 'Bearer' [space] và sau đó nhập token như ví dụ bên dưới.
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            services.AddDbContext<ComicDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("ConnectComic"));
            });


            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ComicDbContext>()
                .AddDefaultTokenProviders();

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                    builder => {
                        builder.SetIsOriginAllowed(origin => true);
                        //builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                        builder.AllowCredentials();
                        //builder.WithOrigins("https://localhost:5001", "https://localhost:5002", "https://localhost:5003", "http://0831-14-173-206-162.ngrok.io", "http://90af-14-173-206-162.ngrok.io")
                        //    .AllowAnyHeader()
                        //    .AllowAnyMethod()
                        //    .AllowCredentials();

                    });
            });

            services.AddImageService();

            services.AddTransient<IAnnouncementService, AnnouncementService>();
            services.AddTransient<IBookmarkService, BookmarkService>();
            services.AddTransient<IChapterService, ChapterService>();
            services.AddTransient<IComicInGenreService, ComicInGenreService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<IHistoryReadService, HistoryReadService>();
            services.AddTransient<IPageService, PageService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ITranslationOfUserService, TranslationOfUserService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IComicService, ComicService>();

            services.AddTransient<RoleManager<Role>, RoleManager<Role>>();

            services.AddTransient<IWaterMarkService, WaterMarkService>();


            var issuer = Configuration.GetValue<string>("Tokens:Issuer");
            var signingKey = Configuration.GetValue<string>("Tokens:Key");
            var signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters() {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = System.TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                };
            });
            services.AddImageSharp();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebTruyen.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseImageSharp();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
