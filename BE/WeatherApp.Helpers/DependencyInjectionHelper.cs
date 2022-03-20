using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.DataAccess;
using WeatherApp.DataAccess.Implementation;
using WeatherApp.DataAccess.Interfaces;
using WeatherApp.Models.UserModel;
using WeatherApp.Services.Implementations;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            //services.AddDbContext<WeatherAppDbContext>(x =>
            //    x.UseSqlServer(connectionString));

            services.AddDbContext<WeatherAppDbContext>(options => 
            options.UseMySQL(connectionString));
        }

        public static void InjectRepository(IServiceCollection services)
        {

            services.AddTransient<IUserRepository, UserRepository>();

        }


        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IService<UserRegisterModel>, UserService>();
            services.AddTransient<IRegisterService, RegisterService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<ISearchService, SearchService>();

        }
    }
}
