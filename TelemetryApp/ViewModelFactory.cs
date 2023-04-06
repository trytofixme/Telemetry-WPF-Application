using TelemetryApp.Services;
using TelemetryApp.ViewModels;

namespace TelemetryApp
{
    public interface IViewModelFactory
    {
        FileDataVM CreateFileDataVM(FileReaderSevice fileReaderSevice);
        FileManagerVM CreateFileManagerVM(ViewModelSynchronizationService syncronizeService);
    }
    public class ViewModelFactory : IViewModelFactory
    {
        public ViewModelFactory() { }

        public FileDataVM CreateFileDataVM(FileReaderSevice fileReaderSevice)
        {
            return new FileDataVM(fileReaderSevice);
        }
        public FileManagerVM CreateFileManagerVM(ViewModelSynchronizationService syncronizeService)
        {
            return new FileManagerVM(syncronizeService);
        }
    }
}
