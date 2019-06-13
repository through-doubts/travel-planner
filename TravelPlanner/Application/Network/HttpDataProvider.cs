using System.Collections.Generic;
using RestSharp;
using TravelPlanner.Infrastructure.Network;

namespace TravelPlanner.Application.Network
{
    public class HttpDataProvider
    {
        private readonly HttpApi httpApi;

        protected HttpDataProvider(HttpApi httpApi)
        {
            this.httpApi = httpApi;
        }

        protected IRestResponse SendRequestAndGetResponse(Method method, Dictionary<string, string> headers,
            Dictionary<string, string> parameters, string resource, string url)
        {
            return httpApi.SendRequestAndReturnResponse(method, headers, parameters, resource, url);
        }
    }
}