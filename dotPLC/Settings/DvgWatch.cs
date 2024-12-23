using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotPLC.Settings
{
    public class DvgWatch
    {
        public string Name { get; set; }
        public string  CurrentValue { get; set; }
        public string DisplayFormat { get; set; }
        public string DataType { get; set; }
    }
}
