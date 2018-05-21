using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
namespace BAL.MailKit
{
    public class EmailService
    {
        public void SendEmailAsync(string Password, string FromEmail, string ToEmail, string UserMail, string Message, string Phone, string Name)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(FromEmail);
            mail.To.Add(ToEmail);
            mail.Subject = "Сообщение с сайта Правозащитный Союз Украины";
            mail.Body = $@"Новое сообщение от: Имя {Name} Email {UserMail}
Телефон: {Phone}
Текст сообщения: {Message}";

            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = true;
            SmtpServer.Credentials = new System.Net.NetworkCredential(FromEmail, Password);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}