using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
    public class DataChecking
    {
         #region Common
            public string RECIPE { get; set; } = string.Empty;
            public string CONFIG { get; set; } = string.Empty;
            public string LOT_NUMBER { get; set; } = string.Empty;
            #endregion

            #region CODE IL
            public string QrCodeJigIL { get; set; } = string.Empty;
            public string QrCodeFPCBIL { get; set; } = string.Empty;
            #endregion

            #region CODE MIX
            public string QrCodeJigMIX { get; set; } = string.Empty;
            public string QrCodeFPCBMIX { get; set; } = string.Empty;
            #endregion

            public DateTime TimeCheck { get; set; } = DateTime.Now;
            public string OnlineChecking { get; set; }
            public bool ResultCheckMES { get; set; }
        }
   
}
