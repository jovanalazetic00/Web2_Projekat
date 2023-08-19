using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Web_Projekat_PR111_2019.Interfaces.IServices;
using Web_Projekat_PR111_2019.Models;
using MailKit;
using MimeKit.Text;
using System.Threading;

namespace Web_Projekat_PR111_2019.Services
{
    public class EmailService : IEmailService
    {
        private readonly Email emailKonfiguracija;
        public IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        private Email EmailConfig
        {
            get
            {
                var emailConfig = configuration.GetSection("EmailConfig").Get<Email>();
                return emailConfig;
            }
        }

        public void SlanjePoruke(string email, string verifikacija)
        {
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse("milosavalazetic@outlook.com"));
            mail.To.Add(MailboxAddress.Parse(email));

            mail.Subject = "Verifikacija";
            mail.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = "Korisnikova verifikacija " + verifikacija };

            using var smtp = new SmtpClient(new ProtocolLogger("smtp.log"));

            ;
            smtp.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("milosavalazetic@outlook.com", "milosava12345");
            smtp.Send(mail);
            smtp.Disconnect(true);


        }
    }
}
