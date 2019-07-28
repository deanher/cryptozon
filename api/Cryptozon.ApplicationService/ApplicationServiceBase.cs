namespace Cryptozon.ApplicationService
{
  public class ApplicationServiceBase
  {
    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);
    public string ErrorMessage { get; protected set; }
  }
}