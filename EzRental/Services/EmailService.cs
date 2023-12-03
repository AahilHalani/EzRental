using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace DB_API_20K0290.Services
{
    public class EmailService : IEmailService
    {   
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendEmail(Email _email)
        {
            try
            {
                _email.From = _configuration.GetSection("EmailUserName").Value;
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_email.From));
                email.To.Add(MailboxAddress.Parse(_email.To));
                email.Subject = _email.Subject;
                email.Body = new TextPart(TextFormat.Text) { Text = _email.Body };

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                    smtp.Authenticate(_email.From, _configuration.GetSection("EmailPassword").Value);
                    smtp.Send(email);
                }

                return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
