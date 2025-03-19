using DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
     class MyLogger
    {
        private static Object objLock = new Object();

        private String prefix = "";

        public MyLogger(String prefix)
        {
            this.prefix = prefix;
        }

        public void Create(String content , LogLevel logLevel)
        {
            // Get FilePath:
            var fileName = String.Format("{0}.log", DateTime.Now.ToString("yyyy-MM-dd"));
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "DebugLogs", "Unit");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var filePath = System.IO.Path.Combine(folder, fileName);

            lock (objLock)
            {
                try
                {
                    var log = String.Format("\r\n{0}-[{1}]-{2}: {3}", DateTime.Now.ToString("HH:mm:ss.ff") ,logLevel.ToString(), this.prefix, content);

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
