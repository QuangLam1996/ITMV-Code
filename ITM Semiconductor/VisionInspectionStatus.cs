using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
    class VisionInspectionStatus : INotifyPropertyChanged
    {
        public String MtbaString { get; set; } = "";
        public String MtbfString { get; set; } = "";
        public String IndexString { get; set; } = "";
        public String TotalString { get; set; }
        public String LossString { get; set; }
        public String StopString { get; set; }
        public String RunString { get; set; }
        public String TotalStringCH2 { get; set; }
        public String LossStringCH2 { get; set; }
        public String StopStringCH2 { get; set; }
        public String RunStringCH2 { get; set; }

        public double totalTime { get; set; }
        public double lossTime { get; set; }
        public double stopTime { get; set; }

        public double runTime { get; set; }
        public double totalTimeCH2 { get; set; }
        public double lossTimeCH2 { get; set; }
        public double stopTimeCH2 { get; set; }

        public double runTimeCH2 { get; set; }

        public double alarmTime { get; set; }
        public int startCount { get; set; }
        public int stopCount { get; set; }

        public String ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static VisionInspectionStatus FromJSON(String js)
        {
            var j = JsonConvert.DeserializeObject<VisionInspectionStatus>(js);
            return j;
        }

        public void UpdateUI()
        {
            var ts = TimeSpan.FromSeconds(this.totalTime);
            this.TotalString = String.Format("{0}h {1:D2}m {2:D2}s", ts.Hours, ts.Minutes, ts.Seconds);
            ts = TimeSpan.FromSeconds(this.lossTime);
            this.LossString = String.Format("{0}h {1:D2}m {2:D2}s", ts.Hours, ts.Minutes, ts.Seconds);
            ts = TimeSpan.FromSeconds(this.stopTime);
            this.StopString = String.Format("{0}h {1:D2}m {2:D2}s", ts.Hours, ts.Minutes, ts.Seconds);

            ts = TimeSpan.FromSeconds(this.runTime);
            this.RunString = String.Format("{0}h {1:D2}m {2:D2}s", ts.Hours, ts.Minutes, ts.Seconds);

            ts = TimeSpan.FromSeconds(this.totalTimeCH2);
            this.TotalStringCH2 = String.Format("{0}h {1:D2}m {2:D2}s", ts.Hours, ts.Minutes, ts.Seconds);
            ts = TimeSpan.FromSeconds(this.lossTimeCH2);
            this.LossStringCH2 = String.Format("{0}h {1:D2}m {2:D2}s", ts.Hours, ts.Minutes, ts.Seconds);
            ts = TimeSpan.FromSeconds(this.stopTimeCH2);
            this.StopStringCH2 = String.Format("{0}h {1:D2}m {2:D2}s", ts.Hours, ts.Minutes, ts.Seconds);

            ts = TimeSpan.FromSeconds(this.runTimeCH2);
            this.RunStringCH2 = String.Format("{0}h {1:D2}m {2:D2}s", ts.Hours, ts.Minutes, ts.Seconds);

            if (this.startCount > 0)
            {
                var mtba = this.totalTime / this.startCount;
                ts = TimeSpan.FromSeconds(mtba);
                this.MtbaString = String.Format("{0}h {1:D2}m {2:D2}s", ts.Hours, ts.Minutes, ts.Seconds);
            }
            if (this.stopCount > 0)
            {
                var mtbf = this.stopTime / this.stopCount;
                ts = TimeSpan.FromSeconds(mtbf);
                this.MtbfString = String.Format("{0}h {1:D2}m {2:D2}s", ts.Hours, ts.Minutes, ts.Seconds);
            }

            OnPropertyChanged("MtbaString");
            OnPropertyChanged("MtbfString");
            OnPropertyChanged("IndexString");
            OnPropertyChanged("TotalString");
            OnPropertyChanged("LossString");
            OnPropertyChanged("StopString");
            OnPropertyChanged("RunString");
            OnPropertyChanged("TotalStringCH2");
            OnPropertyChanged("LossStringCH2");
            OnPropertyChanged("StopStringCH2");
            OnPropertyChanged("RunStringCH2");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
