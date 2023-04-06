using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using TelemetryApp.Commands;
using TelemetryApp.Models;
using TelemetryApp.Extensions;
using TelemetryApp.Services;

namespace TelemetryApp.ViewModels
{
    public interface IFileDataVM : INotifyPropertyChanged
    {
        void UpdateFileData(object? parameter = null);
    }
    public class FileDataVM : Notifier, IFileDataVM
    {        
        private readonly FileReaderSevice _fileReaderSevice;
        private TelemetryFileDataModel _fileDataModel;
        private TelemetryFrame _selectedTelemetryFrame;
        private ICommand _openCommand;
        private ushort[] _serviceFramePart;
        private ushort[] _infoFramePart;
        private int _currentIndex;        

        public ObservableCollection<ushort> ServiceFramePart
        {
            get
            {
                return _serviceFramePart.ToObservableCollection();
            }
            set
            {
                _serviceFramePart = value.ToArray();
                NotifyPropertyChanged(nameof(ServiceFramePart));
            }
        }

        public ObservableCollection<ushort> InfoFramePart
        {
            get
            {
                return _infoFramePart.ToObservableCollection();
            }
            set
            {
                _infoFramePart = value.ToArray();
                NotifyPropertyChanged(nameof(InfoFramePart));
            }
        }

        public TelemetryFrame SelectedTelemetryFrame
        {
            get
            {
                return _selectedTelemetryFrame;
            }
            set
            {
                if (_selectedTelemetryFrame == null || value == null)
                {
                    _selectedTelemetryFrame = new TelemetryFrame();
                }
                else
                {
                    _selectedTelemetryFrame = value;
                }
                ServiceFramePart = _selectedTelemetryFrame.Frame.Take(Consts.SERVICE_FRAME_PART_SIZE)
                                   .ToArray().ToObservableCollection();
                InfoFramePart = _selectedTelemetryFrame.Frame.Skip(Consts.SERVICE_FRAME_PART_SIZE)
                                .ToArray().ToObservableCollection();
                NotifyPropertyChanged(nameof(SelectedTelemetryFrame));
            }
        }

        public string FilePath
        {
            get
            {
                return _fileDataModel.FileDetails.Path;
            }
            set
            {
                _fileDataModel.FileDetails.Path = value;
                NotifyPropertyChanged(nameof(FilePath));
            }
        }
        public int CurrentIndex
        {
            get
            {
                return _currentIndex;
            }
            set
            {
                _currentIndex = value;
                NotifyPropertyChanged(nameof(CurrentIndex));
            }
        }

        public ICommand OpenCommand => _openCommand ??= new RelayCommand(
                                         obj => UpdateFileData(FilePath));

        public FileDataVM(FileReaderSevice fileReaderSevice)
        {
            _fileReaderSevice = fileReaderSevice;
            _fileDataModel = new TelemetryFileDataModel();
            SelectedTelemetryFrame = new TelemetryFrame();
            InfoFramePart = new ObservableCollection<ushort>();
            ServiceFramePart = new ObservableCollection<ushort>();
            CurrentIndex = 0;
        }
        
        public void UpdateFileData(object? parameter = null)
        {
            if (parameter == null) return;
            TelemetryFileDataModel newFileData = _fileReaderSevice.GetFileData(parameter.ToString());
            _fileDataModel.UpdateFileData(newFileData);
            FilePath = _fileDataModel.FileDetails.Path;
            CurrentIndex = 0;
            SelectedTelemetryFrame = _fileDataModel.TelemetryData.GetFrameAt(CurrentIndex);
        }

        public void UpdateSelectedFrame(int index = 0)
        {
            CurrentIndex = index;
            SelectedTelemetryFrame = _fileDataModel.TelemetryData.GetFrameAt(CurrentIndex);
        }
    }
}
