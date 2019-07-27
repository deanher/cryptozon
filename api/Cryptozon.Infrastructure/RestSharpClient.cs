using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace Cryptozon.Infrastructure
{
  public class RestSharpClient : IHttpRestClient
  {
    private readonly RestClient _client;

    public RestSharpClient(string baseUrl)
    {
      _client = new RestClient(baseUrl);
    }

    public async Task<T> ExecuteAsync<T>(string resource, Method httpMethod, IDictionary<string, string> headers)
    {
      var request = CreateRequest(resource, httpMethod, headers);
      var response = await _client.ExecuteTaskAsync<T>(request);
      if (!response.IsSuccessful)
        // todo: custom exception?
        throw new ApplicationException(response.ErrorMessage);

      return response.Data;
    }

    private IRestRequest CreateRequest(string resource, Method method, IDictionary<string, string> headers)
    {
      var request = new RestRequest(resource, method);
      if (headers == null)
        return request;

      foreach (var (key, value) in headers)
      {
        request.AddHeader(key, value);
      }

      return request;
    }
  }
}