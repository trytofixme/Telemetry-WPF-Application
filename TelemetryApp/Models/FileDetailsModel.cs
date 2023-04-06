namespace TelemetryApp.Models
{
    public class FileDetailsModel
    {
        public string Name { get; set; } = "";
        public string Path { get; set; } = "";
        public string FileExtension { get; set; } = "";
        public bool IsDirectory { get; set; } = false;
        public bool IsSelected { get; set; } = false;
        public bool IsExists { get; set; } = false;
        public string Type => IsDirectory ? "Folder" : "File";
    }
}
