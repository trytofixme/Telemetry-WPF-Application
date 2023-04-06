using System.IO;
using TelemetryApp.Utils;
using TelemetryApp.Models;

namespace TelemetryApp.Services
{
    public interface IFileReaderSevice
    {
        TelemetryFileDataModel GetFileData(FileDetailsModel fileDetailsModel);
    }
    public class FileReaderSevice : IFileReaderSevice
    {
        private readonly ITelemetryFileBuildService _telemetryFileBuildService;

        public FileReaderSevice() 
        {
            _telemetryFileBuildService = new TelemetryFileBuildService();
        }

        public TelemetryFileDataModel GetFileData(string filePath)
        {

            if (!PathUtil.IsValidFileName(filePath))
            {
                return new TelemetryFileDataModel();
            }
            string stringData = GetStringDataFromFile(filePath);
            TelemetryFileDataModel telemetryFile = _telemetryFileBuildService.BuildTelemetryFile(stringData);
            var fileDetails = new FileDetailsModel() 
            {
                Path = filePath,
                IsDirectory = false,
                IsExists = true,
                FileExtension = PathUtil.GetFileExtension(filePath),
            };
            telemetryFile.FileDetails = fileDetails;
            return telemetryFile;
        }

        public TelemetryFileDataModel GetFileData(FileDetailsModel fileDetailsModel)
        {
            if (fileDetailsModel.IsExists && !fileDetailsModel.IsDirectory 
                && PathUtil.IsValidFileName(fileDetailsModel.Path))
            {
                return new TelemetryFileDataModel();
            }
            string stringData = GetStringDataFromFile(fileDetailsModel.Path);
            var telemetryFile = _telemetryFileBuildService.BuildTelemetryFile(stringData);
            telemetryFile.FileDetails = fileDetailsModel;
            return telemetryFile;
        }

        private static string GetStringDataFromFile(string stringData)
        {
            var fileInf = new FileInfo(stringData);
            if (fileInf.Exists)
            {
                string fileText = File.ReadAllText(stringData);
                return fileText;
            }
            else
            {
                try
                {
                    var projectDirectoryPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, stringData);
                    string fileText = File.ReadAllText(projectDirectoryPath);
                    return fileText;
                }
                catch 
                {
                    return string.Empty;
                }
            }
        }
    }
}
