namespace Cryptozon.ApplicationService.Users
{
  public class AuthenticatedUser
  {
    public string Username { get; }
    public string FirstName { get; }
    public string Surname { get; }
    public string Token { get; }

    public AuthenticatedUser(string username, string firstName, string surname, string token)
    {
      Username = username;
      FirstName = firstName;
      Surname = surname;
      Token = token;
    }
  }
}