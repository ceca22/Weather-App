using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models.UserModel;
using WeatherApp.Services.Interfaces;
using WeatherApp.Shared.Exceptions;

namespace WeatherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;


        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost]
        public IActionResult Register([FromBody] UserRegisterModel user)
        {
            try
            {
                _registerService.Register(user);
                return StatusCode(StatusCodes.Status201Created, "You have been successfully registered!");
            }
            catch (UserException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
