using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace mySharedLib
{
    public class MyLog
    {
        private static readonly string LogFilePath;

        static MyLog()
        {
            var entryPath = Assembly.GetEntryAssembly()?.Location;
            var logFileName = Path.GetFileName(entryPath);
            if (string.IsNullOrEmpty(logFileName)) logFileName = "log";
            LogFilePath = Path.Combine(Utility.ExecutingAssemblyPath(), $"{logFileName}.log");

        }

        public static void Log(string msg)
        {
            using (var w = File.AppendText(LogFilePath))
            {
                w.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}\t{msg}");
            }
            Debug.WriteLine(msg);

        }

        public static bool OpenLog(out string msg)
        {
            msg = string.Empty;
            if (!File.Exists(LogFilePath))
            {
                msg = $"日志文件\"{LogFilePath}\"不存在！";
                return false;
            }
            try
            {
                var p = new Process()
                {
                    StartInfo = new ProcessStartInfo(LogFilePath) { UseShellExecute = true }
                };
                p.Start();

            }
            catch (Exception e)
            {
                msg = e.Message;
                return false;
            }

            return true;
        }

        public static void ClearLog()
        {
            if (!File.Exists(LogFilePath)) return;
            var fileStream = File.Open(LogFilePath,FileMode.Open);
            fileStream.SetLength(0);
            fileStream.Close(); // This flushes the content, too.
        }
    }
}
