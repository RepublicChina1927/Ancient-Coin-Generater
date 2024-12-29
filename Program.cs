using Ancient_Coin_Generater.Data;
using Ancient_Coin_Generater.Models_for_DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Microsoft.AspNetCore.Identity;

namespace Ancient_Coin_Generater
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddScoped<dbService>();

            // Use this AddDbContextFactory with the connection string from appsettings.json
            builder.Services.AddDbContextFactory<ApplicationDBContext>(options =>
            {
                options.UseSqlServer("Persist Security Info=False;Server=TWS3650.database.windows.net;Initial Catalog=AppDesign;User ID=ClassSharedUser;Password=AppDesign2024_Pa");
            });

            builder.Services.AddRadzenComponents();

            builder.Services.AddHttpClient("DALL·E", client =>
            {
                client.BaseAddress = new Uri("https://api.openai.com");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "sk-proj-FrFjF4JW0pI56CaWwcJOT3BlbkFJnF2Ty3xDAlnbpBTpTWwE");
            });

            builder.Services.AddScoped<ImageService>(sp =>
            {
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                
              
                var httpClient = httpClientFactory.CreateClient("DALL·E");
                return new ImageService(httpClient);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}

