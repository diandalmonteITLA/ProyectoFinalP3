using App.Core.Application.Mappings;
using Microsoft.AspNetCore.Identity;
using App.Infrastructure.Identity;
using AutoMapper;
using App.Core.Application.Interfaces;
using App.Core.Application.Mappings;
using App.Core.Application.Services;

namespace Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(StudentMappingProfile).Assembly);
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IGuardianService, GuardianService>();
            builder.Services.AddControllers();
            builder.Services.AddRazorPages();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //builder.Services.AddAutoMapper(cfg => cfg.AddProfile<GradeMappingProfile>());
            var app = builder.Build();

            await app.Services.RunIdentitySeedAsync();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllers();
            app.MapRazorPages()
               .WithStaticAssets();

            await app.RunAsync();
        }
    }
}
