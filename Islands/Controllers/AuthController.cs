using BLL.DTOs;
using BLL.Exceptions;
using BLL.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ValidateModel]
        public IActionResult Registration(UserRegistrationRequestDto userRegistrationResquest)
        {
            try
            {
                _authService.Registration(userRegistrationResquest);
                return Ok("User successfully created.");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("Email")) 
                {
                    return BadRequest("A user with this email address already exists.");
                }

                if (ex.InnerException != null && ex.InnerException.Message.Contains("Username"))
                {
                    return BadRequest("A user with this username already exists.");
                }

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ValidateModel]
        public IActionResult VerifyEmail(string token)
        {
            try
            {
                _authService.VerifyEmail(token);
                return Ok("Email successfully confirmed.");
            }
            catch (EmailValidationTokenExpiredException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Token does not exist.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ValidateModel]
        public IActionResult ResendValidationEmail(string username)
        {
            try
            {
                _authService.ResendEmailValidationEmail(username);
                return Ok("Email Validation email sended.");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("User does not exist.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ValidateModel]
        public IActionResult Login(UserLoginRequestDto userLoginRequest)
        {
            try
            {
                _authService.Login(userLoginRequest, out string token);
                return Ok(token);
            }
            catch (LoginValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("User does not exist.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ResetPasswordRequest(string email)
        {
            try
            {
                _authService.ResetPasswordRequest(email);
                return Ok("Reset password email sended");
            }
            catch (InvalidOperationException)
            {
                return NotFound("User does not exist with this email.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SetNewPassword(string token, string password)
        {
            try
            {
                _authService.SetNewPassword(token, password);
                return Ok();
            }
            catch (PasswordResetTokenExpiredException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException)
            {
                return NotFound("there is no such token.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}
