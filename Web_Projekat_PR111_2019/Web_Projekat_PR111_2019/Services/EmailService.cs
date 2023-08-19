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
        private readonly Email emailConfig;
        public IConfiguration configuration;

        public EmailService(Email emailConfig)
        {
            this.emailConfig = emailConfig;
        }

       
        public void SlanjePoruke(string email, string verifikacija)
        {
            var email_ = new MimeMessage();
            email_.From.Add(MailboxAddress.Parse("milosavalazetic@outlook.com"));
            email_.To.Add(MailboxAddress.Parse(email));

            email_.Subject = "Verifikacija";
            email_.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = "Korisnikova verifikacija " + verifikacija };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("milosavalazetic@outlook.com", "milosava12345");
            smtp.Send(email_);
            smtp.Disconnect(true);


        }
    }
}
