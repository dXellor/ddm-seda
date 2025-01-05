using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using seda_api.Validators.Auth;
using seda_bll.Contracts;
using seda_bll.Dtos.Auth;
using seda_bll.Dtos.Users;

namespace seda_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }
    
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [Authorize]
    [HttpPut]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser([FromBody] UserDto updatedUser)
    {
        try
        {
            var updatedUserDto = await _userService.UpdateAsync(updatedUser);
    
            if (updatedUserDto == null)
            {
                return NotFound($"User with email {updatedUser.Email} not found.");
            }

            return Ok(updatedUserDto);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while updating the user.");
        }
    }
    
    //Auth endpoints
    [HttpPost("register")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegistrationDto registrationDto)
    {
        var validator = new RegistrationDtoValidator();
        var validationResult = await validator.ValidateAsync(registrationDto);
        if (!validationResult.IsValid) 
            return BadRequest(validationResult.Errors.First());
        
        var result = await _userService.Register(registrationDto);
        if (result != null)
        {
            return Created("Success", result);
        }
        return BadRequest("Unable to register user");
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var validator = new LoginDtoValidator();
        var validationResult = await validator.ValidateAsync(loginDto);
        if (!validationResult.IsValid) 
            return BadRequest(validationResult.Errors.First());
        
        var result = await _userService.Login(loginDto);
        if (result == null)
        {
            _logger.LogInformation("{@RequestName} by {@UserInfo} from {@IpAddress}", "Failed login", loginDto.Email, Request.HttpContext.Connection.RemoteIpAddress.ToString());
            return Unauthorized("Invalid credentials");
        }
        
        return Ok(result);
    }
}

