using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecFlowApiFramework;

namespace SpecFlowApiFramework
{
    public class ApiClient : IApiClient, IDisposable
    {
        readonly RestClient client;

        public ApiClient(string baseUrl) 
        {
            var options = new RestClientOptions(baseUrl)
            {
                Authenticator = new ApiAuthenticator()
            };
            client = new RestClient(options);


        }
        public async Task<RestResponse> CreateUser<T>(T payload) where T : class
        {
            var request = new RestRequest(ApiConstants.Create_User, Method.Post);
            request.AddBody(payload);
            return await client.ExecuteAsync(request);
        }

        public async Task<RestResponse> DeleteUser(string id)
        {
            var request = new RestRequest(ApiConstants.Delete_User, Method.Delete);
            request.AddUrlSegment(id, id);
            return await client.ExecuteAsync(request);
        }
        public static void Main()
        {

        }

        public void Dispose()
        {
            client?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<RestResponse> GetUser(string id)
        {
            var request = new RestRequest(ApiConstants.GetSingle_User, Method.Get);
            request.AddUrlSegment(id, id);
            return await client.ExecuteAsync(request);

        }

        public async Task<RestResponse> ListofUsers(int PageNumber)
        {
            var request = new RestRequest(ApiConstants.GetSingle_User, Method.Get);
            request.AddQueryParameter("page", PageNumber);
            return await client.ExecuteAsync(request);
        }

        public async Task<RestResponse> UpdateUser<T>(T payload, string id) where T : class
        {
            var request = new RestRequest(ApiConstants.Update_User, Method.Put);
            request.AddUrlSegment(id, id);
            request.AddBody(payload);
            return await client.ExecuteAsync<T>(request);
        }
    }
}
