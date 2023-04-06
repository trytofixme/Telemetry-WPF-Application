using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using TelemetryApp.Models;
using TelemetryApp.Services;
using TelemetryApp.Utils;

namespace TelemetryApp.ViewModels
{
    public class FileManagerVM : Notifier
    {
        private readonly ViewModelSynchronizationService _synchronizationService;

        public string CurrentDirectory { get; set; } = @"C:\";
        //public string PreviousDirectory { get; set; }
        //public string ParentDirectory { get; set; }
        //public string NextDirectory { get; set; }

        public ObservableCollection<FileDetailsModel> Files { get; set; } = new ObservableCollection<FileDetailsModel>();
        //public ObservableCollection<FileDetailsModel> ThisComputerFolders { get; set; } = new ObservableCollection<FileDetailsModel>();
        //public ObservableCollection<FileDetailsModel> QuickAccessFolders { get; set; } = new ObservableCollection<FileDetailsModel>();
        public ObservableCollection<FileDetailsModel> Drives { get; set; } = new ObservableCollection<FileDetailsModel>();

        private readonly BackgroundWorker bgGetFilesBackgroundWorker = new()
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };
        #region Functions
        public void LoadFrameFiles()
        {
            Files.Clear();
            Drives.Clear();
            if (bgGetFilesBackgroundWorker.IsBusy)
            {
                bgGetFilesBackgroundWorker.CancelAsync();
                return;
            }
            bgGetFilesBackgroundWorker.RunWorkerAsync();
        }

        public void UpdateFileData(string filePath)
        {
            _synchronizationService.UpdateFileDataVM(filePath);
        }        

        private void BgGetFilesBackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e){ }

        private void BgGetFilesBackgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            string fileName = e?.UserState?.ToString();
            if (fileName == null) return;
            var fileDetails = new FileDetailsModel
            {
                Name = Path.GetFileName(fileName),
                Path = fileName,
                FileExtension = PathUtil.GetFileExtension(fileName),
                IsExists = true,
                IsDirectory = false
            };
            Files.Add(fileDetails);
        }

        private void BgGetFilesBackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {            
            foreach (var drive in DriveInfo.GetDrives())
            {
                Drives.Add(new FileDetailsModel
                {
                    Name = drive.Name,
                    Path = drive.RootDirectory.FullName,
                    FileExtension = PathUtil.GetFileExtension(drive.RootDirectory.FullName),
                    IsExists = true,
                    IsDirectory = true
                });
                IEnumerable<string> frameTypeFiles = Directory.EnumerateFiles(drive.Name, Consts.FRAME_FILE_EXTENSION, new EnumerationOptions
                {
                    IgnoreInaccessible = true,
                    RecurseSubdirectories = true
                });
                foreach (var file in frameTypeFiles)
                    bgGetFilesBackgroundWorker.ReportProgress(1, file);
            }
        }
        #endregion

        public FileManagerVM(ViewModelSynchronizationService synchronizationService)
        {
            _synchronizationService = synchronizationService;            
            bgGetFilesBackgroundWorker.DoWork += BgGetFilesBackgroundWorker_DoWork;
            bgGetFilesBackgroundWorker.ProgressChanged += BgGetFilesBackgroundWorker_ProgressChanged;
            bgGetFilesBackgroundWorker.RunWorkerCompleted += BgGetFilesBackgroundWorker_RunWorkerCompleted;
            LoadFrameFiles();
        }           
    }
}
