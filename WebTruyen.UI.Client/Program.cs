using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using WebTruyen.UI.Client.Service.ChapterService;
using WebTruyen.UI.Client.Service.ComicService;
using WebTruyen.UI.Client.Service.CommentService;
using WebTruyen.UI.Client.Service.GenreService;
using WebTruyen.UI.Client.Service.ImageService;
using WebTruyen.UI.Client.Service.PageService;
using WebTruyen.UI.Client.Service.UserService;
using WebTruyen.UI.Client.Shared;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using WebTruyen.UI.Client.Authentication;
using WebTruyen.UI.Client.Contant;

namespace WebTruyen.UI.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            Console.WriteLine(">>>" + builder.Configuration["BaseAddress"]);

            BaseAddress.Address = builder.Configuration["BaseAddress"];

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["BaseAddress"]) });

            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

            builder.Services.AddTransient<IComicApiClient, ComicApiClient>();
            builder.Services.AddTransient<IChapterApiClient, ChapterApiClient>();
            builder.Services.AddTransient<IPageService, PageService>();
            builder.Services.AddTransient<IGenreService, GenreService>();
            builder.Services.AddTransient<IImageService, ImageService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<ICommentService, CommentService>();

            builder.Services.AddBlazoredToast();


            await builder.Build().RunAsync();
        }
    }
}
