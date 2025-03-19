using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DTO;

namespace ITM_Semiconductor
{
    internal class MCS_PACKET
    {
        public String EquiqmentId { get; set; }
        public String Opcode { get; set; }
        public String LotNo { get; set; }
        public String PackingID { get; set; }
        public List<String> QRCodes { get; set; }
        public String Checksum { get; set; }
    }

    internal class MesComm
    {
        private const String M001 = "E001";
        private const String M011 = "E071";
        private const String M031 = "M031";
        private const String M021 = "M021";
        private const String M151 = "M151";
        private const String M161 = "M161";

        private const String M002 = "E002";
        private const String M012 = "E072";
        private const String M032 = "M032";
        private const String M022 = "M022";
        private const String M152 = "M152";
        private const String M162 = "M162";

        private static MyLogger logger = new MyLogger("mc");
        private int TotalIdexQrCode;

        private byte[] equiqmentId = ASCIIEncoding.ASCII.GetBytes("XBND001  ");
        private byte[] lotNo = ASCIIEncoding.ASCII.GetBytes("P91101");
        private byte[] PackingID = ASCIIEncoding.ASCII.GetBytes("P91101");
        private AppSettings appSettings;
        // TCP server:
        private TcpListener tcpListener;

        private Thread tcpManagerThread;
        private TcpClient tcpClientMcs;

        private Boolean cancelFlag = false;
        Thread checkConnectionThread;
        // Events:
        public delegate void ConnectionHandler(EndPoint remoteEP, Boolean isConnected);

        public event ConnectionHandler ConnectionChanged;

        public delegate void RxPacketHandler(MCS_PACKET packet);

        public event RxPacketHandler PacketReceived;

        public delegate void RxRawDataHandler(byte[] buf);

        public event RxRawDataHandler RawDataReceived;
        public Boolean IsConnected
        {
            get
            {
                try
                {
                    if (tcpClientMcs != null)
                    {
                        var sk = tcpClientMcs.Client;
                        if ((!sk.Connected) || sk.Poll(100, SelectMode.SelectRead) && (sk.Available == 0))
                        {
                            return false;
                        }
                        return true;
                    }
                }
                catch { }
                return false;
            }
        }
        public MesComm(String ipAddr, int port)
        {
            try
            {
                this.tcpListener = new TcpListener(IPAddress.Parse(ipAddr), port);
            }
            catch (Exception ex)
            {
                logger.Create("McServerComm error:" + ex.Message, LogLevel.Error);
             
            }
        }

        public void Setup(String eqId, String lotId)
        {
            try
            {
                logger.Create(String.Format(" -> Setup: equiqmentId={0}, lotId={1}", eqId, lotId),LogLevel.Error);

                if (!String.IsNullOrEmpty(eqId))
                {
                    var arr = eqId.ToCharArray();
                    int sz = arr.Length;
                    if (sz > 9)
                    {
                        sz = 9;
                    }
                    this.equiqmentId = new byte[9];
                    for (int i = 0; i < sz; i++)
                    {
                        this.equiqmentId[i] = (byte)arr[i];
                    }
                    for (int i = sz; i < 9; i++)
                    {
                        this.equiqmentId[i] = 0;
                    }
                }

                if (!String.IsNullOrEmpty(lotId))
                {
                    var arr = lotId.ToCharArray();
                    var sz = arr.Length;
                    if (sz > 10)
                    {
                        sz = 10;
                    }
                    this.lotNo = new byte[10];
                    for (int i = 0; i < sz; i++)
                    {
                        this.lotNo[i] = (byte)arr[i];
                    }
                    for (int i = sz; i < 10; i++)
                    {
                        this.lotNo[i] = 0;
                    }
                }

                var strBuf = new StringBuilder(" -> EqId, LotNo: ");
                for (int i = 0; i < 9; i++)
                {
                    strBuf.Append(String.Format("{0:X2} ", this.equiqmentId[i]));
                }
                for (int i = 0; i < lotId.Length; i++)
                {
                    strBuf.Append(String.Format("{0:X2} ", this.lotNo[i]));
                }
                logger.Create(strBuf.ToString(),LogLevel.Information);
            }
            catch (Exception ex)
            {
                logger.Create("setup error:" + ex.Message,LogLevel.Error);
            }
        }

