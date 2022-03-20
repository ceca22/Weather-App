using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Models.UserModel;

namespace WeatherApp.Services.Interfaces
{
    public interface IRegisterService
    {
        void Register(UserRegisterModel userRegisterModel);
    }
}
