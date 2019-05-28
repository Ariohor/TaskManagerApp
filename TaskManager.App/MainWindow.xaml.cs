using DataObjects;
using System.Windows;
using ViewModels;

namespace TaskManager.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ProcessInfoViewModel();
        }

        private void KillProcessClick(object sender, RoutedEventArgs e)
        {
            var item = new ProcessInfo();
            if ((item = (ProcessInfo)processesListView.SelectedItem) != null)
            {
                var result = (DataContext as ProcessInfoViewModel).KillProcess(item);
                if (result) MessageBox.Show("Процесс остановлен");
                else MessageBox.Show("Не удалось остановить процесс");
            }
            else MessageBox.Show("Выберите процесс!");
        }
    }
}
