using FluentValidation;
using seda_bll.Dtos.Auth;

namespace seda_api.Validators.Auth;

public class LoginDtoValidator: AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(loginDto => loginDto.Email).NotEmpty().EmailAddress();
        RuleFor(loginDto => loginDto.Password).NotEmpty();
    }
}