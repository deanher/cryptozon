using System.Threading.Tasks;
using Cryptozon.ApplicationService.Users;
using Cryptozon.Domain.Users;
using Moq;
using Xunit;

namespace Cryptozon.Test.ApplicationServices
{
  public class UsersRegistrationShould
  {
    [Fact]
    public async Task RegisterUser()
    {
      //given
      var mockUsersRepo = new Mock<IUsersRepo>();

      var expected = new User("Dean", "Herringer", "deanher@gmail.com", "salt", "hash");
      mockUsersRepo.Setup(repo => repo.RegisterUserAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                                         It.IsAny<string>(), It.IsAny<string>()))
                   .ReturnsAsync(expected);

      var userRegistration = new UsersRegistration(mockUsersRepo.Object);

      //when
      var user = await userRegistration.RegisterAsync("Dean", "Herringer", "deanher@gmail.com", "password");

      //then
      Assert.False(userRegistration.HasError, userRegistration.ErrorMessage);
      Assert.NotNull(user);
      Assert.Equal(expected.FirstName, user.FirstName);
      Assert.Equal(expected.Surname, user.Surname);
      Assert.Equal(expected.Username, user.Username);
      Assert.NotNull(user.PasswordSalt);
      Assert.NotNull(user.PasswordHash);
    }
  }
}
