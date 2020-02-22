using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32.TaskScheduler;
namespace Overwatch
{
    class cInstallTaskScheduler
    {
        public cInstallTaskScheduler() {
            TaskService.Instance.AddTask("Overwatch", QuickTriggerType.Daily, "Overwatch.exe", "-a arg");
        }
    }
}
