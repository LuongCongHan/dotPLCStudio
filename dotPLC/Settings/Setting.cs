using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotPLC.Settings
{
    public class Setting
    {
        public string IPAddess { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 502;
        public bool AnyAddress { get; set; } = true;
        public string CpuName { get; set; } = "FX5U-32MT/ES";
        public bool[] StatusOptions { get; set; } = new bool[] { false, true, true, true, true };
    }
}
