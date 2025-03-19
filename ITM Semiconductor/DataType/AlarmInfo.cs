using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace ITM_Semiconductor
{
    public class AlarmInfo
    {
        #region Choose Mode Auto/ Manual
        public const int MODE_AUTO = 0;
        public const int MODE_MANUAL = 1;
        #endregion

        #region PLC Interface
        public const int WRITE__MULTIDOUBLEWORDS_NOT_COMPLETE = 1;
        public const int PLC_CAN_NOT_CONNECT = 2;
        public const int MES_CHECK_TIME_OUT = 10;
        public const int MES_CHECK_RETURN_INCORRECT_FORMAT = 11;
        #endregion




        private static Dictionary<int, String> alarmMessageMap = new Dictionary<int, string>() {
# region PLC Interface
          
            {WRITE__MULTIDOUBLEWORDS_NOT_COMPLETE, "Write Multi Double Words Not Complete."},
# endregion
       

            // PLC Connect PC
            {PLC_CAN_NOT_CONNECT, "PC không thể kết nối được PLC."},

            // MES CHECK TIME OUT
            {MES_CHECK_TIME_OUT, " MES_CHECK_TIME_OUT - PC không thể kết nối được tới MES."},

            //MES CHECK RETURN INCORRECT FORMAT
            {MES_CHECK_RETURN_INCORRECT_FORMAT, "Định dạng chuỗi dữ liệu MES trả về không đúng"},


        };
        private static Dictionary<int, String> alarmSolutionMap = new Dictionary<int, string>()
        {
# region PLC Interface
         


         
            {WRITE__MULTIDOUBLEWORDS_NOT_COMPLETE, "Write Data  Multi Double Words To PLC Incomplete"},
# endregion
            // Enter Mode Auto Break Mode Auto
          

             // PLC Connect PC
               {PLC_CAN_NOT_CONNECT, "- Kiểm tra xem PLC đã có nguồn điện chưa." +
                                  "\r\n- Kiểm tra địa chỉ IP dùng để kết nối đến Máy tính." +
                                  "\r\n- Kiểm tra dây mạng."},

               {MES_CHECK_TIME_OUT, "  - Kiểm tra địa chỉ IP dùng để kết nối đến Máy tính." +
                                  "\r\n- Kiểm tra dây mạng." +
                                  "\r\n- Nếu không được , Liên hệ Bộ phận IT , Kỹ thuật để xử lý"},

                {MES_CHECK_RETURN_INCORRECT_FORMAT, "  - Liên hệ với bộ phận IT và Kỹ thuật để xử lý"},



        };

        public static string getMessage(int alarmType)
        {
            string ret;
            if (!alarmMessageMap.TryGetValue(alarmType, out ret))
            {
                ret = "-";
            }
            return ret;
        }

        public static String getSolution(int alarmType)
        {
            String ret;
            if (!alarmSolutionMap.TryGetValue(alarmType, out ret))
            {
                ret = "-";
            }
            return ret;
        }
        public int id { get; set; }
        public int alarmCode { get; set; }
        public DateTime createdTime { get; set; }
        public int mode { get; set; }
        public string message { get; set; }
        public string solution { get; set; }

        public String getMode()
        {
            if (mode == MODE_AUTO)
            {
                return "AUTO";
            }
            return "MANUAL";
        }
        public AlarmInfo() { }

        public AlarmInfo( int code, String msg, String sol)
        {
           // this.id = 0;
           // this.mode = mode;
            this.alarmCode = code;
            this.createdTime = DateTime.Now;
            this.message = msg;
            this.solution = sol;
        }
    }
}
