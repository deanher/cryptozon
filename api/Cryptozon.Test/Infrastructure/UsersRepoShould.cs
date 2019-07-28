using System;
using System.Data;
using System.Threading.Tasks;
using Cryptozon.Domain.Users;
using Cryptozon.Infrastructure;
using Dapper;
using Moq;
using Xunit;

namespace Cryptozon.Test.Infrastructure
{
  public class UsersRepoShould
  {
    [Fact]
    public async Task GetUser()
    {
      //given
      var mockDatabaseAdapter = new Mock<IDatabaseAdapter>();

      var expectedUser = new User("Dean", "Herringer", "deanher@gmail.com",
                                  Guid.NewGuid().ToString("N"), 
                                  Guid.NewGuid().ToString("N"),
                                  Guid.NewGuid());
      mockDatabaseAdapter.Setup(adapter =>
                                  adapter.ExecuteQueryAsync<User>(It.IsAny<string>(), It.IsAny<DynamicParameters>(), It.IsAny<CommandType>()))
                         .ReturnsAsync(new[] { expectedUser });

      var usersRepo = new UsersRepo(mockDatabaseAdapter.Object);

      //when
      var user = await usersRepo.GetUserAsync("deanher@gmail.com");

      //then
      Assert.NotNull(user);
      Assert.Equal(expectedUser.UserId, user.UserId);
      Assert.Equal(expectedUser.Username, user.Username);
      Assert.Equal(expectedUser.PasswordSalt, user.PasswordSalt);
    }
  }
}
