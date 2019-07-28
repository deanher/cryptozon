using System;
using System.Threading.Tasks;
using Cryptozon.ApplicationService.Users;
using Cryptozon.Domain.Users;
using Moq;
using Xunit;

namespace Cryptozon.Test.ApplicationServices
{
  public class UsersAuthenticationShould
  {
    [Fact]
    public async Task AuthenticateUser()
    {
      //given
      var mockUsersRepo = new Mock<IUsersRepo>();
      var expectedUser = new User("Dean", "Herringer", "deanher@gmail.com",
                           "0cKd3R0GSTtQwxSO8PyXEX8W6d5QUgBHd/RKGYcklfwBz9L+Yf/qCuxAwTCojvGvDAUukl9ho33wmGXLcWOvHtePKCBUtmp4QREV+/ssAEsRLTqd840l1vjFsRx1lufW0fO2wMf+gLNuGFOR1Mq0AO45Kw2Yx5ql9WihHVO9jSA=",
                           "XVGjfHLLHO2/62pvduZO/hsdFzxyLjyQw/KAxcvs7FHxHaT8Lxs6L4eaauG/fKBbrQS6iIYr9zpJ4Od6jE9fqg==");
      mockUsersRepo.Setup(repo => repo.GetUserAsync(It.IsAny<string>()))
                   .ReturnsAsync(expectedUser);

      var usersAuth = new UsersAuthentication(mockUsersRepo.Object, Guid.NewGuid().ToString(), 7);

      //when
      var authedUser = await usersAuth.AuthenticateAsync("deanher@gmail.com", "password");

      //then
      Assert.False(usersAuth.HasError, usersAuth.ErrorMessage);
      Assert.NotNull(authedUser);
      Assert.Equal(expectedUser.FirstName, authedUser.FirstName);
      Assert.Equal(expectedUser.Surname, authedUser.Surname);
      Assert.Equal(expectedUser.Username, authedUser.Username);
      Assert.NotNull(authedUser.Token);
    }
  }
}
