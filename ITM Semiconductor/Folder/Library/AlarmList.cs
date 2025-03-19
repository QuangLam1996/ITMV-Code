using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OfficeOpenXml;
namespace ITM_Semiconductor
{
    //class AlarmList
    //{
    //    public static Dictionary<int, string> _AlarmList = new Dictionary<int, string>();
    //    public static Dictionary<int, string> _SolutionList = new Dictionary<int, string>();

    //    public static void LoadAlarm()
    //    {
    //        string Alarmpath = AppDomain.CurrentDomain.BaseDirectory + Properties.Settings.Default.alarmList;
    //        if (!File.Exists(Alarmpath))
    //            return;
    //        string[] lines = File.ReadAllLines(Alarmpath);
    //        foreach (string line in lines)
    //        {
    //            if (string.IsNullOrWhiteSpace(line))
    //                continue;
    //            string[] text = line.Split(',');
    //            _AlarmList.Add(Convert.ToInt32(text[0].ToString()), text[1]);
    //            _SolutionList.Add(Convert.ToInt32(text[0].ToString()), text[2]);
    //        }
    //    }
    //    public static string GetMes(int AlarmKey)
    //    {
    //        if (!_AlarmList.ContainsKey(AlarmKey))
    //            return "";
    //        string mes = "";
    //        _AlarmList.TryGetValue(AlarmKey, out mes);
    //        return mes;
    //    }
    //    public static string GetSolution(int AlarmKey)
    //    {
    //        if (!_SolutionList.ContainsKey(AlarmKey))
    //            return "";
    //        string solution = "";
    //        _SolutionList.TryGetValue(AlarmKey, out solution);
    //        return solution;
    //    }

    //}




    class AlarmList
    {
        public static Dictionary<int, string> _AlarmList = new Dictionary<int, string>();
        public static Dictionary<int, string> _SolutionList = new Dictionary<int, string>();

        public static Dictionary<int, string> _NameButton = new Dictionary<int, string>();
        public static Dictionary<int, string> _DeviceButton = new Dictionary<int, string>();
        public static Dictionary<int, string> _DeviceCodeButton = new Dictionary<int, string>();

        public static Dictionary<int, string> _DeviceLampButton = new Dictionary<int, string>();
        public static Dictionary<int, string> _DeviceCodeLampButton = new Dictionary<int, string>();


        public static void LoadAlarm()
        {
            string Alarmpath = AppDomain.CurrentDomain.BaseDirectory + Properties.Settings.Default.alarmList;
            if (!File.Exists(Alarmpath))
                return;

            // Đảm bảo rằng EPPlus được cấp 
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(Alarmpath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Đọc sheet đầu tiên
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++) // Bắt đầu từ hàng thứ 2, giả sử hàng đầu tiên là tiêu đề
                {
                    int alarmKey;
                    if (int.TryParse(worksheet.Cells[row, 1].Text, out alarmKey))
                    {
                        string alarmMessage = worksheet.Cells[row, 2].Text.Replace("\\n", Environment.NewLine);
                        string solutionMessage = worksheet.Cells[row, 3].Text.Replace("\\n", Environment.NewLine);

                        string NameButton = worksheet.Cells[row, 4].Text.Replace("\\n", Environment.NewLine);
                        string DeviceButton = worksheet.Cells[row, 6].Text.Replace("\\n", Environment.NewLine);
                        string DeviceCodeButton = worksheet.Cells[row, 5].Text.Replace("\\n", Environment.NewLine);

                        string DeviceLampButton = worksheet.Cells[row, 8].Text.Replace("\\n", Environment.NewLine);
                        string DeviceCodeLampButton = worksheet.Cells[row, 7].Text.Replace("\\n", Environment.NewLine);

                        _AlarmList[alarmKey] = alarmMessage;
                        _SolutionList[alarmKey] = solutionMessage;

                        _NameButton[alarmKey]= NameButton;
                        _DeviceButton[alarmKey]= DeviceButton;
                        _DeviceCodeButton[alarmKey]= DeviceCodeButton;

                        _DeviceLampButton[alarmKey] = DeviceLampButton;
                        _DeviceCodeLampButton[alarmKey] = DeviceCodeLampButton;
                    }
                }
            }
        }

        public static string GetMes(int AlarmKey)
        {
            if (!_AlarmList.ContainsKey(AlarmKey))
                return "";
            return _AlarmList[AlarmKey];
        }

        public static string GetSolution(int AlarmKey)
        {
            if (!_SolutionList.ContainsKey(AlarmKey))
                return "";
            return _SolutionList[AlarmKey];
        }




        public static string GetNameButton(int AlarmKey)
        {
            if (!_NameButton.ContainsKey(AlarmKey))
                return "";
            return _NameButton[AlarmKey];
        }

        public static string GetDeviceButton(int AlarmKey)
        {
            if (!_DeviceButton.ContainsKey(AlarmKey))
                return "";
            return _DeviceButton[AlarmKey];
        }

        public static string GetDeviceCodeButton(int AlarmKey)
        {
            if (!_DeviceCodeButton.ContainsKey(AlarmKey))
                return "";
            return _DeviceCodeButton[AlarmKey];
        }



        public static string GetDeviceLampButton(int AlarmKey)
        {
            if (!_DeviceButton.ContainsKey(AlarmKey))
                return "";
            return _DeviceLampButton[AlarmKey];
        }

        public static string GetDeviceCodeLampButton(int AlarmKey)
        {
            if (!_DeviceCodeButton.ContainsKey(AlarmKey))
                return "";
            return _DeviceCodeLampButton[AlarmKey];
        }


    }




}
