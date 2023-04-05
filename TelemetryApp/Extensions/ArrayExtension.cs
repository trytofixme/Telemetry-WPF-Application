using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TelemetryApp.Extensions
{
    static class ListExtension
    {
        public static ObservableCollection<ushort> ToObservableCollection(this ushort[] array)
        {
            var result = new ObservableCollection<ushort>();
            if (array == null)
            {
                return result;
            }

            for (var i = 0; i < array.Length; i++)
            {
                result.Add(array[i]);
            }
            return result;
        }
    }
}
