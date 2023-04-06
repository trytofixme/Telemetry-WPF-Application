using System;
using System.ComponentModel;

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
    public interface ITelemetryFileDataModel
    {
        event EventHandler<FileDataEventArgs> FileDataChanged;
        void UpdateFileData(TelemetryFileDataModel telemetryFileDataModel);
    }
    public class FileDataEventArgs
    {
        public FileDetailsModel FileDetails { get; set; }
        public TelemetryDataModel TelemetryData { get; set; }

        public FileDataEventArgs (FileDetailsModel fileDetails, TelemetryDataModel telemetryData)
        {
            FileDetails = fileDetails;
            TelemetryData = telemetryData;
        }
    }
    public class TelemetryFileDataModel : ITelemetryFileDataModel
    {
        public FileDetailsModel FileDetails { get; set; } = new();
        public TelemetryDataModel TelemetryData { get; set; } = new();

        public event EventHandler<FileDataEventArgs> FileDataChanged = delegate { };
        
        public void UpdateFileData(TelemetryFileDataModel telemetryFileDataModel)
        {
            FileDetails = telemetryFileDataModel.FileDetails;
            TelemetryData = telemetryFileDataModel.TelemetryData;
            FileDataChanged(this, new FileDataEventArgs(FileDetails, TelemetryData));
        }
    }
}
