using System;
using TelemetryApp.Models;

namespace TelemetryApp.Extensions
{
    public static class TelemetryFileDataModelEx
    {
        public static TelemetryFrame GetFrameAt(this TelemetryDataModel telemetryFrameDataModel, int index)
        {
            try
            {
                return telemetryFrameDataModel.FramesArray[index];
            }
            catch (Exception)
            {
                return new TelemetryFrame();
            }
        }
    }
}
