using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using User.Application.DTO.Request;
using User.Application.DTO.Response;
using User.Application.Manager.Interface;
using User.Domain.Entities;
using User.Domain.Enum;
using User.Domain.Services.Interface;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static User.Application.Common.CommonUtilities;

namespace User.Application.Manager.Implementation
{
    public class LoginManager : ILoginManager
    {
        private readonly ILoginService _loginService;
        private readonly IEmployeeService _employeeService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;


        public LoginManager(ILoginService loginService, IEmployeeService employeeService, IEmailService emailService, IConfiguration configuration)
        {
            _loginService = loginService;
            _employeeService = employeeService;
            _emailService = emailService;
            _configuration = configuration;
        }
       

        public async Task<ServiceResult<LoginResponses>> LoginUserAsync(LoginRequestDTO loginRequest)
        {
            if (loginRequest == null)
            {
                return new ServiceResult<LoginResponses>
                {

                    Result = ResultStatus.unHandeledError,
                    Message = "Enter valid email and password",
                    Data = new LoginResponses() 
                };
            }
            var getUser = await _employeeService.GetEmployeeByEmail(loginRequest.Email);
            if (getUser == null)
            {
                return new ServiceResult<LoginResponses>
                {
                    Result = ResultStatus.Ok,
                    Message = "Logged in failed! Enter vaild email",
                    Data = new LoginResponses() 
                };
            }
            bool isPasswordCorrect = VerifyPassword(loginRequest.Password, getUser.Password);
            if (isPasswordCorrect)
            {
                var token = GenerateJWTToken(getUser);
                return new ServiceResult<LoginResponses>
                {
                    Result = ResultStatus.ParameterError,
                    Message = "Logged in successfully! ",
                    Data = new LoginResponses()
                    {
                        Token = token,
                        EmployeeId = getUser.Id,
                        Role = getUser.Roles.ToString(),
                    }
                };
            }
            else
            {
                return new ServiceResult<LoginResponses>
                {
                    Result = ResultStatus.ParameterError,
                    Message = "Logged in failed!",
                    Data = new LoginResponses()
                };
            }

        }
        //public async Task<LoginResponse> LoginUserAsync(LoginRequestDTO loginRequest)
        //{
        //    var getUser = await _employeeService.GetEmployeeByEmail(loginRequest.Email);
        //    if (getUser == null) return new LoginResponse(false, "", "", 0);
        //    bool isPasswordCorrect = VerifyPassword(loginRequest.Password, getUser.Password);
        //    if (isPasswordCorrect)
        //    {
        //        var token = GenerateJWTToken(getUser);
        //        return new LoginResponse(true, "Correct", token, getUser.Id);
        //    }
        //    else
        //    {
        //        return new LoginResponse(false, "", "", 0);
        //    }
        //}

        private string GenerateJWTToken(Employee getUser)
        {
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,getUser.Id.ToString()),
                new Claim(ClaimTypes.Email,getUser.Email),
                new Claim("FullName", getUser.FirstName),
                new Claim(ClaimTypes.Role,getUser.Roles.ToString())
            };

