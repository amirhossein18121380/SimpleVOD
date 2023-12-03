using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using IdentityModel;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using System.Text;

namespace VOD.Service
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message);
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IOptions<SmtpOptions> options;

        public SmtpEmailSender(IOptions<SmtpOptions> options)
        {
            this.options = options;
        }
        public async Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message)
        {
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "541306234427-1d4lpjtjrfovid9cnf824trhpo4qi1tl.apps.googleusercontent.com",
                    ClientSecret = "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"
                },
                new[] { GmailService.Scope.GmailSend },
                "user",
                CancellationToken.None);

            var service = new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "VOD"
            });

            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("VOD Support", fromAddress));
            mailMessage.To.Add(new MailboxAddress("Recipient Name", toAddress));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart("plain") { Text = message };

            var rawMessage = Base64Url.Encode(Encoding.UTF8.GetBytes(mailMessage.ToString()));
            var messageRequest = new Message { Raw = rawMessage };

            await service.Users.Messages.Send(messageRequest, "me").ExecuteAsync();
        }


        //public async Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message)
        //{
        //    var mailMessage = new MailMessage(fromAddress, toAddress, subject, message);

        //    using (var client = new SmtpClient(options.Value.Host, options.Value.Port)
        //    {
        //        UseDefaultCredentials = false, // Make sure to set this to false
        //        Credentials = new NetworkCredential(options.Value.Username, options.Value.Password),
        //        EnableSsl = true // Enable SSL/TLS

        //    })
        //    {
        //        await client.SendMailAsync(mailMessage);
        //    }
        //}
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using (var smtpClient = new SmtpClient())
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("amirhossein.gholamitousi@gmail.com"), // Replace with a valid sender email
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
