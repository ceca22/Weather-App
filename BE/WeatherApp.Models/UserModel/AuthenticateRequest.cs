using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models.UserModel
{
    public class AuthenticateRequest
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
