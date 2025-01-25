using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.DTO.Request;
using User.Domain.Services.Interface;

namespace User.Infrastructure.Service.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly IEmailService _emailService;
        private static readonly ConcurrentDictionary<string, (string Otp, DateTime Expiration)> OtpStorage = new ConcurrentDictionary<string, (string, DateTime)>();

        public LoginService(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task SendEmailUsingOAuth2(string toEmail, string subject, string body)
        {
            try
            {
                // Load client secrets from the JSON file downloaded from Google Cloud
                var clientSecrets = new ClientSecrets
                {
                    ClientId = "",
                    ClientSecret = ""
                };

                // Scopes for Gmail API
                var scopes = new[] { "https://mail.google.com/" };

                // Create the OAuth2 credential
                var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    clientSecrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore("TokenStore", true)
                );
                // Check if the token is expired and refresh if necessary
                //if (credential.Token.IsExpired(Google.Apis.Util.SystemClock.Default))
                //{
                //    await credential.RefreshTokenAsync(CancellationToken.None);
                //}
                // Create the email
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Urja", "urja.bajracharya@gmail.com"));
                message.To.Add(new MailboxAddress("", toEmail));
                message.Subject = subject;
                message.Body = new TextPart("plain") { Text = body };

                // Send the email using MailKit's SmtpClient with OAuth2
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                    // Use the OAuth2 access token to authenticate
                    var oauth2 = new SaslMechanismOAuth2("urja.bajracharya@gmail.com", credential.Token.AccessToken);
                    await client.AuthenticateAsync(oauth2);

                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
                
                Console.WriteLine(ex.StackTrace);
            }
        }
        public async Task<bool> SendOtpForPasswordResetAsync(string toEmail)
        {
            var otp = GenerateOtp(); 
            var subject = "Password Reset OTP";
            var body = $"Your OTP for password reset is: {otp}. This OTP is valid for 10 minutes.";
            var emailverify = new EmailDto()
            {
                To = toEmail,
                Subject = subject,
                Body = body
            };
            await _emailService.SendEmail(emailverify);
            //await SendEmailUsingOAuth2(toEmail, subject, body);

            
            StoreOtpForUser(toEmail, otp, DateTime.UtcNow.AddMinutes(10));
            return true;
        }
        private string GenerateOtp(int length = 6)
        {
            var random = new Random();
            var otp = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                otp.Append(random.Next(0, 10)); 
            }

            return otp.ToString();
        }
        private void StoreOtpForUser(string email, string otp, DateTime expiration)
        {
            OtpStorage[email] = (otp, expiration);
        }
        public async Task<bool> ValidateOtp(string email, string otp)
        {
            if (OtpStorage.TryGetValue(email, out var storedOtpInfo))
            {
                if (storedOtpInfo.Expiration > DateTime.UtcNow && storedOtpInfo.Otp == otp)
                {
                    // OTP is valid
                    return true;
                }
            }
            // OTP is invalid or expired
            return false;
        }
        public async Task<bool> RemoveOtp(string email, string otp)
        {
            // Optionally, remove OTP from storage after successful reset
            OtpStorage.TryRemove(email, out _);
            return true;
        }
    }
}
