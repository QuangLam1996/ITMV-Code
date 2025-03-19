using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
    class SettingPLCCom
    {
        private String portName;
        private Int32 baudrate;
        private Int32 dataBits;
        private StopBits stopBits;
        private Parity parity;
        private Int32 address;
        private Handshake handshake;

        public string PortName { get => portName; set => portName = value; }
        public int Baudrate { get => baudrate; set => baudrate = value; }
        public int DataBits { get => dataBits; set => dataBits = value; }
        public StopBits StopBits { get => stopBits; set => stopBits = value; }
        public Parity Parity { get => parity; set => parity = value; }
        public int Address { get => address; set => address = value; }
        public Handshake Handshake { get => handshake; set => handshake = value; }
       

        public SettingPLCCom()
        {
            this.portName = "COM1";
            this.baudrate = 9600;
            this.dataBits = 8;
            this.stopBits = StopBits.One;
            this.Address = 1;
            this.parity = Parity.None;
        }
    }
}
