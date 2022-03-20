using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models.SearchModel;
using WeatherApp.Services.Interfaces;
using WeatherApp.Shared.Helpers;

namespace WeatherApp.Services.Implementations
{
    public class SearchService : ISearchService
    {

        
        public async Task<string> Search(CityModel search)
        {
            ApiHelper.InitializeClient();

            DataProccessor data = new DataProccessor();
            string response = await data.LoadData(search.City);


            return response;

        }
    }
}
