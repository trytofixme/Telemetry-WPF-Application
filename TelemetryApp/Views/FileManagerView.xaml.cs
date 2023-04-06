using System.Windows;
using System.Windows.Input;
using TelemetryApp.Models;
using TelemetryApp.ViewModels;

namespace TelemetryApp.Views
{
    /// <summary>
    /// Логика взаимодействия для FileManagerView.xaml
    /// </summary>
    public partial class FileManagerView : Window
    {
        public FileManagerView()
        {
            InitializeComponent();
        }
        private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }
        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListViewControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedFile = (FileDetailsModel)ListViewControl.SelectedItem;
            if (selectedFile != null && selectedFile.IsExists)
            {
                ((FileManagerVM)DataContext).UpdateFileData(selectedFile.Path);
            }
            Close();
        }
    }
}
