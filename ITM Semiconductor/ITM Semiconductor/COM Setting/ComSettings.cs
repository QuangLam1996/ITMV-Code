using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
    public class ComSettings
    {
        public String portName { get; set; }
        public Int32 baudrate { get; set; }
        public Int32 dataBits { get; set; }
        public StopBits stopBits { get; set; }
        public Parity parity { get; set; }

        // "COM1,9600,8,One,None"
        public static ComSettings Parse(String s)
        {
            var ret = new ComSettings();
            try
            {
                if (s != null)
                {
                    var arr = s.Split(',');
                    if (arr.Length >= 5)
                    {
                        ret.portName = arr[0];
                        ret.baudrate = int.Parse(arr[1]);
                        ret.dataBits = int.Parse(arr[2]);
                        ret.stopBits = ParseStopBits(arr[3]);
                        ret.parity = ParseParity(arr[4]);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(" -> ComSettings Pase error:" + ex.Message);
            }
            return ret;
        }
        // xoá mặch định
        public static ComSettings Clone(ComSettings settings)
        {
            var ret = new ComSettings();
            if (settings == null)
            {
                return null;
            }
            ret.portName = settings.portName;
            ret.baudrate = settings.baudrate;
            ret.dataBits = settings.dataBits;
            ret.stopBits = settings.stopBits;
            ret.parity = settings.parity;
            return ret;
        }

        public static StopBits ParseStopBits(string s)
        {
            if (s == null)
            {
                return StopBits.None;
            }
            if (s.Equals("One"))
            {
                return StopBits.One;
            }
            else if (s.Equals("Two"))
            {
                return StopBits.Two;
            }
            return StopBits.None;
        }

        public static Parity ParseParity(string s)
        {
            if (s == null)
            {
                return Parity.None;
            }
            if (s.Equals("Even"))
            {
                return Parity.Even;
            }
            else if (s.Equals("Odd"))
            {
                return Parity.Odd;
            }
            else if (s.Equals("Mark"))
            {
                return Parity.Mark;
            }
            else if (s.Equals("Space"))
            {
                return Parity.Space;
            }
            return Parity.None;
        }

        public ComSettings()
        {
            this.portName = "COM1";
            this.baudrate = 9600;
            this.dataBits = 8;
            this.stopBits = StopBits.One;
            this.parity = Parity.None;
        }

        public override String ToString()
        {
            return String.Format("{0},{1},{2},{3},{4}", portName, baudrate, dataBits, stopBits, parity);
        }

        public ComSettings Clone()
        {
            return new ComSettings
            {
                portName = this.portName,
                baudrate = this.baudrate,
                dataBits = this.dataBits,
                stopBits = this.stopBits,
                parity = this.parity
            };
        }
    }
}
