using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dotPLC.SocketServer
{
    public class SLMPServer
    {
        public event EventHandler<StatusEventArgs> ChangeStatus;
        public event EventHandler<ErrorCommEventArgs> Error;    
        private TcpListener server;
        public IPAddress IPAddress { get; set; }
        public string CPUName { get; set; } = "FX5U-32MT/ES";
        public int Port { get; set; }
        private Thread mainThread;
        public Memory Memory { get; set; } = new Memory();
        public List<TcpClient> _lsClient = new List<TcpClient>();

        public List<TcpClient> ListClients { get => _lsClient; }
        public event EventHandler<ConnectEventArgs> LostConnect;
        public event EventHandler<ConnectEventArgs> NewConnect;
        private static byte[] Frame = new byte[] { 0x00, 0xFF,0xFF,0x03,0x00};
        public Socket listener;
        public SLMPServer()
        {
        }
        private void ParameterSetting(byte[] buffer)
        {
            buffer[0] = 0xD0; //Subheader
            buffer[1] = 0x00; //same
            buffer[2] = 0x00; //Network No.
            buffer[3] = 0xff; //Station No.
            buffer[4] = 0xff; //Module I/O no.
            buffer[5] = 0x03; //same
            buffer[6] = 0x00; // Multidrop
            buffer[7] = 0x02; //Reponse Data [CHANGE]
            buffer[8] = 0x00; //same
            buffer[9] = 0x00; //Error code   [CHANGE]
            buffer[10] = 0x00; //same

        }
        List<Thread> _threads = new List<Thread>();
        public void Stop()
        {
            server.Stop();
        }
        public void Start()
        {
            _lsClient = new List<TcpClient>();
            _threads.Clear();
            server = new TcpListener(IPAddress, Port);
            server.Start();
            mainThread = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        TcpClient client = server.AcceptTcpClient();
                        _lsClient.Add(client);
                        NewConnect?.Invoke(this, new ConnectEventArgs(client));
                        Thread thread_receive = new Thread(ReadWriteData);
                        thread_receive.IsBackground = true;
                        thread_receive.Start(client);
                        _threads.Add(thread_receive);
                    }
                }
                catch (Exception)
                {
                    foreach (var tcpClient in _lsClient.ToList())
                    {
                        LostConnect?.Invoke(this, new ConnectEventArgs(tcpClient));
                        tcpClient.Close();
                    }
                    _lsClient.Clear();
                }
            });
            mainThread.IsBackground = true;
            mainThread.Start();
        }
        private void ReadWriteData(object obj)
        {
            TcpClient tcpClient = obj as TcpClient;
            NetworkStream stream = tcpClient.GetStream();
            byte[] ReceiveBuffer = new byte[1024 * 500]; //500MB
            byte[] SendBuffer = new byte[1024 * 500];
            ParameterSetting(SendBuffer);
            try
            {
                while (stream.Read(ReceiveBuffer, 0, ReceiveBuffer.Length) > 0)
                {
                    GetData(ReceiveBuffer, SendBuffer, stream);
                }
            }
            catch(Exception) { }
            try
            {
                int read = tcpClient.Available;
                //Nếu tcpClient đã close thì nó dispose luôn nên tcpClient.Available không tồn tại nữa nên xảy
                // ra lỗi ObjectDisposedException, lúc này không cần LostConnect;
            }
            catch (ObjectDisposedException)
            {
                _lsClient.Remove(tcpClient);
                stream.Close();
                return;
            }

            LostConnect?.Invoke(this, new ConnectEventArgs(tcpClient));
            _lsClient.Remove(tcpClient);
            stream.Close();
            tcpClient.Close();
        }
        public byte[] GetRegisters(string device)
        {
            switch (device)
            {
                case "D":
                    return Memory.D;
                case "SW":
                    return Memory.SW;
                case "W":
                    return Memory.W;
                case "TN":
                    return Memory.TN;
                case "SD":
                    return Memory.SD;
                case "R":
                    return Memory.R;
                case "Z":
                    return Memory.Z;
                case "LZ":
                    return Memory.LZ;
                case "CN":
                    return Memory.CN;
                case "LCN":
                    return Memory.LCN;
                case "SN":
                    return Memory.SN;
                default:
                    throw new ArgumentOutOfRangeException("The specified device does not belong to the memory of the PLC");
            }
        }
        public bool[] GetCoils(string device)
        {
            switch (device)
            {
                case "X":
                    return Memory.X;
                case "Y":
                    return Memory.Y;
                case "M":
                    return Memory.M;
                case "L":
                    return Memory.L;
                case "F":
                    return Memory.F;
                case "B":
                    return Memory.B;
                case "S":
                    return Memory.S;
                case "SS":
                    return Memory.SS;
                case "SC":
                    return Memory.SC;
                case "TC":
                    return Memory.TC;
                case "TS":
                    return Memory.TS;
                case "CS":
                    return Memory.CS;
                case "CC":
                    return Memory.CC;
                case "SB":
                    return Memory.SB;
                case "SM":
                    return Memory.SM;
                case "BL":
                    return Memory.BL;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private byte[] GetRegisters(byte device)
        {
            switch (device)
            {
                case 0xA8:
                    return Memory.D;
                case 0xB5:
                    return Memory.SW;
                case 0xB4:
                    return Memory.W;
                case 0xC2:
                    return Memory.TN;
                case 0xA9:
                    return Memory.SD;
                case 0xAF:
                    return Memory.R;
                case 0xCC:
                    return Memory.Z;
                case 0x62:
                    return Memory.LZ;
                case 0xC5:
                    return Memory.CN;
                case 0x56:
                    return Memory.LCN;
                case 0xC8:
                    return Memory.SN;
                default:
                    throw new ArgumentOutOfRangeException("The specified device does not belong to the memory of the PLC");
            }
        }
        private bool[] GetCoils(byte device)
        {
            switch (device)
            {
                case 0x9C:
                    return Memory.X;
                case 0x9D:
                    return Memory.Y;
                case 0x90:
                    return Memory.M;
                case 0x92:
                    return Memory.L;
                case 0x93:
                    return Memory.F;
                case 0xA0:
                    return Memory.B;
                case 0x98:
                    return Memory.S;
                case 0xC7:
                    return Memory.SS;
                case 0xC6:
                    return Memory.SC;
                case 0xC0:
                    return Memory.TC;
                case 0xC1:
                    return Memory.TS;
                case 0xC4:
                    return Memory.CS;
                case 0xC3:
                    return Memory.CC;
                case 0xA1:
                    return Memory.SB;
                case 0x91:
                    return Memory.SM;
                case 0xDC:
                    return Memory.BL;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// Chuyển đổi 01
        /// </summary>
        /// <param name="num_of_points"></param>
        /// <param name="num_head_decive"></param>
        /// <param name="coils"></param>
        /// <returns></returns>
        private byte[] ConvertBoolArrayToArrayByte(int num_of_points, int num_head_decive, bool[] coils)
        {
            StringBuilder hex = new StringBuilder();
            // string hex = string.Empty;
            for (int i = num_head_decive; i < (num_of_points + num_head_decive); i++)
            {
                if (coils[i])
                {
                    hex.Append('1');
                }
                else
                    hex.Append('0');
            }
            if (num_of_points % 2 != 0)
                hex.Append('0');
            return Enumerable.Range(0, hex.Length / 2).Select(x => byte.Parse(hex.ToString().Substring(2 * x, 2), NumberStyles.HexNumber)).ToArray();
        }
        /// <summary>
        /// Chuyển đổi bool to byte
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private static byte ConvertBoolArrayToByte(bool[] source)
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

        private bool IsCoil(byte device)
        {
            switch (device)
            {
                //Registers
                case 0xA8:
                    return false;
                case 0xB5:
                    return false;
                case 0xB4:
                    return false;
                case 0xC2:
                    return false;
                case 0xA9:
                    return false;
                case 0xAF:
                    return false;
                case 0xCC:
                    return false;
                case 0x62:
                    return false;
                case 0xC5:
                    return false;
                case 0x56:
                    return false;
                case 0xC8:
                    return false;
                //Coils
                case 0x9C:
                    return true;
                case 0x9D:
                    return true;
                case 0x90:
                    return true;
                case 0x92:
                    return true;
                case 0x93:
                    return true;
                case 0x94:
                    return true;
                case 0xA0:
                    return true;
                case 0x98:
                    return true;
                case 0xC7:
                    return true;
                case 0xC6:
                    return true;
                case 0xC0:
                    return true;
                case 0xC1:
                    return true;
                case 0xC4:
                    return true;
                case 0xC3:
                    return true;
                case 0xA1:
                    return true;
                case 0x91:
                    return true;
                case 0xDC:
                default:
                    throw new ArgumentOutOfRangeException("The specified device does not belong to the memory of the PLC");
            }
        }
        public static byte ConvertBoolArrayToByteWithIndex(bool[] source, int startIndex)
        {
            byte result = 0;
            // This assumes the array never contains more than 8 elements!
            int index = 0;
            // Loop through the array
            for (int i = startIndex; i < startIndex + 8; i++)
            {
                if (source[i])
                {
                    result |= (byte)(1 << (index));
                }
                index++;
            }
            return result;
        }
        private static byte ConvertBoolArrayToByteWithIndexDaoNguoc(bool[] source, int startIndex)
        {
            byte result = 0;
            // This assumes the array never contains more than 8 elements!
            int index = 0;

            // Loop through the array
            for (int i = startIndex; i < startIndex + 8; i++)
            {
                if (source[i])
                {
                    result |= (byte)(1 << (7 - index));
                }
                index++;
            }

            return result;
        }
        private static bool[] ConvertByteToBoolArray(byte b)
        {
            // prepare the return result
            bool[] result = new bool[8];

            // check each bit in the byte. if 1 set to true, if 0 set to false
            for (int i = 0; i < 8; i++)
                result[i] = (b & (1 << i)) != 0;

            // reverse the array
            // Array.Reverse(result);

            return result;
        }

        private static bool[] ConvertByteToBoolArrayDaoNguoc(byte b)
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
        private static bool[] ConvertByteArrayToBoolArray(byte[] data, int startindex, int size)
        {
            bool[] boolArray = new bool[size];
            int num = startindex + (size % 2 == 0 ? size / 2 : size / 2 + 1);
            int k = 0;
            for (int i = startindex; i < num; ++i)
            {
                boolArray[k] = (data[i] >> 4 & 1) == 1;
                if (k + 1 < size)
                {
                    boolArray[k + 1] = (data[i] & 1) == 1;
                    k += 2;
                }
                else
                    break;
            }
            return boolArray;
        }

        private void ReadDeviceBit(byte[] ReceiveBuffer, byte[] SendBuffer, NetworkStream stream)
        {
            int num_head_decive = ReceiveBuffer[15] | (ReceiveBuffer[16] << 8) | (ReceiveBuffer[17] << 16);
            byte device = ReceiveBuffer[18];
            int num_of_points = ReceiveBuffer[19] | (ReceiveBuffer[20] << 8);
            /////////////////////
            byte[] coils = ConvertBoolArrayToArrayByte(num_of_points, num_head_decive, GetCoils(device));
            Array.Copy(coils, 0, SendBuffer, 11, coils.Length);

            int response_data_lenght = 2 + ((num_of_points % 2 == 0) ? num_of_points : (num_of_points + 1)) / 2;
            byte[] response_byte_lenght = BitConverter.GetBytes(response_data_lenght);
            SendBuffer[7] = response_byte_lenght[0];
            SendBuffer[8] = response_byte_lenght[1];
            stream.Write(SendBuffer, 0, 11 + response_data_lenght - 2);
        }
        private void ReadDeviceWord(byte[] ReceiveBuffer, byte[] SendBuffer, NetworkStream stream)
        {
            int num_head_decive =
                ReceiveBuffer[15] | (ReceiveBuffer[16] << 8) | (ReceiveBuffer[17] << 16);
            byte device = ReceiveBuffer[18];
            int num_of_points = ReceiveBuffer[19] | (ReceiveBuffer[20] << 8);
            /////////
            if (IsCoil(device)) //Device Bit
            {
                bool[] device_coils = GetCoils(device);
                for (int i = 0; i < num_of_points * 2; i++)
                {
                    SendBuffer[11 + i] = ConvertBoolArrayToByteWithIndex(device_coils, num_head_decive + i * 8);
                }
            }
            else //Device WORD
            {
                num_head_decive = num_head_decive * 2;
                byte[] device_word = GetRegisters(device);
                for (int i = 0; i < num_of_points * 2; i++)
                {
                    SendBuffer[11 + i] = device_word[i + num_head_decive];
                }
            }
            int response_data_lenght = 2 + num_of_points * 2;
            byte[] response_byte_lenght = BitConverter.GetBytes(response_data_lenght);
            SendBuffer[7] = response_byte_lenght[0];
            SendBuffer[8] = response_byte_lenght[1];
            stream.Write(SendBuffer, 0, 11 + num_of_points * 2);
        }
        private void WriteDeviceBit(byte[] ReceiveBuffer, byte[] SendBuffer, NetworkStream stream)
        {
            int num_head_decive =
                ReceiveBuffer[15] | (ReceiveBuffer[16] << 8) | (ReceiveBuffer[17] << 16);
            byte device = ReceiveBuffer[18];
            int num_of_points = ReceiveBuffer[19] | (ReceiveBuffer[20] << 8);
            var coils = ConvertByteArrayToBoolArray(ReceiveBuffer, 21, num_of_points);
            Array.Copy(coils, 0, GetCoils(device), num_head_decive, num_of_points);
            ResponseNoData(SendBuffer, stream);
        }
        private void WriteDeviceWord(byte[] ReceiveBuffer, byte[] SendBuffer, NetworkStream stream)
        {
            int num_head_decive =
                ReceiveBuffer[15] | (ReceiveBuffer[16] << 8) | (ReceiveBuffer[17] << 16);
            byte device = ReceiveBuffer[18];
            int num_of_points = ReceiveBuffer[19] | (ReceiveBuffer[20] << 8);
            if (IsCoil(device))
            {
                int j = 0;
                for (int i = 21; i < (21 + num_of_points * 2); i++)
                {
                    bool[] coils = Memory.ConvertByteToBoolArray(ReceiveBuffer[i]);
                    Array.Copy(coils, 0, GetCoils(device), num_head_decive + j, 8);
                    j += 8;
                }
            }
            else
            {
                num_head_decive = num_head_decive * 2;
                Array.Copy(ReceiveBuffer, 21, GetRegisters(device), num_head_decive, num_of_points * 2);
            }
            ResponseNoData(SendBuffer, stream);
        }
        private void ReadDeviceRandom(byte[] ReceiveBuffer, byte[] SendBuffer, NetworkStream stream)
        {
            //Word access points
            int num_of_W = ReceiveBuffer[15];
            if (num_of_W > 0)
            {
                byte[] Words = new byte[num_of_W]; //Lấy tên của các W
                int[] HeadWs = new int[num_of_W];
                for (int i = 0; i < num_of_W; i++)
                {
                    Words[i] = ReceiveBuffer[20 + i * 4];
                    HeadWs[i] = ReceiveBuffer[17 + i * 4] | (ReceiveBuffer[18 + i * 4] << 8)
                        | (ReceiveBuffer[19 + i * 4] << 16);
                }
                int index = 0;
                for (int i = 0; i < num_of_W; i++)
                {
                    if (IsCoil(Words[i]))
                    {
                        bool[] device_coils = GetCoils(Words[i]);
                        for (int j = 0; j < 2; j++)
                        {
                            SendBuffer[11 + index] =
                                ConvertBoolArrayToByteWithIndex(device_coils, HeadWs[i] + j * 8);
                            index++;
                        }

                    }
                    else
                    {
                        byte[] device_word = GetRegisters(Words[i]);
                        for (int j = 0; j < 2; j++)
                        {
                            SendBuffer[11 + index] = device_word[j + HeadWs[i] * 2];
                            index++;
                        }
                    }
                }
            }
            //Double-word access points
            int num_of_DW = ReceiveBuffer[16];
            if (num_of_DW > 0)
            {
                int index = 0;
                byte[] DWords = new byte[num_of_DW]; //Lấy tên của các DW
                int[] HeadDWs = new int[num_of_DW];
                for (int i = 0; i < num_of_DW; i++)
                {
                    DWords[i] = ReceiveBuffer[20 + num_of_W * 4 + i * 4];
                    HeadDWs[i] = ReceiveBuffer[17 + num_of_W * 4 + i * 4]
                        | (ReceiveBuffer[18 + num_of_W * 4 + i * 4] << 8)
                        | (ReceiveBuffer[19 + num_of_W * 4 + i * 4] << 16);
                }
                for (int i = 0; i < num_of_DW; i++)
                {
                    if (IsCoil(DWords[i]))
                    {
                        //4byte
                        bool[] device_Dcoils = GetCoils(DWords[i]);
                        for (int j = 0; j < 4; j++)
                        {
                            SendBuffer[11 + num_of_W * 2 + index] =
                                ConvertBoolArrayToByteWithIndex(device_Dcoils, HeadDWs[i] + j * 8);
                            index++;
                        }
                    }
                    else
                    {
                        byte[] device_Dword = GetRegisters(DWords[i]);
                        for (int j = 0; j < 4; j++)
                        {
                            SendBuffer[11 + num_of_W * 2 + index] = device_Dword[j + HeadDWs[i] * 2];
                            index++;
                        }
                    }
                }
            }
            int response_data_lenght = 2 + num_of_W * 2 + num_of_DW * 4;
            byte[] response_byte_lenght = BitConverter.GetBytes(response_data_lenght);
            SendBuffer[7] = response_byte_lenght[0];
            SendBuffer[8] = response_byte_lenght[1];
            stream.Write(SendBuffer, 0, 11 + num_of_W * 2 + num_of_DW * 4);
        }
        private void WriteDeviceRandomBit(byte[] ReceiveBuffer, byte[] SendBuffer, NetworkStream stream)
        {
            int num_head_decive =
                ReceiveBuffer[15] | (ReceiveBuffer[16] << 8) | (ReceiveBuffer[17] << 16);
            int num_of_points = ReceiveBuffer[19] | (ReceiveBuffer[20] << 8);
            int num_of_bit = ReceiveBuffer[15];
            byte[] Bits = new byte[num_of_bit];
            int[] HeadBits = new int[num_of_bit];
            for (int i = 0; i < num_of_bit; i++)
            {
                Bits[i] = ReceiveBuffer[19 + i * 5];
                HeadBits[i] = ReceiveBuffer[16 + i * 5] | (ReceiveBuffer[17 + i * 5] << 8)
                    | (ReceiveBuffer[18 + i * 5] << 16);
            }
            for (int i = 0; i < num_of_bit; i++)
            {
                bool[] coils = GetCoils(Bits[i]);
                if (ReceiveBuffer[20 + i * 5] == 1)
                    coils[HeadBits[i]] = true;
                else if (ReceiveBuffer[20 + i * 5] == 0)
                    coils[HeadBits[i]] = false;
            }
            ResponseNoData(SendBuffer, stream);
        }
        private void WriteDeviceRandomWord(byte[] ReceiveBuffer, byte[] SendBuffer, NetworkStream stream)
        {
            //Word access points
            int num_of_W = ReceiveBuffer[15];
            if (num_of_W > 0)
            {
                int index = 0;
                byte[] Words = new byte[num_of_W];
                int[] HeadWs = new int[num_of_W];
                for (int i = 0; i < num_of_W; i++)
                {
                    Words[i] = ReceiveBuffer[20 + i * 6];
                    HeadWs[i] = ReceiveBuffer[17 + i * 6]
                            | (ReceiveBuffer[18 + i * 6] << 8)
                            | (ReceiveBuffer[19 + i * 6] << 16);
                }
                for (int i = 0; i < num_of_W; i++)
                {
                    if (IsCoil(Words[i]))
                    {

                        int k = 0;
                        bool[] device_coils = GetCoils(Words[i]);
                        for (int j = 0; j < 2; j++)
                        {
                            //Tạo ra 8 bit từ 1 byte
                            bool[] coils = Memory.ConvertByteToBoolArray(ReceiveBuffer[21 + j + index]);
                            Array.Copy(coils, 0, device_coils, HeadWs[i] + k, 8);
                            k += 8;
                        }
                        //chuyển qua coils khác
                        index += 6;
                    }
                    else
                    {
                        Array.Copy(ReceiveBuffer, 21 + i * 6, GetRegisters(Words[i]), HeadWs[i] * 2, 2);
                    }
                }
            }
            //Double-word access points
            int num_of_DW = ReceiveBuffer[16];
            if (num_of_DW > 0)
            {
                int index = 0;
                byte[] DWords = new byte[num_of_DW];
                int[] HeadDWs = new int[num_of_DW];
                for (int i = 0; i < num_of_DW; i++)
                {
                    DWords[i] = ReceiveBuffer[20 + num_of_W * 6 + i * 8];
                    HeadDWs[i] = ReceiveBuffer[17 + num_of_W * 6 + i * 8]
                            | (ReceiveBuffer[18 + num_of_W * 6 + i * 8] << 8)
                            | (ReceiveBuffer[19 + num_of_W * 6 + i * 8] << 16);
                }
                for (int i = 0; i < num_of_DW; i++)
                {
                    if (IsCoil(DWords[i]))
                    {

                        int k = 0;
                        bool[] device_coils = GetCoils(DWords[i]);
                        //4byte
                        for (int j = 0; j < 4; j++)
                        {
                            //Tạo ra 8 bit từ 1 byte
                            bool[] coils = Memory.ConvertByteToBoolArray(ReceiveBuffer[21 + num_of_W * 6 + j + index]);
                            Array.Copy(coils, 0, device_coils, HeadDWs[i] + k, 8);
                            k += 8;
                        }
                        //chuyển qua coils khác
                        index += 8;
                    }
                    else
                    {
                        //4byte
                        Array.Copy(ReceiveBuffer, 21 + num_of_W * 6 + i * 8, GetRegisters(DWords[i]), HeadDWs[i] * 2, 4);
                    }
                }
            }
            ResponseNoData(SendBuffer, stream);
        }
        private void GetCPUName(byte[] ReceiveBuffer, byte[] SendBuffer, NetworkStream stream)
        {
            byte[] data_bytes = Encoding.ASCII.GetBytes(this.CPUName);
            int length = CPUName.Length;
            byte[] datalength_bytes = BitConverter.GetBytes((short)(2 + length));
            SendBuffer[7] = datalength_bytes[0];
            SendBuffer[8] = datalength_bytes[1];
            for (int i = 0; i < length; i++)
            {
                SendBuffer[11 + i] = data_bytes[i];
            }
            stream.Write(SendBuffer, 0, 11 + length);
        }
        private void SelfTest(byte[] ReceiveBuffer, byte[] SendBuffer, NetworkStream stream)
        {
            int numberOfLoopback = (ReceiveBuffer[16] << 8) | ReceiveBuffer[15];
            int j = 0;
            for (int i = 17; i < 17 + numberOfLoopback; i++)
            {
                SendBuffer[13 + j] = ReceiveBuffer[i];
                j++;
            }
            SendBuffer[11] = ReceiveBuffer[15];  // Number of loopback data
            SendBuffer[12] = ReceiveBuffer[16];
            int response_data_length = 2 + 2 + numberOfLoopback;
            var response_data_length_bytes = BitConverter.GetBytes(response_data_length);
            SendBuffer[7] = response_data_length_bytes[0];
            SendBuffer[8] = response_data_length_bytes[1];
            stream.Write(SendBuffer, 0, 11 + 2 + numberOfLoopback);
        }

        
        private void ResponseNoData(byte[] sendbuffer, NetworkStream stream)
        {
            sendbuffer[7] = 0x02; //Reponse Data [CHANGE]
            sendbuffer[8] = 0x00; //same
            stream.Write(sendbuffer, 0, 11);
        }

        private void ResponseErrorData(byte[] sendbuffer, byte[] readbuffer, byte errorLo, byte errorHi, NetworkStream stream)
        {
            for (int i = 0; i < 7; i++)
            {
                sendbuffer[i] = readbuffer[i];
            }
            sendbuffer[7] = 0x0B; //Reponse Data [11byte]
            sendbuffer[8] = 0x00; //same
            sendbuffer[9]= errorLo;
            sendbuffer[10]= errorHi;
            Array.Copy(Frame, 0, sendbuffer, 11, 5);
            sendbuffer[16] = readbuffer[11]; //Command Lo
            sendbuffer[17] = readbuffer[12]; //Command Hi
            sendbuffer[18] = readbuffer[13]; //Subcommand Lo
            sendbuffer[19] = readbuffer[14]; //Subcommand Hi
            stream.Write(sendbuffer, 0, 20);
        }

        private byte GetNameDevice(string device)
        {
            switch (device)
            {
                //Register
                case "D":
                    return 0x20;  //Decimal
                case "SW":
                    return 0x31; //Hexadecimal
                case "W":
                    return 0x30; //Hexadecimal
                case "TN":
                    return 0x42; //Timer word //Decimal 
                case "SD":
                    return 0x21; //Decimal
                case "R":
                    return 0x27; // Decimal
                case "Z":
                    return 0x60;  // Decimal
                case "LZ":
                    return 0x62;  // Decimal
                case "CN":
                    return 0x46; //Counter
                case "LCN":
                    return 0x56;     //Long counter  // Decimal
                case "SN":
                    return 0x4a;   // Retentive timer-Timer có nhớ ST0.. //Decimal
                case "STN":
                    return 0x4a;  // Giống ở trên //Decimal

                //Bit:
                case "X":
                    return 0x10; //OCtal
                case "Y":
                    return 0x11;  //OCtal
                case "M":
                    return 0x01; //Decimal
                case "L":
                    return 0x03;  //Decimal
                case "F":
                    return 0x04;  //Decimal
                case "B":
                    return 0x14;  //Hexca
                case "S":
                    return 0x08;    //Step relay //Decimal
                case "SS":
                    return 0x49;    //Retentive timer  ST0 //Decimal Bit
                case "SC":
                    return 0x48;    //Retentive timer ST0 //Decimal Bit
                case "TC":
                    return 0x40;    //Timer T0 (bật) Decimal
                case "TS":
                    return 0x41;   // Timer T0 Decimal
                case "CS":
                    return 0x45;    //Counter  C0  Decimal
                case "CC":
                    return 0x44;    //Counter C0 Decimal
                case "SB":
                    return 0x15;  //Link special relay Hex
                case "SM":
                    return 0x02; //Special relay Decmal
                case "BL":
                    return 0x72;    //SFC block device Decimal
                default:
                    return 0x00;
            }
        }
        private void GetData(byte[] ReceiveBuffer, byte[] SendBuffer, NetworkStream stream)
        {

            int cmd = (ReceiveBuffer[12] << 8) + ReceiveBuffer[11];
            Command command = (Command)cmd;
            int subcmd = (ReceiveBuffer[14] << 8) + ReceiveBuffer[13];
            Subcommand subcommand = (Subcommand)subcmd;
            switch (command)
            {
                case Command.DEVICE_READ:
                    if (subcommand == Subcommand.BIT)
                        ReadDeviceBit(ReceiveBuffer, SendBuffer, stream);
                    else if (subcommand == Subcommand.WORD)
                        ReadDeviceWord(ReceiveBuffer, SendBuffer, stream);
                    else
                    {
                        ResponseNoData(SendBuffer, stream);
                        Error?.Invoke(this, new ErrorCommEventArgs(subcommand, command));
                    }
                    break;
                case Command.DEVICE_WRITE:
                    if (subcommand == Subcommand.BIT)
                        WriteDeviceBit(ReceiveBuffer, SendBuffer, stream);
                    else if (subcommand == Subcommand.WORD)
                        WriteDeviceWord(ReceiveBuffer, SendBuffer, stream);
                    else
                    {
                        ResponseNoData(SendBuffer, stream);
                        Error?.Invoke(this, new ErrorCommEventArgs(subcommand, command));
                    }
                    break;
                case Command.DEVICE_READ_RANDOM:
                    ReadDeviceRandom(ReceiveBuffer, SendBuffer, stream);
                    break;
                case Command.DEVICE_WRITE_RANDOM:
                    if (subcommand == Subcommand.BIT) //chỉ bit 
                        WriteDeviceRandomBit(ReceiveBuffer, SendBuffer, stream);
                    else if (subcommand == Subcommand.WORD) //word và dword
                        WriteDeviceRandomWord(ReceiveBuffer, SendBuffer, stream);
                    else
                    {
                        ResponseNoData(SendBuffer, stream);
                        Error?.Invoke(this, new ErrorCommEventArgs(subcommand, command));
                    }
                    break;
                case Command.DEVICE_READ_BLOCK:
                    ResponseNoData(SendBuffer, stream);
                    break;
                case Command.DEVICE_WRITE_BLOCK:
                    ResponseNoData(SendBuffer, stream);
                    break;
                case Command.GET_CPU_NAME:
                    if (subcommand == Subcommand.WORD)
                        GetCPUName(ReceiveBuffer, SendBuffer, stream);
                    else
                    {
                        ResponseNoData(SendBuffer, stream);
                        Error?.Invoke(this, new ErrorCommEventArgs(subcommand, command));
                    }
                    break;
                case Command.SELF_TEST:
                    if (subcommand == Subcommand.WORD)
                        SelfTest(ReceiveBuffer, SendBuffer, stream);
                    else
                    {
                        ResponseNoData(SendBuffer, stream);
                        Error?.Invoke(this, new ErrorCommEventArgs(subcommand, command));
                    }
                    break;
                case Command.REMOTE_RUN:
                    ResponseNoData(SendBuffer, stream);
                    if (subcommand == Subcommand.WORD)
                        ChangeStatus?.Invoke(this, new StatusEventArgs(subcommand, command));
                    else
                        Error?.Invoke(this, new ErrorCommEventArgs(subcommand, command));
                    break;
                case Command.REMOTE_PAUSE:
                    ResponseNoData(SendBuffer, stream);
                    if (subcommand == Subcommand.WORD)
                        ChangeStatus?.Invoke(this, new StatusEventArgs(subcommand, command));
                    else
                        Error?.Invoke(this, new ErrorCommEventArgs(subcommand, command));
                    break;
                case Command.REMOTE_STOP:
                    ResponseNoData(SendBuffer, stream);
                    if (subcommand == Subcommand.WORD)
                        ChangeStatus?.Invoke(this, new StatusEventArgs(subcommand, command));
                    else
                        Error?.Invoke(this, new ErrorCommEventArgs(subcommand, command));
                    break;
                case Command.REMOTE_RESET:
                    ResponseNoData(SendBuffer, stream);
                    if (subcommand == Subcommand.WORD)
                        ChangeStatus?.Invoke(this, new StatusEventArgs(subcommand, command));
                    else
                        Error?.Invoke(this, new ErrorCommEventArgs(subcommand, command));
                    break;
                case Command.CLEAR_LACTCH:
                    ResponseNoData(SendBuffer, stream);
                    if (subcommand == Subcommand.WORD)
                        ChangeStatus?.Invoke(this, new StatusEventArgs(subcommand, command));
                    else
                        Error?.Invoke(this, new ErrorCommEventArgs(subcommand, command));
                    break;
                case Command.CLEAR_ERROR:
                    ResponseNoData(SendBuffer, stream);
                    if (subcommand == Subcommand.WORD)
                        ChangeStatus?.Invoke(this, new StatusEventArgs(subcommand, command));
                    else
                        Error?.Invoke(this, new ErrorCommEventArgs(subcommand, command));
                    break;
                case Command.LOCK:
                    ResponseNoData(SendBuffer, stream);
                    if (subcommand == Subcommand.WORD)
                        ChangeStatus?.Invoke(this, new StatusEventArgs(subcommand, command));
                    else
                        Error?.Invoke(this, new ErrorCommEventArgs(subcommand, command));
                    break;
                case Command.UNLOCK:
                    ResponseNoData(SendBuffer, stream);
                    if (subcommand == Subcommand.WORD)
                        ChangeStatus?.Invoke(this, new StatusEventArgs(subcommand, command));
                    else
                        Error?.Invoke(this, new ErrorCommEventArgs(subcommand, command));
                    break;
                default:
                    //C060H - There is a request that cannot be executed for the target CPU module.
                    ResponseErrorData(SendBuffer, ReceiveBuffer, 0x5F, 0xC0, stream);
                    Error?.Invoke(this, new ErrorCommEventArgs(subcommand,command));
                    break;
            }
            //Clear Command & Subcommand
            for (int i = 11; i < 15; i++)
            {
                ReceiveBuffer[i] = 0;
            }
        }
    }
    public enum Command
    {
        DEVICE_READ = 0x0401,
        DEVICE_WRITE = 0x1401,
        DEVICE_READ_RANDOM = 0x0403,
        DEVICE_WRITE_RANDOM = 0x1402,
        DEVICE_READ_BLOCK = 0x0406,
        DEVICE_WRITE_BLOCK = 0x1406,
        GET_CPU_NAME = 0x0101,
        SELF_TEST = 0x0619,
        REMOTE_RUN = 0x1001,
        REMOTE_STOP = 0x1002,
        REMOTE_PAUSE = 0x1003,
        REMOTE_RESET = 0x1006,
        CLEAR_LACTCH = 0x1005,
        CLEAR_ERROR = 0x1617,
        LOCK = 0x1631,
        UNLOCK = 0x1630
    }
    public enum Subcommand
    {
        BIT = 1,
        WORD = 0
    }
    public class ConnectEventArgs : EventArgs
    {
        public TcpClient SocketClient { get; }
        public ConnectEventArgs(TcpClient client)
        {
            this.SocketClient = client;
        }
    }
    public class StatusEventArgs : EventArgs
    {
        public Command Command { get; }
        public Subcommand Subcommand { get; }
        public StatusEventArgs(Subcommand subcommand, Command command)
        {
            Command = command;
            Subcommand = Subcommand;
        }
    }
    public class ErrorCommEventArgs : EventArgs
    {
        public Command Command { get; }
        public Subcommand Subcommand { get; }
        public ErrorCommEventArgs(Subcommand subcommand, Command command)
        {
            Command = command;
            Subcommand = Subcommand;
        }
    }
}
