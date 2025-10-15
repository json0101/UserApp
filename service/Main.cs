using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

using UserApp.Domain;
using UserApp.Repository;
using Microsoft.EntityFrameworkCore;
using UserApp.Service.Global;
using UserApp.Service.Services.Users;
using UserApp.Service.Services.Users.Mapper;
using UserApp.Service.Services.UsersApplications;
using UserApp.Service.Services.UsersScreens;
using UserApp.Service.Services.Autentication;
using UserApp.Service.Services.Screens.Service;
using UserApp.Service.Services.Application;
using UserApp.Service.Services.RolesScreens;
using UserApp.Service.Services.Roles;

namespace UserApp.Service
{
    public static class Main
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services, string userAppConnectionString, int applicationId)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserScreenService, UserScreenService>();
            services.AddScoped<IUserApplicationService, UserApplicationService>();
            services.AddScoped<IUserAppAuthService, UserAppAuthService>();
            services.AddScoped<IScreenService, ScreenService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IRoleScreenService, RoleScreenService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<UserAppContext>(options => options.UseSqlServer(userAppConnectionString));
            var configurationMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
            });

            services.AddAutoMapper(
                typeof(UserProfile)
            );

            ApplicationGlobal.ApplicationGlobalID = applicationId;
            return services;
        }
    }
}
