using Islands.DTOs;
using Islands.Exceptions;
using Islands.Filters;
using Islands.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService; 

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequestDto userRegistrationResquest)
        {
            try
            {
                await authService.RegistrationAsync(userRegistrationResquest);

                return StatusCode(201, "User successfully created.");
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail([FromBody] string token)
        {
            try
            {
                if (await authService.VerifyEmailAsync(token))
                {
                    return StatusCode(201, "Email verified.");
                }

                return StatusCode(500, "Validation token expired");
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResendVerifyEmail([FromBody] string username)
        {
            try
            {
                await authService.ResendVerifyEmailAsync(username);

                return StatusCode(200, "Email verification email sended.");
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Login(LoginRequestWithUsernameDto userLoginRequest)
        {
            try
            {
                string token = await authService.LoginAsync(userLoginRequest);

                return StatusCode(200, token);
            }
            catch (LoginFailedException ex)
            {
                return StatusCode(401, ex.Message);
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> SetTemporaryPassword([FromBody] string email)
        {
            try
            {
                await authService.SetTemporaryPasswordAsync(email);

                return StatusCode(201, "Email with temporary password sended.");
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        [ValidateModel]
        public async Task<IActionResult> ResetPassword(PasswordResetDto passwordReset)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                await authService.UpdatePasswordAsync(username, passwordReset);

                return Ok("Password successfully reseted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
