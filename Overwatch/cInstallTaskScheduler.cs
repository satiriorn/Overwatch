using Microsoft.Win32.TaskScheduler;
namespace Overwatch
{
    class cInstallTaskScheduler
    {
        public static void AddTask(){
            TaskService.Instance.AddTask("Overwatch", QuickTriggerType.Daily, "Overwatch.exe", "-a arg");
        }
    }
}
    