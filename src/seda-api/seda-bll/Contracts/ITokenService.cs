using System.IdentityModel.Tokens.Jwt;
using seda_bll.Dtos.Users;

namespace seda_bll.Contracts;

public interface ITokenService
{
    string GenerateAccessToken(UserDto user);
    JwtSecurityToken? ParseAndValidateAccessToken(string accessToken);
    int? GetUserIdFromToken(string accessToken);
}