using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
    public class AlarmInfo
    {
        #region Choose Mode Auto/ Manual
        public const int MODE_AUTO = 0;
        public const int MODE_MANUAL = 1;
        #endregion

        #region PLC Interface
        public const int CANNOT_READ_DATA_TYPE_WORD_IN_MODE_READ_BIT = 10;
        public const int ADDRESS_START_READ_DATA_ERROR = 11;
        public const int ADDRESS_COUNT_READ_DATA_ERROR = 12;
        public const int READ_DATA_MULITBITS_ERROR = 13;
        public const int READ_DATA_MULITBITS_WORD_ERROR = 14;
        public const int READ_DATA_WORD_ERROR = 15;
        public const int READ_DATA_MULTIWORDS_ERROR = 16;
        public const int READ_DATA_MULTIDOUBLEWORDS_ERROR = 17;

        public const int CANNOT_WRITE_DATA_TYPE_WORD_IN_MODE_WRITE_BIT = 20;
        public const int ADDRESS_START_WRITE_DATA_ERROR = 21;
        public const int NOT_INPUT_DATA_WRITE = 22;

        public const int DATA_WRITE_BIT_ERROR = 23;
        public const int WRITE_BIT_NOT_COMPLETE = 24;

        public const int DATA_WRITE_MULTI_BITS_ERROR = 25;
        public const int WRITE_MULTI_BITS_NOT_COMPLETE = 26;

        public const int DATA_WRITE_DOUBLEWORD_ERROR = 27;
        public const int WRITE_DOUBLEWORD_NOT_COMPLETE = 28;

        public const int DATA_WRITE_WORD_ERROR = 29;
        public const int WRITE_WORD_NOT_COMPLETE = 30;

        public const int DATA_WRITE_MULTIWORDS_ERROR = 31;
        public const int WRITE__MULTIWORDS_NOT_COMPLETE = 32;

        public const int DATA_WRITE_MULTIDOUBLEWORDS_ERROR = 33;
        public const int WRITE__MULTIDOUBLEWORDS_NOT_COMPLETE = 34;
        #endregion

        #region Error Default Of Machine
        // Enter Mode Auto Break Mode Auto
        public const int ENTER_MODE_AUTO = 1100;
        public const int EXIT_MODE_AUTO = 1101;
        public const int RESET_MODE_AUTO = 1102;

        public const int PLC_CAN_NOT_CONNECT = 1200;
        #endregion

        #region Error Machine
        public const int ERROR_CENTERING = 1114;
        public const int ERROR_PICKUP_PCB_HAND_ROBOT = 1118;
        public const int ERROR_PICKUP_SUS_HAND_ROBOT = 1128;
        #endregion 

        private static Dictionary<int, String> alarmMessageMap = new Dictionary<int, string>() {
# region PLC Interface
            {CANNOT_READ_DATA_TYPE_WORD_IN_MODE_READ_BIT, "Canot Read Data Type Word In Mode Read Bit."},
            {ADDRESS_START_READ_DATA_ERROR, "Address Start Read Data Not Correct."},
            {ADDRESS_COUNT_READ_DATA_ERROR, "Address Count Read Data Not Correct."},
            {READ_DATA_MULITBITS_ERROR, "Read Data Multi Bits Error."},
            {READ_DATA_MULITBITS_WORD_ERROR, "Read Data Multi Bits * 16 Error."},
            {READ_DATA_WORD_ERROR, "Read Data Word Error."},
            {READ_DATA_MULTIWORDS_ERROR, "Read Data Multis Word Error."},
            {READ_DATA_MULTIDOUBLEWORDS_ERROR, "Read Data Multis Double Word Error."},

            {CANNOT_WRITE_DATA_TYPE_WORD_IN_MODE_WRITE_BIT, "Canot Write Data Type Word In Mode Write Bit."},
            {ADDRESS_START_WRITE_DATA_ERROR, "Address Start Write Data Not Correct."},
            {NOT_INPUT_DATA_WRITE, "Not Input Data Write."},

            {DATA_WRITE_BIT_ERROR, "Data Write Bit Not Correct Fomat."},
            {WRITE_BIT_NOT_COMPLETE, "Write Bit Not Complete."},

            {DATA_WRITE_MULTI_BITS_ERROR, "Data Write Multi Bits Not Correct Fomat."},
            {WRITE_MULTI_BITS_NOT_COMPLETE, "Write Multi Bits Not Complete."},

            {DATA_WRITE_DOUBLEWORD_ERROR, "Data Write Double Word Not Correct Fomat."},
            {WRITE_DOUBLEWORD_NOT_COMPLETE, "Write Double Word Not Complete."},

            {DATA_WRITE_WORD_ERROR, "Data Write Word Not Correct Fomat."},
            {WRITE_WORD_NOT_COMPLETE, "Write Word Not Complete."},

            {DATA_WRITE_MULTIWORDS_ERROR, "Data Write Multi Words Not Correct Fomat."},
            {WRITE__MULTIWORDS_NOT_COMPLETE, "Write Multi Words Not Complete."},

            {DATA_WRITE_MULTIDOUBLEWORDS_ERROR, "Data Write Multi Double Words Not Correct Fomat."},
            {WRITE__MULTIDOUBLEWORDS_NOT_COMPLETE, "Write Multi Double Words Not Complete."},
# endregion
            // Enter Mode Auto Break Mode Auto
            {ENTER_MODE_AUTO, "Write Bit Start chế độ Auto Mode lỗi."},
            {EXIT_MODE_AUTO, "Write Bit Stop chế độ Auto Mode lỗi." },
            {RESET_MODE_AUTO, "Write Bit Reset chế độ Auto Mode lỗi." },

            // PLC Connect PC
            {PLC_CAN_NOT_CONNECT, "PC không thể kết nối được PLC."},

#region Update Error From PLC
            {ERROR_CENTERING, "Vacumm khu vực Centering không đạt áp lực." },
            {ERROR_PICKUP_PCB_HAND_ROBOT, "Vacumm khu vực tay Robot khi hút PCB không đạt áp lực." },
            {ERROR_PICKUP_SUS_HAND_ROBOT, "Vacumm khu vực tay Robot khi hút SUS không đạt áp lực." },
# endregion
        };
        private static Dictionary<int, String> alarmSolutionMap = new Dictionary<int, string>()
        {
# region PLC Interface
           {CANNOT_READ_DATA_TYPE_WORD_IN_MODE_READ_BIT, "- Check Again Bit Device You Need Action." +
                                                         "\r\n- Please Read Manual Book Of MC Protocol."},
           {ADDRESS_START_READ_DATA_ERROR, "Please Input Start Read Data Correct."},
           {ADDRESS_COUNT_READ_DATA_ERROR, "Please Input Count Read Data Correct."},
           {READ_DATA_MULITBITS_ERROR, "- Check Error In PLC." +
                                        "\r\n- Please Read Manual Book Of MC Protocol."},
           {READ_DATA_MULITBITS_WORD_ERROR, "- Check Error In PLC." +
                                        "\r\n- Please Read Manual Book Of MC Protocol."},
           {READ_DATA_WORD_ERROR,        "- Check Error In PLC." +
                                        "\r\n- Please Read Manual Book Of MC Protocol."},
           {READ_DATA_MULTIWORDS_ERROR, "- Check Error In PLC." +
                                        "\r\n- Please Read Manual Book Of MC Protocol."},
           {READ_DATA_MULTIDOUBLEWORDS_ERROR, "- Check Error In PLC." +
                                        "\r\n- Please Read Manual Book Of MC Protocol."},


            {CANNOT_WRITE_DATA_TYPE_WORD_IN_MODE_WRITE_BIT, "- Check Again Bit Device You Need Action." +
                                                         "\r\n- Please Read Manual Book Of MC Protocol."},
            {ADDRESS_START_WRITE_DATA_ERROR, "Please Input Start Write Data Correct."},
            {NOT_INPUT_DATA_WRITE, "Please Input Data Write"},

            {DATA_WRITE_BIT_ERROR, "Data Write Bit Is (True Or False)."},
            {WRITE_BIT_NOT_COMPLETE, "Write Data Bit To PLC Incomplete"},

            {DATA_WRITE_MULTI_BITS_ERROR, "Data Write Multi Bits Is (True Or False)."},
            {WRITE_MULTI_BITS_NOT_COMPLETE, "Write Data Multi Bits To PLC Incomplete"},

            {DATA_WRITE_DOUBLEWORD_ERROR, "Data Write Double Word Is {Uint32 Type}."},
            {WRITE_DOUBLEWORD_NOT_COMPLETE, "Write Data Double Word To PLC Incomplete"},

            {DATA_WRITE_WORD_ERROR,  "Data Write Word Is {Uint16 Type}."},
            {WRITE_WORD_NOT_COMPLETE, "Write Data Word To PLC Incomplete"},

            {DATA_WRITE_MULTIWORDS_ERROR, "Data Write Multi Words Is {Uint16 Type}."},
            {WRITE__MULTIWORDS_NOT_COMPLETE, "Write Data  Multi Words To PLC Incomplete"},

            {DATA_WRITE_MULTIDOUBLEWORDS_ERROR, "Data Write Multi Double Words Is {Uint32 Type}."},
            {WRITE__MULTIDOUBLEWORDS_NOT_COMPLETE, "Write Data  Multi Double Words To PLC Incomplete"},
# endregion
            // Enter Mode Auto Break Mode Auto
            {ENTER_MODE_AUTO,     "- Kiểm tra xem PLC đã có nguồn điện chưa." +
                                  "\r\n- Kiểm tra địa chỉ IP dùng để kết nối đến Máy tính." +
                                  "\r\n- Kiểm tra phương thức kết nối giữa PLC và Máy tính." +
                                  "\r\n- Kiểm tra dây mạng."},

            {EXIT_MODE_AUTO,      "- Kiểm tra xem PLC đã có nguồn điện chưa." +
                                  "\r\n- Kiểm tra địa chỉ IP dùng để kết nối đến Máy tính." +
                                  "\r\n- Kiểm tra phương thức kết nối giữa PLC và Máy tính." +
                                  "\r\n- Kiểm tra dây mạng."},

             {RESET_MODE_AUTO,    "- Kiểm tra xem PLC đã có nguồn điện chưa." +
                                  "\r\n- Kiểm tra địa chỉ IP dùng để kết nối đến Máy tính." +
                                  "\r\n- Kiểm tra phương thức kết nối giữa PLC và Máy tính." +
                                  "\r\n- Kiểm tra dây mạng."},

             // PLC Connect PC
               {PLC_CAN_NOT_CONNECT, "- Kiểm tra xem PLC đã có nguồn điện chưa." +
                                  "\r\n- Kiểm tra địa chỉ IP dùng để kết nối đến Máy tính." +
                                  "\r\n- Kiểm tra dây mạng."},

#region Update Error From PLC
            {ERROR_CENTERING, " - kiểm tra xem đầu Vacumm Pad có bị hỏng không." +
                              "\r\n- Kiểm tra có khí đầu vào chưa." +
                              "\r\n- Kiểm tra bộ tạo khí hút ở khu vực Centering có bị hỏng không."+
                              "\r\n- Kiểm tra tín hiệu X134, X135, X136, X137, X200."},
            {ERROR_PICKUP_PCB_HAND_ROBOT, " - kiểm tra xem đầu Vacumm Pad có bị hỏng không." +
                              "\r\n- Kiểm tra có khí đầu vào chưa." +
                              "\r\n- Kiểm tra bộ tạo khí hút ở khu vực Tay Robot khi hút PCB có bị hỏng không."+
                              "\r\n- Kiểm tra tín hiệu X140, X141, X142, X143, X144, X145, X146, X147, X150, X151."},
            {ERROR_PICKUP_SUS_HAND_ROBOT," - kiểm tra xem đầu Vacumm Pad có bị hỏng không." +
                              "\r\n- Kiểm tra có khí đầu vào chưa." +
                              "\r\n- Kiểm tra bộ tạo khí hút ở khu vực Tay Robot khi hút SUS có bị hỏng không."+
                              "\r\n- Kiểm tra tín hiệu X152."},
# endregion
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
