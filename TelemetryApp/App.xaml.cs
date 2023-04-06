using System;
using System.Windows;

namespace TelemetryApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                if (e.Args.Length < 2 && e.Args.Length != 0)
                {
                    Shutdown(1);
                    return;
                }
                base.OnStartup(e);
            }
            catch (Exception)
            {
                Shutdown(1);
                return;
            }
        }
    }
}
