using System;

namespace TelemetryApp.Models
{
    public interface ITelemetryDataModel
    {
        TelemetryFrame[] FramesArray { get; set; }
        event EventHandler<TelemetryDataEventArgs> TelemetryFrameChanged;
        void UpdateTelemetryData(ITelemetryDataModel telemetryData);
    }

    public class TelemetryDataEventArgs : EventArgs
    {
        public TelemetryFrame[] FramesArray { get; set; }

        public TelemetryDataEventArgs(TelemetryFrame[] framesArray)
        {
            FramesArray = framesArray;
        }
    }

    public class TelemetryDataModel : ITelemetryDataModel
    {
        public TelemetryFrame[] FramesArray { get; set; } = Array.Empty<TelemetryFrame>();

        public TelemetryDataModel() { }

        public event EventHandler<TelemetryDataEventArgs> TelemetryFrameChanged = delegate { };

        #region Functions
        public void UpdateTelemetryData(ITelemetryDataModel telemetryData)
        {
            if (telemetryData == null) return;
            FramesArray = telemetryData.FramesArray;
        }
        #endregion
    }

    public class TelemetryFrame
    {
        public ushort[] Frame { get; set; } = Array.Empty<ushort>();

        public TelemetryFrame() { }
    }
}
