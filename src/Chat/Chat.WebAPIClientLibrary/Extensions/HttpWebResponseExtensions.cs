using Chat.Core.DTOs.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chat.WebAPIClientLibrary.Extensions
{
    public static class HttpWebResponseExtensions
    {
        public async static Task<T> TryDeserialize<T>(this HttpWebResponse httpWebResponse)
        {
            using (var reader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                var content = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
        }
    }
}
