using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cryptozon.Infrastructure;
using Moq;
using RestSharp;
using Xunit;

namespace Cryptozon.Test.Infrastructure
{
  public class ProductsRepoShould
  {
    [Fact]
    public async Task GetProducts()
    {
      //given
      var mock = new Mock<IHttpRestClient>();
      var expected = new CoinMarketCapResponse
                     {
                       Status = new RequestStatus { ErrorCode = 0, ErrorMessage = string.Empty },
                       Data = new List<Coin>
                              {
                                new Coin
                                {
                                  Id = 1027,
                                  Name = "Ethereum",
                                  Symbol = "ETH",
                                  MaxSupply = 10000000l,
                                  TotalSupply = 99999l,
                                  Quote = new Dictionary<string, Quote>
                                          {{"USD", new Quote{Price = 209.770552851m}}}
                                }
                              }
                     };

      mock.Setup(client => client.ExecuteAsync<CoinMarketCapResponse>(It.IsAny<string>(), It.IsAny<Method>(), It.IsAny<Dictionary<string, string>>()))
          .ReturnsAsync(expected);

      var repo = new ProductsRepo(mock.Object, "0123456789ABCDEF");

      //when
      var products = (await repo.GetProductsAsync()).ToList();

      //then
      Assert.NotNull(products);
      Assert.NotEmpty(products);
      var expectedCrypto = expected.Data.First();
      Assert.Contains(products, product =>
                                  product.Name == expectedCrypto.Name &&
                                  product.Symbol == expectedCrypto.Symbol &&
                                  product.Id == expectedCrypto.Id &&
                                  product.Price == expectedCrypto.Quote["USD"].Price);
    }
  }
}