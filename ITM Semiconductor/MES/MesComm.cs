
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
using System.Security.Cryptography;
using static Mitsubishi.Q_Enthernet;
using System.Windows.Markup;

namespace ITM_Semiconductor
{
     class MCS_PACKET
    {
        public string EquipmentId { get; set; }
        public string Status { get; set; }
        public string LotID { get; set; }
        public string QrCodeCarier { get; set; }
        public string Result { get; set; }
        public string CheckSum { get; set; }
    }

     class MesComm
    {
        private static MyLogger logger = new MyLogger("MESComm");

        private const String M001 = "M001"; // MES Ask Ready OK ?
        private const String M002 = "M002"; // Feedback MES READYOK/ READYNG 
        private const String M211 = "M211"; // PCB QrCode + Carrier Qrcode Client Send
        private const String M212 = "M212"; // PCB QrCode + Carrier Qrcode Client Reciver

        //private int TotalIdexQrCode;

        private byte[] equiqmentId = ASCIIEncoding.ASCII.GetBytes("XBND002  "); // Get Byte[] Off Equipment
        private byte[] lotNo = ASCIIEncoding.ASCII.GetBytes("P91101    "); // Get Byte[] Off LotNO
        private Byte[] jigID = ASCIIEncoding.ASCII.GetBytes("L0IHI1STMP                              ");// Get Byte[] Off Jig ID
        private Byte[] pcbCode = ASCIIEncoding.ASCII.GetBytes("B2820045040237501000301                                                                             "); // Get Byte[] Off PCB Qrcode

        private AppSettings appSettings;
        // TCP server:
        private TcpListener tcpListener;

        private Thread tcpManagerThread;
        private TcpClient tcpClientMcs;

        private Boolean cancelFlag = false;
        Thread checkConnectionThread;
        // Events:
        private event DlOneParam ConnectionChanged;

        // Event MES Reciver Data
        public delegate void RxPacketHandler(MCS_PACKET packet);

        public event RxPacketHandler PacketReceived;
       


        public delegate void LogCallback(String msg);
 
        private Boolean CancelFlag = false;

        private bool IsRunning = false; // Is Running
        private volatile Boolean isReady = false;
       // private volatile int bypassRsp = 0;
        private volatile string qrResults;
        private volatile Boolean qrReplied = false;
       
