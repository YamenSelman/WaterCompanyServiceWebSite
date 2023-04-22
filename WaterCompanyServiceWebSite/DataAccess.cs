using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;
using WaterCompanyServicesAPI;

namespace WaterCompanyServiceWebSite
{
    public static class DataAccess
    {
        private static string BaseURL = "https://wcsapi.bsite.net/";

        public static User Login(User user)
        {
            User result = null;
            using (var httpClient = new HttpClient())
            {
                String json = JsonConvert.SerializeObject(user);
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{BaseURL}user/login"),
                    Content = new StringContent(json, Encoding.UTF8, "application/json"),
                };

                using (var response =  httpClient.SendAsync(request))
                {
                    if(response.Result.IsSuccessStatusCode)
                    {
                        result = response.Result.Content.ReadFromJsonAsync<User>().Result;
                    }
                }
                return result;
            }
        }
    }
}
