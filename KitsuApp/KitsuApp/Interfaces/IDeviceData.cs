using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuApp.Interfaces
{
    public interface IDeviceData
    {
        string Name { get; }
        string Model { get; }
        string Manufacturer { get; }
        string Version { get; }
        string Platform { get; }
        string Idiom { get; }
        string DeviceType { get; }

        void GetDeviceData();

        void PrintDeviceData();

    }
}
