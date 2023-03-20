using System.Net;

namespace Chat.WebAPIClientLibrary.Handlers
{
    internal interface IHttpResponseHandler<T>
    {
        Task<T>? HandleResponse(HttpWebResponse response);
    }
}