        public Boolean IsConnected
        {
            get
            {
                if (this.tcpClientMcs != null) // Check Socket Is Null
                {
                    try
                    {
                        if (this.tcpClientMcs.Client.Poll(0, SelectMode.SelectRead))
                        {
                            byte[] buff = new byte[1];
                            if (this.tcpClientMcs.Client.Receive(buff, SocketFlags.Peek) == 0)
                                return false;
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            
        }
        public MesComm(String ipAddr, int port)
        {
            this.tcpListener = new TcpListener(IPAddress.Parse(ipAddr), port);
        }
        #region Setup Equipment ID And Lot ID
        public void Setup(String eqId, String lotId)
        {
            try
            {
                logger.Create(String.Format(" -> Setup: equiqmentId={0}, lotId={1}", eqId, lotId),LogLevel.Error);

                this.SetupEquipment(eqId);
                this.SetupLotID(lotId);
            }
            catch (Exception ex)
            {
                logger.Create("setup error:" + ex.Message,LogLevel.Error);
            }
        }
        private void SetupEquipment(String eqId)
        {
            if (!String.IsNullOrEmpty(eqId))
            {
                var arr = eqId.ToCharArray();
                int sz = arr.Length;
                if (sz > 9) { sz = 9; }
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
        }
        private byte[] ReturnEquipment(String eqId)
        {
            var ret = new byte[9];
            if (!String.IsNullOrEmpty(eqId))
            {
                var arr = eqId.ToCharArray();
                int sz = arr.Length;
                if (sz > 9) { sz = 9; }
                for (int i = 0; i < sz; i++)
                {
                    ret[i] = (byte)arr[i];
                }
                for (int i = sz; i < 9; i++)
                {
                    ret[i] = 0;
                }
            }
            return ret;
        }
        private void SetupLotID(string lotID)
        {
            if (!String.IsNullOrEmpty(lotID))
            {
                var arr = lotID.ToCharArray();
                int sz = arr.Length;
                if (sz > 10) { sz = 10; }
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
        }
        private byte[] ReturnLot(String lotID)
        {
            var ret = new byte[10];
            if (!String.IsNullOrEmpty(lotID))
            {
                var arr = lotID.ToCharArray();
                int sz = arr.Length;
                if (sz > 10) { sz = 10; }
                for (int i = 0; i < sz; i++)
                {
                    ret[i] = (byte)arr[i];
                }
                for (int i = sz; i < 10; i++)
                {
                    ret[i] = 0;
                }
            }
            return ret;
        }
        #endregion
        public void SetupQrCarrier_SetupQrPCB(String QrCodeJig, String QrCodePCB)
        {
            try
            {
                logger.Create(String.Format(" -> SetupPackingID: equiqmentId={0}, PackingId={1}", QrCodeJig, QrCodePCB), LogLevel.Information);


                // Check Format QrCodeCarrier Input
                if (!String.IsNullOrEmpty(QrCodeJig))
                {
                    var arr = QrCodeJig.ToCharArray();
                    int sz = arr.Length;
                    if (sz > 40)
                    {
                        sz = 40;
                    }
                    this.jigID = new byte[40];
                    for (int i = 0; i < sz; i++)
                    {
                        this.jigID[i] = (byte)arr[i];
                    }
                    for (int i = sz; i < 40; i++)
                    {
                        this.jigID[i] = 0;
                    }
                }

                // Check Format QrCodePCB Input
                if (!String.IsNullOrEmpty(QrCodePCB))
                {
                    var arr = QrCodePCB.ToCharArray();
                    var sz = arr.Length;
                    if (sz > 100)
                    {
                        sz = 100;
                    }
                    this.pcbCode = new byte[100];
                    for (int i = 0; i < sz; i++)
                    {
                        this.pcbCode[i] = (byte)arr[i];
                    }
                    for (int i = sz; i < 100; i++)
                    {
                        this.pcbCode[i] = 0;
                    }
                }


                // Write Log Affter Setup  QrCodeCarrier Input + QrCodePCB Input
                var strBuf = new StringBuilder("Data After Setup: QrCode Carrier, QrCode PCB: ");
                for (int i = 0; i < 40; i++)
                {
                    strBuf.Append(String.Format("{0:X2} ", this.jigID[i]));
                }
                for (int i = 0; i < 100; i++)
                {
                    strBuf.Append(String.Format("{0:X2} ", this.pcbCode[i]));
                }
                logger.Create(strBuf.ToString(),LogLevel.Information);
            }
            catch (Exception ex)
            {
                logger.Create("setupPakingID error:" + ex.Message, LogLevel.Error);
            }
        }
        #region Check MES Ready E001
        public Boolean CheckMCSReady(int timeout) // Check MES Ready
        {
            this.isReady = false;

            this.Send_READY_REQ();

            for (int i = 0; i < 10; i++)
            {
                if (this.isReady)
                {
                    break;
                }
                Thread.Sleep(timeout / 10);
            }
            if (!this.isReady)
            {
                DbWrite.createEvent(new EventLog(EventLog.EV_MES_READY_TIMEOUT));
            }
            return this.isReady;
        }
        private void Send_READY_REQ()
        {
            try
            {
                if (!this.IsConnected)
                {
                    logger.Create(String.Format(" -> TCP Connection Not Ready -> Discard Sending READY_REQ!"),LogLevel.Information);
                    return;
                }
                var packet = new List<byte>();
                packet.AddRange(this.equiqmentId);
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(M001));
                var txBuf = packet.ToArray();
                logger.Create(String.Format("MES.SEND: " + ASCIIEncoding.ASCII.GetString(txBuf)),LogLevel.Information);
                this.tcpClientMcs.Client.Send(txBuf);

                // Write Log
                var arr = new byte[txBuf.Length];
                Array.Copy(txBuf, 0, arr, 0, arr.Length);
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == 0x00) { arr[i] = 0x20; }
                }
             //   this.CreateLog(ASCIIEncoding.ASCII.GetString(arr));

            }
            catch (Exception ex)
            {
                logger.Create("Send_READY_REQ Error: " + ex.Message,LogLevel.Error);
            }
        }
        private void Return_READY_REQ()
        {
            try
            {
                if (!this.IsConnected)
                {
                    logger.Create(String.Format(" -> TCP Connection Not Ready -> Discard Sending Return READY_REQ!"), LogLevel.Information);
                    return;
                }
                var packet = new List<byte>();
                packet.AddRange(this.equiqmentId);
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(M002));
                var txBuf = packet.ToArray();
                logger.Create(String.Format("MES.SEND: " + ASCIIEncoding.ASCII.GetString(txBuf)), LogLevel.Information);
                this.tcpClientMcs.Client.Send(txBuf);

                // Write Log
                var arr = new byte[txBuf.Length];
                Array.Copy(txBuf, 0, arr, 0, arr.Length);
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == 0x00) { arr[i] = 0x20; }
                }
             //   this.CreateLog(ASCIIEncoding.ASCII.GetString(arr));
            }
            catch (Exception ex)
            {
                logger.Create("Send Return READY_REQ Error: " + ex.Message, LogLevel.Error);
            }
        }
        #endregion

