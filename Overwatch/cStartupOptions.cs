using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.Text;

namespace Overwatch
{
    class cStartupOptions
    {
        static appLog Log = new appLog();
        public static void ProcessStartInfo()
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                Process.Start(startInfo);
            }
            catch (Exception ex) { Log.Write(ex.Message, true); }
        }
    }
}
