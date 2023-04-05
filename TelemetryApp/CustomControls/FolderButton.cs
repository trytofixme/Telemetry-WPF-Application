using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TelemetryApp.CustomControls
{
    public class FolderButton : RadioButton
    {
        public PathGeometry Icon
        {
            get { return (PathGeometry)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(PathGeometry), typeof(FolderButton));
    }
}