        public void SetupPackingID(String eqId, String Packing)
        {
            try
            {
                logger.Create(String.Format(" -> SetupPackingID: equiqmentId={0}, PackingId={1}", eqId, Packing), LogLevel.Information);


                if (!String.IsNullOrEmpty(eqId))
                {
                    var arr = eqId.ToCharArray();
                    int sz = arr.Length;
                    if (sz > 9)
                    {
                        sz = 9;
                    }
                    this.equiqmentId = new byte[9];
                    for (int i = 0; i < sz; i++)
                    {
                        this.equiqmentId[i] = (byte)arr[i];
                    }
                    for (int i = sz; i < 9; i++)
                    {
                        this.equiqmentId[i] = 0;
                    }
                }

                if (!String.IsNullOrEmpty(Packing))
                {
                    var arr = Packing.PadRight(12, ' ').ToCharArray();
                    var sz = arr.Length;
                    this.lotNo = new byte[arr.Length];
                    for (int i = 0; i < sz; i++)
                    {
                        this.lotNo[i] = (byte)arr[i];
                    }
                }

                var strBuf = new StringBuilder(" -> EqId, LotNo: ");
                for (int i = 0; i < 9; i++)
                {
                    strBuf.Append(String.Format("{0:X2} ", this.equiqmentId[i]));
                }
                for (int i = 0; i < 12; i++)
                {
                    strBuf.Append(String.Format("{0:X2} ", this.lotNo[i]));
                }
                logger.Create(strBuf.ToString(),LogLevel.Information);
            }
            catch (Exception ex)
            {
                logger.Create("setupPakingID error:" + ex.Message, LogLevel.Error);
            }
        }

        public Boolean Start()
        {
            try
            {
                logger.Create("Start TCP server...",LogLevel.Information);

                this.PacketReceived += this.McServerComm_PacketReceived;

                this.tcpListener.Start();
                this.tcpManagerThread = new Thread(new ThreadStart(this.tcpManager));
                this.tcpManagerThread.IsBackground = true;
                this.tcpManagerThread.Start();
                return true;
            }
            catch (Exception ex)
            {
                logger.Create("Start error:" + ex.Message,LogLevel.Error);
            }
            return false;
        }
        private bool PingIP()
        {
            bool kq = false;
            try
            {
                var x = UiManager.ip.Split(':');

                string ip = x[0];
                logger.Create(ip + "ping", LogLevel.Information);
                string ipAddress = ip; // Địa chỉ IP bạn muốn ping
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send(ipAddress);

                if (reply.Status == IPStatus.Success)
                {
                    kq = true;
                }
                else
                {
                    kq = false;
                }
            }
            catch (Exception ex)
            {
                logger.Create("PingIP() error : " + ex.Message,LogLevel.Error);
            }
            return kq;
        }
        public void Stop()
        {
            try
            {
                this.tcpListener.Stop();
                if (this.tcpClientMcs != null)
                {
                    this.checkConnectionThread.Abort();
                    this.tcpClientMcs.Close();
                    this.tcpClientMcs = null;
                }
                if (this.tcpManagerThread != null)
                {
                    this.tcpManagerThread.Abort();
                    this.tcpManagerThread = null;
                }
            }
            catch (Exception ex)
            {
                logger.Create("Stop error:" + ex.Message, LogLevel.Error);
            }
        }

        public void Cancel()
        {
            cancelFlag = true;
        }

        public void Send(byte[] txBuf)
        {
            if (this.tcpClientMcs != null && txBuf != null && txBuf.Length > 0)
            {
                this.tcpClientMcs.Client.Send(txBuf);
            }
        }

        public Boolean CheckMCSReady(int timeout)
        {
            isReady = false;
            Send_READY_REQ();

            // Wait for result:
            for (int i = 0; i < 200; i++)
            {
                if (isReady)
                {
                    break;
                }
                Thread.Sleep(timeout / 10);
            }
            if (!isReady)
            {
             //   DbWrite.createEvent(new EventLog(EventLog.EV_MES_READY_TIMEOUT));
            }
            return isReady;
        }

