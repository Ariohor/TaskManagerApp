using DataObjects;
using System.Collections.Generic;
using System.Diagnostics;

namespace Services
{
    public static class SystemResourceCounter
    {
        public static void Think(IList<ProcessInfo> listProcessInfo)
        {
            CPUResourceCount(listProcessInfo);

            RAMResourceCount(listProcessInfo);
        }

        private static void RAMResourceCount(IList<ProcessInfo> listProcessInfo)
        {
            for (int i = 0; i < listProcessInfo.Count; i++)
            {
                var counter = new PerformanceCounter("Process", "Working Set - Private", listProcessInfo[i].ProcessName);
                // to Mb
                listProcessInfo[i].MemoryStatistic = (counter.NextValue() / 1024) / 1024;
            }
        }

        private static void CPUResourceCount(IList<ProcessInfo> listProcessInfo)
        {
            for (int i = 0; i < listProcessInfo.Count; i++)
            {
                var counter = new PerformanceCounter("Process", "% Processor Time", listProcessInfo[i].ProcessName);
                listProcessInfo[i].CPUStatistic = counter.NextValue();
            }
        }
    }
}
