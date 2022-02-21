using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Amiibopedia.Helpers
{
    public class HttpHelper<T>
    {
        public async Task<T> GetRestServiceDataAsync(string serviceAddress)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(serviceAddress);
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            _ = response.EnsureSuccessStatusCode();
            string jsonResult = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(jsonResult);
            return result;
        }
    }
}
