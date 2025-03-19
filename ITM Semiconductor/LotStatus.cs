using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
    class LotStatus : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public DateTime UpdatedTime { get; set; }
        public String LotId { get; set; }
        public int InputCount { get; set; }
        public int TotalCount { get; set; }
        public int NgCount { get; set; }
        public int OkCount { get; set; }
        public int EmCount { get; set; }
        public long totalSeconds { get; set; }

        public DateTime EndTime { get; set; }

        public String TotalSecondsString { get; private set; }
        public String YieldString { get; set; }

        public String TimeString { get; set; }

        private long _lastUpdateTime = 0;

        public LotStatus()
        {
            this.UpdatedTime = DateTime.Now;
            _lastUpdateTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
        }

        public LotStatus(String lotId)
        {
            this.LotId = lotId;
            this.UpdatedTime = DateTime.Now;
            _lastUpdateTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
        }

        public void updateCounter(int testCnt, int testNg, int emCnt = 0)
        {
            if (testCnt > 0 && testCnt >= testNg + emCnt)
            {
                this.TotalCount += testCnt;
                this.NgCount += testNg + emCnt;
                this.EmCount += emCnt;
                this.OkCount = this.TotalCount - this.NgCount;
                double yield = (this.OkCount * 100.0) / this.TotalCount;
                this.YieldString = String.Format("{0:N2}", yield);

                OnPropertyChanged("InputCount");
                OnPropertyChanged("TotalCount");
                OnPropertyChanged("NgCount");
                OnPropertyChanged("OkCount");
                OnPropertyChanged("YieldString");
            }
        }

        public void UpdateTime()
        {
            var dif = DateTime.Now.Ticks / TimeSpan.TicksPerSecond - _lastUpdateTime;
            this.totalSeconds += dif;
            var ts = TimeSpan.FromSeconds(this.totalSeconds);
            this.TotalSecondsString = String.Format("{0}h {1}m {2}s", ts.Hours, ts.Minutes, ts.Seconds);

            OnPropertyChanged("TotalSecondsString");
        }

        public void UpdateUI()
        {
            if (this.TotalCount > 0)
            {
                this.OkCount = this.TotalCount - this.NgCount;
                double yield = (this.OkCount * 100.0) / this.TotalCount;
                this.YieldString = String.Format("{0:N2}", yield);
            }

            OnPropertyChanged("InputCount");
            OnPropertyChanged("TotalCount");
            OnPropertyChanged("NgCount");
            OnPropertyChanged("OkCount");
            OnPropertyChanged("YieldString");
            OnPropertyChanged("TotalSecondsString");
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