            var token = new JwtSecurityToken(

                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: userClaims,
                    expires: DateTime.Now.AddDays(2),
                    signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ServiceResult<bool>> ChangePassword(ChangePasswordRequesDTO changePasswordRequest)
        {
            if (changePasswordRequest == null)
            {
                return new ServiceResult<bool>
                {
                    Result = ResultStatus.unHandeledError,
                    Message = "Enter email and password",
                    Data = false
                };
            }
            var checkExistingEmail = await _employeeService.GetEmployeeByEmail(changePasswordRequest.Email);
            if (checkExistingEmail == null)
            {
                return new ServiceResult<bool>
                {

                    Result = ResultStatus.unHandeledError,
                    Message = "The email adddress is not valid",
                    Data = false
                };
            }
            bool isPasswordCorrect = VerifyPassword(changePasswordRequest.OldPassword, checkExistingEmail.Password);
            if (isPasswordCorrect == false)
            {
                return new ServiceResult<bool>
                {

                    Result = ResultStatus.unHandeledError,
                    Message = "The old password is incorrect",
                    Data = false
                };
            }
            if (changePasswordRequest.OldPassword == changePasswordRequest.NewPassword)
            {
                return new ServiceResult<bool>
                {

                    Result = ResultStatus.unHandeledError,
                    Message = "The new password is same as old password. Please enter another password",
                    Data = false
                };
            }
            var password = changePasswordRequest.NewPassword;
            var passwordHash = HashPassword(password);
            checkExistingEmail.Password = passwordHash;
            checkExistingEmail.ConfirmPassword = checkExistingEmail.Password;
            await _employeeService.UpdateRegisterEmployee(checkExistingEmail);
            return new ServiceResult<bool>
            {

                Result = ResultStatus.Ok,
                Message = "Password changed successfully!",
                Data = true
            };
        }

        public async Task<ServiceResult<bool>> ResetPassword(ResetPasswordDto resetPasswordRequest)
        {
            var isOTPvalid = await _loginService.ValidateOtp(resetPasswordRequest.Email, resetPasswordRequest.Otp);
            if (isOTPvalid == false)
            {
                return new ServiceResult<bool>
                {
                    Result = ResultStatus.unHandeledError,
                    Message = "OTP is not valid",
                    Data = false
                };
            }

            var checkExistingEmail = await _employeeService.GetEmployeeByEmail(resetPasswordRequest.Email);
            bool isPasswordCorrect = VerifyPassword(resetPasswordRequest.NewPassword, checkExistingEmail.Password);
            if (isPasswordCorrect == true)
            {
                return new ServiceResult<bool>
                {

                    Result = ResultStatus.ParameterError,
                    Message = "The new password cant be same as old password",
                    Data = false
                };
            }

            var password = resetPasswordRequest.NewPassword;
            var passwordHash = HashPassword(password);
            checkExistingEmail.Password = passwordHash;
            checkExistingEmail.ConfirmPassword = checkExistingEmail.Password;
            await _employeeService.UpdateRegisterEmployee(checkExistingEmail);
            await _loginService.RemoveOtp(resetPasswordRequest.Email, resetPasswordRequest.Otp);
            return new ServiceResult<bool>
            {

                Result = ResultStatus.Ok,
                Message = "Password changed successfully!",
                Data = true
            };
        }
       
        public async Task<ServiceResult<bool>> SendEmail(EmailDto request)
        {
            _emailService.SendEmail(request);
            return new ServiceResult<bool>
            {

                Result = ResultStatus.Ok,
                Message = "Email sent successfully!",
                Data = true
            };
        }

        public async Task<ServiceResult<bool>> RequestOTP(RequestOtpDto request)
        {
            try
            {
                var checkExistingEmail = await _employeeService.GetEmployeeByEmail(request.Email);
                if (checkExistingEmail == null)
                {
                    return new ServiceResult<bool>
                    {

                        Result = ResultStatus.ParameterError,
                        Message = "The email adddress is not valid",
                        Data = false
                    };
                }
                var result = await _loginService.SendOtpForPasswordResetAsync(request.Email);
                if (result)
                {
                    return new ServiceResult<bool>
                    {

                        Result = ResultStatus.Ok,
                        Message = "OTP sent successfully!",
                        Data = true
                    };
                }
                return new ServiceResult<bool>
                {
                    Result = ResultStatus.unHandeledError,
                    Message = "OTP sent failed",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ServiceResult<TokenResponseDto>> GetDetailsFromToken(string token)
        {
            // Get token from header

            if (token.StartsWith("Bearer"))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }
            var handler = new JwtSecurityTokenHandler();

            // Returns all claims present in the token

            JwtSecurityToken jwt = handler.ReadJwtToken(token);
            int id = int.Parse(jwt.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            var name = jwt.Claims.First(claim => claim.Type == "FullName").Value;
            var email = jwt.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            var role = jwt.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;

            return new ServiceResult<TokenResponseDto>()
            {
                Result = Domain.Enum.ResultStatus.Ok,
                Message = "Token details fetched successfully",
                Data = new TokenResponseDto()
                {
                    EmployeeId = id,
                    Name = name,
                    Email = email,
                    Role = role,

                }
            };
        }
    }
}
