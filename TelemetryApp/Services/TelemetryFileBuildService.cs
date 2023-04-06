using System.Globalization;
using System.Text;
using TelemetryApp.Models;

namespace TelemetryApp.Services
{
    public interface ITelemetryFileBuildService
    {
        TelemetryFileDataModel BuildTelemetryFile(string stringData);
    }
    public class TelemetryFileBuildService : ITelemetryFileBuildService
    {     
        public TelemetryFileBuildService() { }

        #region Functions
        public TelemetryFileDataModel BuildTelemetryFile(string stringData)
        {
            return GetTelemetryFileData(stringData);
        }

        static TelemetryFileDataModel GetTelemetryFileData(string stringData)
        {
            return new TelemetryFileDataModel()
            {
                TelemetryData = GetTelemetryData(stringData)
            };
        }

        static TelemetryDataModel GetTelemetryData(string stringData)
        {
            if (stringData.Length != Consts.FRAME_COUNT * Consts.FRAME_SIZE)
            {
                return new TelemetryDataModel();
            }
            var telemetryData = new TelemetryFrame[Consts.FRAME_COUNT];

            for (var frameCount = 0; frameCount < Consts.FRAME_COUNT; frameCount += 1)
            {
                string stringFrame = stringData.Substring(frameCount * Consts.FRAME_SIZE, Consts.FRAME_SIZE);
                telemetryData[frameCount] = GetTelemetryFrame(stringFrame);
            }
            return new TelemetryDataModel()
            {
                FramesArray = telemetryData
            };
        }

        static TelemetryFrame GetTelemetryFrame(string stringFrame)
        {
            var newTelemetryFrame = new TelemetryFrame()
            {
                Frame = new ushort[Consts.FRAME_BODY_SIZE]
            };
            string[] splittedStringFrame = GetSplittedFrame(stringFrame);

            ushort mask = 0x03FF;
            for (var i = 0; i < Consts.SERVICE_FRAME_PART_SIZE; i++)
            {
                if (ushort.TryParse(splittedStringFrame[i], NumberStyles.HexNumber, CultureInfo.CurrentCulture, out ushort uInt16))
                    newTelemetryFrame.Frame[i] = (ushort)(mask & uInt16);
            }

            mask = 0x01FE;
            for (var i = Consts.SERVICE_FRAME_PART_SIZE; i < Consts.FRAME_BODY_SIZE; i++)
            {
                if (ushort.TryParse(splittedStringFrame[i], NumberStyles.HexNumber, CultureInfo.CurrentCulture, out ushort uInt16))
                    newTelemetryFrame.Frame[i] = (ushort)((mask & uInt16) >> 1);
            }

            return newTelemetryFrame;
        }

        static string[] GetSplittedFrame(string frame)
        {
            var sb = new StringBuilder(frame);
            sb.Remove(0, Consts.HEADER_SIZE);
            sb.Remove(sb.Length - 1, 1);
            string[] splittedFrame = sb.ToString().Split(' ');
            return splittedFrame;
        }
        #endregion
    }
}
