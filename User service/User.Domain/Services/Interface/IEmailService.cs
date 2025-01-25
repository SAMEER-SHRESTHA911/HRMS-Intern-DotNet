using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.DTO.Request;

namespace User.Domain.Services.Interface
{
    public interface IEmailService
    {
        Task SendEmail(EmailDto request);
    }
}
