using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using TelemetryApp.ViewModels;
using TelemetryApp.Utils;
using TelemetryApp.Views;

namespace TelemetryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _dragStarted = false;
        private Compositor Compositor { get; } = new Compositor();
        public MainWindow()
        {
            var container = Compositor.Container;
            InitializeComponent();
            DataContext = container.GetExportedValueOrDefault<IFileDataVM>();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_dragStarted && int.TryParse(e.NewValue.ToString(), out int intVal)) 
            {
                ((FileDataVM)DataContext).InitIndex(intVal);
            }
        }

        private void Slider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if ((PathUtil.IsValidFileName(((FileDataVM)DataContext).FilePath)) &&
                int.TryParse(((Slider)sender).Value.ToString(), out int intVal))
                ((FileDataVM)DataContext).InitIndex(intVal);
            _dragStarted = false;
        }

        private void Slider_DragStarted(object sender, DragStartedEventArgs e)
        {
            _dragStarted = true;
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            if (PathUtil.IsValidFileName(((FileDataVM)DataContext).FilePath))
            {
                serviceName.Content = Consts.SERVICE_PART_NAME_PROPERTY;
                infoName.Content = Consts.INFO_PART_NAME_PROPERTY;
            }
        }
        private void ExploreButton_Click(object sender, RoutedEventArgs e)
        {
            FileManagerView wind = new();
            wind.Show();
        }
    }
}
