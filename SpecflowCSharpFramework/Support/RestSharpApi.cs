using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SpecflowCSharpFramework.Support
{
    public class RestSharpApi
    {
        private readonly RestClient _client;

        public RestSharpApi(string baseUrl, IAuthenticator? authenticator = null)
        {
            var options = new RestClientOptions(baseUrl)
            {
                Authenticator = authenticator,
                ThrowOnAnyError = false, // Prevent exceptions for non-2xx responses
                Timeout = TimeSpan.FromSeconds(10) // Set a default timeout of 10 seconds
            };

            _client = new RestClient(options);
        }

        /// <summary>
        /// Adds default headers to the RestClient.
        /// </summary>
        public void AddDefaultHeaders(Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                _client.AddDefaultHeader(header.Key, header.Value);
            }
        }

        /// <summary>
        /// Sends a GET request to the specified endpoint.
        /// </summary>
        public async Task<RestResponse> GetAsync(string endpoint, Dictionary<string, string>? queryParams = null)
        {
            var request = new RestRequest(endpoint, Method.Get);
            AddQueryParameters(request, queryParams);
            return await ExecuteRequestAsync(request);
        }

        /// <summary>
        /// Sends a POST request to the specified endpoint with a JSON body.
        /// </summary>
        public async Task<RestResponse> PostAsync(string endpoint, object body)
        {
            var request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(body);
            return await ExecuteRequestAsync(request);
        }

        /// <summary>
        /// Sends a PUT request to the specified endpoint with a JSON body.
        /// </summary>
        public async Task<RestResponse> PutAsync(string endpoint, object body)
        {
            var request = new RestRequest(endpoint, Method.Put);
            request.AddJsonBody(body);
            return await ExecuteRequestAsync(request);
        }

        /// <summary>
        /// Sends a PATCH request to the specified endpoint with a JSON body.
        /// </summary>
        public async Task<RestResponse> PatchAsync(string endpoint, object body)
        {
            var request = new RestRequest(endpoint, Method.Patch);
            request.AddJsonBody(body);
            return await ExecuteRequestAsync(request);
        }

        /// <summary>
        /// Sends a DELETE request to the specified endpoint.
        /// </summary>
        public async Task<RestResponse> DeleteAsync(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Delete);
            return await ExecuteRequestAsync(request);
        }

        /// <summary>
        /// Executes the request and handles errors.
        /// </summary>
        private async Task<RestResponse> ExecuteRequestAsync(RestRequest request)
        {
            var response = await _client.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
            {
                return response;
            }

            // Log or handle errors
            throw new HttpRequestException($"Request failed with status code: {response.StatusCode}, Content: {response.Content}");
        }

        /// <summary>
        /// Adds query parameters to the request.
        /// </summary>
        private void AddQueryParameters(RestRequest request, Dictionary<string, string>? queryParams)
        {
            if (queryParams == null) return;

            foreach (var param in queryParams)
            {
                request.AddQueryParameter(param.Key, param.Value);
            }
        }
    }
}
