using Chat.WebAPIClientLibrary.Exceptions;
using Chat.WebAPIClientLibrary.Extensions;
using Chat.WebAPIClientLibrary.Interfaces;
using System.Net;

namespace Chat.WebAPIClientLibrary.Implementation
{
    /// <summary>
    /// Обработчик ответов
    /// 
    /// Приоритет выполнения Обработчиков:
    /// Handle для конкретного Status-кода
    /// HandleSuccess
    /// HandleOthers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class HttpResponseHandler<T> : IHttpResponseHandler<T>
    {
        protected readonly IDictionary<HttpStatusCode, Func<HttpWebResponse, Task<T>>> _responseHandlers;
        protected Func<HttpWebResponse, Task<T>>? _othersHandler;
        protected Func<HttpWebResponse, Task<T>> _successHandler; // HTTP Code 200-299 

        public HttpResponseHandler()
        {
            _responseHandlers = new Dictionary<HttpStatusCode, Func<HttpWebResponse, Task<T>>>();
        }

        public HttpResponseHandler<T> Handle(HttpStatusCode statusCode, Func<HttpWebResponse, Task<T>> responseHandler)
        {
            _responseHandlers[statusCode] = responseHandler;

            return this;
        }

        public HttpResponseHandler<T> HandleSuccess(Func<HttpWebResponse, Task<T>> handler)
        {
            _successHandler = handler;

                return this;
        }

        public HttpResponseHandler<T> HandleOthers(Func<HttpWebResponse, Task<T>> responseHandler)
        {
            _othersHandler = responseHandler;

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        /// <exception cref="HttpResponseHandlerException"></exception>
        public Task<T> HandleResponse(HttpWebResponse response)
        {
            var statusCode = response.StatusCode;

            if (_responseHandlers.TryGetValue(statusCode, out var handler))
            {
                return handler(response);
            }
            else if (((int)statusCode).Between(200, 299) && _successHandler is not null)
            {
                return _successHandler(response);
            }
            else
            {
                if (_othersHandler is null)
                    throw new NoHttpResponseHandlerException("Нет явного указания для обработки полученного HTTP-кода");

                return _othersHandler.Invoke(response);
            }
        }
    }
}
