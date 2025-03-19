using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
    class ScannerStatus : INotifyPropertyChanged
    {
        public int cycleCount { get; set; }
        public int calibCount { get; set; }

        public static ScannerStatus FromJSON(String js)
        {
            var j = JsonConvert.DeserializeObject<ScannerStatus>(js);
            return j;
        }

        public String ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void UpdateUI()
        {
            OnPropertyChanged("cycleCount");
            OnPropertyChanged("calibCount");
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
