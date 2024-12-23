using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dotPLC.SocketServer
{
    public static class DataConvertExtendsion
    {
        public const string U_WORD = "Word [Unsigned]/Bit String [16-bit]";
        public const string U_DOUBLEWORD = "Double Word [Unsigned]/Bit String [32-bit]";
        public const string WORD = "Word [Signed]";
        public const string DOUBLEWORD = "Double Word [Signed]";
        public const string FLOAT = "FLOAT [Single Precision]";
        public const string STRING = "String";
        public const string TIME = "Time";
        public const string BIN = "BIN";
        public const string Decimal = "Decimal";
        public const string Hexadecimal = "Hexadecimal";
        public const int date = 3600 * 24 * 1000;
        public const int hours = 3600 * 1000;
        public const int min = 60 * 1000;
        public const int second = 1000;
        public static readonly Regex sWhitespace = new Regex(@"\s+");
        public static short GetWord(this byte[] buffer, int num)
        {
            num = num * 2;
            return (short)((buffer[num + 1] << 8) | buffer[num]);
        }
        public static void SetWord(this byte[] buffer, short register, int num)
        {
            Array.Copy(BitConverter.GetBytes(register), 0, buffer, num * 2, 2);
        }
        public static bool TryParse<T>(this string s, out T value)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                value = (T)converter.ConvertFromString(s);
                return true;
            }
            catch
            {
                value = default(T);
                return false;
            }
        }
        public static T SetWord1<T>(this string value, int num)
        {
            T result;
            if (value.TryParse(out result))
            {
                return (dynamic)result;
            }
            return result;
        }
        public static void SetString(this byte[] buffer, string value, int num)
        {
            if (value.Length > 2) return;
            byte[] results = Encoding.ASCII.GetBytes(value);
            Array.Copy(results, 0, buffer, num * 2, value.Length);
        }
        public static void SetWord<T>(this byte[] buffer, string value, int num)
        {
            T result;
            if (value.TryParse(out result))
            {
                byte[] temp = BitConverter.GetBytes((dynamic)result);
                //Array.Reverse(temp);
                Array.Copy(temp, 0, buffer, num * 2, 2);
            }
        }
        public static void SetWord<T>(this byte[] buffer, T value, int num)
        {
            byte[] temp = BitConverter.GetBytes((dynamic)value);
            //Array.Reverse(temp);
            Array.Copy(temp, 0, buffer, num * 2, 2);
        }
        public static void SetDoubleWord<T>(this byte[] buffer, string value, int num)
        {
            T result;
            if (value.TryParse(out result))
            {
                byte[] temp = BitConverter.GetBytes((dynamic)result);
                //Array.Reverse(temp);
                Array.Copy(temp, 0, buffer, num * 2, 4);
            }
        }
        public static byte[] SetDoubleWord1<T>(string value, int num)
        {
            byte[] buffer = new byte[4];
            T result;
            if (value.TryParse(out result))
            {
                buffer = BitConverter.GetBytes((dynamic)result);
            }
            return buffer;
        }
        public static void SetFloat(this byte[] buffer, float register, int num)
        {
            Array.Copy(BitConverter.GetBytes(register), 0, buffer, num * 2, 4);
        }
        public static void SetWord(this byte[] buffer, ushort register, int num)
        {
            Array.Copy(BitConverter.GetBytes(register), 0, buffer, num * 2, 2);
        }
        public static bool GetCoil(this bool[] buffer, int num)
        {
            return buffer[num];
        }
        public static void SetBinWord(this byte[] buffer, string value, int num)
        {
            value = sWhitespace.Replace(value, "").PadLeft(16, '0');
            //neu lon hon 16 bit =>out
            if (value.Length > 16) return;
            bool ismatch = Regex.IsMatch(value, "^[01]+$");//chua 1 va khong
            if (ismatch)
            {
                byte[] datas = new byte[2];
                for (int i = 0; i < 2; ++i)
                {
                    datas[i] = Convert.ToByte(value.Substring(8 * i, 8), 2);
                }
                Array.Reverse(datas);
                Array.Copy(datas, 0, buffer, num * 2, 2);
            }
        }
        public static void SetBinDWord(this byte[] buffer, string value, int num)
        {
            value = sWhitespace.Replace(value, "").PadLeft(32, '0');
            //neu lon hon 16 bit =>out
            if (value.Length > 16) return;
            bool ismatch = Regex.IsMatch(value, "^[01]+$");//chua 1 va khong
            if (ismatch)
            {
                byte[] datas = new byte[2];
                for (int i = 0; i < 4; ++i)
                {
                    datas[i] = Convert.ToByte(value.Substring(8 * i, 8), 2);
                }
                Array.Reverse(datas);
                Array.Copy(datas, 0, buffer, num * 2, 4);
            }
        }
        public static void SetHexWord(this byte[] buffer, string value, int num)
        {
            value = sWhitespace.Replace(value, "").ToUpper();
            ushort data;
            if (value[0] == 'H' && value.Length <= 5)
            {
                value = value.TrimStart('H');
                bool isMatch = ushort.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out data);
                if (isMatch)
                {
                    Array.Copy(BitConverter.GetBytes(data), 0, buffer, num * 2, 2);
                }
            }
            else if (value.Length < 5)
            {
                value = value.TrimStart();
                bool isMatch = ushort.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out data);
                if (isMatch)
                {
                    Array.Copy(BitConverter.GetBytes(data), 0, buffer, num * 2, 2);
                }
            }
        }
        public static void SetHexDWord(this byte[] buffer, string value, int num)
        {
            value = sWhitespace.Replace(value, "").ToUpper();
            uint data;
            if (value[0] == 'H' && value.Length <= 9)
            {
                value = value.TrimStart('H');
                bool isMatch = uint.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out data);
                if (isMatch)
                {
                    Array.Copy(BitConverter.GetBytes(data), 0, buffer, num * 2, 4);
                }
            }
            else if (value.Length < 9)
            {
                value = value.TrimStart();
                bool isMatch = uint.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out data);
                if (isMatch)
                {
                    Array.Copy(BitConverter.GetBytes(data), 0, buffer, num * 2, 4);
                }
            }
        }
        public static string SettingTimeFormat(int indextime)
        {
            switch (indextime)
            {
                case 0:
                    return "d";
                case 1:
                    return "h";
                case 2:
                    return "m";
                case 3:
                    return "s";
                case 4:
                    return "ms";
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        public static string GetTime(this byte[] buffer, int num)
        {
            //Time is DoubleWord
            int value_ms = BitConverter.ToInt32(buffer, num * 2);
            StringBuilder time = new StringBuilder();
            time.Append("T#");

            TimeSpan t = TimeSpan.FromMilliseconds(value_ms);
            int[] Times = new int[] { Math.Abs(t.Days),  Math.Abs(t.Hours),
                 Math.Abs(t.Minutes),  Math.Abs(t.Seconds),  Math.Abs(t.Milliseconds) };
            int startindex = 0;
            int endindex = 0;

            // Nếu time=0;
            if (value_ms == 0)
            {
                return time.Append("0ms").ToString();
            }
            else if (value_ms < 0)
            {
                value_ms = Math.Abs(value_ms);
                time.Append('-');
            }
            // Tìm điểm bắt đầu
            for (int i = 0; i < Times.Length; i++)
            {
                if (Times[i] > 0)
                {
                    startindex = i;
                    break;
                }
            }
            // Tìm điểm cuối

            for (int i = Times.Length - 1; i > 0; --i)
            {
                if (Times[i] > 0)
                {
                    endindex = i;
                    break;
                }
            }
            for (int i = startindex; i < endindex + 1; i++)
            {
                time.Append(Times[i]);
                time.Append(SettingTimeFormat(i));
            }

            return time.ToString();
        }
        public static void SetDatetime(this byte[] buffer, string datetime, int num)
        {
            // string txt = "T#-24d0h59m2h3ms";
            //string txt = "T#-1d2h3m4s5ms";
            if (datetime[0] != 'T' && datetime[1] != '#')
            {
                return;

            }
            int days_idex = datetime.IndexOf('d');
            int hou_idex = datetime.IndexOf('h');
            int min_idex = -1;
            int sec_idex = -1;
            int mil_idex = datetime.IndexOf("ms");
            int negative_idex = datetime.IndexOf('-');
            bool isnegative = false;

            //Kiem tra co ms khong
            if (mil_idex > 0)
            {
                min_idex = datetime.IndexOf('m', 0, datetime.Length - 2);
                sec_idex = datetime.IndexOf('s', 0, datetime.Length - 2);
            }
            else
            {//khong co [ms]
                min_idex = datetime.IndexOf('m');
                sec_idex = datetime.IndexOf('s');
            }

            int[] idx1 = new int[] {1,days_idex,  hou_idex,
                min_idex,sec_idex, mil_idex};
            if (negative_idex == 2)
            {
                isnegative = true;
                idx1[0] = negative_idex;
            }
            else if (negative_idex != -1 || negative_idex > 2)
            {
                return;

            }
            int lastIndex = 0;
            for (int i = 0; i < idx1.Length; i++)
            {
                if (idx1[i] > 0)
                {
                    lastIndex = idx1[i];
                }
            }
            if (mil_idex > 0)
            {
                if (datetime.IndexOf("ms") + 2 < datetime.Length) //neu "T#-24d20h31m23s648ms[122]";
                {
                    return;

                }
            }
            else
            {
                if (lastIndex + 1 < datetime.Length)
                {
                    return;
                    ///neu "T#-24d20h31m23s648";
                }
            }
            if (!IsSorted(idx1))
            {
                return;
                // sai thu tu 
                // VD gio truoc nhay T#-24h20d31m23s648"
            }
            int result = 0;
            int temp = 0;
            for (int i = 0; i < idx1.Length; i++)
            {
                if (idx1[i] > 0)
                {
                    for (int j = i + 1; j < idx1.Length; j++)
                    {
                        if (idx1[j] > 0)
                        {
                            temp = 0;
                            if (int.TryParse(datetime.Substring(idx1[i] + 1, idx1[j] - idx1[i] - 1), out temp))
                            {
                                try
                                {
                                    checked
                                    {
                                        if (isnegative)
                                        {
                                            if (TimeLimit(temp, j))
                                                result -= temp * TimeIndex(j);
                                            else
                                                return;
                                        }
                                        else
                                        {
                                            if (TimeLimit(temp, j))
                                                result += temp * TimeIndex(j);
                                            else
                                                return;
                                        }
                                    }
                                }
                                catch
                                {
                                    return;
                                }
                                break;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
            }
            Array.Copy(BitConverter.GetBytes(result), 0, buffer, num * 2, 4);
        }
        static int TimeIndex(int i)
        {
            switch (i)
            {
                case 1://days
                    return 24 * 3600 * 1000;
                case 2://Hours
                    return 3600 * 1000;
                case 3://Min
                    return 60 * 1000;
                case 4://Second
                    return 1000;
                case 5://milisecond
                    return 1;
                default://days
                    throw new IndexOutOfRangeException();
            }
        }
        static bool IsSorted(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] > 0)
                {
                    for (int j = i + 1; j < arr.Length; j++)
                    {
                        if (arr[j] > 0)
                        {
                            if (arr[i] > arr[j])
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        static bool TimeLimit(int time, int i)
        {
            switch (i)
            {
                case 1://days
                    if (time >= 25 || time <= -25)
                        return false;
                    else
                        return true;
                case 2://Hours
                    if (time >= 25 || time <= -25)
                        return false;
                    else
                        return true;
                case 3://Min
                    if (time >= 60 || time <= -60)
                        return false;
                    else
                        return true;
                case 4://Second
                    if (time >= 60 || time <= -60)
                        return false;
                    else
                        return true;
                case 5://milisecond
                    if (time >= 1000 || time <= -1000)
                        return false;
                    else
                        return true;
                default://days
                    throw new IndexOutOfRangeException();
            }
        }
        public static void SetDoubleWord(this byte[] buffer, int register, int num)
        {
            Array.Copy(BitConverter.GetBytes(register), 0, buffer, num * 2, 4);
        }
        public static int GetDoubleWord(this byte[] buffer, int num)
        {
            num = num * 2;
            int result = (buffer[num + 3] << 24) + (buffer[num + 2] << 16)
                + (buffer[num + 1] << 8) + buffer[num];
            return result;
        }
        public static float GetFloat(this byte[] buffer, int num)
        {
            num = num * 2;
            float result = BitConverter.ToSingle(buffer, num);
            return result;
        }
        public static string GetString(this byte[] buffer, int num)
        {
            return Encoding.ASCII.GetString(buffer, num * 2, 2);
        }
    }
}
