using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32.TaskScheduler;
namespace Overwatch
{
    class cInstallTaskScheduler
    {
        public void Task()
        {
            TaskService ts = new TaskService();
            TaskDefinition td = ts.NewTask();
            td.RegistrationInfo.Description = "I`m here";
            td.Triggers.Add(new DailyTrigger { DaysInterval = 2 });
            td.Actions.Add(new ExecAction("notepad.exe", "c:\\test.log", null));
            ts.RootFolder.RegisterTaskDefinition(@"Test", td);
            //ts.RootFolder.DeleteTask("Test");
        }

    }
}
