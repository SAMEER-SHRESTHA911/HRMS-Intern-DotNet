using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Services.Interface
{
    public interface ILoginService
    {
        Task SendEmailUsingOAuth2(string toEmail, string subject, string body);
        Task<bool> SendOtpForPasswordResetAsync(string toEmail);
        Task<bool> ValidateOtp(string email, string otp);
        Task<bool> RemoveOtp(string email, string otp);
    }
}
