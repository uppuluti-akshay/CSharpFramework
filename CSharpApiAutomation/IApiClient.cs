using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowApiFramework
{
    public interface IApiClient
    {

        Task<RestResponse> CreateUser<T>(T payload) where T : class;
        Task<RestResponse> UpdateUser<T>(T payload, string id) where T : class;
        Task<RestResponse> GetUser(string id);
        Task<RestResponse> DeleteUser(string id);
        Task<RestResponse> ListofUsers(int PageNumber);
        
    }
}