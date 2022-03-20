using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Models.UserModel;

namespace WeatherApp.Services.Interfaces
{
    public interface ILoginService
    {
        string Login(AuthenticateRequest userEntityLoginModel);
    }
}
