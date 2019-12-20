using System;
using System.Collections.Generic;
using System.Drawing;
using System.DirectoryServices;
using System.Windows.Forms;
using System.Text;

namespace Overwatch
{
    class cReceiveInformation
    {
        static appLog Log = new appLog();
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

        public static void ScreenShoot()
        {
            try
            {
                Graphics graph = null;
                var bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                graph = Graphics.FromImage(bmp);
                graph.CopyFromScreen(0, 0, 0, 0, bmp.Size);
                bmp.Save("D:\\Image.bmp");
            }
            catch (Exception ex) { Log.Write(ex.Message, true); }
        }

    }
}
