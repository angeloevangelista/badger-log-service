using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using BadgerLogService.Shared.Contracts.Services;
using BadgerLogService.Shared.Exceptions;
using Newtonsoft.Json;

namespace BadgerLogService.Shared.Services
{
  public class JwtService : IJwtService
  {
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
      this._configuration = configuration;
    }

    public bool CheckTokenIsTrustworthy(string token)
    {
      var tokenIsTrustworthy = true;

      var appSecret = this._configuration
        .GetSection("Security")
        .GetSection("AppSecret")
        .Value;

      var tokenHandler = new JwtSecurityTokenHandler();
      var encodedAppSecret = Encoding.ASCII.GetBytes(appSecret);

      try
      {
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(encodedAppSecret),
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = false,
        }, out SecurityToken validatedToken);
      }
      catch
      {
        tokenIsTrustworthy = false;
      }

      return tokenIsTrustworthy;
    }

    public T DecodeToken<T>(string token, bool validateExpiration = true)
    {
      var appSecret = this._configuration
        .GetSection("Security")
        .GetSection("AppSecret")
        .Value;

      var tokenHandler = new JwtSecurityTokenHandler();
      var encodedAppSecret = Encoding.ASCII.GetBytes(appSecret);

      try
      {
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(encodedAppSecret),
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = validateExpiration,
          ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;

        var tokenPayload = JsonConvert.DeserializeObject<T>(
          jwtToken.Payload.SerializeToJson()
        );

        return tokenPayload;
      }
      catch
      {
        throw new BusinessException("Invalid payload format.");
      }
    }

    public string GenerateToken<T>(T payloadObject, string identifierProperty)
    {
      var payloadType = payloadObject.GetType();

      var securitySection = this._configuration.GetSection("Security");

      var appSecret = securitySection.GetSection("AppSecret").Value;

      var ExpiresIn = long.Parse(
        securitySection.GetSection("ExpiresIn").Value
      );

      var tokenHandler = new JwtSecurityTokenHandler();
      var encodedAppSecret = Encoding.ASCII.GetBytes(appSecret);

      var payloadProperties = payloadType.GetProperties();

      var tokenClaims = payloadProperties
        .Select(p =>
          new Claim(
            p.Name,
            p.GetValue(payloadObject).ToString()
          )
        );

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Issuer = payloadType.GetProperty(identifierProperty).GetValue(payloadObject).ToString(),
        Subject = new ClaimsIdentity(tokenClaims),
        Expires = DateTime.UtcNow.AddHours(ExpiresIn),
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(encodedAppSecret),
          SecurityAlgorithms.HmacSha256Signature
        )
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}
