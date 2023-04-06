using TelemetryApp.ViewModels;

namespace TelemetryApp.Services
{
    public class ViewModelSynchronizationService
    {
        private FileDataVM _fileDataVM;
        public ViewModelSynchronizationService(FileDataVM fileDataVM) 
        {
            _fileDataVM = fileDataVM;
        }

        public void UpdateFileDataVM(string filePath)
        {
            _fileDataVM.UpdateFileData(filePath);
        }
    }
}
