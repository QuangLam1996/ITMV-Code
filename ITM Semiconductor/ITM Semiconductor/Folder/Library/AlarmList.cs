using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ITM_Semiconductor
{
    class AlarmList
    {
        public static Dictionary<int, string> _AlarmList = new Dictionary<int, string>();
        public static Dictionary<int, string> _SolutionList = new Dictionary<int, string>();

        public static void LoadAlarm()
        {
            string Alarmpath = AppDomain.CurrentDomain.BaseDirectory + Properties.Settings.Default.alarmList;
            if (!File.Exists(Alarmpath))
                return;
            string[] lines = File.ReadAllLines(Alarmpath);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                string[] text = line.Split(',');
                _AlarmList.Add(Convert.ToInt32(text[0].ToString()), text[1]);
                _SolutionList.Add(Convert.ToInt32(text[0].ToString()), text[2]);
            }
        }
        public static string GetMes(int AlarmKey)
        {
            if (!_AlarmList.ContainsKey(AlarmKey))
                return "";
            string mes = "";
            _AlarmList.TryGetValue(AlarmKey, out mes);
            return mes;
        }
        public static string GetSolution(int AlarmKey)
        {
            if (!_SolutionList.ContainsKey(AlarmKey))
                return "";
            string solution = "";
            _SolutionList.TryGetValue(AlarmKey, out solution);
            return solution;
        }

    }
}
