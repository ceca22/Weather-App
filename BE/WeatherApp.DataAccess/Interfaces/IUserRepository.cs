using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Domain.Models;

namespace WeatherApp.DataAccess.Interfaces
{
    public interface IUserRepository:IRepository<User>
    {
        User GetByUsername(string username);
        User Login(string username, string password);
    }
}
