using FluentValidation;
using seda_bll.Dtos.Auth;

namespace seda_api.Validators.Auth;

public class RegistrationDtoValidator: AbstractValidator<RegistrationDto>
{
    public RegistrationDtoValidator()
    {
        RuleFor(registrationDto => registrationDto.Email).NotEmpty().EmailAddress().Length(1, 255);
        RuleFor(registrationDto => registrationDto.FirstName).NotEmpty().Length(1, 25);
        RuleFor(registrationDto => registrationDto.LastName).NotEmpty().Length(1, 25);
        
        //Password rules
        RuleFor(registrationDto => registrationDto)
            .Must(registrationDto => registrationDto.Password.Equals(registrationDto.RepeatedPassword)).WithMessage("Passwords do not match");
        
        RuleFor(registrationDto => registrationDto.Password)
            .MinimumLength(16).WithMessage("Your password must be at least 16 characters long")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
            .Matches(@"[\!\?\*\.\$\-_]+").WithMessage("Your password must contain at least one special character.");
    } 
}