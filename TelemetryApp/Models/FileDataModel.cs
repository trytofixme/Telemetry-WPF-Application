using System;
using System.ComponentModel;
using TelemetryApp.Common.Services;

namespace TelemetryApp.Models
{
    public class Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public interface IFileDataModel
    {
        event EventHandler<FileDataEventArgs> FileDataChanged;
        void UpdateFileData(string filePath);
    }
    public class FileDataEventArgs
    {
        public string FilePath { get; set; }
        public TelemetryData TelemetryWords { get; set; }

        public FileDataEventArgs (string filePath, TelemetryData telemetryWords)
        {
            FilePath = filePath;
            TelemetryWords = telemetryWords;
        }
    }
    public class FileDataModel : IFileDataModel
    {
        public event EventHandler<FileDataEventArgs> FileDataChanged = delegate { };
        public string FilePath { get; set; } = string.Empty;

        private readonly FileReaderSevice _fileReaderSevice;
        public FileReaderSevice FileReaderSevice
        {
            get { return _fileReaderSevice;}
        }
        public TelemetryData TelemetryFrame { get; set; } = new();
        public Guid FileId { get; set; }

        public FileDataModel(FileReaderSevice fileReaderSevice)
        {
            _fileReaderSevice = fileReaderSevice;
        }
        public void UpdateFileData(string filePath)
        {
            FilePath = filePath;
            var newTelemetryData = _fileReaderSevice.GetFileData(FilePath);
            TelemetryFrame.UpdateTelemetryData(newTelemetryData);
            FileDataChanged(this, new FileDataEventArgs(FilePath, TelemetryFrame));
        }
    }
}
