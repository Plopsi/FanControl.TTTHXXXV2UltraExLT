using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FanControl.Plugins;
using HidLibrary;

namespace FanControl.TTTHXXXV2UltraExLT
{
    public class TTTHXXXV2UltraExLT : IPlugin
    {
        private const int VENDOR = 9802;
        private const int PRODUCT = 9020;
        private const int INTERFACE = 0;

        private HidDevice device;

        private bool deviceReady = false;        
        public string Name => "Thermaltake THXXX V2 Ultra EX ARGB";

        public void Close()
        {
            device.CloseDevice();
        }

        public void Initialize()
        {            
            //typeof(HidLibrary).Assembly.Location


            device = HidDeviceHelper.GetFirstHidDevice(VENDOR, PRODUCT, INTERFACE);

            if (device != null)
            {                
                device.OpenDevice();                                
                if (device.IsOpen)
                {                    
                    deviceReady = true;
                } else
                {                    
                    throw new Exception("Initialize: Couldn't open Device!");
                }
            } else
            {                
                throw new Exception("Initialize: Device not found!");
            }
        }

        public void Load(IPluginSensorsContainer _container)
        {
            if (deviceReady)
            {
                _container.TempSensors.Add(new TempSensor(device));
            }
            else
            {
                throw new Exception("Load: Device not ready");
            }
        }
    }
}
