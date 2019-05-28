using DataObjects;
using System.Diagnostics;

namespace Services
{
    public static class ProcessWorker
    {
        public static bool KillProcess(ProcessInfo processInfo)
        {
            try
            {
                var process = Process.GetProcessById(processInfo.Id);
                process.Kill();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}