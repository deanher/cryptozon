using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptozon.Infrastructure;
using Moq;
using RestSharp;
using Xunit;

namespace Cryptozon.Test
{
  public class ProductsRepoShould
  {
    [Fact]
    public async Task GetProducts()
    {
      //given
      var mock = new Mock<IHttpRestClient>();
      var expected = new
                     {
                       Status = new {ErrorCode = 0, ErrorMessage = string.Empty},
                       Data = new[]
                              {
                                new
                                {
                                  Id = 1027,
                                  Name = "Ethereum",
                                  Symbol = "ETH",
                                  MaxSupply = 10000000l,
                                  TotalSupply = 99999l,
                                  Quote = new Dictionary<string, dynamic>
                                          {{"USD", new Quote {Price = 10087.3779318m}}}
                                }
                              }
                     };

      mock.Setup(client => client.ExecuteAsync<dynamic>(It.IsAny<string>(), It.IsAny<Method>(), It.IsAny<Dictionary<string, string>>()))
          .ReturnsAsync(expected);

      var repo = new ProductsRepo(mock.Object, "0123456789ABCDEF");

      //when
      var products = await repo.GetProductsAsync();

      //then
      Assert.NotNull(products);
      Assert.NotEmpty(products);
    }
  }
}