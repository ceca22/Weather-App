using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models.SearchModel;
using WeatherApp.Services.Interfaces;
using WeatherApp.Shared.Exceptions;

namespace WeatherApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }


        [HttpGet("{city}")]
        //public ActionResult<string> Search([FromBody] CityModel searchInput)
        public ActionResult<string> Search(string city)

        {
            try
            {
                string result = _searchService.Search(city).Result;
                return StatusCode(StatusCodes.Status200OK, result);

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
