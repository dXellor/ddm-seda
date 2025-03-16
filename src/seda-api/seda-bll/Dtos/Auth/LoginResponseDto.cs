using seda_bll.Dtos.Users;

namespace seda_bll.Dtos.Auth;

public class LoginResponseDto
{
    public UserDto User { get; set; } 
    public string AccessToken { get; set; }
}