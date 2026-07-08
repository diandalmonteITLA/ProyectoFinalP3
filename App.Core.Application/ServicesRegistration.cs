using AutoMapper.Configuration;
using System.Reflection;
using App.Core.Application.Helpers;
using App.Core.Application.Interfaces;
using App.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Transactions;

namespace App.Core.Application
{
    public static class ServicesRegistration
    {
        public static void AddApplicationLayerIoc(this IServiceCollection services)
        {
            #region Configurations
            var currentAssembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(currentAssembly);
            });
            #endregion
            #region Services IOC
            services.AddScoped<IGuardianService, GuardianService>();
            services.AddScoped<IGradeService, GradeService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddTransient<IPhoneNumberValidator, PhoneNumberValidator>();
            #endregion
        }

    }
}
