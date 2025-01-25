using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace User.Application.Helpers
{
    public static class ApiClient
    {
        public static async Task<RestResponse> Get(string baseUrl, string apiUrl)
        {
            RestClient restClient = new RestClient(baseUrl);
            RestRequest request = new RestRequest(apiUrl);
            request.AddHeader("Secret-key", AppServiceHelper.Configuration["ApiSecretKey"]);
            return await restClient.ExecuteAsync(request);
        }
        public static async Task<RestResponse> Post(string baseUrl, string body, string apiUrl)
        {
            RestClient restClient = new RestClient(baseUrl);
            RestRequest request = new RestRequest(apiUrl, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Secret-key", AppServiceHelper.Configuration["ApiSecretKey"]);
            request.AddBody(body, "application/json");
            return await restClient.ExecuteAsync(request);
        }

    }
}
