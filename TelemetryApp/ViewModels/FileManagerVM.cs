using System.Collections.ObjectModel;
using TelemetryApp.Models;

namespace TelemetryApp.ViewModels
{
    public class FileManagerVM : Notifier
    {
        public string CurrentDirectory { get; set; }
        public string PreviousDirectory { get; set; }
        public string ParentDirectory { get; set; }
        public string NextDirectory { get; set; }
        public ObservableCollection<FileDetailsModel> Files { get; set; }
        public ObservableCollection<FileDetailsModel> TheseComputerFolders { get; set; }
        public ObservableCollection<FileDetailsModel> EasyFolders { get; set; }

    }
}
