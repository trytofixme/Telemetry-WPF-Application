using System;
using System.IO;
using System.Text.RegularExpressions;

namespace TelemetryApp.Utils
{
    public static class PathUtil
    {
        private static readonly Regex FilePathRegEx = new(@"^(?:[a-zA-Z]\:|\\\\[\w\.]+\\[\w.$]+)\\(?:[\w]+\\)*\w([\w.])+$");

        public static bool IsValidFileName(string fileName)
        {
            return !string.IsNullOrEmpty(fileName) && fileName.EndsWith(".kdr");
        }
        public static string GetFileExtension(string filePath)
        {
            if (filePath == null) return string.Empty;
            string extension = Path.GetExtension(filePath);
            return extension;
        }
    }
}
