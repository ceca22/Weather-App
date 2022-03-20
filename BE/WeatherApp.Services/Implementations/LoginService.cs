using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WeatherApp.DataAccess.Interfaces;
using WeatherApp.Domain.Models;
using WeatherApp.Models.UserModel;
using WeatherApp.Services.Interfaces;
using WeatherApp.Shared.Custom;
using WeatherApp.Shared.Exceptions;

namespace WeatherApp.Services.Implementations
{
    public class LoginService : ILoginService
    {

        private readonly IUserRepository _userRepository;

        private IOptions<AppSettings> _options;
        public LoginService(IUserRepository userRepository,  IOptions<AppSettings> options)
        {
            _userRepository = userRepository;
            _options = options;
        }
        public string Login(AuthenticateRequest userLoginModel)
        {
            var md5 = new MD5CryptoServiceProvider();

            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(userLoginModel.Password));

            var hashedPassword = Encoding.ASCII.GetString(md5Data);


            string username = _userRepository
                            .GetAll()
                            .Where(x => x.Email == userLoginModel.Email)
                            .Select(x => x.Email)
                            .FirstOrDefault();
                            

            string password = _userRepository
                            .GetAll()
                            .Where(x => x.Password == userLoginModel.Password)
                            .Select(x => x.Password)
                            .FirstOrDefault();



            User userDb = _userRepository.Login(userLoginModel.Email, hashedPassword);

            if(userDb == null)
            {
                ValidateInput(username, password);
            }

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            //get the SecretKey from AppSettings
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_options.Value.SecretKey);
            //configure the token
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(10),
                //signature definition
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature),
                //payload   

                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userDb.Id.ToString()),
                        new Claim(ClaimTypes.Name, userDb.Email),
                        new Claim("userFullName", $"{userDb.FirstName} {userDb.LastName}"),

                    }

            )



            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            // convert it to string
            string tokenString = jwtSecurityTokenHandler.WriteToken(token);

            return tokenString;


        }




        private void ValidateInput(string username, string password)
        {
            if (username == null && password != null)
            {
                throw new UserException($"Incorrect username!");
            }
            if (password == null && username != null)
            {
                throw new UserException($"Incorrect password!");
            }
            if (password == null && username == null)
            {
                throw new UserException($"No User found!");
            }

        }
    }
}
