using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using seda_bll.Contracts;
using seda_bll.Dtos.Users;

namespace seda_bll.Services;

public class TokenService: ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(UserDto user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Cryptography:Tokens:JwtSecretKey"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512); 
        var claims = new List<Claim>()
        {
            new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Aud, _configuration["Cryptography:Tokens:JwtAudience"]!),
            new (JwtRegisteredClaimNames.Iss, _configuration["Cryptography:Tokens:JwtIssuer"]!),
        };

        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public JwtSecurityToken? ParseAndValidateAccessToken(string accessToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters()
        {
            ValidIssuer = _configuration["Cryptography:Tokens:JwtIssuer"],
            ValidAudience = _configuration["Cryptography:Tokens:JwtAudience"],
            ValidateLifetime = false,
            ValidateIssuer = true,
            ValidateAudience = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Cryptography:Tokens:JwtSecretKey"]!))
        };

        try
        {
            tokenHandler.ValidateToken(accessToken, validationParameters, out var validatedToken);
            return tokenHandler.ReadJwtToken(accessToken);
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public int? GetUserIdFromToken(string accessToken)
    {
        var token = ParseAndValidateAccessToken(accessToken);
        if (token != null)
        {
            var userId = token.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            if (userId != null)
            {
                return int.Parse(userId);
            }
            else
            {
                return null;
            }
        }
        return null;
    }
}