        #region Check MES Result E091
        public String CheckQRCodes(int timeout , DataChecking data)
        {
            this.SetupQrCarrier_SetupQrPCB(data.QrCodeJigMIX, data.QrCodeFPCBMIX);
            this.CancelFlag = false;

            this.qrResults = String.Empty;
            this.qrReplied = false;
            this.Send_QRCODE_REQ();

            // Wait for result:
            const int CHECK_TIME = 500; // 0.5s
            while ((!this.CancelFlag) && (timeout > 0))
            {
                if (this.qrReplied)
                {
                    break;
                }
                timeout -= CHECK_TIME;
                Thread.Sleep(CHECK_TIME);
            }

            if (this.qrReplied)
            {
                return this.qrResults;
            }
            DbWrite.createEvent(new EventLog(EventLog.EV_MES_CHECK_TIMEOUT));
            return null;
        }

        #endregion
        private void Send_QRCODE_REQ()
        {
            try
            {
                if (!this.IsConnected)
                {
                    logger.Create(" -> TCP MES Connection Not Ready -> Discard Sending QRCODE_REQ!", LogLevel.Information);
                    return;
                }
                // Create Payload:
                var packet = new List<byte>();
                packet.AddRange(equiqmentId);
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(M211));
                packet.AddRange(this.lotNo);
                packet.AddRange(this.jigID);
                packet.Add((Byte)';');
                packet.AddRange(this.pcbCode);
                packet.Add((Byte)';');
                packet.AddRange(this.lotNo);

                var txBuf = packet.ToArray();
                logger.Create("MES.SEND:" + ASCIIEncoding.ASCII.GetString(txBuf), LogLevel.Information);
                this.tcpClientMcs.Client.Send(txBuf);

                // Write Log
                var arr = new byte[txBuf.Length];
                Array.Copy(txBuf, 0, arr, 0, arr.Length);
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == 0x00) { arr[i] = 0x20; }
                }
                // this.CreateLog(ASCIIEncoding.ASCII.GetString(arr));
            }
            catch (Exception ex)
            {
                logger.Create("Send_QRCODE_REQ error:" + ex.Message, LogLevel.Error);
            }
        }
        public Boolean Start()
        {
            try
            {
                logger.Create("Start TCP Server Connection MES...", LogLevel.Information);

                this.PacketReceived += this.MESComm_PacketReceived;

                this.tcpListener.Start();
                this.IsRunning = true;
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
        public void Stop()
        {
            try
            {
                this.tcpListener.Stop();
                if (this.tcpClientMcs != null)
                {
                    this.IsRunning = false;
                    this.checkConnectionThread.Abort();
                    this.tcpClientMcs.Close();
                    this.tcpClientMcs = null;
                }
                if (this.tcpManagerThread != null)
                {
                    this.IsRunning = false;
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
        private void MESComm_PacketReceived(MCS_PACKET packet)
        {
            if (packet.Status.Equals(M002)) 
            {
                this.isReady = true; 
            }
            else if (packet.Status.Equals(M001))
            {
                if (!String.Equals(ASCIIEncoding.ASCII.GetString(this.equiqmentId), packet.EquipmentId))
                {
                    UiManager.appSettings.connection.EquipmentName = packet.EquipmentId;
                    UiManager.SaveAppSetting();

                    this.SetupEquipment(packet.EquipmentId);
                }
                this.Return_READY_REQ();
            }
            else if (packet.Status.Equals(M212))
            {
                qrResults = packet.Result;
                qrReplied = true;
            }
        }

        private void tcpManager()
        {
            this.appSettings = UiManager.appSettings;
            while (this.IsRunning)
            {
                
                try
                {
                    this.tcpClientMcs = this.tcpListener.AcceptTcpClient();
                    if (this.ConnectionChanged != null)
                    {
                        this.ConnectionChanged(this.tcpClientMcs.Client.RemoteEndPoint, true);
                    }
                    NetworkStream stream = this.tcpClientMcs.GetStream();
                    while (stream != null && this.IsRunning && this.IsConnected)
                    {
                        if (stream.DataAvailable)
                        {
                            var rxBuf = new byte[this.tcpClientMcs.Available];
                            var rxLen = 0;
                            rxLen = stream.Read(rxBuf, 0, rxBuf.Length);
                            if (rxLen > 0)
                            {
                                // Write Log
                                var arr = new byte[rxLen];
                                Array.Copy(rxBuf, 0, arr, 0, rxLen);
                                for (int i = 0; i < arr.Length; i++)
                                {
                                    if (arr[i] == 0x00) { arr[i] = 0x20; }
                                }
                              //  this.CreateLog(ASCIIEncoding.ASCII.GetString(arr));

                                logger.Create(String.Format("MES.RECIVER:{0}", ASCIIEncoding.ASCII.GetString(rxBuf, 0, rxLen)),LogLevel.Information);
                                var rxData = new byte[rxLen];
                                Array.Copy(rxBuf, 0, rxData, 0, rxLen);
                                var packet = this.GetPacket(rxData, rxLen);
                                if (packet != null)
                                {
                                    if (this.PacketReceived != null)
                                    {
                                        this.PacketReceived(packet);
                                    }
                                }
                            }
                        }
                    }
                    if (!this.IsConnected)
                    {
                        logger.Create("MES Disconnect !!!",LogLevel.Information);
                       // this.CreateLog("MES Disconnect !!!");
                        if (this.ConnectionChanged != null)
                        {
                            this.ConnectionChanged(this.tcpClientMcs.Client.RemoteEndPoint, false);
                        }
                        stream.Close();
                        stream.Dispose();

                        this.tcpClientMcs.Close();
                        this.tcpClientMcs.Dispose();

                        continue;
                    }
                    if (!this.IsRunning)
                    {
                        logger.Create(String.Format("-> User STOP MES Connect !!!"), LogLevel.Information);
                  //      this.CreateLog("-> User STOP MES Connect !!!");
                        if (this.ConnectionChanged != null)
                        {
                            this.ConnectionChanged(this.tcpClientMcs.Client.RemoteEndPoint, false);
                        }
                        stream.Close();
                        stream.Dispose();

                        this.tcpClientMcs.Close();
                        this.tcpClientMcs.Dispose();

                        continue;
                    }
                    if (stream == null)
                    {
                        logger.Create(String.Format("-> MES Close Stream Socket !!!"), LogLevel.Information);
                        //this.CreateLog(String.Format("-> MES Close Stream Socket !!!"));

                        if (this.ConnectionChanged != null)
                        {
                            this.ConnectionChanged(this.tcpClientMcs.Client.RemoteEndPoint, false);
                        }

                        stream.Close();
                        stream.Dispose();

                        this.tcpClientMcs.Close();
                        this.tcpClientMcs.Dispose();
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    logger.Create("Tcp Manager MES Error: " + ex.Message,LogLevel.Error);
                }
            }
        }
        private MCS_PACKET GetPacket(byte[] buf, int rxLen)
        {
            // Reciver data by Socket ==> MES_PACKET data
            var ret = new MCS_PACKET();
            byte[] arr;
            int idx = 0;

            #region Equipment_Id [9 byte]
            if (idx + 9 > rxLen) // Equipment ID (9byte)
            {
                var msg = String.Format("-> Equipment ID Too Short! " + "Via The Format: Equipment ID == 9 Byte");
                // this.CreateLogShow(msg);
                logger.Create(msg, LogLevel.Information);

                return null;
            }
            arr = new byte[10];
            Array.Copy(buf, idx, arr, 0, 9);

            var equipment = ASCIIEncoding.ASCII.GetString(arr).Trim();
            var equipmentbyte = this.ReturnEquipment(equipment);
            var equipmentFeedback = ASCIIEncoding.ASCII.GetString(equipmentbyte);

            idx += 9;
            ret.EquipmentId = equipmentFeedback;
            #endregion

            #region Status [4 byte]          
            if (idx + 4 > rxLen) // Status (4byte)
            {
                var msg = String.Format("Status->  Too Short! " + "Via The Format: Status == 4 Byte");
                // this.CreateLogShow(msg);
                logger.Create(msg, LogLevel.Information);
                return null;
            }
            arr = new byte[4];
            Array.Copy(buf, idx, arr, 0, 4);

            var cmdFeedback = ASCIIEncoding.ASCII.GetString(arr);
            if (!(string.Equals(M001, cmdFeedback) || string.Equals(M002, cmdFeedback) || string.Equals(M212, cmdFeedback)))
            {
                var msg = String.Format("-> Status MES Feedback : {0} Not Matching With {1}/{2}/{3}", cmdFeedback, M001, M002, M212);
                // this.CreateLogShow(msg);
                logger.Create(msg, LogLevel.Information);
                return null;
            }

            idx += 4;
            ret.Status = ASCIIEncoding.ASCII.GetString(arr);
            if (string.Equals(M001, ret.Status) || string.Equals(M002, ret.Status))
            {
                return ret;
            }
            #endregion

            #region Check status Of Feedback
            if (ret.Status.Equals(M002)) { return ret; }
            #endregion

            #region Lot ID [10 byte]
            if (idx + 10 > rxLen) // CMD (10byte)
            {
                var msg = String.Format("-> Lot ID-> Too Short! " + "Via The Format: Lot ID == 10 Byte");
                //this.CreateLogShow(msg);
                logger.Create(msg, LogLevel.Information);
                return null;
            }
            arr = new byte[10];
            Array.Copy(buf, idx, arr, 0, 10);
            var lotID = ASCIIEncoding.ASCII.GetString(arr).Trim();
            var lotIDbyte = this.ReturnLot(lotID);
            var lotIDFeedback = ASCIIEncoding.ASCII.GetString(lotIDbyte);

            var lotIDSetup = ASCIIEncoding.ASCII.GetString(this.lotNo);
            if (!String.Equals(lotIDFeedback, lotIDSetup))
            {
                var msg = String.Format("-> Lot ID MES Feedback :{0}     Not Matching With Lot ID MES Setup :{1}", lotID, lotIDFeedback);
                //this.CreateLogShow(msg);
                logger.Create(msg, LogLevel.Information);
                return null;
            }
            idx += 10;
            ret.LotID = lotIDFeedback;
            #endregion

            #region QrCode Carrier [40_byte]
            if (idx + 40 > rxLen) // QrCode Carrier (40byte)
            {
                var msg = String.Format("-> Qrcode Carrier-> Too Short! " + "Via The Format: Qrcode Carrier == 40 Byte");
               // this.CreateLogShow(msg);
                return null;
            }
            arr = new byte[40];
            Array.Copy(buf, idx, arr, 0, 40);
            var QrCodeCarrier = ASCIIEncoding.ASCII.GetString(arr).Trim();
            var QrCodeCarrierbyte = this.ReturnQrCodeCarrier(QrCodeCarrier);
            var QrCodeCarrierFeedback = ASCIIEncoding.ASCII.GetString(QrCodeCarrierbyte);

            var QrcodeCarrierSetup = ASCIIEncoding.ASCII.GetString(this.jigID);
            if (!String.Equals(QrCodeCarrierFeedback, QrcodeCarrierSetup))
            {
                var msg = String.Format("-> QrCode Carrier MES Feedback :{0}       Not Matching With Qrcode Carrier MES Setup :{1}", QrCodeCarrier, QrcodeCarrierSetup);
                //this.CreateLogShow(msg);
                return null;
            }
            idx += 40;
            ret.QrCodeCarier = QrCodeCarrierFeedback;
            #endregion

            #region Skip ; [1 byte]
            idx += 1;   // Not Caculator ';'
            #endregion

            #region Result Check QrCode And CheckSum
            ret.Result = string.Empty;
            if (ret.Status.Equals(M212))
            {
                arr = new byte[buf.Length - idx];
                Array.Copy(buf, idx, arr, 0, arr.Length);
                idx += arr.Length;
                var str = ASCIIEncoding.ASCII.GetString(arr);
                var sortData = str.Split(';');
                if (sortData.Length == 2)
                {
                    ret.Result = sortData[0].Trim();
                    var checkSum = sortData[1].Trim();
                    var checkSumbyte = this.ReturnLot(checkSum);
                    var checkSumFeedback = ASCIIEncoding.ASCII.GetString(checkSumbyte);
                    if (!String.Equals(checkSumFeedback, lotIDSetup))
                    {
                        var msg = String.Format(" -> Checksum MES Feedback : {0} Not Matching With Lot ID MES Setup : {1}", checkSumFeedback, lotIDSetup);
                       /// this.CreateLogShow(msg);
                        return null;
                    }
                    ret.CheckSum = checkSumFeedback;
                }
                else
                {
                    var msg = String.Format(" -> MES Feedback: {0} Not Enough Element/ Setup : {1}.", sortData.Length, 2);
                    //this.CreateLogShow(msg);
                    return null;
                }
            }
            else
            {
                var msg = String.Format(" -> Status MES Feedback : {0} Not Matching With Status MES Protocol Format: {1}.", ret.Status, M212);
               // this.CreateLogShow(msg);
                return null;
            }
            #endregion

            return ret;
        }

        private byte[] ReturnQrCodeCarrier(String QrCodeCarrier)
        {
            var ret = new byte[40];
            if (!String.IsNullOrEmpty(QrCodeCarrier))
            {
                var arr = QrCodeCarrier.ToCharArray();
                int sz = arr.Length;
                if (sz > 40) { sz = 40; }
                for (int i = 0; i < sz; i++)
                {
                    ret[i] = (byte)arr[i];
                }
                for (int i = sz; i < 9; i++)
                {
                    ret[i] = 0;
                }
            }
            return ret;
        }
    }

}
