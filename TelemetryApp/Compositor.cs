using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using TelemetryApp.Services;
using TelemetryApp.ViewModels;
using TelemetryApp.Views;

namespace TelemetryApp
{
    public class Compositor
    {
        public CompositionContainer Container { get; set; }
        public Compositor() 
        {
            var container = new CompositionContainer();
            var batch = new CompositionBatch();

            var fileReaderService = new FileReaderSevice();
            var viewModelFactory = new ViewModelFactory();
            var fileDataVM = viewModelFactory.CreateFileDataVM(fileReaderService);

            var viewModelSyncronizeService = new ViewModelSynchronizationService(fileDataVM);            
            var fileManagerVM = viewModelFactory.CreateFileManagerVM(viewModelSyncronizeService);
            
            batch.AddExportedValue<IFileReaderSevice>(fileReaderService);
            batch.AddExportedValue<IViewModelFactory>(viewModelFactory);
            batch.AddExportedValue(fileManagerVM);
            batch.AddExportedValue<IFileDataVM>(fileDataVM);

            container.Compose(batch);
            Container = container;
        }
    }
}
