using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebTruyen.API.Repository.Announcement;
using WebTruyen.API.Repository.Bookmark;
using WebTruyen.API.Repository.Chapter;
using WebTruyen.API.Repository.Comic;
using WebTruyen.API.Repository.ComicInGenre;
using WebTruyen.API.Repository.Comment;
using WebTruyen.API.Repository.Genre;
using WebTruyen.API.Repository.HistoryRead;
using WebTruyen.API.Repository.Page;
using WebTruyen.API.Repository.Report;
using WebTruyen.API.Repository.Role;
using WebTruyen.API.Repository.TranslationOfUser;
using WebTruyen.API.Repository.User;
using WebTruyen.API.Service;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities;

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
            services.AddDbContext<ComicDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ConnectComic"));
            });

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ComicDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllers();
            services.AddTransient<IStorageService, FileService>();
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

            services.AddTransient<UserManager<User>, UserManager<User>>();
            services.AddTransient<SignInManager<User>, SignInManager<User>>();
            services.AddTransient<RoleManager<Role>, RoleManager<Role>>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebTruyen.API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
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
                });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebTruyen.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
