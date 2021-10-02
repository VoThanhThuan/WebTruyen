using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using WebTruyen.UI.Client.Service.ComicService;

namespace WebTruyen.UI.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            Console.WriteLine(">>>" + builder.Configuration["BaseAddress"]);

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["BaseAddress"]) });
            builder.Services.AddBlazoredSessionStorage();

            builder.Services.AddTransient<IComicApiClient, ComicApiClient>();

            await builder.Build().RunAsync();
        }
    }
}
