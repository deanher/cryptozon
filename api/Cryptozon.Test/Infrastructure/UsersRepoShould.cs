using System;
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

      var expectedUser = new User("deanher@gmail.com",
                                  Guid.NewGuid().ToString("N"),
                                  Guid.NewGuid().ToString("N"),
                                  Guid.NewGuid());
      mockDatabaseAdapter.Setup(adapter =>
                                  adapter.ExecuteAsync<User>(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
                         .ReturnsAsync(expectedUser);

      var usersRepo = new UsersRepo(mockDatabaseAdapter.Object);

      //when
      var user = await usersRepo.GetUserAsync("deanher@gmail.com");

      //then
      Assert.NotNull(user);
      Assert.Equal(expectedUser.UserId, user.UserId);
      Assert.Equal(expectedUser.Username, user.Username);
      Assert.Equal(expectedUser.PasswordSalt, user.PasswordSalt);
      Assert.Equal(expectedUser.PasswordHash, user.PasswordHash);
    }
  }
}
