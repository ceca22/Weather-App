using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models.SearchModel;

namespace WeatherApp.Services.Interfaces
{
    public interface ISearchService
    {
        //Task<string> Search(CityModel search);
        Task<string> Search(string search);

    }
}
