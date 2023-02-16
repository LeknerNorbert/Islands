using Islands.DTOs;
using Islands.Exceptions;
using Islands.Filters;
using Islands.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService; 

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequestDTO userRegistrationResquest)
        {
            try
            {
                await _authService.Registration(userRegistrationResquest);

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
                if (await _authService.VerifyEmail(token))
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
                await _authService.ResendEmailVerificationEmail(username);

                return StatusCode(200, "Email verification email sended.");
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Login(LoginRequestWithUsernameDTO userLoginRequest)
        {
            try
            {
                string token = await _authService.Login(userLoginRequest);

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
                await _authService.GenerateTemporaryPassword(email);

                return StatusCode(201, "Email with temporary password sended.");
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> ResetPassword(PasswordResetDTO passwordReset)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                await _authService.ResetPassword(username, passwordReset.Password);

                return Ok("Password successfully reseted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
