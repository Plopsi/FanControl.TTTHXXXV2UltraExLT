using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using FanControl.Plugins;
using HidLibrary;

namespace FanControl.TTTHXXXV2UltraExLT
{
    public class TempSensor : IPluginSensor
    {
        private byte[] request = new byte[5] { 0x00, 0x82, 0x01, 0x00, 0x80 };
        private HidDevice _device;
        public TempSensor(HidDevice device)
        {
            _device = device;
            Value = float.NaN;
        }
        public string Id => "TTTHXXXV2ULTRAEXARGB_LiquidTempSensor";

        public string Name => "Liquid Temperature";

        public float? Value { get; private set; }

        public void Update()
        {
            bool success = _device.Write(request);

            if (success)
            {                
                HidReport report = _device.ReadReport();
                if (report != null && report.Exists)
                {
                    int temp = (report.Data[4] | report.Data[5] << 8);
                    Value = ((float)temp) / 100.0f;                    
                }
            }
        }
    }
}
