using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
    class MesLogger
    {
        private static Object objLock = new Object();
        public void Create(String Model  ,String QRJIG ,String QRPCB,String Result, String LogType)
        {
            String LotID = UiManager.appSettings.lotData.LotId;
            String Config = UiManager.appSettings.lotData.Config;
            string MES;
            if (UiManager.appSettings.run.mesOnline)
            {
                MES = "Mes Online";
            }    
            else
            {
                MES = "Mes Offline";
            }
            string MesLogType;
            if (LogType == "IL")
            {
                MesLogType = "MesIL";
            }
            else if (LogType == "MIX")
            {
                MesLogType = "MesMix";
            }
            else
            {
                // Default case if neither "IL" nor "Mix" is provided
                MesLogType = "Unknown";
            }
            // Get FilePath:
            var fileName = String.Format("{0}-{1}.log", DateTime.Now.ToString("yyyy-MM-dd"),MesLogType);
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "MesLogs", $"{LotID}");
            var filePath = System.IO.Path.Combine(folder, fileName);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            bool fileExists = File.Exists(filePath);
            lock (objLock)
            {
                try
                {
                    if (!fileExists)
                    {
                        var header = "Time , Model , Lot ID , Config , QR Code JIG , QR Code PCB , Result OK/NG , Mes Online/Mes Offline";
                        using (var strWriter = new StreamWriter(filePath, true))
                        {
                            strWriter.WriteLine(header);
                            strWriter.Flush();
                        }
                    }
                    // 0 :Time
                    // 1 :Model
                    // 2 :LotID
                    // 3 :Config
                    // 4 :QR CODE JIG 
                    // 5 :QR CODE PCB
                    // 6 :RESULT OK/NG
                    // 7 :MES ONLINE / MES OFFLINE
                    var log = String.Format("\r\n{0},{1},{2},{3},{4},{5},{6},{7}", DateTime.Now.ToString("HH:mm:ss.ff"),Model,LotID,Config,QRJIG,QRPCB,Result,MES);

                    System.Diagnostics.Debug.Write(log);

                    using (var strWriter = new StreamWriter(filePath, true))
                    {
                        strWriter.Write(log);
                        strWriter.Flush();
                    }
                }
                catch (Exception ex)
                {
                    Debug.Write("\r\nMyLoger.Create error:" + ex.Message);
                }
            }
        }
    }
}
