using System;
using System.IO;
using System.Text;

namespace Overwatch
{
    class appLog
    {
        public void Write(string inputText, bool newLine = false){saveLog(inputText, newLine);}
        public void Write(object inputText, bool newLine = false){saveLog(inputText.ToString(), newLine);}

        private void saveLog(string inputText, bool newLine = false)
        {
            string textLog = $" {inputText}\r\n";
            if (newLine)
                textLog = "\r\n" + $"[{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}] " + "\r\n" + textLog;

            string log_file_path = Path.Combine(Path.GetDirectoryName(appConfing.MyFullPath), "Log.txt");

            using (FileStream fs = new FileStream(log_file_path, FileMode.Append))
            {
                byte[] bytes = Encoding.Default.GetBytes(textLog);
                fs.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
