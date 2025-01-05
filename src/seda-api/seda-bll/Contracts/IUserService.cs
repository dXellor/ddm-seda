using seda_bll.Dtos.Auth;
using seda_bll.Dtos.Users;

namespace seda_bll.Contracts;

public interface IUserService: ICrudService<UserDto>
{
    Task<UserDto?> Register( RegistrationDto registrationDto );
    Task<LoginResponseDto?> Login( LoginDto loginDto );
}