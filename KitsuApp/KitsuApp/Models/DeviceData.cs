using KitsuApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Essentials;

namespace KitsuApp.Models
{
    public class DeviceData : IDeviceData
    {
        public string Name { get { return DeviceInfo.Name; } }
        public string Model { get { return DeviceInfo.Model; } }
        public string Manufacturer { get { return DeviceInfo.Manufacturer; } }
        public string Version { get { return DeviceInfo.VersionString; } }
        public string Platform { get { return DeviceInfo.Platform.ToString(); } }
        public string Idiom { get { return DeviceInfo.Idiom.ToString(); } }
        public string DeviceType { get { return DeviceInfo.DeviceType.ToString(); } }
        
        public DeviceData()
        {
            GetDeviceData();
        }
        public void GetDeviceData()
        {
            PrintDeviceData();
        }

        public void PrintDeviceData()
        {
            Debug.WriteLine($"Name: {Name}");
            Debug.WriteLine($"Model: {Model}");
            Debug.WriteLine($"Manufacturer: {Manufacturer}");
            Debug.WriteLine($"Version: {Version}");
            Debug.WriteLine($"Platform: {Platform}");
            Debug.WriteLine($"Idiom: {Idiom}");
            Debug.WriteLine($"DeviceType: {DeviceType}");
        }
    }

}
