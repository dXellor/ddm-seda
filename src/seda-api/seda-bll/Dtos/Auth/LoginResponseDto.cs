namespace seda_bll.Dtos.Auth;

public class LoginResponseDto
{
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string AccessToken { get; set; }
}