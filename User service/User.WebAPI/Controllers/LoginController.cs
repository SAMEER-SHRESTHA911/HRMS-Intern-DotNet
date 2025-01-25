using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using User.Application.DTO.Request;
using User.Application.DTO.Response;
using User.Application.Manager.Interface;
using User.Domain.Services.Interface;
using static User.Application.Common.CommonUtilities;

namespace User.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginManager _loginManager;
        private readonly ILoginService _loginService;

        public LoginController(ILoginManager loginManager,ILoginService loginService)
        {
            _loginManager = loginManager;
            _loginService = loginService;
        }
       
        [HttpPost("ChangePassword")]
        public async Task<ServiceResult<bool>> ChangePassword(ChangePasswordRequesDTO changePasswordRequest)
        {
            var result = await _loginManager.ChangePassword(changePasswordRequest);
            return result;
        }
      
        //[HttpPost("SendEmail")]
        //public IActionResult SendEmail(EmailDto request)
        //{
        //    _loginManager.SendEmail(request);
        //    return Ok();
        //}
        [HttpPost("Login")]

        public async Task<ActionResult<LoginResponse>> Login(LoginRequestDTO loginDTO)
        {
            var result = await _loginManager.LoginUserAsync(loginDTO);
            return Ok(result);
        }
        [HttpGet("GetDetailsFromToken")]
        public async Task<ServiceResult<TokenResponseDto>> GetDetailsFromToken()
        {
            // Get token from header

            string token = Request.Headers["Authorization"];
            var result = await _loginManager.GetDetailsFromToken(token);
            return result;

        }
        // [HttpGet("GetDetailsFromToken")]
        //public IActionResult GetDetailsFromToken()
        //{
        //    // Get token from header

        //    string token = Request.Headers["Authorization"];

        //    if (token.StartsWith("Bearer"))
        //    {
        //        token = token.Substring("Bearer ".Length).Trim();
        //    }
        //    var handler = new JwtSecurityTokenHandler();

        //    // Returns all claims present in the token

        //    JwtSecurityToken jwt = handler.ReadJwtToken(token);

        //    var claims = "List of Claims: \n\n";

        //    foreach (var claim in jwt.Claims)
        //    {
        //        claims += $"{claim.Type}: {claim.Value}\n";
        //    }

        //    return Ok(claims);
        //}


        //This is email send from google cloud scope using OAuth
        //[HttpPost("EmailByAuth")]
        //public IActionResult EmailByAuth(string toEmail, string subject, string body)
        //{
        //    _loginService.SendEmailUsingOAuth2(toEmail, subject, body);

        //    return Ok(new { Message = $"Email verified successfully." });
        //}

        [HttpPost("RequestOTP")]
        public async Task<ServiceResult<bool>> RequestOTP([FromBody] RequestOtpDto request)
        {
            var result = await _loginManager.RequestOTP(request);
            return result;
        }
        [HttpPost("ResetPassword")]
        public async Task<ServiceResult<bool>> ResetPassword([FromBody] ResetPasswordDto request)
        {
            var result = await _loginManager.ResetPassword(request);
            return result;
        }

    }
}
