using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using TelemetryApp.Commands;
using TelemetryApp.Common.Services;
using TelemetryApp.Models;
using TelemetryApp.Extensions;

namespace TelemetryApp.ViewModels
{
    public interface IFileDataVM : INotifyPropertyChanged
    {
        void UpdateFileDataVM(string filePath);
        FileDataModel FileDataModel { get; }
    }
    public class FileDataVM : Notifier, IFileDataVM
    {
        private readonly FileReaderSevice _fileReaderSevice;

        private int _currentIndex;
        public int CurrentIndex 
        {   get
            {
                return _currentIndex;
            }
            set 
            {
                _currentIndex = value;
                UpdateSelectedFrame();
                NotifyPropertyChanged(nameof(CurrentIndex));
            }
        }
        private readonly FileDataModel _fileDataModel;
        public FileDataModel FileDataModel
        {
            get { return _fileDataModel; }
        }
        public string FilePath
        {
            get
            {
                return FileDataModel.FilePath;
            }
            set
            {
                FileDataModel.FilePath = value;
                NotifyPropertyChanged(nameof(FilePath));
            }
        }

        private ICommand _updateCommand;
        public ICommand UpdateCommand => _updateCommand ??= new RelayCommand(
                                         obj => UpdateFileDataVM(FilePath));

        private ushort[] _serviceFramePart;
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

        private ushort[] _infoFramePart;
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

        public ushort[] _selectedTelemetryFrame;
        public ObservableCollection<ushort> SelectedTelemetryFrame
        {
            get
            {
                return _selectedTelemetryFrame.ToObservableCollection();
            }
            set
            {
                _selectedTelemetryFrame = value.ToArray();
                NotifyPropertyChanged(nameof(SelectedTelemetryFrame));
            }
        }

        public FileDataVM(FileReaderSevice fileReaderSevice)
        {
            _fileReaderSevice = fileReaderSevice;
            _fileDataModel = new FileDataModel(_fileReaderSevice);
            _selectedTelemetryFrame = Array.Empty<ushort>();
            _serviceFramePart = Array.Empty<ushort>();
            _infoFramePart= Array.Empty<ushort>();
            _updateCommand = new RelayCommand(obj => UpdateFileDataVM(FilePath));
            InitIndex(0);
        }
        
        public void UpdateFileDataVM(string? parameter=null)
        {
            FileDataModel.UpdateFileData(FilePath);
            UpdateSelectedFrame();
        }

        public void UpdateSelectedFrame()
        {
            SelectedTelemetryFrame = FileDataModel.TelemetryFrame
                                    .GetCurrentFrame(CurrentIndex).CurrentFrame.ToObservableCollection();
            ServiceFramePart = _selectedTelemetryFrame.Take(Consts.SERVICE_PART_FRAME_SIZE)
                               .ToArray().ToObservableCollection();
            InfoFramePart = _selectedTelemetryFrame.Skip(Consts.SERVICE_PART_FRAME_SIZE)
                            .ToArray().ToObservableCollection();
        }

        public void InitIndex(int index = 0)
        {
            CurrentIndex = index;
        }
    }

}
