using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
     class Setting_PLCTcp
    {
        private String Ip;
        private Int32 Port;


        public string Ip1 { get => Ip; set => Ip = value; }
        public int Port1 { get => Port; set => Port = value; }

        public Setting_PLCTcp() 
        { 
            this.Ip = "127.0.0.1";
            this.Port = 6000;
        } 
    }
}
