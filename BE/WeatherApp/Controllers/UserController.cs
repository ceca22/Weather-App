using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models.UserModel;
using WeatherApp.Services.Interfaces;
using WeatherApp.Shared.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IService<UserRegisterModel> _userService;
        public UserController(IService<UserRegisterModel> userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserRegisterModel>> Get()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _userService.GetAllEntities());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<UserController>/5

        [HttpGet("{id}")]
        public ActionResult<UserRegisterModel> Get(int id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _userService.GetEntityById(id));
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        // PUT api/<UserController>/
        [HttpPut]
        public IActionResult Put([FromBody] UserRegisterModel user)
        {
            try
            {
                _userService.UpdateEntity(user);
                return StatusCode(StatusCodes.Status200OK, "User Updated!");
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (UserException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<UserController>/5

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _userService.DeleteEntity(id);
                return StatusCode(StatusCodes.Status200OK, "User Deleted!");
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



    }
}
