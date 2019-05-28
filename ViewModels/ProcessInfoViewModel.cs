using DataObjects;
using Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace ViewModels
{
    public class ProcessInfoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ProcessInfo> Processes { get; } = new ObservableCollection<ProcessInfo>();

        private float _totalCount;
        public float TotalCount
        {
            get { return _totalCount; }
            set
            {
                _totalCount = value;
                NotifyPropertyChanged("TotalCount");
            }
        }

        private float _totalMemory;
        public float TotalMemory
        {
            get { return _totalMemory; }
            set
            {
                _totalMemory = value;
                NotifyPropertyChanged("TotalMemory");
            }
        }

        protected DispatcherTimer timer;

        public ProcessInfoViewModel()
        {
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
            timer.Tick += UpdateProcesses;
            timer.Start();
        }

        public bool KillProcess(ProcessInfo processInfo)
        {
            return ProcessWorker.KillProcess(processInfo);
        }

        private void UpdateProcesses(object sender, EventArgs e)
        {
            var dataCollector = new ProcessesDataCollector();
            dataCollector.CollectData(Processes);

            TotalCount = TotalMemory = 0;
            foreach (var process in Processes)
            {
                TotalMemory += process.MemoryStatistic;
            }
            TotalMemory=(float)Math.Round(TotalMemory, 3);
            TotalCount = Processes.Count;
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
