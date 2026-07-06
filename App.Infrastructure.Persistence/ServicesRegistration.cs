using App.Infrastructure.Persistence.Contexts;
using App.Infrastructure.Persistence.Repositories;
using App.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.Persistence
{
    public static class ServicesRegistration
    {
        public static void AddPersistenceLayerIoc(this IServiceCollection services, IConfiguration config)
        {
            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<AppDbContext>(opt =>
                opt.UseInMemoryDatabase("AppDb"));
            }
            else
            {
                var connectionString = config.GetConnectionString("DefaultConnection");
                services.AddDbContext<AppDbContext>(
                    (serviceProvider, opt) =>
                    {
                        opt.UseSqlServer(connectionString,
                        m => m.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
                    },
                    contextLifetime: ServiceLifetime.Scoped,
                    optionsLifetime: ServiceLifetime.Scoped
                );


            }

            services.AddScoped(typeof(IGenericRepository<>), typeof(IGenericRepository<>));
            services.AddScoped<IGradeRepository, GradeRepository>();
        }


    }
}
