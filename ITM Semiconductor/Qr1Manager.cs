using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITM_Semiconductor;
using DTO;

namespace ITM_Semiconductor
{
    class Qr1Manager
    {
        private const int DB_UPDATE_CYCLE = 30;
        private static MyLogger logger = new MyLogger("Qr1Manager");
        private static System.Timers.Timer timer = new System.Timers.Timer(1000);
        private static VisionInspectionStatus qr1Status = new VisionInspectionStatus();
        private static volatile bool isAutoRunning = false;
        private static volatile bool isAlarming = false;

        private static volatile bool isAutoRunningCH2 = false;
        private static volatile bool isAlarmingCH2 = false;
        private static DateTime lastTime;
        private static int dbUpdateCnt = 0;
        public static void Init()
        {
            try
            {
                // Load data from DB:
                var db = DbRead.GetQr1Status1();
                if (db != null)
                {
                    qr1Status = db;
                }

                // Start update timer:
                timer.Elapsed += Timer_Elapsed;
                timer.Start();
                lastTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                logger.Create("Init error:" + ex.Message,LogLevel.Error);
            }
        }

        public static void Reset()
        {
            qr1Status = new VisionInspectionStatus();
            // DbWrite.updateQr1Status(qr1Status);
            qr1Status.UpdateUI();
        }

        public static VisionInspectionStatus GetQr1Status()
        {
            //qr1Status.UpdateUI();
            return qr1Status;
        }

        public static void StartEventCH1()
        {
            qr1Status.startCount++;
            if (!isAutoRunning)
            {
                isAutoRunning = true;
            }
            //DbWrite.updateQr1Status(qr1Status);
        }

        public static void StartEventCH2()
        {
            qr1Status.startCount++;
            if (!isAutoRunningCH2)
            {
                isAutoRunningCH2 = true;
            }
            //DbWrite.updateQr1Status(qr1Status);
        }

        public static void StopEventCH1()
        {
            qr1Status.stopCount++;
            if (isAutoRunning)
            {
                isAutoRunning = false;
            }
            //DbWrite.updateQr1Status(qr1Status);
        }
        public static void StopEventCH2()
        {
            qr1Status.stopCount++;
            if (isAutoRunningCH2)
            {
                isAutoRunningCH2 = false;
            }
            //DbWrite.updateQr1Status(qr1Status);
        }

        public static void AlarmBegin()
        {
            if (!isAlarming)
            {
                isAlarming = true;
            }
        }

        public static void AlarmBeginCH2()
        {
            if (!isAlarmingCH2)
            {
                isAlarmingCH2 = true;
            }
        }

        public static void AlarmEnd()
        {
            if (isAlarming)
            {
                isAlarming = false;
            }
        }
        public static void AlarmEndCH2()
        {
            if (isAlarmingCH2)
            {
                isAlarmingCH2 = false;
            }
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                var dif = DateTime.Now.Subtract(lastTime).TotalSeconds;
                lastTime = DateTime.Now;
                qr1Status.totalTime += dif;
                qr1Status.totalTimeCH2 += dif;
                if (isAutoRunning)
                {
                    if (isAlarming)
                    {
                        qr1Status.lossTime += dif;
                    }
                    else
                    {
                        qr1Status.runTime += dif;
                    }
                }
                else
                {
                    qr1Status.stopTime += dif;
                }

                if (isAutoRunningCH2)
                {
                    if (isAlarmingCH2)
                    {
                        qr1Status.lossTimeCH2 += dif;
                    }
                    else
                    {
                        qr1Status.runTimeCH2 += dif;
                    }
                }
                else
                {
                    qr1Status.stopTimeCH2 += dif;
                }
                // Update status:
                qr1Status.UpdateUI();

                // Store to DB: every 30s
                if (++dbUpdateCnt >= DB_UPDATE_CYCLE)
                {
                    dbUpdateCnt = 0;
                    //DbWrite.updateQr1Status(qr1Status);
                }
            }
            catch (Exception ex)
            {
                logger.Create("Timer_Elapsed error:" + ex.Message,LogLevel.Error);
            }
        }
    }
}
