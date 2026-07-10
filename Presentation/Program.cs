using App.Infrastructure.Persistence;
using App.Infrastructure.Identity;
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
            await app.Services.RunDataSeedAsync();

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
            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            await app.RunAsync();
        }
    }
}
