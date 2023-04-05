using TelemetryApp.Common.Services;
using TelemetryApp.ViewModels;

namespace TelemetryApp
{
    public interface IViewModelFactory
    {
        FileDataVM CreateFileDataVM(FileReaderSevice fileReaderSevice);
    }
    public class ViewModelFactory : IViewModelFactory
    {
        public ViewModelFactory() { }

        public FileDataVM CreateFileDataVM(FileReaderSevice fileReaderSevice)
        {
            return new FileDataVM(fileReaderSevice);
        }
    }
}
