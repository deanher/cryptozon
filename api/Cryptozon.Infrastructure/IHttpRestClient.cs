using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace Cryptozon.Infrastructure
{
  public interface IHttpRestClient
  {
    Task<T> ExecuteAsync<T>(string resource, Method httpMethod, IDictionary<string, string> headers);
  }
}