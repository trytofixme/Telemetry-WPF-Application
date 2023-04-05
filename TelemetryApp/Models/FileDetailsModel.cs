using System.Windows.Media;

namespace TelemetryApp.Models
{
    public class FileDetailsModel
    {
        public string Name { get; set; }
        public string Path { get; set; } 
        public PathGeometry FileIcon { get; set; }
        public string FileExtension { get; set; }
        public string FileSize { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedOn { get; set; }
        public bool IsDirectory { get; set; }
        public bool IsSelected { get; set; }
        internal string _Type { get; set; }
        public string Type => _Type = IsDirectory ? "Folder" : "File";

    }
}
