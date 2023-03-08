using System.Net;

namespace Chat.WebAPIClientLibrary.Interfaces
{
    internal interface IHttpResponseHandler<T>
    {
        Task<T>? HandleResponse(HttpWebResponse response);
    }
}
