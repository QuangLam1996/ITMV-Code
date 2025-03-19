using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
    class ScannerManager
    {
        private static MyLogger logger = new MyLogger("ScannerManager");

        private static ScannerStatus scanner = new ScannerStatus();
        private volatile static Boolean counterChanged = false;

        public static void Init()
        {
            try
            {
                // Load data from DB:
                var db = DbRead.GetScannerStatus();
                if (db != null)
                {
                    scanner = db;
                }

                // Create update-counter thread:
                var threadUpdateCounter = new Thread(new ThreadStart(() => {
                    while (true)
                    {
                        if (counterChanged)
                        {
                            counterChanged = false;
                            DbWrite.updateScannerStatus(scanner);
                        }

                        // Check every 10s:
                        Thread.Sleep(1000 * 10);
                    }
                }));
                threadUpdateCounter.IsBackground = true;
                threadUpdateCounter.Start();
            }
            catch (Exception ex)
            {
                logger.Create("Init error:" + ex.Message,LogLevel.Error);
            }
        }

        public static ScannerStatus GetStatus()
        {
            return scanner;
        }

        public static void ResetCycle()
        {
            scanner.cycleCount = 0;
            scanner.UpdateUI();
            DbWrite.updateScannerStatus(scanner);
        }

        public static void ResetCalib()
        {
            scanner.calibCount = 0;
            scanner.UpdateUI();
            DbWrite.updateScannerStatus(scanner);
        }

        public static void UpdateCounters()
        {
            scanner.calibCount++;
            scanner.cycleCount++;
            scanner.UpdateUI();
            counterChanged = true;
            //DbWrite.updateScannerStatus(scanner);
        }
    }
}
