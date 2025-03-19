using DTO;
using ITM_Semiconductor;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Mitsubishi
{
    public enum DeviceCode
    {
        SM = 0x91,
        SD = 0xA9,
        X = 0x9C,
        Y = 0x9D,
        M = 0x90,
        L = 0x92,
        F = 0x93,
        V = 0x94,
        B = 0xA0,
        D = 0xA8,
        W = 0xB4,
        SB = 0xA1,
        R = 0xAF,
        ZR = 0xB0
    }
    public class Q_Enthernet
    {
       
        #region Properties
        private static MyLogger logger = new MyLogger ("PLC Q_Enthernet");
        // Socket
        private Socket client;
        private string ipAdress;
        private int portNo;
        private bool isConnected;

        //SLMP:
        private int netWorkNo;
        private int pcNo;
        private int stationNo;

        public string IpAdress { get => ipAdress; set => ipAdress = value; }
        public int PortNo { get => portNo; set => portNo = value; }
        public bool IsConnected { get => isConnected; private set => isConnected = value; }
        public int NetWorkNo { get => netWorkNo; set => netWorkNo = value; }
        public int PcNo { get => pcNo; set => pcNo = value; }
        public int StationNo { get => stationNo; set => stationNo = value; }


        #endregion

        #region Method
        public Q_Enthernet()
        {
            try
            {
                this.ipAdress = "192.168.3.39";
                this.portNo = 1026;
                this.netWorkNo = 0;
                this.pcNo = 0xFF;
                this.stationNo = 0;
                if (client == null)
                {
                    client = new Socket(SocketType.Stream, ProtocolType.Tcp);
                }
            }
            catch (Exception)
            {
            }
        }
        public Q_Enthernet(string _ipAddress, int _portNo)
        {
            try
            {
                this.ipAdress = _ipAddress;
                this.portNo = _portNo;
                this.netWorkNo = 0;
                this.pcNo = 0xFF;
                this.stationNo = 0;
                if (client == null)
                {
                    client = new Socket(SocketType.Stream, ProtocolType.Tcp);
                }
            }
            catch (Exception err)
            {
                logger.Create(String.Format("Q_Enthernet settong IP_Port Error: " + err.Message), LogLevel.Error);
            }
        }
        public Q_Enthernet(int _netWorkNo, int _pcNo, int _stationNo)
        {
            try
            {
                this.ipAdress = "192.168.3.39";
                this.portNo = 1026;
                this.netWorkNo = _netWorkNo;
                this.pcNo = _pcNo;
                this.stationNo = _stationNo;
                if (client == null)
                {
                    client = new Socket(SocketType.Stream, ProtocolType.Tcp);
                }
            }
            catch (Exception err)
            {
                logger.Create(String.Format("Q_Enthernet setting  Error: " + err.Message), LogLevel.Error);
            
            }
        }
        public void SetProperty(string _ipAdress, int _portNo, int _netWorkNo, int _pcNo, int _stationNo)
        {
            try
            {
                this.ipAdress = _ipAdress;
                this.portNo = _portNo;
                this.netWorkNo = _netWorkNo;
                this.pcNo = _pcNo;
                this.stationNo = _stationNo;
            }
            catch (Exception err)
            {
                logger.Create(String.Format("Q_Enthernet setProperty Error: " + err.Message), LogLevel.Error);
            }
        
        }
        public bool ConnectWithTimeOut(int timeOut)
        {
            bool ResultConnect = false;
            Action action = delegate
            {
                ResultConnect = Connect();
            };
            IAsyncResult asyncResult = action.BeginInvoke(null, null);
            if (asyncResult.AsyncWaitHandle.WaitOne(timeOut))
            {
                return ResultConnect;
            }

            return false;
        }
        public bool Connect()
        {
            bool result = false;
            try
            {
                if (client == null)
                {
                    client = new Socket(SocketType.Stream, ProtocolType.Tcp);
                }

                if (client.Connected)
                {
                    result = true;
                    return result;
                }

                client.Connect(ipAdress, portNo);
                if (client.Connected)
                {
                    isConnected = true;
                    result = true;
                }
            }
            catch (Exception)
            {
            }

            return result;
        }


        public bool isOpen()
        {
            if (client != null && client.Connected)
            {
                return true;
            }

            return false;
        }
        public void ShutdownDisconnect()
        {
            if (client != null)
            {
                if (client.Connected)
                {
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                client.Dispose();
                client = null;
            }
        }

        public int Disconnect()
        {
            int retValue = -1;
            try
            {
                if (client == null)
                {
                    retValue = -1;
                    return retValue;
                }
                if (!client.Connected)
                {
                    retValue = 0;
                    return retValue;
                }
                Task tskDissConnect = new Task(new Action(() =>
                {
                    client.Disconnect(false);
                }));
                tskDissConnect.Start();
                tskDissConnect.Wait(3000);
                if (!client.Connected)
                {
                    isConnected = false;
                    retValue = 0;
                    client.Shutdown(SocketShutdown.Both);
                    client = null;
                }

            }
            catch (Exception err)
            {
                logger.Create(String.Format("Q_Enthernet disconnect Error: " + err.Message), LogLevel.Error);
            }

            return retValue;
        }
        public int WriteWord(DeviceCode _devCode, int _devNumber, int _writeValue)
        {
            int retValue = -1;
            try
            {
                if (client == null)
                {
                    return retValue;
                }
                if (!client.Connected)
                {
                    return retValue;
                }
                List<byte> lstSendData = new List<byte>();
                // 1. Header : Tự động thêm vào
                // 2. Subheader: 2 byte từ byte 0 đến byte 1
                lstSendData.Add(0x50);
                lstSendData.Add(0x00);
                //3 . Access route : 2 - 6
                //3.1. Network No: 1 byte
                lstSendData.Add((byte)this.netWorkNo);
                //3.2. PC No: 1 byte
                lstSendData.Add((byte)this.pcNo);
                //3.3. Request destination module I/ O No. 2 byte
                lstSendData.Add(0xFF);
                lstSendData.Add(0x03);
                //3.4. Request destination module station no: 1 byte
                lstSendData.Add((byte)this.stationNo);

                //4.Request Data length: 2 byte từ 7-8
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);

                // 5. Monitoring Timer: 10hexa = 16 thập phân 2 byte 9-10
                // 16*250ms = 4000ms = 4s
                lstSendData.Add(0x10);
                lstSendData.Add(0x00);
                // 6. Request Data:
                //6.1 Command: 1401 (Viết xuống) 2 byte
                lstSendData.Add(0x01);
                lstSendData.Add(0x14);
                //6.2 Subcommand : 00000 ( đơn vị là thanh ghi) 2 byte
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);
                //6.3. Device Access:
                //6.3.1. Device number: 3 byte
                int intDeviceNumber = _devNumber;   // int là kiểu 32 bit nên nó tương ứng 4 byte
                for (int i = 0; i < 3; i++)   // tách 3 byte đầu ra ( Như kiểu từ bit 0 đến bit 23). Bỏ 8 bit sau cùng.
                {
                    lstSendData.Add((byte)(intDeviceNumber >> (i * 8)));
                }
                //6.3.2. Device code: 1 byte
                lstSendData.Add((byte)_devCode);

                // 6.4. Number of device point: 2 byte
                lstSendData.Add(0x01);
                lstSendData.Add(0x00);

                // 6.5. Data: 2 byte với 1 thanh ghi
                int intData = _writeValue;
                for (int i = 0; i < 2; i++)
                {
                    lstSendData.Add((byte)(intData >> (i * 8)));
                }
                // Sửa Request Data length ở byte 7 và byte 8
                int length = lstSendData.Count - 9;
                byte[] arrLength = BitConverter.GetBytes(length);
                // bit: 1 bit
                // byte: 8 bit: 0 - 255
                // short: 16 bit: -32768 đến 32767 tương ứng với 1 Word trong PLC
                // int : 32 bit từ -2.147.483.648 đến 2.147.483.647 tương đương 2 word trong PLC - double word: 4 byte.

                lstSendData[7] = arrLength[0];
                lstSendData[8] = arrLength[1];


                // Gửi đi:
                client.Send(lstSendData.ToArray());

                // Nhận về:
                byte[] arrRcv = new byte[512];
                client.Receive(arrRcv);
                List<byte> lstRcv = new List<byte>();
                lstRcv.AddRange(arrRcv);
                // Kiểm tra dữ liệu nhận về xem có đúng khung MC Protocol hay không ?

                // 1. Subheader: 2 byte D000:
                if (lstRcv[0] != 0xD0 || lstRcv[1] != 0x00)
                {
                    throw new Exception("Subheader Error");
                   
                }
                lstRcv.RemoveRange(0, 2);

                // 2. Access Route: 5 byte 
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0xFF || lstRcv[2] != 0xFF || lstRcv[3] != 0x03 || lstRcv[4] != 0x00)
                {
                    throw new Exception("Access Route Error");
                   
                }
                lstRcv.RemoveRange(0, 5);

                // 3. Response Data Length: 2 byte
                short resLength = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
                if (resLength < 2)
                {
                    throw new Exception("Response Data Length Error");
                   
                }
                lstRcv.RemoveRange(0, 2);

                // 4.End Code: 2 byte
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0x00)
                {
                    throw new Exception(string.Format("Get Error with error code: {0}{1}", lstRcv[0], lstRcv[1]));
                    
                }
                lstRcv.RemoveRange(0, 2);
                retValue = 0;
            }
            catch (Exception err)
            {
                logger.Create(String.Format("Q_Enthernet writework Error: " + err.Message), LogLevel.Error);
            }
            return retValue;
        }
        public int ReadWord(DeviceCode _devCode, int _devNumber, out int _value)
        {
            _value = 0;
            int retValue = -1;
            try
            {
                if (client == null)
                {
                    return retValue;
                }
                if (!client.Connected)
                {
                    return retValue;
                }
                List<byte> lstSendData = new List<byte>();

                // 1. Header : Tự động thêm vào

                // 2. Subheader: 2 byte từ byte 0 đến byte 1
                lstSendData.Add(0x50);
                lstSendData.Add(0x00);

                //3 . Access route : 2 - 6
                //3.1. Network No: 1 byte
                lstSendData.Add((byte)this.netWorkNo);
                //3.2. PC No: 1 byte
                lstSendData.Add((byte)this.pcNo);
                //3.3. Request destination module I/ O No. 2 byte
                lstSendData.Add(0xFF);
                lstSendData.Add(0x03);
                //3.4. Request destination module station no: 1 byte
                lstSendData.Add((byte)this.stationNo);

                //4.Request Data length: 2 byte từ 7-8
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);

                // 5. Monitoring Timer: 10hexa = 16 thập phân 2 byte 9-10
                // 16*250ms = 4000ms = 4s
                lstSendData.Add(0x10);
                lstSendData.Add(0x00);
                // 6. Request Data:
                //6.1 Command: 0401 (Đọc lên) 2 byte
                lstSendData.Add(0x01);
                lstSendData.Add(0x04);
                //6.2 Subcommand : 0000 ( đơn vị là thanh ghi) 2 byte
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);
                //6.3. Device Access:
                //6.3.1. Device number: 3 byte
                int intDeviceNumber = _devNumber;
                for (int i = 0; i < 3; i++)
                {
                    lstSendData.Add((byte)(intDeviceNumber >> (i * 8)));
                }
                //6.3.2. Device code: 1 byte
                lstSendData.Add((byte)_devCode);

                // 6.4. Number of device point: 2 byte
                lstSendData.Add(0x01);   // 1 thanh ghi 16 bit
                lstSendData.Add(0x00);

                //// 6.5. Data: 2 byte với 1 thanh ghi
                //int intData = 1234;
                //for (int i = 0; i < 2; i++)
                //{
                //    lstSendData.Add((byte)(intData >> (i * 8)));
                //}
                // Sửa Request Data length ở byte 7 và byte 8
                int length = lstSendData.Count - 9;
                byte[] arrLength = BitConverter.GetBytes(length);  // Chỗ Request Data length có 2 byte nên khi BitConverter.GetBytes vào mảng byte thì nó chỉ chuyển đổi thành 2 byte được thôi.
                // bit: 1 bit
                // byte: 8 bit: 0 - 255
                // short: 16 bit: -32768 đến 32767 tương ứng với 1 Word trong PLC
                // int : 32 bit từ -2.147.483.648 đến 2.147.483.647 tương đương 2 word trong PLC - double word: 4 byte.

                lstSendData[7] = arrLength[0];
                lstSendData[8] = arrLength[1];

                // Gửi đi:
                client.Send(lstSendData.ToArray());

                // Nhận về:
                byte[] arrRcv = new byte[512];
                client.Receive(arrRcv);
                List<byte> lstRcv = new List<byte>();
                lstRcv.AddRange(arrRcv);
                // Kiểm tra dữ liệu nhận về xem có đúng khung MC Protocol hay không ?

                // 1. Subheader: 2 byte D000:
                if (lstRcv[0] != 0xD0 || lstRcv[1] != 0x00)
                {
                    throw new Exception("Subheader Error");
                   
                }
                lstRcv.RemoveRange(0, 2);

                // 2. Access Route: 5 byte 
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0xFF || lstRcv[2] != 0xFF || lstRcv[3] != 0x03 || lstRcv[4] != 0x00)
                {
                    throw new Exception("Access Route Error");
                    
                }
                lstRcv.RemoveRange(0, 5);

                // 3. Response Data Length: 2 byte
                short resLength = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
                if (resLength < 2)
                {
                    throw new Exception("Response Data Length Error");
                   
                }
                lstRcv.RemoveRange(0, 2);

                // 4.End Code: 2 byte
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0x00)
                {
                    throw new Exception(string.Format("Get Error with error code: {0}{1}", lstRcv[0], lstRcv[1]));
                   
                }
                lstRcv.RemoveRange(0, 2);
                // 5. Lấy giá trị về:
                _value = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
                retValue = 0;
            }
            catch (Exception err)
            {
                logger.Create(String.Format("Q_Enthernet ReadWork Error: " + err.Message), LogLevel.Error);
            }

            return retValue;
        }
        public int WriteDoubleWord(DeviceCode _devCode, int _devNumber, int _writeValue)
        {
            int retValue = -1;
            try
            {
                if (client == null)
                {
                    return retValue;
                }
                if (!client.Connected)
                {
                    return retValue;
                }
                List<byte> lstSendData = new List<byte>();

                // 1. Header : Tự động thêm vào

                // 2. Subheader: 2 byte từ byte 0 đến byte 1
                lstSendData.Add(0x50);
                lstSendData.Add(0x00);

                //3 . Access route : 2 - 6
                //3.1. Network No: 1 byte
                lstSendData.Add((byte)this.netWorkNo);
                //3.2. PC No: 1 byte
                lstSendData.Add((byte)this.pcNo);
                //3.3. Request destination module I/ O No. 2 byte
                lstSendData.Add(0xFF);
                lstSendData.Add(0x03);
                //3.4. Request destination module station no: 1 byte
                lstSendData.Add((byte)this.stationNo);

                //4.Request Data length: 2 byte từ 7-8
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);

                // 5. Monitoring Timer: 10hexa = 16 thập phân 2 byte 9-10
                // 16*250ms = 4000ms = 4s
                lstSendData.Add(0x10);
                lstSendData.Add(0x00);
                // 6. Request Data:
                //6.1 Command: 1401 (Viết xuống) 2 byte
                lstSendData.Add(0x01);
                lstSendData.Add(0x14);
                //6.2 Subcommand : 0000 ( đơn vị là thanh ghi) 2 byte
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);
                //6.3. Device Access:
                //6.3.1. Device number: 3 byte
                int intDeviceNumber = _devNumber;
                for (int i = 0; i < 3; i++)
                {
                    lstSendData.Add((byte)(intDeviceNumber >> (i * 8)));
                }
                //6.3.2. Device code: 1 byte
                lstSendData.Add((byte)_devCode);

                // 6.4. Number of device point: 4 byte (Ghi 32 bit)
                lstSendData.Add(0x02);
                lstSendData.Add(0x00);

                // 6.5. Data: 4 byte với 1 thanh ghi 32 bit
                int intData = _writeValue;
                for (int i = 0; i < 4; i++)
                {
                    lstSendData.Add((byte)(intData >> (i * 8)));
                }
                // Sửa Request Data length ở byte 7 và byte 8
                int length = lstSendData.Count - 9;
                byte[] arrLength = BitConverter.GetBytes(length);
                // bit: 1 bit
                // byte: 8 bit: 0 - 255
                // short: 16 bit: -32768 đến 32767 tương ứng với 1 Word trong PLC
                // int : 32 bit từ -2.147.483.648 đến 2.147.483.647 tương đương 2 word trong PLC - double word: 4 byte.

                lstSendData[7] = arrLength[0];
                lstSendData[8] = arrLength[1];

                // Gửi đi:
                client.Send(lstSendData.ToArray());

                // Nhận về:
                byte[] arrRcv = new byte[512];
                client.Receive(arrRcv);
                List<byte> lstRcv = new List<byte>();
                lstRcv.AddRange(arrRcv);
                // Kiểm tra dữ liệu nhận về xem có đúng khung MC Protocol hay không ?

                // 1. Subheader: 2 byte D000:
                if (lstRcv[0] != 0xD0 || lstRcv[1] != 0x00)
                {
                    throw new Exception("Subheader Error");
                    
                }
                lstRcv.RemoveRange(0, 2);

                // 2. Access Route: 5 byte 
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0xFF || lstRcv[2] != 0xFF || lstRcv[3] != 0x03 || lstRcv[4] != 0x00)
                {
                    throw new Exception("Access Route Error");
                   
                }
                lstRcv.RemoveRange(0, 5);

                // 3. Response Data Length: 2 byte
                short resLength = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
                if (resLength < 2)
                {
                    throw new Exception("Response Data Length Error");
                    
                }
                lstRcv.RemoveRange(0, 2);

                // 4.End Code: 2 byte
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0x00)
                {
                    throw new Exception(string.Format("Get Error with error code: {0}{1}", lstRcv[0], lstRcv[1]));
                   
                }
                lstRcv.RemoveRange(0, 2);
                retValue = 0;
            }
            catch (Exception err)
            {
                logger.Create(String.Format("Q_Enthernet WriteDoubleWork Error: " + err.Message), LogLevel.Error);
            }

            return retValue;
        }
        public int ReadDoubleWord(DeviceCode _devCode, int _devNumber, out int _value)
        {
            int retValue = -1;
            _value = 0;
            try
            {
                if (client == null)
                {
                    return retValue;
                }
                if (!client.Connected)
                {
                    return retValue;
                }
                List<byte> lstSendData = new List<byte>();

                // 1. Header : Tự động thêm vào

                // 2. Subheader: 2 byte từ byte 0 đến byte 1
                lstSendData.Add(0x50);
                lstSendData.Add(0x00);

                //3 . Access route : 2 - 6
                //3.1. Network No: 1 byte
                lstSendData.Add((byte)this.netWorkNo);
                //3.2. PC No: 1 byte
                lstSendData.Add((byte)this.pcNo);
                //3.3. Request destination module I/ O No. 2 byte
                lstSendData.Add(0xFF);
                lstSendData.Add(0x03);
                //3.4. Request destination module station no: 1 byte
                lstSendData.Add((byte)this.stationNo);

                //4.Request Data length: 2 byte từ 7-8
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);

                // 5. Monitoring Timer: 10hexa = 16 thập phân 2 byte 9-10
                // 16*250ms = 4000ms = 4s
                lstSendData.Add(0x10);
                lstSendData.Add(0x00);
                // 6. Request Data:
                //6.1 Command: 0401 (Đọc lên) 2 byte
                lstSendData.Add(0x01);
                lstSendData.Add(0x04);
                //6.2 Subcommand : 0000 ( đơn vị là thanh ghi) 2 byte
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);
                //6.3. Device Access:
                //6.3.1. Device number: 3 byte
                int intDeviceNumber = _devNumber;
                for (int i = 0; i < 3; i++)
                {
                    lstSendData.Add((byte)(intDeviceNumber >> (i * 8)));
                }
                //6.3.2. Device code: 1 byte
                lstSendData.Add((byte)_devCode);

                // 6.4. Number of device point: 4 byte bằng 32 bit
                lstSendData.Add(0x02);  // 2 thanh ghi bằng 32 bit
                lstSendData.Add(0x00);

                //// 6.5. Data: 2 byte với 1 thanh ghi
                //int intData = 1234;
                //for (int i = 0; i < 2; i++)
                //{
                //    lstSendData.Add((byte)(intData >> (i * 8)));
                //}
                // Sửa Request Data length ở byte 7 và byte 8
                int length = lstSendData.Count - 9;
                byte[] arrLength = BitConverter.GetBytes(length);
                // bit: 1 bit
                // byte: 8 bit: 0 - 255
                // short: 16 bit: -32768 đến 32767 tương ứng với 1 Word trong PLC
                // int : 32 bit từ -2.147.483.648 đến 2.147.483.647 tương đương 2 word trong PLC - double word: 4 byte.

                lstSendData[7] = arrLength[0];
                lstSendData[8] = arrLength[1];

                // Gửi đi:
                client.Send(lstSendData.ToArray());

                // Nhận về:
                byte[] arrRcv = new byte[512];
                client.Receive(arrRcv);
                List<byte> lstRcv = new List<byte>();
                lstRcv.AddRange(arrRcv);
                // Kiểm tra dữ liệu nhận về xem có đúng khung MC Protocol hay không ?

                // 1. Subheader: 2 byte D000:
                if (lstRcv[0] != 0xD0 || lstRcv[1] != 0x00)
                {
                    throw new Exception("Subheader Error");
                    
                }
                lstRcv.RemoveRange(0, 2);

                // 2. Access Route: 5 byte 
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0xFF || lstRcv[2] != 0xFF || lstRcv[3] != 0x03 || lstRcv[4] != 0x00)
                {
                    throw new Exception("Access Route Error");
                   
                }
                lstRcv.RemoveRange(0, 5);

                // 3. Response Data Length: 2 byte
                short resLength = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
                if (resLength < 2)
                {
                    throw new Exception("Response Data Length Error");
                    
                }
                lstRcv.RemoveRange(0, 2);

                // 4.End Code: 2 byte
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0x00)
                {
                    throw new Exception(string.Format("Get Error with error code: {0}{1}", lstRcv[0], lstRcv[1]));
                    
                }
                lstRcv.RemoveRange(0, 2);
                // 5. Lấy giá trị về:
                _value = BitConverter.ToInt32(new byte[] { lstRcv[0], lstRcv[1], lstRcv[2], lstRcv[3] }, 0);  // 32 bit bằng 4 byte nên phải lấy 1 mảng 4 byte.
                retValue = 0;
            }
            catch (Exception err)
            {
                logger.Create(String.Format("Q_Enthernet ReadDoubleWork Error: " + err.Message), LogLevel.Error);
            }
            return retValue;
        }
        public int ReadFloat(DeviceCode _devCode, int _devNumber, out float _value)
        {
            _value = 0;
            int retValue = -1;
            try
            {
                if (client == null)
                {
                    return retValue;
                }
                if (!client.Connected)
                {
                    return retValue;
                }
                List<byte> lstSendData = new List<byte>();

                // 1. Header : Tự động thêm vào

                // 2. Subheader: 2 byte từ byte 0 đến byte 1
                lstSendData.Add(0x50);
                lstSendData.Add(0x00);

                //3 . Access route : 2 - 6
                //3.1. Network No: 1 byte
                lstSendData.Add((byte)this.netWorkNo);
                //3.2. PC No: 1 byte
                lstSendData.Add((byte)this.pcNo);
                //3.3. Request destination module I/ O No. 2 byte
                lstSendData.Add(0xFF);
                lstSendData.Add(0x03);
                //3.4. Request destination module station no: 1 byte
                lstSendData.Add((byte)this.stationNo);

                //4.Request Data length: 2 byte từ 7-8
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);

                // 5. Monitoring Timer: 10hexa = 16 thập phân 2 byte 9-10
                // 16*250ms = 4000ms = 4s
                lstSendData.Add(0x10);
                lstSendData.Add(0x00);
                // 6. Request Data:
                //6.1 Command: 0401 (Đọc lên) 2 byte
                lstSendData.Add(0x01);
                lstSendData.Add(0x04);
                //6.2 Subcommand : 0000 ( đơn vị là thanh ghi) 2 byte
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);
                //6.3. Device Access:
                //6.3.1. Device number: 3 byte
                int intDeviceNumber = _devNumber;
                for (int i = 0; i < 3; i++)
                {
                    lstSendData.Add((byte)(intDeviceNumber >> (i * 8)));
                }
                //6.3.2. Device code: 1 byte
                lstSendData.Add((byte)_devCode);

                // 6.4. Number of device point: 4 byte bằng 32 bit
                lstSendData.Add(0x02);  // 2 thanh ghi bằng 32 bit
                lstSendData.Add(0x00);

                //// 6.5. Data: 2 byte với 1 thanh ghi
                //int intData = 1234;
                //for (int i = 0; i < 2; i++)
                //{
                //    lstSendData.Add((byte)(intData >> (i * 8)));
                //}
                // Sửa Request Data length ở byte 7 và byte 8
                int length = lstSendData.Count - 9;
                byte[] arrLength = BitConverter.GetBytes(length);
                // bit: 1 bit
                // byte: 8 bit: 0 - 255
                // short: 16 bit: -32768 đến 32767 tương ứng với 1 Word trong PLC
                // int : 32 bit từ -2.147.483.648 đến 2.147.483.647 tương đương 2 word trong PLC - double word: 4 byte.

                lstSendData[7] = arrLength[0];
                lstSendData[8] = arrLength[1];

                // Gửi đi:
                client.Send(lstSendData.ToArray());

                // Nhận về:
                byte[] arrRcv = new byte[512];
                client.Receive(arrRcv);
                List<byte> lstRcv = new List<byte>();
                lstRcv.AddRange(arrRcv);
                // Kiểm tra dữ liệu nhận về xem có đúng khung MC Protocol hay không ?

                // 1. Subheader: 2 byte D000:
                if (lstRcv[0] != 0xD0 || lstRcv[1] != 0x00)
                {
                    throw new Exception("Subheader Error");
                    
                }
                lstRcv.RemoveRange(0, 2);

                // 2. Access Route: 5 byte 
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0xFF || lstRcv[2] != 0xFF || lstRcv[3] != 0x03 || lstRcv[4] != 0x00)
                {
                    throw new Exception("Access Route Error");
                    
                }
                lstRcv.RemoveRange(0, 5);

                // 3. Response Data Length: 2 byte
                short resLength = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
                if (resLength < 2)
                {
                    throw new Exception("Response Data Length Error");
                    
                }
                lstRcv.RemoveRange(0, 2);

                // 4.End Code: 2 byte
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0x00)
                {
                    throw new Exception(string.Format("Get Error with error code: {0}{1}", lstRcv[0], lstRcv[1]));
                   
                }
                lstRcv.RemoveRange(0, 2);
                // 5. Lấy giá trị về:
                _value = BitConverter.ToSingle(new byte[] { lstRcv[0], lstRcv[1], lstRcv[2], lstRcv[3] }, 0);  // 32 bit bằng 4 byte nên phải lấy 1 mảng 4 byte.
                retValue = 0;
            }
            catch (Exception err)
            {
                logger.Create(String.Format("Q_Enthernet ReadFloat Error: " + err.Message), LogLevel.Error);
            }
            return retValue;
        }
        public int WriteBit(DeviceCode _devCode, int _devNumber, bool _value)
        {
            int retValue = -1;
            try
            {
                if (client == null)
                {
                    return retValue;
                }
                if (!client.Connected)
                {
                    return retValue;
                }
                List<byte> lstSendData = new List<byte>();

                // 1. Header : Tự động thêm vào

                // 2. Subheader: 2 byte từ byte 0 đến byte 1
                lstSendData.Add(0x50);
                lstSendData.Add(0x00);

                //3 . Access route : 2 - 6
                //3.1. Network No: 1 byte
                lstSendData.Add((byte)this.netWorkNo);
                //3.2. PC No: 1 byte
                lstSendData.Add((byte)this.pcNo);
                //3.3. Request destination module I/ O No. 2 byte
                lstSendData.Add(0xFF);
                lstSendData.Add(0x03);
                //3.4. Request destination module station no: 1 byte
                lstSendData.Add((byte)this.stationNo);

                //4.Request Data length: 2 byte từ 7-8
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);

                // 5. Monitoring Timer: 10hexa = 16 thập phân 2 byte 9-10
                // 16*250ms = 4000ms = 4s
                lstSendData.Add(0x10);
                lstSendData.Add(0x00);
                // 6. Request Data:
                //6.1 Command: 1401 (Viết xuống) 2 byte
                lstSendData.Add(0x01);
                lstSendData.Add(0x14);
                //6.2 Subcommand : 0001 ( đơn vị là bit) 2 byte
                lstSendData.Add(0x01);
                lstSendData.Add(0x00);
                //6.3. Device Access:
                //6.3.1. Device number: 3 byte
                int intDeviceNumber = _devNumber;
                for (int i = 0; i < 3; i++)
                {
                    lstSendData.Add((byte)(intDeviceNumber >> (i * 8)));
                }
                //6.3.2. Device code: 1 byte
                lstSendData.Add((byte)_devCode);

                // 6.4. Number of device point: 2 byte
                lstSendData.Add(0x01);  // Số lượng bit truy cập.
                lstSendData.Add(0x00);

                // 6.5. Data: 4 byte với 1 thanh ghi 32 bit
                if (_value)
                {
                    lstSendData.Add(0x10);//ON
                }
                else
                {
                    lstSendData.Add(0x00);//OFF
                }

                //lstSendData.Add(0x00);//OFF
                // Sửa Request Data length ở byte 7 và byte 8
                int length = lstSendData.Count - 9;
                byte[] arrLength = BitConverter.GetBytes(length);
                // bit: 1 bit
                // byte: 8 bit: 0 - 255
                // short: 16 bit: -32768 đến 32767 tương ứng với 1 Word trong PLC
                // int : 32 bit từ -2.147.483.648 đến 2.147.483.647 tương đương 2 word trong PLC - double word: 4 byte.

                lstSendData[7] = arrLength[0];
                lstSendData[8] = arrLength[1];

                // Gửi đi:
                client.Send(lstSendData.ToArray());

                // Nhận về:
                byte[] arrRcv = new byte[512];
                client.Receive(arrRcv);
                List<byte> lstRcv = new List<byte>();
                lstRcv.AddRange(arrRcv);
                // Kiểm tra dữ liệu nhận về xem có đúng khung MC Protocol hay không ?

                // 1. Subheader: 2 byte D000:
                if (lstRcv[0] != 0xD0 || lstRcv[1] != 0x00)
                {
                    throw new Exception("Subheader Error");
                    
                }
                lstRcv.RemoveRange(0, 2);

                // 2. Access Route: 5 byte 
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0xFF || lstRcv[2] != 0xFF || lstRcv[3] != 0x03 || lstRcv[4] != 0x00)
                {
                    throw new Exception("Access Route Error");
                   
                }
                lstRcv.RemoveRange(0, 5);

                // 3. Response Data Length: 2 byte
                short resLength = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
                if (resLength < 2)
                {
                    throw new Exception("Response Data Length Error");
                    
                }
                lstRcv.RemoveRange(0, 2);

                // 4.End Code: 2 byte
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0x00)
                {
                    throw new Exception(string.Format("Get Error with error code: {0}{1}", lstRcv[0], lstRcv[1]));
                   
                }
                lstRcv.RemoveRange(0, 2);
                retValue = 0;
            }
            catch (Exception err)
            {
                logger.Create(String.Format("Q_Enthernet WriteBit Error: " + err.Message), LogLevel.Error);
            }

            return retValue;
        }
        public int ReadBit(DeviceCode _devCode, int _devNumber, out bool _value)
        {
            _value = false;
            int retValue = -1;
            try
            {
                if (client == null)
                {
                    return retValue;
                }
                if (!client.Connected)
                {
                    return retValue;
                }
                List<byte> lstSendData = new List<byte>();

                // 1. Header : Tự động thêm vào

                // 2. Subheader: 2 byte từ byte 0 đến byte 1
                lstSendData.Add(0x50);
                lstSendData.Add(0x00);

                //3 . Access route : 2 - 6
                //3.1. Network No: 1 byte
                lstSendData.Add((byte)this.netWorkNo);
                //3.2. PC No: 1 byte
                lstSendData.Add((byte)this.pcNo);
                //3.3. Request destination module I/ O No. 2 byte
                lstSendData.Add(0xFF);
                lstSendData.Add(0x03);
                //3.4. Request destination module station no: 1 byte
                lstSendData.Add((byte)this.stationNo);

                //4.Request Data length: 2 byte từ 7-8
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);

                // 5. Monitoring Timer: 10hexa = 16 thập phân 2 byte 9-10
                // 16*250ms = 4000ms = 4s
                lstSendData.Add(0x10);
                lstSendData.Add(0x00);
                // 6. Request Data:
                //6.1 Command: 0401 (Đọc giá trị của bit) 2 byte
                lstSendData.Add(0x01);
                lstSendData.Add(0x04);
                //6.2 Subcommand : 0001 ( đơn vị là bit) 2 byte
                lstSendData.Add(0x01);
                lstSendData.Add(0x00);
                //6.3. Device Access:
                //6.3.1. Device number: 3 byte
                int intDeviceNumber = _devNumber;
                for (int i = 0; i < 3; i++)
                {
                    lstSendData.Add((byte)(intDeviceNumber >> (i * 8)));
                }
                //6.3.2. Device code: 1 byte : M, X, Y, L , B, SB
                lstSendData.Add((byte)_devCode);

                // 6.4. Number of device point: 4 byte bằng 32 bit: Muốn đọc bao nhiêu thằng

                lstSendData.Add(0x01);  // 1 bit
                lstSendData.Add(0x00);

                //// 6.5. Data: 2 byte với 1 thanh ghi
                //int intData = 1234;
                //for (int i = 0; i < 2; i++)
                //{
                //    lstSendData.Add((byte)(intData >> (i * 8)));
                //}
                // Sửa Request Data length ở byte 7 và byte 8
                int length = lstSendData.Count - 9;
                byte[] arrLength = BitConverter.GetBytes(length);
                // bit: 1 bit
                // byte: 8 bit: 0 - 255
                // short: 16 bit: -32768 đến 32767 tương ứng với 1 Word trong PLC
                // int : 32 bit từ -2.147.483.648 đến 2.147.483.647 tương đương 2 word trong PLC - double word: 4 byte.

                lstSendData[7] = arrLength[0];
                lstSendData[8] = arrLength[1];

                // Gửi đi:
                client.Send(lstSendData.ToArray());

                // Nhận về:
                byte[] arrRcv = new byte[512];
                client.Receive(arrRcv);
                List<byte> lstRcv = new List<byte>();
                lstRcv.AddRange(arrRcv);
                // Kiểm tra dữ liệu nhận về xem có đúng khung MC Protocol hay không ?

                // 1. Subheader: 2 byte D000:
                if (lstRcv[0] != 0xD0 || lstRcv[1] != 0x00)
                {
                    throw new Exception("Subheader Error");
                   
                }
                lstRcv.RemoveRange(0, 2);

                // 2. Access Route: 5 byte 
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0xFF || lstRcv[2] != 0xFF || lstRcv[3] != 0x03 || lstRcv[4] != 0x00)
                {
                    throw new Exception("Access Route Error");
                   
                }
                lstRcv.RemoveRange(0, 5);

                // 3. Response Data Length: 2 byte
                // Giá trị
                short resLength = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1], lstRcv[2] }, 0);
                if (resLength < 3)  // End code 2 byte cộng Response Data 1 byte
                {
                    throw new Exception("Response Data Length Error");
                   
                }
                lstRcv.RemoveRange(0, 2);

                // 4.End Code: 2 byte
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0x00)
                {
                    throw new Exception(string.Format("Get Error with error code: {0}{1}", lstRcv[0], lstRcv[1]));
                    
                }
                lstRcv.RemoveRange(0, 2);

                // 5. Response Data:

                // lstRcv[0] = 0x00; M1 và M2 đều OFF
                // lstRcv[0] = 0x01; M1 OFF và M2 ON
                // lstRcv[0] = 0x10; M1 ON và M2 OFF
                // lstRcv[0] = 0x11; M1 ON và M2 ON
                if (lstRcv[0] == 0x00)
                {
                    _value = false;
                } 
                else
                {
                    _value = true;
                }
            }
            catch (Exception err)
            {
                logger.Create(String.Format("Q_Enthernet Readbit Error: " + err.Message), LogLevel.Error);
            }

            return retValue;
        }
    

        public int ReadMultiBits(DeviceCode _devCode, int _devNumber, int _count, out List<bool> _lstValue)
        {
            _lstValue = new List<bool>();
            int retValue = -1;
            try
            {
                if (this.client == null)
                {
                    return retValue;
                }
                if (!client.Connected)
                {
                    return retValue;
                }
                List<byte> lstSendData = new List<byte>();

                // 1. Header : Tự động thêm vào

                // 2. Subheader: 2 byte từ byte 0 đến byte 1
                lstSendData.Add(0x50);
                lstSendData.Add(0x00);

                //3 . Access route : 2 - 6
                //3.1. Network No: 1 byte
                lstSendData.Add((byte)this.netWorkNo);

                //3.2. PC No: 1 byte
                lstSendData.Add((byte)this.pcNo);
                //3.3. Request destination module I/ O No. 2 byte
                lstSendData.Add(0xFF);
                lstSendData.Add(0x03);
                //3.4. Request destination module station no: 1 byte
                lstSendData.Add((byte)this.stationNo);

                //4.Request Data length: 2 byte từ 7
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);

                // 5. Monitoring Timer: 10hexa = 16 thập phân 2 byte 9-10
                // 16*250ms = 4000ms = 4s
                lstSendData.Add(0x10);
                lstSendData.Add(0x00);
                // 6. Request Data:
                //6.1 Command: 0401 (Đọc giá trị của bit) 2 byte
                lstSendData.Add(0x01);
                lstSendData.Add(0x04);
                //6.2 Subcommand : 0001 ( đơn vị là bit) 2 byte
                lstSendData.Add(0x01);
                lstSendData.Add(0x00);
                //6.3. Device Access:
                //6.3.1. Device number: 3 byte
                int intDeviceNumber = _devNumber;
                for (int i = 0; i < 3; i++)
                {
                    lstSendData.Add((byte)(intDeviceNumber >> (i * 8)));
                }
                //6.3.2. Device code: 1 byte : M, X, Y, L , B, SB
                lstSendData.Add((byte)_devCode);

                // 6.4. Number of device point: 4 byte bằng 32 bit: Muốn đọc bao nhiêu thằng

                lstSendData.AddRange(BitConverter.GetBytes((short)_count));


                // Sửa Request Data length ở byte 7 và byte 8
                int length = lstSendData.Count - 9;
                byte[] arrLength = BitConverter.GetBytes(length);
                // bit: 1 bit
                // byte: 8 bit: 0 - 255
                // short: 16 bit: -32768 đến 32767 tương ứng với 1 Word trong PLC
                // int : 32 bit từ -2.147.483.648 đến 2.147.483.647 tương đương 2 word trong PLC - double word: 4 byte.

                lstSendData[7] = arrLength[0];
                lstSendData[8] = arrLength[1];

                // Gửi đi:
                client.Send(lstSendData.ToArray());

                // Nhận về:
                byte[] arrRcv = new byte[512];
                client.Receive(arrRcv);
                List<byte> lstRcv = new List<byte>();
                lstRcv.AddRange(arrRcv);
                // Kiểm tra dữ liệu nhận về xem có đúng khung MC Protocol hay không ?

                // 1. Subheader: 2 byte D000:
                if (lstRcv[0] != 0xD0 || lstRcv[1] != 0x00)
                {
                    throw new Exception("Subheader Error");

                }
                lstRcv.RemoveRange(0, 2);

                // 2. Access Route: 5 byte 
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0xFF || lstRcv[2] != 0xFF || lstRcv[3] != 0x03 || lstRcv[4] != 0x00)
                {
                    throw new Exception("Access Route Error");

                }
                lstRcv.RemoveRange(0, 5);

                // 3. Response Data Length: 2 byte
                // Giá trị
                short resLength = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1], lstRcv[2] }, 0);
                if (resLength < 3)  // End code 2 byte cộng Response Data 1 byte
                {
                    throw new Exception("Response Data Length Error");

                }
                lstRcv.RemoveRange(0, 2);

                // 4.End Code: 2 byte
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0x00)
                {
                    throw new Exception(string.Format("Get Error with error code: {0}{1}", lstRcv[0], lstRcv[1]));

                }
                lstRcv.RemoveRange(0, 2);

                // 5. Response Data:

                // lstRcv[0] = 0x00; M1 và M2 đều OFF
                // lstRcv[0] = 0x01; M1 OFF và M2 ON
                // lstRcv[0] = 0x10; M1 ON và M2 OFF
                // lstRcv[0] = 0x11; M1 ON và M2 ON
                List<bool> lstResult = new List<bool>();
                int bytes = 0;
                if (_count % 2 == 0)
                {
                    bytes = _count / 2;
                }
                else
                {
                    bytes = _count / 2 + 1;
                }
                for (int i = 0; i < bytes; i++)
                {
                    var temp1 = (byte)((lstRcv[i] >> 2) & 0x0F);
                    if (temp1 == 0)
                    {
                        lstResult.Add(false);
                    }
                    else
                        lstResult.Add(true);

                    var temp2 = (byte)((lstRcv[i] >> 0) & 0x0F);
                    if (temp2 == 0)
                    {
                        lstResult.Add(false);
                    }
                    else
                        lstResult.Add(true);
                }
                for (int i = 0; i < _count; i++)
                {
                    _lstValue.Add(lstResult[i]);
                }
            }
            catch (Exception err)
            {
                logger.Create(String.Format("Q_Enthernet ReadMutiBit Error: " + err.Message), LogLevel.Error);
            }

            return retValue;
        }
        public int ReadMultiWord(DeviceCode _devCode, int _devNumber, int _count, out List<short> _value)
        {
            _value = new List<short>();
            int retValue = -1;
            try
            {
                if (client == null)
                {
                    return retValue;
                }
                if (!client.Connected)
                {
                    return retValue;
                }
                List<byte> lstSendData = new List<byte>();

                // 1. Header : Tự động thêm vào

                // 2. Subheader: 2 byte từ byte 0 đến byte 1
                lstSendData.Add(0x50);
                lstSendData.Add(0x00);

                //3 . Access route : 2 - 6
                //3.1. Network No: 1 byte
                lstSendData.Add((byte)this.netWorkNo);
                //3.2. PC No: 1 byte
                lstSendData.Add((byte)this.pcNo);
                //3.3. Request destination module I/ O No. 2 byte
                lstSendData.Add(0xFF);
                lstSendData.Add(0x03);
                //3.4. Request destination module station no: 1 byte
                lstSendData.Add((byte)this.stationNo);

                //4.Request Data length: 2 byte từ 7-8
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);

                // 5. Monitoring Timer: 10hexa = 16 thập phân 2 byte 9-10
                // 16*250ms = 4000ms = 4s
                lstSendData.Add(0x10);
                lstSendData.Add(0x00);
                // 6. Request Data:
                //6.1 Command: 0401 (Đọc lên) 2 byte
                lstSendData.Add(0x01);
                lstSendData.Add(0x04);
                //6.2 Subcommand : 0000 ( đơn vị là thanh ghi) 2 byte
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);
                //6.3. Device Access:
                //6.3.1. Device number: 3 byte
                int intDeviceNumber = _devNumber;
                for (int i = 0; i < 3; i++)
                {
                    lstSendData.Add((byte)(intDeviceNumber >> (i * 8)));
                }
                //6.3.2. Device code: 1 byte
                lstSendData.Add((byte)_devCode);

                // 6.4. Number of device point: 2 byte
                lstSendData.AddRange(BitConverter.GetBytes((short)_count));

                //// 6.5. Data: 2 byte với 1 thanh ghi
                //int intData = 1234;
                //for (int i = 0; i < 2; i++)
                //{
                //    lstSendData.Add((byte)(intData >> (i * 8)));
                //}
                // Sửa Request Data length ở byte 7 và byte 8
                int length = lstSendData.Count - 9;
                byte[] arrLength = BitConverter.GetBytes(length);  // Chỗ Request Data length có 2 byte nên khi BitConverter.GetBytes vào mảng byte thì nó chỉ chuyển đổi thành 2 byte được thôi.
                // bit: 1 bit
                // byte: 8 bit: 0 - 255
                // short: 16 bit: -32768 đến 32767 tương ứng với 1 Word trong PLC
                // int : 32 bit từ -2.147.483.648 đến 2.147.483.647 tương đương 2 word trong PLC - double word: 4 byte.

                lstSendData[7] = arrLength[0];
                lstSendData[8] = arrLength[1];

                // Gửi đi:
                client.Send(lstSendData.ToArray());

                // Nhận về:
                byte[] arrRcv = new byte[5000];
                client.Receive(arrRcv);
                List<byte> lstRcv = new List<byte>();
                lstRcv.AddRange(arrRcv);
                // Kiểm tra dữ liệu nhận về xem có đúng khung MC Protocol hay không ?

                // 1. Subheader: 2 byte D000:
                if (lstRcv[0] != 0xD0 || lstRcv[1] != 0x00)
                {
                    throw new Exception("Subheader Error");
                    
                }
                lstRcv.RemoveRange(0, 2);

                // 2. Access Route: 5 byte 
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0xFF || lstRcv[2] != 0xFF || lstRcv[3] != 0x03 || lstRcv[4] != 0x00)
                {
                    throw new Exception("Access Route Error");
                   
                }
                lstRcv.RemoveRange(0, 5);

                // 3. Response Data Length: 2 byte
                short resLength = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
                if (resLength < 2)
                {
                    throw new Exception("Response Data Length Error");
                    
                }
                lstRcv.RemoveRange(0, 2);

                // 4.End Code: 2 byte
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0x00)
                {
                    throw new Exception(string.Format("Get Error with error code: {0}{1}", lstRcv[0], lstRcv[1]));
                   
                }
                lstRcv.RemoveRange(0, 2);
                // 5. Lấy giá trị về:
                //_value = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
                for (int i = 0; i < _count * 2; i += 2)
                {
                    short giaTri = BitConverter.ToInt16(new byte[] { lstRcv[i], lstRcv[i + 1] }, 0);
                    _value.Add(giaTri);
                }
                retValue = 0;
            }
            catch (Exception err)
            {
                logger.Create(String.Format("Q_Enthernet ReadNutiWork Error: " + err.Message), LogLevel.Error);
            }

            return retValue;
        }


        public int ReadMultiDoubleWord2(DeviceCode _devCode, int _devNumber, int _count, out List<int> _value)
        {
            _value = new List<int>();
            int retValue = -1;
            try
            {
                if (client == null)
                {
                    return retValue;
                }
                if (!client.Connected)
                {
                    return retValue;
                }
                List<byte> lstSendData = new List<byte>();

                // 1. Header : Tự động thêm vào

                // 2. Subheader: 2 byte từ byte 0 đến byte 1
                lstSendData.Add(0x50);
                lstSendData.Add(0x00);

                // 3. Access route : 2 - 6
                // 3.1. Network No: 1 byte
                lstSendData.Add((byte)this.netWorkNo);
                // 3.2. PC No: 1 byte
                lstSendData.Add((byte)this.pcNo);
                // 3.3. Request destination module I/O No. 2 byte
                lstSendData.Add(0xFF);
                lstSendData.Add(0x03);
                // 3.4. Request destination module station no: 1 byte
                lstSendData.Add((byte)this.stationNo);

                // 4. Request Data length: 2 byte từ 7-8
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);

                // 5. Monitoring Timer: 10hexa = 16 thập phân 2 byte 9-10
                // 16*250ms = 4000ms = 4s
                lstSendData.Add(0x10);
                lstSendData.Add(0x00);

                // 6. Request Data:
                // 6.1 Command: 0401 (Đọc lên) 2 byte
                lstSendData.Add(0x01);
                lstSendData.Add(0x04);
                // 6.2 Subcommand : 0000 (đơn vị là thanh ghi) 2 byte
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);
                // 6.3. Device Access:
                // 6.3.1. Device number: 3 byte
                int intDeviceNumber = _devNumber;
                for (int i = 0; i < 3; i++)
                {
                    lstSendData.Add((byte)(intDeviceNumber >> (i * 8)));
                }
                // 6.3.2. Device code: 1 byte
                lstSendData.Add((byte)_devCode);

                // 6.4. Number of device point: 4 byte
                lstSendData.AddRange(BitConverter.GetBytes(_count));

                // Sửa Request Data length ở byte 7 và byte 8
                int length = lstSendData.Count - 9;
                byte[] arrLength = BitConverter.GetBytes((short)length);
                lstSendData[7] = arrLength[0];
                lstSendData[8] = arrLength[1];

                // Gửi đi
                client.Send(lstSendData.ToArray());

                // Nhận về
                byte[] arrRcv = new byte[512];
                client.Receive(arrRcv);
                List<byte> lstRcv = new List<byte>();
                lstRcv.AddRange(arrRcv);

                // Kiểm tra dữ liệu nhận về xem có đúng khung MC Protocol hay không?
                // 1. Subheader: 2 byte D000:
                if (lstRcv[0] != 0xD0 || lstRcv[1] != 0x00)
                {
                    throw new Exception("Subheader Error");
                }
                lstRcv.RemoveRange(0, 2);

                // 2. Access Route: 5 byte 
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0xFF || lstRcv[2] != 0xFF || lstRcv[3] != 0x03 || lstRcv[4] != 0x00)
                {
                    throw new Exception("Access Route Error");
                }
                lstRcv.RemoveRange(0, 5);

                // 3. Response Data Length: 2 byte
                short resLength = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
                if (resLength < 2)
                {
                    throw new Exception("Response Data Length Error");
                }
                lstRcv.RemoveRange(0, 2);

                // 4. End Code: 2 byte
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0x00)
                {
                    throw new Exception(string.Format("Get Error with error code: {0}{1}", lstRcv[0], lstRcv[1]));
                }
                lstRcv.RemoveRange(0, 2);

                // 5. Lấy giá trị về:
                for (int i = 0; i < _count * 4; i += 4)
                {
                    int giaTri = BitConverter.ToInt32(new byte[] { lstRcv[i], lstRcv[i + 1], lstRcv[i + 2], lstRcv[i + 3] }, 0);
                    _value.Add(giaTri);
                }
                retValue = 0;
            }
            catch (Exception err)
            {
                logger.Create(String.Format("Q_Enthernet ReadNutiWork Error: " + err.Message), LogLevel.Error);
            }

            return retValue;
        }


        public int ReadMultiDoubleWord(DeviceCode _devCode, int _devNumber, int _count, out List<int> _value)
        {
            _value = new List<int>();
            int retValue = -1;
            try
            {
                if (client == null)
                {
                    return retValue;
                }
                if (!client.Connected)
                {
                    return retValue;
                }
                List<byte> lstSendData = new List<byte>();

                // 1. Header : Tự động thêm vào

                // 2. Subheader: 2 byte từ byte 0 đến byte 1
                lstSendData.Add(0x50);
                lstSendData.Add(0x00);

                //3 . Access route : 2 - 6
                //3.1. Network No: 1 byte
                lstSendData.Add((byte)this.netWorkNo);
                //3.2. PC No: 1 byte
                lstSendData.Add((byte)this.pcNo);
                //3.3. Request destination module I/ O No. 2 byte
                lstSendData.Add(0xFF);
                lstSendData.Add(0x03);
                //3.4. Request destination module station no: 1 byte
                lstSendData.Add((byte)this.stationNo);

                //4.Request Data length: 2 byte từ 7-8
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);

                // 5. Monitoring Timer: 10hexa = 16 thập phân 2 byte 9-10
                // 16*250ms = 4000ms = 4s
                lstSendData.Add(0x10);
                lstSendData.Add(0x00);
                // 6. Request Data:
                //6.1 Command: 0401 (Đọc lên) 2 byte
                lstSendData.Add(0x01);
                lstSendData.Add(0x04);
                //6.2 Subcommand : 0000 ( đơn vị là thanh ghi) 2 byte
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);
                //6.3. Device Access:
                //6.3.1. Device number: 3 byte
                int intDeviceNumber = _devNumber;
                for (int i = 0; i < 3; i++)
                {
                    lstSendData.Add((byte)(intDeviceNumber >> (i * 8)));
                }
                //6.3.2. Device code: 1 byte
                lstSendData.Add((byte)_devCode);

                // 6.4. Number of device point: 2 byte

                lstSendData.AddRange(BitConverter.GetBytes((ushort)_count));

                //// 6.5. Data: 2 byte với 1 thanh ghi
                //int intData = 1234;
                //for (int i = 0; i < 2; i++)
                //{
                //    lstSendData.Add((byte)(intData >> (i * 8)));
                //}
                // Sửa Request Data length ở byte 7 và byte 8
                int length = lstSendData.Count - 9;
                byte[] arrLength = BitConverter.GetBytes(length);  // Chỗ Request Data length có 2 byte nên khi BitConverter.GetBytes vào mảng byte thì nó chỉ chuyển đổi thành 2 byte được thôi.
                // bit: 1 bit
                // byte: 8 bit: 0 - 255
                // short: 16 bit: -32768 đến 32767 tương ứng với 1 Word trong PLC
                // int : 32 bit từ -2.147.483.648 đến 2.147.483.647 tương đương 2 word trong PLC - double word: 4 byte.

                lstSendData[7] = arrLength[0];
                lstSendData[8] = arrLength[1]; 

                // Gửi đi:
                client.Send(lstSendData.ToArray());

                // Nhận về:
                byte[] arrRcv = new byte[10000];
                client.Receive(arrRcv);
                List<byte> lstRcv = new List<byte>();
                lstRcv.AddRange(arrRcv);
                // Kiểm tra dữ liệu nhận về xem có đúng khung MC Protocol hay không ?

                // 1. Subheader: 2 byte D000:
                if (lstRcv[0] != 0xD0 || lstRcv[1] != 0x00)
                {
                    throw new Exception("Subheader Error");

                }
                lstRcv.RemoveRange(0, 2);

                // 2. Access Route: 5 byte 
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0xFF || lstRcv[2] != 0xFF || lstRcv[3] != 0x03 || lstRcv[4] != 0x00)
                {
                    throw new Exception("Access Route Error");

                }
                lstRcv.RemoveRange(0, 5);

                // 3. Response Data Length: 2 byte
                short resLength = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
                if (resLength < 2)
                {
                    throw new Exception("Response Data Length Error");

                }
                lstRcv.RemoveRange(0, 2);

                // 4.End Code: 2 byte
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0x00)
                {
                    throw new Exception(string.Format("Get Error with error code: {0}{1}", lstRcv[0], lstRcv[1]));

                }
                lstRcv.RemoveRange(0, 2);
                // 5. Lấy giá trị về:
                //_value = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
                for (int i = 0; i < _count * 2; i += 2)
                {
                    // short giaTri = BitConverter.ToInt16(new byte[] { lstRcv[i], lstRcv[i + 1] }, 0);
                    int giaTri = BitConverter.ToInt32(new byte[] { lstRcv[i], lstRcv[i + 1], lstRcv[i + 2], lstRcv[i + 3] }, 0);
                    _value.Add(giaTri);
                }
                retValue = 0;
            }
            catch (Exception err)
            {
                logger.Create(String.Format("Q_Enthernet ReadNutiWork Error: " + err.Message), LogLevel.Error);
            }

            return retValue;
        }
        #endregion
    }
}
