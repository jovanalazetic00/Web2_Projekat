using MimeKit;

namespace Web_Projekat_PR111_2019.Models
{
    public class EmailMessage
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }

        public string Content { get; set; }

        public EmailMessage(IEnumerable<string> to, string subject, string content)
        {
            To = to.Select(address => new MailboxAddress(null, address)).ToList();
            Subject = subject;
            Content = content;
        }
    }
}
