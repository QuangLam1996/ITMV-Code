using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
     class StatusMachine
    {

        public static Dictionary<int, string> _StatusList = new Dictionary<int, string>();
        public static Dictionary<int, string> _StatusList01 = new Dictionary<int, string>();

        public static void LoadStatus()
        {
            string Alarmpath = AppDomain.CurrentDomain.BaseDirectory + Properties.Settings.Default.StatusMachine;
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
                    int StatusKey;
                    if (int.TryParse(worksheet.Cells[row, 1].Text, out StatusKey))
                    {
                        string StatusMessage = worksheet.Cells[row, 2].Text.Replace("\\n", Environment.NewLine);
                        string StatusMessage01 = worksheet.Cells[row, 3].Text.Replace("\\n", Environment.NewLine);

                        _StatusList[StatusKey] = StatusMessage;
                        _StatusList01[StatusKey] = StatusMessage01;
                    }
                }
            }
        }

        public static string GetMes(int StatusKey)
        {
            if (!_StatusList.ContainsKey(StatusKey))
                return "";
            return _StatusList[StatusKey];
        }

        public static string GetSolution(int StatusKey)
        {
            if (!_StatusList01.ContainsKey(StatusKey))
                return "";
            return _StatusList01[StatusKey];
        }
    }
}

