using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Chat.WebAPIClientLibrary.Implementation
{
    public class WebRequestBuilder
    {
        protected readonly string _address;
        protected readonly Dictionary<string, string> _headers;
        protected object? _requestBody;
        protected HttpMethod _httpMethod;

        public WebRequestBuilder(string address)
        {
           //if (!Uri.IsWellFormedUriString(_address, UriKind.Absolute))
           //{
           //    throw new ArgumentException("Invalid URL format", nameof(_address));
           //}

            _address = address;
            _headers = new Dictionary<string, string>();
            _httpMethod = HttpMethod.Get;
        }

        public WebRequestBuilder WithJsonBody<TRequestBody>(TRequestBody body)
        {
            _requestBody = body;

            return this;
        }

        public WebRequestBuilder WithHttpMethod(HttpMethod httpMethod)
        {
            _httpMethod = httpMethod;

            return this;
        }

        public WebRequestBuilder WithHeader(string name, string value)
        {
            _headers.TryAdd(name, value);

            return this;
        }

        public WebRequestBuilder WithJwtAccessToken(string token)
        {
            _headers.TryAdd("Authorization", $"Bearer {token}");

            return this;
        }

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