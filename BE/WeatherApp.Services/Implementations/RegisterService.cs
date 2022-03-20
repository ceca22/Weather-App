using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using WeatherApp.DataAccess.Interfaces;
using WeatherApp.Domain.Models;
using WeatherApp.Models.UserModel;
using WeatherApp.Services.Interfaces;
using WeatherApp.Shared.Exceptions;

namespace WeatherApp.Services.Implementations
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _userRepository;
        public RegisterService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void Register(UserRegisterModel userRegisterModel)
        {
            ValidateUser(userRegisterModel);

            var md5 = new MD5CryptoServiceProvider();

            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(userRegisterModel.Password));

            var hashedPassword = Encoding.ASCII.GetString(md5Data);

            User user = new User()
            {
                FirstName = userRegisterModel.FirstName,
                LastName = userRegisterModel.LastName,
                Email = userRegisterModel.Email,
                Password = hashedPassword
                
            };


            _userRepository.Add(user);
            _userRepository.SaveChanges();



        }

        private void ValidateUser(UserRegisterModel userRegisterModel)
        {
            if (string.IsNullOrEmpty(userRegisterModel.Email) || string.IsNullOrEmpty(userRegisterModel.Password) || string.IsNullOrEmpty(userRegisterModel.FirstName) || string.IsNullOrEmpty(userRegisterModel.LastName))
            {
                throw new UserException("The properties Email,Password,Firstname and Lastname are required fields");
            }
            if (userRegisterModel.Email.Contains("@") == false)
            {
                throw new UserException("Email is not in the correct format!");
            }
            if (userRegisterModel.FirstName.Length > 50 || userRegisterModel.LastName.Length > 50)
            {
                throw new UserException("Firstname and Lastname can contain maximum 50 characters!");
            }
            if (!IsUserNameUnique(userRegisterModel.Email, _userRepository))
            {
                throw new UserException("Someone already registered with this email!");
            }
            if (userRegisterModel.Password != userRegisterModel.ConfirmPassword)
            {
                throw new UserException("The passwords do not match!");
            }
            if (!IsPasswordValid(userRegisterModel.Password))
            {
                throw new UserException("The password should be more than 5 character and should contain numbers as well!");
            }
        }


        private bool IsPasswordValid(string password)
        {
            Regex passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            return passwordRegex.Match(password).Success;
        }

        private bool IsUserNameUnique(string username, IUserRepository userRepository)
        {
            if (userRepository.GetByUsername(username) != null)
            {
                return false;
            }
            return true;
        }
    }
}
