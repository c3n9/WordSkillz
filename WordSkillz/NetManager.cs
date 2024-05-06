using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WordSkillz
{
    public static class NetManager
    {
        static string URL = "http://wordskillz-001-site1.btempurl.com/";
        static HttpClient httpClient = new HttpClient();
        public static async Task<T> Get<T>(string controller)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("11175566:60-dayfreetrial")));

            var response = await httpClient.GetAsync(URL + controller);
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(content);
            return data;
        }

        public static async Task<HttpResponseMessage> Post<T>(string controller, T data)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("11175566:60-dayfreetrial")));

            var jsonData = JsonConvert.SerializeObject(data);
            var response = await httpClient.PostAsync(URL + controller, new StringContent(jsonData, Encoding.UTF8, "application/json"));
            return response;
        }

        public static async Task<HttpResponseMessage> Put<T>(string controller, T data)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("11175566:60-dayfreetrial")));

            var jsonData = JsonConvert.SerializeObject(data);
            var response = await httpClient.PutAsync(URL + controller, new StringContent(jsonData, Encoding.UTF8, "application/json"));
            return response;
        }

        public static async Task<HttpResponseMessage> Delete<T>(string controller)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("11175566:60-dayfreetrial")));

            var response = await httpClient.DeleteAsync(URL + controller);
            return response;
        }
    }
}
