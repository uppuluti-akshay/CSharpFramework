using Microsoft.Extensions.Configuration;
using Microsoft.Testing.Platform.Configurations;
using RestSharp;
using System.Net;

namespace SpecflowCSharpFramework.Utilities
{
    public class ApiSetUp : IDisposable
    {
        private RestClient _client;
        private bool _disposed;

        private Uri BaseApiUrl { get; }

        public ApiSetUp(IConfigurationRoot config)
        {
            BaseApiUrl = config.GetValue<Uri>("baseApiUrl")
                         ?? throw new ArgumentNullException(nameof(BaseApiUrl), "BaseApiUrl cannot be null.");
            _client = new RestClient(new RestClientOptions
            {
                BaseUrl = BaseApiUrl,
                UserAgent = "SpecflowCSharpFramework",
                FollowRedirects = true
            });
        }

        public RestResponse ExecuteRequest(RestRequest request)
        {
            if (_client == null)
                throw new InvalidOperationException("RestClient is not initialized.");

            var response = _client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}, Content: {response.Content}");
            }

            return response;
        }

        public void AddDefaultHeaders(Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                _client.AddDefaultHeader(header.Key, header.Value);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _client?.Dispose();
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
