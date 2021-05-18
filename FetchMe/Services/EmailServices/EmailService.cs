using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using FetchMe.Models.EmailConfigurationModels;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FetchMe.Services.EmailServices
{
    public class EmailService : IEmailSender
    {
        public EmailConfiguration _emailConfig { get; }

        public EmailService(IOptions<EmailConfiguration> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new EmailMessage(new string[] { email }, subject, htmlMessage);

            var emailMessage = CreateEmailMessage(message);

            //return Execute(emailMessage)

            return SendAsync(emailMessage);

            //return;
        }

        
        /*
        [Obsolete]
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new EmailMessage(new string[] { email }, subject, htmlMessage);

            var emailMessage = CreateEmailMessage(message);

            await SendAsync(emailMessage);
        }
        */
        
        public void SendEmail(EmailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        public async Task SendEmailAsync(EmailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);

            await SendAsync(emailMessage);
        }

        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("FetchMe.Photography", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            var builder = new BodyBuilder
            {
                // Set the plain-text version of the message text
                HtmlBody = message.Content
            };
            emailMessage.Body = builder.ToMessageBody();
            //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }
        
        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
        
        private async Task SendAsync(MimeMessage mailMessage)
        {
            
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
            
        }

        
    }
}
