using System.IO;
using System.Text;
using TelemetryApp.Utils;
using TelemetryApp.Models;

namespace TelemetryApp.Common.Services
{
    public interface IFileReaderSevice
    {
        TelemetryFrame[] GetFileData(string filePath);
    }
    public class FileReaderSevice : IFileReaderSevice
    {
        public FileReaderSevice() {}
        public TelemetryFrame[] GetFileData(string filePath)
        {
            if (!PathUtil.IsValidFileName(filePath))
            {
                return System.Array.Empty<TelemetryFrame>();
            }
            string fileData = ReadFromFileToText(filePath);
            TelemetryFrame[] telemetryFrames = PrepareDataFrame(fileData);
            return telemetryFrames;
        }

        private static string ReadFromFileToText(string filePath)
        {
            string startupPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, filePath);
            var fileInf = new FileInfo(startupPath);
            if (fileInf.Exists)
            {
                string fileText = File.ReadAllText(startupPath);
                return fileText;
            }
            return string.Empty;
        }
        private static TelemetryFrame[] PrepareDataFrame(string stringData)
        {
            if (stringData.Length != Consts.FRAME_COUNT * Consts.FRAME_SIZE)
            {
                return System.Array.Empty<TelemetryFrame>();
            }
            var res = new TelemetryFrame[Consts.FRAME_COUNT];
            for (var i = 0; i < stringData.Length; i += Consts.FRAME_SIZE)
            {
                int frameCount = i / Consts.FRAME_SIZE;

                var frame = stringData.Substring(i, Consts.FRAME_SIZE);
                var splittedFrame = GetSplittedFrame(frame);
                res[frameCount] = new TelemetryFrame(splittedFrame);
            }
            return res;
        }
        public static string[] GetSplittedFrame(string frame)
        {
            var sb = new StringBuilder(frame);
            sb.Remove(0, Consts.HEADER_SIZE);
            sb.Remove(sb.Length - 1, 1);
            var splittedFrame = sb.ToString().Split(' ');
            return splittedFrame;
        }
    }
}
