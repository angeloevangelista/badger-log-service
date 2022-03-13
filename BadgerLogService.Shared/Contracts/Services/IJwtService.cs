namespace BadgerLogService.Shared.Contracts.Services
{
  public interface IJwtService
  {
    string GenerateToken<T>(T payloadObject, string identifierProperty);

    T DecodeToken<T>(string token, bool validateExpiration = true);
    bool CheckTokenIsTrustworthy(string token);
  }
}
