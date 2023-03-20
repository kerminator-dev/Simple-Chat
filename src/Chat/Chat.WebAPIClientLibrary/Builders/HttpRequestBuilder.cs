using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Chat.WebAPIClientLibrary.Builders
{
    public class WebRequestBuilder
    {
        protected readonly string _address;
        protected readonly Dictionary<string, string> _headers;
        protected object? _requestBody;
        protected HttpMethod _httpMethod;

        public WebRequestBuilder(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                throw new ArgumentNullException(nameof(address), "Address is not specified!");
            }

            _address = address;
            _headers = new Dictionary<string, string>();
            _httpMethod = HttpMethod.Get;
        }

        /// <summary>
        /// С json-телом
        /// </summary>
        /// <typeparam name="TRequestBody"></typeparam>
        /// <param name="body"></param>
        /// <returns></returns>
        public WebRequestBuilder WithJsonBody<TRequestBody>(TRequestBody body)
        {
            _requestBody = body;

            return this;
        }

        /// <summary>
        /// С HTTP-методом. По умолчанию GET.
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        public WebRequestBuilder WithHttpMethod(HttpMethod httpMethod)
        {
            _httpMethod = httpMethod;

            return this;
        }

        /// <summary>
        /// С заголовком
        /// </summary>
        /// <param name="key">Ключ заголовка</param>
        /// <param name="value">Значение</param>
        /// <returns></returns>
        public WebRequestBuilder WithHeader(string key, string value)
        {
            _headers.TryAdd(key, value);

            return this;
        }

        /// <summary>
        /// С JWT-токеном
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public WebRequestBuilder WithJwtAccessToken(string token)
        {
            _headers.TryAdd("Authorization", $"Bearer {token}");

            return this;
        }

        /// <summary>
        /// Построить запрос
        /// </summary>
        /// <returns>Запрос</returns>
        public async Task<WebRequest> Build()
        {
            var request = WebRequest.Create(_address);

            request.Method = _httpMethod.ToString();

            // Добавление заголовков
            if (_headers.Count > 0)
            {
                foreach (var header in _headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            // Добавление json'а
            if (_requestBody is not null)
            {
                var requestDataString = JsonConvert.SerializeObject(_requestBody);
                var requestDataBytes = Encoding.UTF8.GetBytes(requestDataString);
                request.ContentType = "application/json";
                request.ContentLength = requestDataBytes.Length;

                using (var requestStream = await request.GetRequestStreamAsync())
                {
                    await requestStream.WriteAsync(requestDataBytes);
                }
            }

            return request;
        }
    }
}