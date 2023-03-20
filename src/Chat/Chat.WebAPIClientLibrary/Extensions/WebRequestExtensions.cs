using Newtonsoft.Json;
using System.Net;

namespace Chat.WebAPIClientLibrary.Extensions
{
    public static class WebRequestExtensions
    {
        public static async Task<WebResponse> GetWebResponseAsync(this WebRequest request)
        {
            try
            {
                return await request.GetResponseAsync();
            }
            catch (WebException ex)
            {
                return ex.Response;
            }
        }
    }
}
