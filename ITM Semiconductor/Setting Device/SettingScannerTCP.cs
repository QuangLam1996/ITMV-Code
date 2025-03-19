using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
   class SettingScannerTCP
    {
        private String IpAdress;
        private Int32 Port;

        public string IpAdress1 { get => IpAdress; set => IpAdress = value; }
        public int Port1 { get => Port; set => Port = value; }


        public SettingScannerTCP()
        {
            this.IpAdress = "192.168.0.1";
            this.Port = 1000;
        }
    }

}
