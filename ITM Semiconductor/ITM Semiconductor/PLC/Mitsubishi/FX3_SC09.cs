using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitsubishi
{
    public class FX3_SC09
    {
        #region Properties
        // Serial Port:
        private SerialPort Comport = new SerialPort();
        // private SerialPort serial;
        private string portName;
        private int dataBits;
        private Parity parity;
        private StopBits stopbits;
        private int baudRate;
        private bool isConnected;
        // MC Protocol
        private int netWorkNo;
        private int pcNo;
        private int stationNo;
        public string PortName { get => portName; set => portName = value; }
        public int DataBits { get => dataBits; set => dataBits = value; }
        public Parity Parity { get => parity; set => parity = value; }
        public StopBits Stopbits { get => stopbits; set => stopbits = value; }
        public int BaudRate { get => baudRate; set => baudRate = value; }

        public int NetWorkNo { get => netWorkNo; set => netWorkNo = value; }
        public int PcNo { get => pcNo; set => pcNo = value; }
        public int StationNo { get => stationNo; set => stationNo = value; }
        public bool IsConnected { get => isConnected; private set => isConnected = value; }

        #endregion

        private object ComportLock = new object();
        public FX3_SC09()
        {
            if (Comport == null)
            {
                Comport = new SerialPort();
            }
            //Initial
            this.portName = "COM9";
            this.dataBits = 8;
            this.stopbits = StopBits.One;
            this.parity = Parity.Even;
            this.baudRate = 9600;
            //MC Protocol
            this.netWorkNo = 0;
            this.stationNo = 0;
            this.pcNo = 0xff;

            this.Comport.PortName = this.portName;
            this.Comport.DataBits = this.DataBits;
            this.Comport.StopBits = this.Stopbits;
            this.Comport.Parity = this.Parity;
            this.Comport.BaudRate = this.baudRate;

        }
        public FX3_SC09(string _portName, int _databits, StopBits _stopBits, Parity _parity, int _baudRate)
        {
            if (Comport == null)
            {
                Comport = new SerialPort();
            }
            //Initial
            this.portName = _portName; //string.Format("COM{0}", (object)_portName);
            this.dataBits = _databits;
            this.stopbits = _stopBits;
            this.parity = _parity;
            this.baudRate = _baudRate;
            //MC Protocol

            this.Comport.PortName = this.portName;
            this.Comport.DataBits = this.DataBits;
            this.Comport.StopBits = this.Stopbits;
            this.Comport.Parity = this.Parity;
            this.Comport.BaudRate = this.baudRate;

        }
        public void SetProperty(string _portName, int _databits, StopBits _stopBits, Parity _parity, int _baudRate)
        {
            try
            {
                this.portName = _portName; // string.Format("COM{0}", (object)_portName);
                this.dataBits = _databits;
                this.stopbits = _stopBits;
                this.parity = _parity;
                this.baudRate = _baudRate;
            }
            catch (Exception)
            {
            }
        }

        public bool OpenPLC()
        {
            try
            {

                this.Comport.PortName = this.portName;
                this.Comport.BaudRate = this.baudRate;
                this.Comport.Parity = this.Parity;
                this.Comport.DataBits = this.DataBits;
                this.Comport.StopBits = this.Stopbits;
                this.Comport.ReadTimeout = 1000;
                this.Comport.WriteTimeout = 1000;
                this.Comport.ReadBufferSize = 1024;
                this.Comport.WriteBufferSize = 1024;
                if (this.Comport.IsOpen)
                    this.Comport.Close();
                this.Comport.Open();
                return this.Comport.IsOpen;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Open(int Port)
        {
            try
            {

                this.Comport.PortName = string.Format("COM{0}", (object)Port);
                this.Comport.BaudRate = 9600;
                this.Comport.Parity = Parity.Even;
                this.Comport.DataBits = 7;
                this.Comport.StopBits = StopBits.One;
                this.Comport.ReadTimeout = 1000;
                this.Comport.WriteTimeout = 1000;
                this.Comport.ReadBufferSize = 1024;
                this.Comport.WriteBufferSize = 1024;
                if (this.Comport.IsOpen)
                    this.Comport.Close();
                this.Comport.Open();
                return this.Comport.IsOpen;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void CLose() => this.Comport.Close();

        public bool SetBit(string strBitName)
        {
            if (this.Comport == null || !this.Comport.IsOpen)
                throw new Exception("Please check the status of the serial port!");
            int length = strBitName.Length;
            int dwAddress = int.Parse(strBitName.Substring(1, length - 1));
            byte[] numArray;
            if (strBitName.Substring(0, 1).ToUpper().Contains("Y") && length >= 2)
            {
                byte[] outAddress;
                this.AddressToAscii(FX3_SC09.REGISTER_TYPE.WY, dwAddress, out outAddress);
                numArray = new byte[7]
                {
          (byte) 2,
          (byte) 55,
          outAddress[1],
          outAddress[0],
          outAddress[3],
          outAddress[2],
          (byte) 3
                };
            }
            else
            {
                if (!strBitName.Substring(0, 1).ToUpper().Equals("M") || length < 2)
                    throw new Exception("Bit address input error");
                byte[] outAddress;
                this.AddressToAscii(FX3_SC09.REGISTER_TYPE.WM, dwAddress, out outAddress);
                numArray = new byte[7]
                {
          (byte) 2,
          (byte) 55,
          outAddress[1],
          outAddress[0],
          outAddress[3],
          outAddress[2],
          (byte) 3
                };
            }
            byte sum0;
            byte sum1;
            this.CheckSum((IEnumerable<byte>)numArray, 1, numArray.Length - 1, out sum0, out sum1);
            List<byte> byteList = new List<byte>((IEnumerable<byte>)numArray);
            byteList.Add(sum1);
            byteList.Add(sum0);
            lock (this.ComportLock)
            {
                this.Comport.Write(byteList.ToArray(), 0, byteList.Count);
                return this.Set_or_Reset_Bit_Ack();
            }
        }

        public bool ResetBit(string strBitName)
        {
            if (this.Comport == null || !this.Comport.IsOpen)
                throw new Exception("Please check the status of the serial port!");
            int length = strBitName.Length;
            int dwAddress = int.Parse(strBitName.Substring(1, length - 1));
            byte[] numArray;
            if (strBitName.Substring(0, 1).ToUpper().Equals("Y") && length >= 2)
            {
                byte[] outAddress;
                this.AddressToAscii(FX3_SC09.REGISTER_TYPE.WY, dwAddress, out outAddress);
                numArray = new byte[7]
                {
          (byte) 2,
          (byte) 56,
          outAddress[1],
          outAddress[0],
          outAddress[3],
          outAddress[2],
          (byte) 3
                };
            }
            else
            {
                if (!strBitName.Substring(0, 1).ToUpper().Equals("M") || length < 2)
                    throw new Exception("Bit address input error");
                byte[] outAddress;
                this.AddressToAscii(FX3_SC09.REGISTER_TYPE.WM, dwAddress, out outAddress);
                numArray = new byte[7]
                {
          (byte) 2,
          (byte) 56,
          outAddress[1],
          outAddress[0],
          outAddress[3],
          outAddress[2],
          (byte) 3
                };
            }
            byte sum0;
            byte sum1;
            this.CheckSum((IEnumerable<byte>)numArray, 1, numArray.Length - 1, out sum0, out sum1);
            List<byte> byteList = new List<byte>((IEnumerable<byte>)numArray);
            byteList.Add(sum1);
            byteList.Add(sum0);
            lock (this.ComportLock)
            {
                this.Comport.Write(byteList.ToArray(), 0, byteList.Count);
                return this.Set_or_Reset_Bit_Ack();
            }
        }

        public bool ReadBit(string strBitName)
        {
            if (this.Comport == null || !this.Comport.IsOpen)
                throw new Exception("Please check the status of the serial port!");
            int length = strBitName.Length;
            int num = int.Parse(strBitName.Substring(1, length - 1));
            byte[] numArray;
            if (strBitName.Substring(0, 1).ToUpper().Equals("Y") && length >= 2)
            {
                byte[] outAddress;
                this.AddressToAscii(FX3_SC09.REGISTER_TYPE.RY, num, out outAddress);
                numArray = new byte[9]
                {
          (byte) 2,
          (byte) 48,
          outAddress[3],
          outAddress[2],
          outAddress[1],
          outAddress[0],
          (byte) 48,
          (byte) 49,
          (byte) 3
                };
            }
            else if (strBitName.Substring(0, 1).ToUpper().Equals("M") && length >= 2)
            {
                byte[] outAddress;
                this.AddressToAscii(FX3_SC09.REGISTER_TYPE.RM, num, out outAddress);
                numArray = new byte[9]
                {
          (byte) 2,
          (byte) 48,
          outAddress[3],
          outAddress[2],
          outAddress[1],
          outAddress[0],
          (byte) 48,
          (byte) 49,
          (byte) 3
                };
            }
            else
            {
                if (!strBitName.Substring(0, 1).ToUpper().Equals("X") || length < 2)
                    throw new Exception("Bit address input error");
                byte[] outAddress;
                this.AddressToAscii(FX3_SC09.REGISTER_TYPE.RX, num, out outAddress);
                numArray = new byte[9]
                {
          (byte) 2,
          (byte) 48,
          outAddress[3],
          outAddress[2],
          outAddress[1],
          outAddress[0],
          (byte) 48,
          (byte) 49,
          (byte) 3
                };
            }
            byte sum0;
            byte sum1;
            this.CheckSum((IEnumerable<byte>)numArray, 1, numArray.Length - 1, out sum0, out sum1);
            List<byte> byteList = new List<byte>((IEnumerable<byte>)numArray);
            byteList.Add(sum1);
            byteList.Add(sum0);
            lock (this.ComportLock)
            {
                this.Comport.Write(byteList.ToArray(), 0, byteList.Count);
                return this.Read_Bit_Ack(num);
            }
        }

        public short ReadInt(string strRegisterName)
        {
            if (this.Comport == null || !this.Comport.IsOpen)
                throw new Exception("Please check the status of the serial port!");
            int length = strRegisterName.Length;
            if (!strRegisterName.Substring(0, 1).ToUpper().Equals("D") || length < 2)
                throw new Exception("Register address input error");
            byte[] outAddress;
            this.AddressToAscii(FX3_SC09.REGISTER_TYPE.D, int.Parse(strRegisterName.Substring(1, length - 1)), out outAddress);
            byte[] numArray = new byte[9]
            {
        (byte) 2,
        (byte) 48,
        outAddress[3],
        outAddress[2],
        outAddress[1],
        outAddress[0],
        (byte) 48,
        (byte) 50,
        (byte) 3
            };
            byte sum0;
            byte sum1;
            this.CheckSum((IEnumerable<byte>)numArray, 1, numArray.Length - 1, out sum0, out sum1);
            List<byte> byteList = new List<byte>((IEnumerable<byte>)numArray);
            byteList.Add(sum1);
            byteList.Add(sum0);
            lock (this.ComportLock)
            {
                this.Comport.Write(byteList.ToArray(), 0, byteList.Count);
                return this.ReadIntAck();
            }
        }

        public int ReadDint(string strRegisterStartName)
        {
            if (this.Comport == null || !this.Comport.IsOpen)
                throw new Exception("Please check the status of the serial port! ");
            int length = strRegisterStartName.Length;
            if (!strRegisterStartName.Substring(0, 1).ToUpper().Equals("D") || length < 2)
                throw new Exception("Register address input error ");
            byte[] outAddress;
            this.AddressToAscii(FX3_SC09.REGISTER_TYPE.D, int.Parse(strRegisterStartName.Substring(1, length - 1)), out outAddress);
            byte[] numArray = new byte[9]
            {
        (byte) 2,
        (byte) 48,
        outAddress[3],
        outAddress[2],
        outAddress[1],
        outAddress[0],
        (byte) 48,
        (byte) 52,
        (byte) 3
            };
            byte sum0;
            byte sum1;
            this.CheckSum((IEnumerable<byte>)numArray, 1, numArray.Length - 1, out sum0, out sum1);
            List<byte> byteList = new List<byte>((IEnumerable<byte>)numArray);
            byteList.Add(sum1);
            byteList.Add(sum0);
            lock (this.ComportLock)
            {
                this.Comport.Write(byteList.ToArray(), 0, byteList.Count);
                return this.ReadDintAck();
            }
        }

        public bool WriteInt(string strRegisterName, short value)
        {
            if (this.Comport == null || !this.Comport.IsOpen)
                throw new Exception("Please check the status of the serial port!");
            int length = strRegisterName.Length;
            if (!strRegisterName.Substring(0, 1).ToUpper().Equals("D") || length < 2)
                throw new Exception("Register address input error");
            byte[] outAddress;
            this.AddressToAscii(FX3_SC09.REGISTER_TYPE.D, int.Parse(strRegisterName.Substring(1, length - 1)), out outAddress);
            byte ascii0_1;
            byte ascii1_1;
            this.Dec2Ascii((byte)((uint)value & (uint)byte.MaxValue), out ascii0_1, out ascii1_1);
            byte ascii0_2;
            byte ascii1_2;
            this.Dec2Ascii((byte)((int)value >> 8 & (int)byte.MaxValue), out ascii0_2, out ascii1_2);
            byte[] numArray = new byte[13]
            {
        (byte) 2,
        (byte) 49,
        outAddress[3],
        outAddress[2],
        outAddress[1],
        outAddress[0],
        (byte) 48,
        (byte) 50,
        ascii1_1,
        ascii0_1,
        ascii1_2,
        ascii0_2,
        (byte) 3
            };
            byte sum0;
            byte sum1;
            this.CheckSum((IEnumerable<byte>)numArray, 1, numArray.Length - 1, out sum0, out sum1);
            List<byte> byteList = new List<byte>((IEnumerable<byte>)numArray);
            byteList.Add(sum1);
            byteList.Add(sum0);
            lock (this.ComportLock)
            {
                this.Comport.Write(byteList.ToArray(), 0, byteList.Count);
                return this.ReadVoidAck();
            }
        }

        public bool WriteDint(string strRegisterName, int value)
        {
            if (this.Comport == null || !this.Comport.IsOpen)
                throw new Exception("Please check the status of the serial port!");
            int length = strRegisterName.Length;
            if (!strRegisterName.Substring(0, 1).ToUpper().Equals("D") || length < 2)
                throw new Exception("Register address input error");
            byte[] outAddress;
            this.AddressToAscii(FX3_SC09.REGISTER_TYPE.D, int.Parse(strRegisterName.Substring(1, length - 1)), out outAddress);
            byte ascii0_1;
            byte ascii1_1;
            this.Dec2Ascii((byte)(value & (int)byte.MaxValue), out ascii0_1, out ascii1_1);
            byte ascii0_2;
            byte ascii1_2;
            this.Dec2Ascii((byte)(value >> 8 & (int)byte.MaxValue), out ascii0_2, out ascii1_2);
            byte ascii0_3;
            byte ascii1_3;
            this.Dec2Ascii((byte)(value >> 16 & (int)byte.MaxValue), out ascii0_3, out ascii1_3);
            byte ascii0_4;
            byte ascii1_4;
            this.Dec2Ascii((byte)(value >> 24 & (int)byte.MaxValue), out ascii0_4, out ascii1_4);
            byte[] numArray = new byte[17]
            {
        (byte) 2,
        (byte) 49,
        outAddress[3],
        outAddress[2],
        outAddress[1],
        outAddress[0],
        (byte) 48,
        (byte) 52,
        ascii1_1,
        ascii0_1,
        ascii1_2,
        ascii0_2,
        ascii1_3,
        ascii0_3,
        ascii1_4,
        ascii0_4,
        (byte) 3
            };
            byte sum0;
            byte sum1;
            this.CheckSum((IEnumerable<byte>)numArray, 1, numArray.Length - 1, out sum0, out sum1);
            List<byte> byteList = new List<byte>((IEnumerable<byte>)numArray);
            byteList.Add(sum1);
            byteList.Add(sum0);
            lock (this.ComportLock)
            {
                this.Comport.Write(byteList.ToArray(), 0, byteList.Count);
                return this.ReadVoidAck();
            }
        }

        public bool IsOpen() => this.Comport.IsOpen;

        private void CheckSum(
          IEnumerable<byte> byteArray,
          int nStartPos,
          int nEndPos,
          out byte sum0,
          out byte sum1)
        {
            if (nEndPos > byteArray.Count<byte>() || nStartPos < 0)
                throw new Exception("The starting range of the array is incorrect, please check carefully! ");
            List<byte> source = new List<byte>();
            for (int index = nStartPos; index <= nEndPos; ++index)
                source.Add(byteArray.ElementAt<byte>(index));
            int num1 = source.Count<byte>();
            int num2 = 0;
            for (int index = 0; index < num1; ++index)
                num2 += (int)source.ElementAt<byte>(index);
            string upper = string.Format("{0:X2}", (object)(num2 & (int)byte.MaxValue)).ToUpper();
            sum0 = (byte)upper[1];
            sum1 = (byte)upper[0];
        }

        private void AddressToAscii(
          FX3_SC09.REGISTER_TYPE nType,
          int dwAddress,
          out byte[] outAddress)
        {
            outAddress = new byte[4];
            switch (nType)
            {
                case FX3_SC09.REGISTER_TYPE.RX:
                    dwAddress = dwAddress / 8 + 128;
                    string upper1 = string.Format("{0:X4}", (object)dwAddress).ToUpper();
                    for (int index = 0; index < 4; ++index)
                        outAddress[3 - index] = (byte)upper1[index];
                    break;
                case FX3_SC09.REGISTER_TYPE.RY:
                    dwAddress = dwAddress / 8 + 160;
                    string upper2 = string.Format("{0:X4}", (object)dwAddress).ToUpper();
                    for (int index = 0; index < 4; ++index)
                        outAddress[3 - index] = (byte)upper2[index];
                    break;
                case FX3_SC09.REGISTER_TYPE.RM:
                    dwAddress = dwAddress / 8 + 256;
                    string upper3 = string.Format("{0:X4}", (object)dwAddress).ToUpper();
                    for (int index = 0; index < 4; ++index)
                        outAddress[3 - index] = (byte)upper3[index];
                    break;
                case FX3_SC09.REGISTER_TYPE.WY:
                    string str = "05" + this.ConvertDataFx(dwAddress);
                    for (int index = 0; index < 4; ++index)
                        outAddress[3 - index] = (byte)str[index];
                    break;
                case FX3_SC09.REGISTER_TYPE.WM:
                    dwAddress += 2048;
                    string upper4 = string.Format("{0:X4}", (object)dwAddress).ToUpper();
                    for (int index = 0; index < 4; ++index)
                        outAddress[3 - index] = (byte)upper4[index];
                    break;
                case FX3_SC09.REGISTER_TYPE.D:
                    dwAddress = dwAddress * 2 + 4096;
                    string upper5 = string.Format("{0:X4}", (object)dwAddress).ToUpper();
                    for (int index = 0; index < 4; ++index)
                        outAddress[3 - index] = (byte)upper5[index];
                    break;
            }
        }

        private int HexStr2Dec(string szHexData, int nStartPos, int nEndPos)
        {
            int num1 = 0;
            for (int index = nStartPos; index < nEndPos; ++index)
            {
                int num2 = (int)this.HexCh2Dec(szHexData[index]);
                double num3 = Math.Pow(16.0, (double)(nEndPos - nStartPos - index - 1));
                num1 += (int)((double)num2 * num3);
            }
            return num1;
        }

        private byte HexCh2Dec(char ch)
        {
            switch (ch)
            {
                case 'A':
                case 'a':
                    return 10;
                case 'B':
                case 'b':
                    return 11;
                case 'C':
                case 'c':
                    return 12;
                case 'D':
                case 'd':
                    return 13;
                case 'E':
                case 'e':
                    return 14;
                case 'F':
                case 'f':
                    return 15;
                default:
                    return byte.Parse(ch.ToString());
            }
        }

        private void Dec2Ascii(byte nData, out byte ascii0, out byte ascii1)
        {
            string str = string.Format("{0:X2}", (object)nData);
            ascii0 = (byte)str[1];
            ascii1 = (byte)str[0];
        }

        private bool ReadVoidAck()
        {
            long ticks = DateTime.Now.Ticks;
            do
            {
                if (this.Comport.BytesToRead > 0)
                    goto label_1;
            }
            while (TimeSpan.FromTicks(DateTime.Now.Ticks - ticks).TotalMilliseconds <= 2000.0);
            goto label_5;
        label_1:
            return this.Comport.ReadByte() == 6;
        label_5:
            throw new Exception("Communication timeout");
        }

        private short ReadIntAck()
        {
            long ticks = DateTime.Now.Ticks;
            List<byte> byteList = new List<byte>();
            bool flag = false;
            do
            {
                if (this.Comport.BytesToRead > 0)
                {
                    int num = this.Comport.ReadByte();
                    if (num == 2)
                        flag = true;
                    if (flag)
                    {
                        byteList.Add((byte)num);
                        if (byteList.Count == 6 && num == 3)
                            goto label_5;
                    }
                }
            }
            while (TimeSpan.FromTicks(DateTime.Now.Ticks - ticks).TotalMilliseconds <= 2000.0);
            goto label_8;
        label_5:
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append((char)byteList[3]);
            stringBuilder.Append((char)byteList[4]);
            stringBuilder.Append((char)byteList[1]);
            stringBuilder.Append((char)byteList[2]);
            return Convert.ToInt16(stringBuilder.ToString(), 16);
        label_8:
            throw new Exception("Communication timeout");
        }

        private int ReadDintAck()
        {
            long ticks = DateTime.Now.Ticks;
            List<byte> byteList = new List<byte>();
            bool flag = false;
            do
            {
                if (this.Comport.BytesToRead > 0)
                {
                    int num = this.Comport.ReadByte();
                    if (num == 2)
                        flag = true;
                    if (flag)
                    {
                        byteList.Add((byte)num);
                        if (byteList.Count == 10 && num == 3)
                            goto label_5;
                    }
                }
            }
            while (TimeSpan.FromTicks(DateTime.Now.Ticks - ticks).TotalMilliseconds <= 2000.0);
            goto label_8;
        label_5:
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append((char)byteList[7]);
            stringBuilder.Append((char)byteList[8]);
            stringBuilder.Append((char)byteList[5]);
            stringBuilder.Append((char)byteList[6]);
            stringBuilder.Append((char)byteList[3]);
            stringBuilder.Append((char)byteList[4]);
            stringBuilder.Append((char)byteList[1]);
            stringBuilder.Append((char)byteList[2]);
            return Convert.ToInt32(stringBuilder.ToString(), 16);
        label_8:
            throw new Exception("Communication timeout");
        }

        private bool Set_or_Reset_Bit_Ack()
        {
            long ticks = DateTime.Now.Ticks;
            List<byte> byteList = new List<byte>();
            do
            {
                if (this.Comport.BytesToRead > 0 && this.Comport.ReadByte() == 6)
                    goto label_1;
            }
            while (TimeSpan.FromTicks(DateTime.Now.Ticks - ticks).TotalMilliseconds <= 2000.0);
            goto label_3;
        label_1:
            return true;
        label_3:
            throw new Exception("Communication timeout");
        }

        private bool Read_Bit_Ack(int BitCheck)
        {
            long ticks = DateTime.Now.Ticks;
            List<byte> byteList = new List<byte>();
            bool flag = false;
            do
            {
                if (this.Comport.BytesToRead > 0)
                {
                    int num = this.Comport.ReadByte();
                    if (num == 2)
                        flag = true;
                    if (flag)
                    {
                        byteList.Add((byte)num);
                        if (byteList.Count == 4 && num == 3)
                            goto label_5;
                    }
                }
            }
            while (TimeSpan.FromTicks(DateTime.Now.Ticks - ticks).TotalMilliseconds <= 2000.0);
            goto label_8;
        label_5:
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append((char)byteList[1]);
            stringBuilder.Append((char)byteList[2]);
            string str = Convert.ToString(Convert.ToInt32(stringBuilder.ToString(), 16), 2).PadLeft(8, '0');
            BitCheck %= 8;
            return str[7 - BitCheck].ToString() == "1";
        label_8:
            throw new Exception("Communication timeout");
        }

        public string ConvertDataFx(int buff)
        {
            int num1 = 0;
            if (buff > 8)
                num1 = buff / 16;
            int num2 = num1 <= 0 ? buff - 10 * num1 : buff - 10 * (num1 + 1);
            string str = "";
            switch (num2)
            {
                case 0:
                    str = "0";
                    break;
                case 1:
                    str = "1";
                    break;
                case 2:
                    str = "2";
                    break;
                case 3:
                    str = "3";
                    break;
                case 4:
                    str = "4";
                    break;
                case 5:
                    str = "5";
                    break;
                case 6:
                    str = "6";
                    break;
                case 7:
                    str = "7";
                    break;
                case 10:
                    str = "8";
                    break;
                case 11:
                    str = "9";
                    break;
                case 12:
                    str = "A";
                    break;
                case 13:
                    str = "B";
                    break;
                case 14:
                    str = "C";
                    break;
                case 15:
                    str = "D";
                    break;
                case 16:
                    str = "E";
                    break;
                case 17:
                    str = "F";
                    break;
            }
            return num1.ToString() + str;
        }

        private enum REGISTER_TYPE
        {
            RX,
            RY,
            RM,
            WY,
            WM,
            D,
            T,
            C,
            S,
        }

        private enum CMD
        {
            STX = 2,
            ETX = 3,
            ACK = 6,
            NCK = 21, // 0x00000015
            R = 48, // 0x00000030
            W = 49, // 0x00000031
            SET = 55, // 0x00000037
            RST = 56, // 0x00000038
        }
    }
}
