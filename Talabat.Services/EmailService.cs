using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Talabat.Core.Entities.EmailSettings;
using Talabat.Core.Interfaces.Services;
namespace Talabat.Services
{
    public class EmailService : ImailSettings
    {
        private MailSettings _options;
        public EmailService(IOptions<MailSettings> options)
        {
            _options = options.Value;
        }
        public void SendMail(Email email)
        {
            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_options.Email),
                Subject = email.Subject
            };
            mail.To.Add(MailboxAddress.Parse(email.To));
            mail.From.Add(new MailboxAddress(_options.DisplayName, _options.Email));

            var builder = new BodyBuilder();
            builder.TextBody = email.Body;
            mail.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            smtp.Connect(_options.Host, _options.Port, SecureSocketOptions.StartTls);

            smtp.Authenticate(_options.Email, _options.Password);
            smtp.Send(mail);

            smtp.Disconnect(true);
        }
    }
}
