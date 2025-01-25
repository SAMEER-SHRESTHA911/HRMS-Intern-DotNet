using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static User.Application.Common.CommonUtilities;
using User.Application.DTO.Request;
using User.Application.DTO.Response;
using User.Domain.Entities;

namespace User.Application.Manager.Interface
{
    public interface ILoginManager
    {
      
        Task<ServiceResult<bool>> ChangePassword(ChangePasswordRequesDTO changePasswordRequest);
        Task<ServiceResult<bool>> SendEmail(EmailDto request);
        Task<ServiceResult<LoginResponses>> LoginUserAsync(LoginRequestDTO loginRequest);
        Task<ServiceResult<bool>> RequestOTP(RequestOtpDto request);
        Task<ServiceResult<bool>> ResetPassword(ResetPasswordDto request);
        Task<ServiceResult<TokenResponseDto>> GetDetailsFromToken(string token);
    }
}
