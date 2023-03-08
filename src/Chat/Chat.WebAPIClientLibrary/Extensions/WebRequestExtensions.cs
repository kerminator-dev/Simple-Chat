using Newtonsoft.Json;
using System.Net;

namespace Chat.WebAPIClientLibrary.Extensions
{
    public static class WebRequestExtensions
    {
        public static async Task<TResponse> GetResponseAsync<TResponse>(this WebRequest webRequest)
        {
            using (var response = await webRequest.GetResponseAsync())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<TResponse>(content);
                }
            }
        }
    }
}
