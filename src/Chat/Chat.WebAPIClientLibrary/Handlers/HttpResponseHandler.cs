using Chat.WebAPIClientLibrary.Exceptions;
using Chat.WebAPIClientLibrary.Exceptions.Internal;
using Chat.WebAPIClientLibrary.Extensions;
using System.Net;

namespace Chat.WebAPIClientLibrary.Handlers
{
    /// <summary>
    /// Обработчик ответов
    /// 
    /// Приоритет выполнения Обработчиков:
    /// 1. Handle для конкретного Status-кода
    /// 2. Остальные обработчики
    /// </summary>
    /// <typeparam name="T">Response Model</typeparam>
    internal class HttpResponseHandler<T> : IHttpResponseHandler<T>
    {
        // Обработчики
        protected readonly IDictionary<HttpStatusCode, Func<HttpWebResponse, Task<T>>> _responseHandlers;
        protected Func<HttpWebResponse, Task<T>>? _successHandler;             // HTTP Code 200-299 
        protected Func<HttpWebResponse, Task<T>>? _clientErrorHandler;         // HTTP Code 400-499 
        protected Func<HttpWebResponse, Task<T>>? _internalServerErrorHandler; // HTTP Code 400-499 
        protected Func<HttpWebResponse, Task<T>>? _othersHandler;              // Для всех остальных кодов, которые не были указаны 

        public HttpResponseHandler()
        {
            _responseHandlers = new Dictionary<HttpStatusCode, Func<HttpWebResponse, Task<T>>>();
        }

        /// <summary>
        /// Обработать конкретный HTTP-код
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="responseHandler">Обработчик ответа от сервера, если код ответа равен <paramref name="statusCode"/></param>
        /// <returns></returns>
        public HttpResponseHandler<T> Handle(HttpStatusCode statusCode, Func<HttpWebResponse, Task<T>> responseHandler)
        {
            _responseHandlers[statusCode] = responseHandler;

            return this;
        }

        /// <summary>
        /// Обработать HTTP-коды 2XX (Success)
        /// </summary>
        /// <param name="handler">Обработчик ответа от сервера, если код ответа 2XX</param>
        /// <returns></returns>
        public HttpResponseHandler<T> HandleSuccess(Func<HttpWebResponse, Task<T>> handler)
        {
            _successHandler = handler;

            return this;
        }

        /// <summary>
        /// Обработать HTTP-коды 4XX (Client Error)
        /// </summary>
        /// <param name="handler">Обработчик ответа от сервера, если код ответа 4XX</param>
        /// <returns></returns>
        public HttpResponseHandler<T> HandleClientError(Func<HttpWebResponse, Task<T>> handler)
        {
            _clientErrorHandler = handler;

            return this;
        }

        /// <summary>
        /// Обработать HTTP-коды 5XX (Internal Server Error)
        /// </summary>
        /// <param name="handler">Обработчик ответа от сервера, если код ответа 5XX</param>
        /// <returns></returns>
        public HttpResponseHandler<T> HandleInternalServerError(Func<HttpWebResponse, Task<T>> handler)
        {
            _internalServerErrorHandler = handler;

            return this;
        }

        /// <summary>
        /// Обработать остальные HTTP-коды, для которых не были указаны обработчики
        /// </summary>
        /// <param name="handler">Обработчик ответа от сервера</param>
        /// <returns></returns>
        public HttpResponseHandler<T> HandleOthers(Func<HttpWebResponse, Task<T>> handler)
        {
            _othersHandler = handler;

            return this;
        }

        /// <summary>
        /// Выполнить обработку запроса
        /// </summary>
        /// <param name="response">Ответ от сервера</param>
        /// <returns>Результат выполнения обработчика ответа от сервера</returns>
        /// <exception cref="ResponseHandlerNotSpecifiedException"></exception>
        public Task<T> HandleResponse(HttpWebResponse response)
        {
            var statusCode = (int)response.StatusCode;

            if (_responseHandlers.TryGetValue(response.StatusCode, out var handler))
            {
                return handler(response);
            }
            else if (statusCode.Between(200, 299) && _successHandler is not null)
            {
                return _successHandler(response);
            }
            else if (statusCode.Between(400, 499) && _clientErrorHandler is not null)
            {
                return _clientErrorHandler(response);
            }
            else if (statusCode.Between(500, 599) && _internalServerErrorHandler is not null)
            {
                return _internalServerErrorHandler(response);
            }
            else
            {
                if (_othersHandler is null)
                    throw new UnexpectedStatusCodeException();

                return _othersHandler.Invoke(response);
            }
        }
    }
}
