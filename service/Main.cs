using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

using UserApp.Domain;
using UserApp.Repository;
using UserApp.Service.Users.Mapper;
using UserApp.Service.Users;
using UserApp.Service.UsersScreens;
using Microsoft.EntityFrameworkCore;

namespace UserApp.Service
{
    public static class Main
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services, string userAppConnectionString)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserScreenService, UserScreenService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<UserAppContext>(options => options.UseSqlServer(userAppConnectionString));
            var configurationMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
            });

            services.AddAutoMapper(
                typeof(UserProfile)
            );

            return services;
        }
    }
}
