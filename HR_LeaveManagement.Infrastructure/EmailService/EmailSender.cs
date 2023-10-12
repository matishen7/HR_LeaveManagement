using HR_LeaveManagement.Application.Contracts.Email;
using HR_LeaveManagement.Application.Contracts.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace HR_LeaveManagement.Infrastructure.EmailService
{
    public class EmailSender : IEmailSender
    {
        public EmailSettings _emailSettings { get; }
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            this._emailSettings = emailSettings.Value;
        }
        public async Task<bool> SendEmail(EmailMessage email)
        {
            var client = new SendGridClient(_emailSettings.Apikey);
            var to = new EmailAddress(email.To);
            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };

            var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
            var response = await client.SendEmailAsync(message);
            return HttpStatusCode.OK == response.StatusCode || HttpStatusCode.Accepted == response.StatusCode;
        }
    }
}
