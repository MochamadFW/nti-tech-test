using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NTI_Technical_Test.Models;
using NTI_Technical_Test.Services;

namespace NTI_Technical_Test.Controllers
{
    [Route("api/v1/[controller]")]
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

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                // Check existing user email (unique validation)
                var checkUser = await _userService.GetUserByEmailAsync(user.Email);
                if (checkUser != null) {
                    return BadRequest(new
                    {
                        status = false,
                        message = "Unable to register new user because email is already registered!"
                    });
                }

                var registerUser = await _userService.Register(user);

                return Created();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"ArgumentException: {ex}");
                return BadRequest(new
                {
                    status = false,
                    message = ex
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = false,
                    message = "An unexpected error occurred." + ex
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _userService.Login(loginDto.Email, loginDto.Password);
                if (token == null)
                {
                    return Unauthorized(new { 
                        status = false,
                        message = "Invalid email or password"
                    });
                }

                return Ok(new { 
                    status = true,
                    message = "Login Success!",
                    data = "Bearer " + token
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"ArgumentException: {ex}");
                return BadRequest(new
                {
                    status = false,
                    message = ex
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = false,
                    message = "An unexpected error occurred." + ex
                });
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var result = await _userService.Logout(token);

                if (!result)
                {
                    return Unauthorized(new { 
                        status = false,
                        message = "Unauthorized, logout failed!"
                    });
                }

                return Ok(new { 
                    status = true,
                    message = "Logged out successfully"
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"ArgumentException: {ex}");
                return BadRequest(new
                {
                    status = false,
                    message = ex
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = false,
                    message = "An unexpected error occurred." + ex
                });
            }
        }
    }

    public class LoginDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
