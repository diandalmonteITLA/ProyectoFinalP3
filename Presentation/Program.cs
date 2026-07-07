using App.Core.Application.Mappings;
using App.Infrastructure.Persistence;
using App.Infrastructure.Identity;
using AutoMapper;
using App.Core.Application.Interfaces;
using App.Core.Application.Services;
using App.Core.Application;

namespace Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddRazorPages();

            builder.Services.AddApplicationLayerIoc();
            builder.Services.AddIdentityLayerIoc(builder.Configuration);
            builder.Services.AddPersistenceLayerIoc(builder.Configuration);
            var app = builder.Build();

            await app.Services.RunIdentitySeedAsync();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllers();
            app.MapRazorPages()
               .WithStaticAssets();

            await app.RunAsync();
        }
    }
}
