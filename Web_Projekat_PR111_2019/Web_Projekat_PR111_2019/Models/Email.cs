namespace Web_Projekat_PR111_2019.Models
{
    public class Email
    {
        public string From { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SmtpServer { get; set; }
    }
}
