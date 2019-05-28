using DataObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Services
{
    public class ProcessesDataCollector
    {
        [DllImport("Kernel32.dll")]
        static extern uint QueryFullProcessImageName(IntPtr hProcess, uint flags, StringBuilder text, out uint size);

        public IList<ProcessInfo> CollectData(IList<ProcessInfo> listProcessesInfo)
        {
            var currentIds = listProcessesInfo.Select(p => p.Id).ToList();


            foreach (var process in Process.GetProcesses())
            {
                if (!currentIds.Remove(process.Id))
                {
                    var processInfo = new ProcessInfo
                    {
                        Id = process.Id,
                        ProcessName = process.ProcessName,
                        Status = process.Responding ? "Работает" : "Не отвечает",
                        UserName = process.StartInfo.EnvironmentVariables["USERNAME"],
                        FileLocation = GetPathToApp(process)
                    };

                    listProcessesInfo.Add(processInfo);
                }
            }

            foreach (var id in currentIds)
            {
                listProcessesInfo.Remove(listProcessesInfo.First(p => p.Id == id));
            }

            SystemResourceCounter.Think(listProcessesInfo);

            return listProcessesInfo;
        }


        private string GetPathToApp(Process proc)
        {
            string pathToExe = string.Empty;

            if (null != proc)
            {
                uint nChars = 256;
                StringBuilder Buff = new StringBuilder((int)nChars);

                try
                {
                    uint success = QueryFullProcessImageName(proc.Handle, 0, Buff, out nChars);
                    if (0 != success)
                    {
                        pathToExe = Buff.ToString();
                    }
                    else
                    {
                        int error = Marshal.GetLastWin32Error();
                        pathToExe = ("Error = " + error + " when calling GetProcessImageFileName");
                    }
                }
                catch (Exception exeption)
                {
                    return exeption.Message;
                }

            }



            return pathToExe;
        }
    }
}
