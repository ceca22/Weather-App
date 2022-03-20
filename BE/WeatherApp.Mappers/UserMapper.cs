using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Domain.Models;
using WeatherApp.Models.UserModel;

namespace WeatherApp.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this UserRegisterModel userRegisterModel)
        {
            return new User
            {
                Id = userRegisterModel.Id,
                FirstName = userRegisterModel.FirstName,
                LastName = userRegisterModel.LastName,
                Email = userRegisterModel.Email,
                Password = userRegisterModel.Password


            };
        }

        public static UserRegisterModel ToUserRegisterModel(this User user)
        {
            return new UserRegisterModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                ConfirmPassword = user.Password,
            };
        }
    }
}
