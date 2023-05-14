using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;
using WaterCompanyServicesAPI;

namespace WaterCompanyServiceWebSite
{
    public static class DataAccess
    {
        private static string BaseURL = "https://wcsapi.bsite.net/";
        //private static string BaseURL = "https://localhost:7186/";
        public static User CurrentUser = null;

        public static User Login(User user)
        {
            User result = null;
            using (var httpClient = new HttpClient())
            {
                String json = JsonConvert.SerializeObject(user);
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
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

        public static bool UserNameExists(string userName)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{BaseURL}user/exists/{userName}")                
                };

                using (var response = httpClient.SendAsync(request))
                {
                    if (response.Result.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<bool>(response.Result.Content.ReadAsStringAsync().Result);
                    }
                }
                return true;
            }
        }

        public static void AddConsumer(Consumer consumer)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String json = JsonConvert.SerializeObject(consumer);
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri($"{BaseURL}consumer"),
                        Content = new StringContent(json, Encoding.UTF8, "application/json"),
                    };

                    using (var response = httpClient.SendAsync(request))
                    {
                        if (!response.Result.IsSuccessStatusCode)
                        {
                            throw new Exception();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<User> GetUsers()
        {
            List<User> result = new List<User>();
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{BaseURL}user"),
                };

                using (var response = httpClient.SendAsync(request))
                {
                    if (response.Result.IsSuccessStatusCode)
                    {
                        result = response.Result.Content.ReadFromJsonAsync<List<User>>().Result;
                    }
                }
                return result;
            }
        }

        public static User GetUser(int id)
        {
            User result = null;
            try
            { 
                using (var httpClient = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"{BaseURL}user/{id}"),
                    };

                    using (var response = httpClient.SendAsync(request))
                    {
                        if (response.Result.IsSuccessStatusCode)
                        {
                            result = response.Result.Content.ReadFromJsonAsync<User>().Result;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
            return result;
        }

        public static void UpdateUser(User user)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String json = JsonConvert.SerializeObject(user);
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Put,
                        RequestUri = new Uri($"{BaseURL}user/{user.Id}"),
                        Content = new StringContent(json, Encoding.UTF8, "application/json"),
                    };

                    using (var response = httpClient.SendAsync(request))
                    {
                        if (response.Result.IsSuccessStatusCode)
                        {
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