        public Boolean CheckBypassOk(int timeout)
        {
            bypassRsp = 0;
            Send_BYPASS_REQ();

            // Wait for result:
            for (int i = 0; i < 10; i++)
            {
                if (bypassRsp != 0)
                {
                    break;
                }
                Thread.Sleep(timeout / 10);
            }
            return bypassRsp == 1;
        }

        public List<String> CheckQRCodes(List<String> qrCodes, int timeout)
        {
            cancelFlag = false;

            qrResults = new List<String>();
            qrReplied = false;
            //if(appSettings.run.PackingOnline)
            //{
            //    CheckPackingIDQr(qrCodes);
            //}    
            Send_QRCODE_REQ(qrCodes);

            // Wait for result:
            const int CHECK_TIME = 500; // 0.5s
            while ((!cancelFlag) && (timeout > 0))
            {
                if (qrReplied)
                {
                    break;
                }
                timeout -= CHECK_TIME;
                Thread.Sleep(CHECK_TIME);
            }
            //for (int i = 0; i < N; i++) {
            //    if (qrReplied) {
            //        break;
            //    }
            //    Thread.Sleep(timeout / N);
            //}
            if (qrReplied)
            {
                return qrResults;
            }
          //  DbWrite.createEvent(new EventLog(EventLog.EV_MES_CHECK_TIMEOUT));
            return null;
        }
        public List<String> CheckPackingID(string qrCodes, int timeout)
        {
            cancelFlag = false;

            qrResults = new List<String>();
            qrReplied = false;
            Send_PACKINGID(qrCodes);

            // Wait for result:
            const int CHECK_TIME = 500; // 0.5s
            while ((!cancelFlag) && (timeout > 0))
            {
                if (qrReplied)
                {
                    break;
                }
                timeout -= CHECK_TIME;
                Thread.Sleep(CHECK_TIME);
            }
            //for (int i = 0; i < N; i++) {
            //    if (qrReplied) {
            //        break;
            //    }
            //    Thread.Sleep(timeout / N);
            //}
            if (qrReplied)
            {
                return qrResults;
            }
            //DbWrite.createEvent(new EventLog(EventLog.EV_MES_CHECK_TIMEOUT));
            return null;
        }
        public List<String> CheckPackingIDQr(List<String> qrCodes, int timeout)
        {
            cancelFlag = false;

            qrResults = new List<String>();
            qrReplied = false;
            Send_PACKINGIDQr(qrCodes);

            // Wait for result:
            const int CHECK_TIME = 500; // 0.5s
            while ((!cancelFlag) && (timeout > 0))
            {
                if (qrReplied)
                {
                    break;
                }
                timeout -= CHECK_TIME;
                Thread.Sleep(CHECK_TIME);
            }
            //for (int i = 0; i < N; i++) {
            //    if (qrReplied) {
            //        break;
            //    }
            //    Thread.Sleep(timeout / N);
            //}
            if (qrReplied)
            {
                return qrResults;
            }
            DbWrite.createEvent(new EventLog(EventLog.EV_MES_CHECK_TIMEOUT));
            return null;
        }
        private volatile Boolean isReady = false;
        private volatile int bypassRsp = 0;
        private volatile List<String> qrResults;
        private volatile Boolean qrReplied = false;

        private void McServerComm_PacketReceived(MCS_PACKET packet)
        {
            if (packet.Opcode.Equals(M002))
            {
                isReady = true;
            }
            else if (packet.Opcode.Equals(M012) || packet.Opcode.Equals(M032) || packet.Opcode.Equals(M152) || packet.Opcode.Equals(M162))
            {
                if (qrResults != null)
                {
                    foreach (var x in packet.QRCodes)
                    {
                        qrResults.Add(x);
                    }
                    qrReplied = true;
                }
            }
            else if (packet.Opcode.Equals(M022))
            {
                if (packet.LotNo.Equals("PASSOK"))
                {
                    bypassRsp = 1;
                }
                else
                {
                    bypassRsp = 2;
                }
            }
        }

