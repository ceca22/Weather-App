using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using WeatherApp.DataAccess.Interfaces;
using WeatherApp.Domain.Models;
using WeatherApp.Mappers;
using WeatherApp.Models.UserModel;
using WeatherApp.Services.Interfaces;
using WeatherApp.Shared.Exceptions;

namespace WeatherApp.Services.Implementations
{
    public class UserService : IService<UserRegisterModel>
    {
        private  IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserRegisterModel AddEntity(UserRegisterModel entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(int id)
        {
            User deleteUser = _userRepository.GetById(id);
            if (deleteUser == null)
            {
                throw new NotFoundException($"User with Id {id} was not found");
            }
            _userRepository.Delete(deleteUser);
        }

        public List<UserRegisterModel> GetAllEntities()
        {
            List<User> userDb = _userRepository.GetAll().ToList();
            List<UserRegisterModel> userRegisterModels = new List<UserRegisterModel>();
            foreach (User user in userDb)
            {
                userRegisterModels.Add(user.ToUserRegisterModel());
            }
            return userRegisterModels;
        }

        public UserRegisterModel GetEntityById(int id)
        {
            User userDb = _userRepository.GetById(id);
            if (userDb == null)
            {
                throw new NotFoundException($"User with id {id} was not found");
            }
            return userDb.ToUserRegisterModel();
        }

        public void UpdateEntity(UserRegisterModel userRegisterModel)
        {
            User userDb = _userRepository.GetById(userRegisterModel.Id);
            if (userDb == null)
            {
                throw new NotFoundException($"User with Id {userRegisterModel.Id} was not found");
            }

            if (string.IsNullOrEmpty(userRegisterModel.Email) || string.IsNullOrEmpty(userRegisterModel.Password) || string.IsNullOrEmpty(userRegisterModel.FirstName) || string.IsNullOrEmpty(userRegisterModel.LastName))
            {
                throw new UserException("The properties Email,Password,Firstname and Lastname are required");
            }
            if (!userRegisterModel.Email.Contains("@"))
            {
                throw new UserException("The property Email is not in the correct format!");
            }
            if (userRegisterModel.FirstName.Length > 50 || userRegisterModel.LastName.Length > 50)
            {
                throw new UserException("Firstname and Lastname can contain maximum 50 characters!");
            }



            if (!IsPasswordValid(userRegisterModel.Password))
            {
                throw new UserException("The password should be more than 5 character and must contain numbers as well!");
            }

            var md5 = new MD5CryptoServiceProvider();

            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(userRegisterModel.Password));

            var hashedPassword = Encoding.ASCII.GetString(md5Data);

            userDb.FirstName = userRegisterModel.FirstName;
            userDb.LastName = userRegisterModel.LastName;
            userDb.Email = userRegisterModel.Email;
            userDb.Password = hashedPassword;

            _userRepository.Update(userDb);
        }

        private bool IsPasswordValid(string password)
        {
            Regex passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            return passwordRegex.Match(password).Success;
        }
    }
}
