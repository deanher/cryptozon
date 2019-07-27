using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cryptozon.Domain.Products;
using RestSharp;

namespace Cryptozon.Infrastructure
{
  public class ProductsRepo : IProductsRepo
  {
    private const string ApiKeyHeader = "X-CMC_PRO_API_KEY";
    private readonly IHttpRestClient _client;

    private readonly IDictionary<string, string> _headerValues = new Dictionary<string, string>
                                                               {
                                                                 {
                                                                   "Accept",
                                                                   "application/json"
                                                                 },
                                                                 {
                                                                   "Accept-Encoding",
                                                                   "deflate/gzip"
                                                                 }
                                                               };

    public ProductsRepo(IHttpRestClient client, string key)
    {
      _client = client;
      _headerValues.Add(ApiKeyHeader, key);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
      const string resource = @"/cryptocurrency/listings/latest";
      var response = await _client.ExecuteAsync<CoinMarketCapResponse>(resource, Method.GET, _headerValues);
      if (response.Status.ErrorCode > 0)
        throw new ApplicationException(response.Status.ErrorMessage);

      return CreateProducts(response);
    }

    private IEnumerable<Product> CreateProducts(CoinMarketCapResponse response)
    {
      // inject currency code if allowed to purchase in different currencies
      const string currency = "USD";
      var products = from coin in response.Data
                     let quote = coin.Quote[currency]
                     select Product.Create(coin.Id, coin.Name, coin.Symbol, quote.Price);

      return products;
    }
  }
}