using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.DTO.Request
{
    public class LoginRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class ChangePasswordRequesDTO
    {
        public string Email { get; set; }
        public string? OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
    public class RequestOtpDto
    {
        [Required]
        public string Email { get; set; }
    }
    public class ResetPasswordDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string Otp { get; set; }
    }
    public class ForgotPasswordDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string NewPassword { get; set; }

    }
}
