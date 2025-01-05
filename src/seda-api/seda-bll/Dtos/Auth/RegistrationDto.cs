namespace seda_bll.Dtos.Auth;

public class RegistrationDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string RepeatedPassword { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}