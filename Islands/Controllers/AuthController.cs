using BLL.Exceptions;
using BLL.Services.AuthService;
using DAL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

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
        public async Task<IActionResult> Registration(RegistrationRequestDto userRegistrationResquest)
        {
            try
            {
                await _authService.RegistrationAsync(userRegistrationResquest);

                return Ok("User successfully created.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            try
            {
                if (await _authService.VerifyEmailAsync(token))
                {
                    return Ok("Email verified.");
                }

                return StatusCode(500, "Validation token expired.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> ResendVerifyEmail() 
        { 
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                await _authService.ResendVerifyEmailAsync(username);

                return Ok("Email verification email sended.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Login(LoginRequestDto userLoginRequest)
        {
            try
            {
                string token = await _authService.LoginAsync(userLoginRequest);

                return Ok(token);
            }
            catch (LoginValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Authorize]
        [ValidateModel]
        public async Task<IActionResult> GetEmailValidationDate()
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                DateTime emailValidationDate = await _authService
                    .GetEmailValidationDateByUsernameAsync(username);

                return Ok(emailValidationDate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        public async Task<IActionResult> SetTemporaryPassword(string email)
        {
            try
            {
                await _authService.SetTemporaryPasswordAsync(email);

                return Ok("Email with temporary password sended.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Authorize]
        [ValidateModel]
        public async Task<IActionResult> ResetPassword(PasswordChangeDto passwordReset)
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "Username").Value;
                await _authService.UpdatePasswordAsync(username, passwordReset);

                return Ok("Password successfully reseted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
