using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dotPLC.SocketServer
{
    public class Memory
    {
        public byte[] D { get; set; } = new byte[16000];//7999------- +2 là vì D7999 cần 2 byte cuối(xử lý tạm thời) [16000 +2];
        public byte[] SW { get; set; } = new byte[65536 ]; //1ff -sai [(7fff+1)*2]
        public byte[] W { get; set; } = new byte[65536 ]; //same
        public byte[] TN { get; set; } = new byte[2048 ]; //511 sai 1023

        public byte[] SD { get; set; } = new byte[24000 ]; //SD11999
        public byte[] R { get; set; } = new byte[65536 ]; //R32767
        public byte[] Z { get; set; } = new byte[40 ]; //19
        public byte[] LZ { get; set; } = new byte[4]; //1Dw
        public byte[] CN { get; set; } = new byte[2048 ];//1023 sai
        public byte[] LCN { get; set; } = new byte[2048 ]; //63 => 64*4= -sai 1023
        public byte[] SN { get; set; } = new byte[2048 ]; //15 => 16*2=32 -sai same
        public byte[] STN { get; set; } = new byte[2048 ]; //15 => 16*2=32-sai same

        public bool[] X { get; set; } = new bool[1024]; // 1777(octal) =1023(decimal)
        public bool[] Y { get; set; } = new bool[1024]; //same

        public bool[] M { get; set; } = new bool[32768]; //M7679_sai 32767

        public bool[] L { get; set; } = new bool[32768]; //same sai same
        public bool[] F { get; set; } = new bool[32768]; //f127 sai same
        public bool[] B { get; set; } = new bool[0x8000]; //ff=255 sai 7fff
        public bool[] S { get; set; } = new bool[4096]; //S4095
        public bool[] SS { get; set; } = new bool[1024]; //ss15 sai 1023

        public bool[] SC { get; set; } = new bool[1024]; //same sai same
        public bool[] TC { get; set; } = new bool[1024]; //TC511 sai same
        public bool[] TS { get; set; } = new bool[1024]; //same sai same
        public bool[] CS { get; set; } = new bool[1024]; //255 sai same
        public bool[] CC { get; set; } = new bool[1024]; // same
        public bool[] SB { get; set; } = new bool[0x8000]; //1ff =511
        public bool[] SM { get; set; } = new bool[10000]; //9999
        public bool[] BL { get; set; } = new bool[32]; //31
        public static byte ConvertBoolArrayToByte(bool[] source)
        {
            byte result = 0;
            // This assumes the array never contains more than 8 elements!
            int index = 8 - source.Length;

            // Loop through the array
            foreach (bool b in source)
            {
                // if the element is 'true' set the bit at that position
                if (b)
                    result |= (byte)(1 << (7 - index));

                index++;
            }

            return result;
        }
        public static bool[] ConvertByteToBoolArray(byte b)
        {
            // prepare the return result
            bool[] result = new bool[8];

            // check each bit in the byte. if 1 set to true, if 0 set to false
            for (int i = 0; i < 8; i++)
                result[i] = (b & (1 << i)) != 0;

            // reverse the array
            Array.Reverse(result);

            return result;
        }
        public void Clear()
        {
            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                var coils = properties[i].GetValue(this) as bool[];
                if (coils != null)
                    Array.Clear(coils, 0, coils.Length);
                else
                {
                    var registers = properties[i].GetValue(this) as byte[];
                    if (registers != null)
                        Array.Clear(registers, 0, registers.Length);
                }
            }
        }
    }
}
