using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Logging;
using seda_bll.Contracts;
using seda_bll.Dtos.Auth;
using seda_bll.Dtos.Users;
using seda_dll.Contracts;
using seda_dll.Models;

namespace seda_bll.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;
 
    public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
        _tokenService = tokenService;
    }
    
    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        try
        {
            var result = await _userRepository.GetAllAsync();
            return result.Select( u => _mapper.Map<UserDto>(u)).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            throw;
        }
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var result = await _userRepository.GetByIdAsync(id);
        if (result == null) return null;
        return _mapper.Map<User, UserDto>(result);
    }

    public Task<UserDto> CreateAsync(UserDto newObject)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto> UpdateAsync(UserDto updatedObject)
    {
        var existingUser = await _userRepository.GetByEmailAsync(updatedObject.Email);

        if (existingUser == null)
        {
            return null;
        }

        _mapper.Map(updatedObject, existingUser);

        var updatedEntity = await _userRepository.UpdateAsync(existingUser);
        return _mapper.Map<User, UserDto>(updatedEntity);
    }

    public Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto?> Register(RegistrationDto registrationDto)
    {
        var userEntity = _mapper.Map<RegistrationDto, User>(registrationDto);
        userEntity.Password = HashPassword(userEntity.Password);
        try
        {
            var newUser = await _userRepository.CreateAsync(userEntity);
            return _mapper.Map<User, UserDto>(newUser);
        }
        catch (Exception e)
        {
            _logger.LogError($"Failed to register user: {e.Message}");
            return null;
        }
    }

    public async Task<LoginResponseDto?> Login(LoginDto loginDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);
        if (user == null || user.Password != HashPassword(loginDto.Password) )
            return null;
        
        var userDto = _mapper.Map<User, UserDto>(user);

        return new LoginResponseDto()
        {
            User = userDto,
            AccessToken = _tokenService.GenerateAccessToken(userDto)
        };
    }

    private static string HashPassword(string password)
    {
        var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password)); 
        return Convert.ToBase64String(hashedBytes);
    }
}