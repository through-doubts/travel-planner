using System.Collections.Generic;
using RestSharp;

namespace TravelPlanner.Infrastructure.Network
{
    public class HttpApi
    {
        private RestRequest GetRequest(
            Method method, Dictionary<string, string> headers,
            Dictionary<string, string> parameters, string resource) 
        {
            var request = new RestRequest(resource, method);
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }

            foreach (var parameter in parameters)
            {
                request.AddParameter(parameter.Key, parameter.Value);
            }

            return request;
        }

        public IRestResponse SendRequestAndReturnResponse(Method method, Dictionary<string, string> headers,
            Dictionary<string, string> parameters, string resource, string url)
        {
            var client = new RestClient(url);
            var request = GetRequest(method, headers, parameters, resource);

            var response = client.Execute(request);
            if (!response.IsSuccessful) throw new NetworkException(response.Content);
            return response;
        }
    }
}