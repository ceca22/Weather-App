using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Shared.Helpers
{
    public class DataProccessor
    {
        public async Task<string> LoadData(string city)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={ city }&appid=d556833ecd60adcc995eac657208f750";

            using(HttpResponseMessage response = await ApiHelper.client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Task<string> content = response.Content.ReadAsStringAsync();
                    return content.Result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            return null;
        }
    }
}
