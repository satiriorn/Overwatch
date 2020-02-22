using System;
using System.Diagnostics;
using System.IO;

namespace Overwatch
{
    class cProcesses
    {
        public static bool IsFileActiveProcess(string pathFile)
        {
            string processName = string.Empty;
            // Get the name of the file without the extension for the given file path
            processName = Path.GetFileNameWithoutExtension(pathFile);
            foreach (Process proc in Process.GetProcessesByName(processName))
            {
                if (proc.MainModule.FileName == pathFile)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool KillFileProcess(string pathFile, bool killAllCopy = false)
        {
            string processName = string.Empty;

            // Got a file name without extension at the given file path
            processName = Path.GetFileNameWithoutExtension(pathFile);

            foreach (Process proc in Process.GetProcessesByName(processName))
            {
                if (!killAllCopy)
                {
                    if (proc.MainModule.FileName == pathFile)
                    {
                        proc.Kill();
                        return true;
                    }
                }
                else
                {
                    proc.Kill(); 
                    return true;
                }
            }

            return false;
        }
    }
}
