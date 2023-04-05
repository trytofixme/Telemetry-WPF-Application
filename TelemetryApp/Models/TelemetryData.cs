using System;

namespace TelemetryApp.Models
{


    public interface ITelemetryData
    {
        ushort[] TelemetryWords { get; set; }
        event EventHandler<TelemetryDataEventArgs> TelemetryFrameChanged;
        void UpdateTelemetryFrame(TelemetryData telemetryFrame);
    }

    public class TelemetryDataEventArgs : EventArgs
    {
        public ushort[] TelemetryData { get; set; }

        public TelemetryDataEventArgs(ushort[] telemetryFrameData)
        {
            TelemetryData = telemetryFrameData;
        }
    }
    public class TelemetryData
    {
        private TelemetryFrame[] _framesArray;
        private int _currentFrameIndex;

        public TelemetryFrame[] FramesArray
        {
            get { return _framesArray; }
        }
        public int CurrentFrameIndex
        {
            get { return _currentFrameIndex; }
        }

        public TelemetryData()
        {
            _framesArray = Array.Empty<TelemetryFrame>();
        }
        public void UpdateTelemetryData(TelemetryFrame[] telemetryData)
        {
            if (telemetryData == null)                
                return;
            _framesArray = telemetryData;
        }

        public TelemetryFrame GetCurrentFrame(int index)
        {
            try
            {
                _currentFrameIndex = index;
                return FramesArray[index];
            }
            
            catch
            {
                return new TelemetryFrame();
            }
        }
    }

    public class TelemetryFrame
    {
        private readonly ushort[] _currentFrame;
        public TelemetryFrame(string[] frameData)
        {
            _currentFrame = new ushort[Consts.FRAME_BODY_SIZE];
            InitCurrentFrame(frameData);
        }
        public TelemetryFrame()
        {
            _currentFrame = new ushort[0];
        }
        public ushort[] CurrentFrame
        {
            get { return _currentFrame; }
        }

        private void InitCurrentFrame(string[] frameData)
        {
            ushort mask = 0x03FF;
            var i = 0;
            while (i < Consts.SERVICE_PART_FRAME_SIZE)
            {
                var ushortVal = ushort.Parse(frameData[i], System.Globalization.NumberStyles.HexNumber);
                _currentFrame[i] = (ushort)(mask & ushortVal);
                i++;
            }
            mask = 0x01FE;
            while (i < Consts.FRAME_BODY_SIZE)
            {
                var ushortVal = ushort.Parse(frameData[i], System.Globalization.NumberStyles.HexNumber);
                _currentFrame[i] = (ushort)((mask & ushortVal) >> 1);
                i++;
            }
        }
    }
}