        // Packet() = [Head][Body(0)]
        // Head()   = [EquiqmentId(9)][Status(4)]
        private void Send_READY_REQ()
        {
            try
            {
                if (this.tcpClientMcs == null)
                {
                    logger.Create(" -> TCP connection not ready -> discard sending READY_REQ!", LogLevel.Information);
                    return;
                }
                var packet = new List<byte>();
                packet.AddRange(equiqmentId);
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(M001));
                var txBuf = packet.ToArray();

                logger.Create("MES.SEND:" + ASCIIEncoding.ASCII.GetString(txBuf),LogLevel.Information);

                this.tcpClientMcs.Client.Send(txBuf);
            }
            catch (Exception ex)
            {
                logger.Create("Send_READY_REQ error:" + ex.Message, LogLevel.Error);
            }
        }

        // Packet()     = [Head][Body()]
        // Head()       = [EquiqmentId(9)][Status(4)]
        // Body()       = [LotNo(6)][;(1)][Payload(~)][LotNo(6)]
        // Payload()    = [Qr1(M)][;(1)][Qr2(M)][;(1)]...[QrN(M)][;(1)]   Payload.Size = (M + 1) * N
        private void Send_QRCODE_REQ(List<String> qrList)
        {
            try
            {
                if (this.tcpClientMcs == null)
                {
                    logger.Create(" -> TCP connection not ready -> discard sending QRCODE_REQ!", LogLevel.Information);
                    return;
                }

                // Create Payload:
                var payload = new List<byte>(0);
                foreach (var qr in qrList)
                {
                    var arr = ASCIIEncoding.ASCII.GetBytes(qr);
                    payload.AddRange(arr);
                    payload.Add((byte)';');
                }

                // Create Packet:
                var packet = new List<byte>();
                packet.AddRange(equiqmentId);
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(M011));
                packet.AddRange(lotNo);
                packet.Add((Byte)';');
                packet.AddRange(payload);
                packet.AddRange(lotNo);

                var txBuf = packet.ToArray();

                logger.Create("MES.SEND:" + ASCIIEncoding.ASCII.GetString(txBuf),LogLevel.Information);

                this.tcpClientMcs.Client.Send(txBuf);
            }
            catch (Exception ex)
            {
                logger.Create("Send_QRCODE_REQ error:" + ex.Message,LogLevel.Error);
            }
        }
        private void Send_PACKINGID(String PackingID)
        {
            try
            {
                if (this.tcpClientMcs == null)
                {
                    logger.Create(" -> TCP connection not ready -> discard sending QRCODE_REQ!",LogLevel.Information);
                    return;
                }
                // Create Packet:
                var packet = new List<byte>();
                packet.AddRange(equiqmentId);
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(M151));
                packet.AddRange(this.lotNo);
                packet.Add((Byte)';');
                packet.AddRange(this.lotNo);
                var mmm = ASCIIEncoding.ASCII.GetString(this.lotNo);
                var txBuf = packet.ToArray();

                logger.Create("MES.SEND Packing ID:" + ASCIIEncoding.ASCII.GetString(txBuf), LogLevel.Information);

                this.tcpClientMcs.Client.Send(txBuf);
            }
            catch (Exception ex)
            {
                logger.Create("Send_PACKINGID error:" + ex.Message,LogLevel.Error);
            }
        }
        private void Send_PACKINGIDQr(List<String> PackingIDqr)
        {
            try
            {
                if (this.tcpClientMcs == null)
                {
                    logger.Create(" -> TCP connection not ready -> discard sending QRCODE_REQ!", LogLevel.Information);
                    return;
                }
                var payload = new List<byte>(0);
                foreach (var qr in PackingIDqr)
                {
                    var arr = ASCIIEncoding.ASCII.GetBytes(qr);
                    payload.AddRange(arr);
                    payload.Add((byte)';');
                }
                // Create Packet:
                var packet = new List<byte>();
                packet.AddRange(equiqmentId);
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(M161));
                packet.AddRange(this.lotNo);
                packet.Add((Byte)';');
                packet.AddRange(payload);
                packet.AddRange(this.lotNo);

                var txBuf = packet.ToArray();

                logger.Create("MES.SEND Packing ID:" + ASCIIEncoding.ASCII.GetString(txBuf),LogLevel.Information);
                logger.Create("Total Send MES QR PACKING ID:" + PackingIDqr.Count, LogLevel.Information);

                this.tcpClientMcs.Client.Send(txBuf);
            }
            catch (Exception ex)
            {
                logger.Create("Send_PACKINGID error:" + ex.Message,LogLevel.Error);
            }
        }

        // Packet()     = [Head][Body()]
        // Head()       = [EquiqmentId(9)][Status(4)]
        // Body()       = [ByPass(6)][;(1)]
        private void Send_BYPASS_REQ()
        {
            try
            {
                if (this.tcpClientMcs == null)
                {
                    logger.Create(" -> TCP connection not ready -> discard sending BYPASS_REQ!", LogLevel.Information);
                    return;
                }
                // Create payload:
                var payload = ASCIIEncoding.ASCII.GetBytes("BYPASS;");

                // Create Packet:
                var packet = new List<byte>();
                packet.AddRange(equiqmentId);
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(M021));
                packet.AddRange(payload);

                var txBuf = packet.ToArray();
                this.tcpClientMcs.Client.Send(txBuf);
            }
            catch (Exception ex)
            {
                logger.Create("Send_BYPASS_REQ error:" + ex.Message,LogLevel.Error);
            }
        }
        private void CheckConnection(object obj)
        {

            TcpClient client = (TcpClient)obj;
            while (true)
            {
                try
                {
                    Thread.Sleep(7000); // Kiểm tra mỗi giây

                    if (!string.IsNullOrEmpty(UiManager.ip))
                    {
                        if (!PingIP())
                        {
                            if (this.ConnectionChanged != null)
                            {
                                this.ConnectionChanged(null, false);
                            }
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Create("CheckConnection error : " + ex.Message, LogLevel.Error);
                }
            }
        }

        private bool IsClientConnected(TcpClient client)
        {
            try
            {
                if (client != null && client.Client != null && client.Client.Connected)
                {
                    if (client.Client.Poll(0, SelectMode.SelectRead))
                    {
                        byte[] buffer = new byte[1];
                        if (client.Client.Receive(buffer, SocketFlags.Peek) == 0)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        private void tcpManager()
        {
            this.appSettings = UiManager.appSettings;
            while (true)
            {
                try
                {
                    tcpClientMcs = this.tcpListener.AcceptTcpClient();
                    checkConnectionThread = new Thread(CheckConnection);
                    checkConnectionThread.Start();
                    if (this.ConnectionChanged != null)
                    {
                        this.ConnectionChanged(tcpClientMcs.Client.RemoteEndPoint, true);
                    }

                    // Get a stream object for reading and writing
                    NetworkStream stream = tcpClientMcs.GetStream();

                    while (stream != null)
                    {
                        // Check connection:
                        if (!this.IsConnected)
                        {
                            logger.Create(" -> disconnect -> discard receiving!",LogLevel.Information);
                            if (this.ConnectionChanged != null)
                            {
                                this.ConnectionChanged(null, false);
                            }
                            break;
                        }
                        //byte[] buffer = new byte[1024];
                        //var bytesRead = stream.Read(buffer, 0, buffer.Length);
                        //if(bytesRead==0)
                        //{
                        //    //Mat ket noi.
                        //    this.ConnectionChanged(null, false);
                        //}    
                        // Wait for new RX packet:
                        stream.ReadTimeout = 1000;
                        var rxBuf = new byte[10000];
                        var rxLen = 0;
                        try
                        {
                            rxLen = stream.Read(rxBuf, 0, rxBuf.Length);
                        }
                        catch (IOException iex)
                        {
                            Debug.Write("IOEX:" + iex.Message);
                        }

                        // Get packet:
                        if (rxLen > 0)
                        {
                            var strBuilder = new StringBuilder(" -> New Packet: Len=" + rxLen.ToString() + ", Data=");
                            for (int i = 0; i < rxLen; i++)
                            {
                                strBuilder.Append(rxBuf[i].ToString("X2"));
                            }
                            logger.Create(strBuilder.ToString(), LogLevel.Information);
                            logger.Create("Data(ASCII)=" + ASCIIEncoding.ASCII.GetString(rxBuf, 0, rxLen),LogLevel.Information);

                            var rxData = new byte[rxLen];
                            Array.Copy(rxBuf, 0, rxData, 0, rxLen);
                            if (this.RawDataReceived != null)
                            {
                                this.RawDataReceived(rxData);
                            }
                            MCS_PACKET packet = new MCS_PACKET();
                            if (appSettings.run.PackingOnline && !appSettings.run.PackingEnd)
                            {
                                logger.Create("GET PACKET PACKING ID", LogLevel.Information);
                                packet = this.GetPacketPackingID(rxData, rxLen);
                            }
                            else
                            {
                                logger.Create("GET PACKET",LogLevel.Information);
                                packet = this.GetPacket(rxData, rxLen);
                            }

                            if (packet != null)
                            {
                                if (this.PacketReceived != null)
                                {
                                    this.PacketReceived(packet);
                                }
                            }
                        }
                    }
                    stream.Close();
                }
                catch (Exception ex)
                {
                    logger.Create("tcpManager error:" + ex.Message,LogLevel.Information);
                    if (this.ConnectionChanged != null)
                    {
                        this.ConnectionChanged(null, false);
                    }
                }
            }
        }


        //415332445152303033 4D303031
        private MCS_PACKET GetPacket(byte[] buf, int rxLen)
        {
            var ret = new MCS_PACKET();
            byte[] arr;
            int idx = 0;

            // EquiqId:
            if (idx + 9 > rxLen)
            {
                logger.Create(" -> EquiqId too short!", LogLevel.Information);
                return null;
            }
            arr = new byte[9];
            Array.Copy(buf, idx, arr, 0, 9);
            idx += 9;
            ret.EquiqmentId = ASCIIEncoding.ASCII.GetString(arr);

            // Status:
            if (idx + 4 > rxLen)
            {
                logger.Create(" -> STATUS too short!",LogLevel.Information);
                return null;
            }
            arr = new byte[4];
            Array.Copy(buf, idx, arr, 0, 4);
            idx += 4;
            ret.Opcode = ASCIIEncoding.ASCII.GetString(arr);

            // Check M002 packet:
            if (ret.Opcode.Equals(M002))
            {
                return ret;
            }

            // M012, M022 packets:

            // LotNo:
            if (idx + lotNo.Length > rxLen)
            {
                logger.Create(" -> LotNo.Start too short!", LogLevel.Information);
                return null;
            }
            arr = new byte[lotNo.Length];
            Array.Copy(buf, idx, arr, 0, lotNo.Length);
            idx += lotNo.Length;
            ret.LotNo = ASCIIEncoding.ASCII.GetString(arr);

            // Start of Body.QRCode: ';'
            idx += 1;

            // Body.Payload:
            ret.QRCodes = new List<string>(0);
            if (ret.Opcode.Equals(M012) || ret.Opcode.Equals(M032) || ret.Opcode.Equals(M152) || ret.Opcode.Equals(M162))
            {
                Array.Copy(buf, idx, arr = new byte[buf.Length - idx], 0, buf.Length - idx);
                //arr = new byte[14];
                string Tray = "";
                int Indexof = 0;
                Tray = ASCIIEncoding.ASCII.GetString(arr);
                string[] arrQR = Tray.Split(';');
                for (int i = 0; i < arrQR.Length; i++)
                {
                    Indexof = arrQR[i].IndexOf("^");
                    if (Indexof >= 0)
                    {
                        ret.QRCodes.Add(arrQR[i]);
                    }
                    else
                    {
                        ret.Checksum = arrQR[i];
                    }
                }
                if (ret.QRCodes.Count <= 0)
                {
                    logger.Create("Result Data MES -> Not Data Result!",LogLevel.Information);
                    return null;
                }
                if (ret.Checksum == "" || ret.Checksum == null)
                {
                    logger.Create("MES Check Sum -> Check sum to Short!", LogLevel.Information);
                    return null;
                }
                return ret;
            }
            else if (ret.Opcode.Equals(M022))
            {
                // PASSOK or PASSNG
                if (idx + 6 > rxLen)
                {
                    logger.Create(" -> BYPASS.Result too short!",LogLevel.Information);
                    return null;
                }
                arr = new byte[6];
                Array.Copy(buf, idx, arr, 0, 6);
                idx += 6;
                ret.LotNo = ASCIIEncoding.ASCII.GetString(arr);
                return ret;
            }

            logger.Create(" -> Invalid format!", LogLevel.Information);
            return null;
        }
        private MCS_PACKET GetPacketPackingID(byte[] buf, int rxLen)
        {
            logger.Create("GetPacketPackingID",LogLevel.Information);
            var ret = new MCS_PACKET();
            byte[] arr;
            int idx = 0;

            // EquiqId:
            if (idx + 9 > rxLen)
            {
                logger.Create(" -> EquiqId too short!", LogLevel.Information);
                return null;
            }
            arr = new byte[9];
            Array.Copy(buf, idx, arr, 0, 9);
            idx += 9;
            ret.EquiqmentId = ASCIIEncoding.ASCII.GetString(arr);

            // Status:
            if (idx + 4 > rxLen)
            {
                logger.Create(" -> STATUS too short!",LogLevel.Information);
                return null;
            }
            arr = new byte[4];
            Array.Copy(buf, idx, arr, 0, 4);
            idx += 4;
            ret.Opcode = ASCIIEncoding.ASCII.GetString(arr);

            // Check M002 packet:
            if (ret.Opcode.Equals(M002))
            {
                return ret;
            }

            // M012, M022 packets:

            // LotNo:
            if (idx + lotNo.Length > rxLen)
            {
                logger.Create(" -> LotNo.Start too short!",LogLevel.Information);
                return null;
            }
            arr = new byte[lotNo.Length];
            Array.Copy(buf, idx, arr, 0, lotNo.Length);
            idx += lotNo.Length;
            ret.LotNo = ASCIIEncoding.ASCII.GetString(arr);

            idx += 1;

            // Body.Payload:
            ret.QRCodes = new List<string>(0);
            if (ret.Opcode.Equals(M012) || ret.Opcode.Equals(M032) || ret.Opcode.Equals(M152) || ret.Opcode.Equals(M162))
            {

                Array.Copy(buf, idx, arr = new byte[buf.Length - idx], 0, buf.Length - idx);
                //arr = new byte[14];
                string Tray = "";
                int Indexof = 0;
                Tray = ASCIIEncoding.ASCII.GetString(arr);
                string[] arrQR = Tray.Split(';');
                for (int i = 0; i < arrQR.Length; i++)
                {
                    Indexof = arrQR[i].IndexOf("^");
                    if (Indexof >= 0)
                    {
                        ret.QRCodes.Add(arrQR[i]);
                    }
                    else
                    {
                        ret.Checksum = arrQR[i];
                    }
                }
                if (ret.QRCodes.Count <= 0)
                {
                    logger.Create("Result Data MES -> Not Data Result!", LogLevel.Information);
                    return null;
                }
                if (ret.Checksum == "" || ret.Checksum == null)
                {
                    logger.Create("MES Check Sum -> Check sum to Short!", LogLevel.Information);
                    return null;
                }
                return ret;
            }
            else if (ret.Opcode.Equals(M022))
            {
                // PASSOK or PASSNG
                if (idx + 6 > rxLen)
                {
                    logger.Create(" -> BYPASS.Result too short!",LogLevel.Information);
                    return null;
                }
                arr = new byte[6];
                Array.Copy(buf, idx, arr, 0, 6);
                idx += 6;
                ret.LotNo = ASCIIEncoding.ASCII.GetString(arr);
                return ret;
            }

            logger.Create(" -> Invalid format!", LogLevel.Information);
            return null;
        }
    }
}
