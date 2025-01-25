using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.DTO.Request;
using User.Domain.Services.Interface;


namespace User.Infrastructure.Service.Implementation
{
    public class EmailService : IEmailService
    {
        public async Task SendEmail(EmailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("raheem.sipes@ethereal.email"));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("raheem.sipes@ethereal.email", "fm4VsBNDwhwY8DPTtG");
            smtp.Send(email);
            smtp.Disconnect(true);

        }
    }
}
