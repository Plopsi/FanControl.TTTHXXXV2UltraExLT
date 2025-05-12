using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HidLibrary;

namespace FanControl.TTTHXXXV2UltraExLT
{
    internal static class HidDeviceHelper
    {     
        public static IEnumerable<HidDevice> GetHidDevices(int vendorId, int productId, int? interfaceIndex = null)
        {
            var allDevices = HidDevices.Enumerate(vendorId, productId);

            if (interfaceIndex == null)
                return allDevices;

            string interfaceTag = $"mi_{interfaceIndex.Value:00}";

            return allDevices.Where(d =>
                d.DevicePath.ToLower().Contains(interfaceTag));
        }
        
        public static HidDevice GetFirstHidDevice(int vendorId, int productId, int interfaceIndex)
        {
            return GetHidDevices(vendorId, productId, interfaceIndex).FirstOrDefault();
        }
    }
}
