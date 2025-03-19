using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
    class EventLog
    {
        // Page Main
        public const int EV_AUTO_START_FAILED = 0;
        public const int EV_AUTO_STOP_FAILED = 1;
        public const int EV_AUTO_RESET_FAILED = 2;
        public const int EV_MES_READY_TIMEOUT = 30;
        public const int EV_MES_CHECK_TIMEOUT = 31;
        // Event Log Of MC Protocol
        public const int EV_MCPROTOCOL_READ_BIT_ERROR = 50;
        public const int EV_MCPROTOCOL_READ_MULTI_BITS_ERROR = 51;
        public const int EV_MCPROTOCOL_READ_MULTI_BITS_16_ERROR = 52;
        public const int EV_MCPROTOCOL_READ_WORD_ERROR = 53;
        public const int EV_MCPROTOCOL_READ_MULTI_WORDS_ERROR = 54;
        public const int EV_MCPROTOCOL_READ_MULTI_DOUBLE_WORDS_ERROR = 55;

        public const int EV_MCPROTOCOL_WRITE_BIT_ERROR = 60;
        public const int EV_MCPROTOCOL_WRITE_MULTI_BITS_ERROR = 61;
        public const int EV_MCPROTOCOL_WRITE_DOUBLE_WORD_ERROR = 62;
        public const int EV_MCPROTOCOL_WRITE_WORD_ERROR = 63;
        public const int EV_MCPROTOCOL_WRITE_MULTI_WORDS_ERROR = 64;
        public const int EV_MCPROTOCOL_WRITE_MULTI_DOUBLE_WORDS_ERROR = 65;

        // Event Log Of MC Protocol Name And Data
        public const int EV_MCPROTOCOL_PLC_IS_NOT_CONNECT = 70;
        public const int EV_MCPROTOCOL_DEVICE_NAME_NOT_CORRECT = 71;
        public const int EV_MCPROTOCOL_ADDRESS_NOT_CORRECT = 72;
        public const int EV_MCPROTOCOL_DATA_RESPONSE_NOT_CORRECT = 73;
        public const int EV_MCPROTOCOL_CANNOT_TYPE_WORD_IN_BIT = 74;
        public const int EV_MCPROTOCOL_WRITE_DATA_FALSE = 75;
        public const int EV_MCPROTOCOL_DATA_TRANS_NOT_CORRECT = 76;

        public int Id { get; set; }
        public String CreatedTime { get; set; }
        public String Message { get; set; }
        public int EventType { get; set; }

        public EventLog() { }

        public EventLog(int ev)
        {
            this.Id = 0;
            this.EventType = ev;
            this.CreatedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff");
            this.Message = getMessageFromEvent(ev);
        }
        private static String getMessageFromEvent(int ev)
        {
            switch (ev)
            {
                // Event Log Of MC Protocol
                case EV_MCPROTOCOL_READ_BIT_ERROR:
                    return "PLC Read Bit Failed";
                case EV_MCPROTOCOL_READ_MULTI_BITS_ERROR:
                    return "PLC Read Multi Bits Failed";
                case EV_MCPROTOCOL_READ_MULTI_BITS_16_ERROR:
                    return "PLC Read Multi Bits 16 Failed";
                case EV_MCPROTOCOL_READ_WORD_ERROR:
                    return "PLC Read Word Failed";
                case EV_MCPROTOCOL_READ_MULTI_WORDS_ERROR:
                    return "PLC Read Multi Words Failed";
                case EV_MCPROTOCOL_READ_MULTI_DOUBLE_WORDS_ERROR:
                    return "PLC Read Multi Double Words Failed";
                case EV_MCPROTOCOL_WRITE_BIT_ERROR:
                    return "PLC Write Bit Failed";
                case EV_MCPROTOCOL_WRITE_MULTI_BITS_ERROR:
                    return "PLC Write Multi Bits Failed";
                case EV_MCPROTOCOL_WRITE_DOUBLE_WORD_ERROR:
                    return "PLC Write Double Words Failed";
                case EV_MCPROTOCOL_WRITE_WORD_ERROR:
                    return "PLC Write Word Failed";
                case EV_MCPROTOCOL_WRITE_MULTI_WORDS_ERROR:
                    return "PLC Write Multi Words Failed";
                case EV_MCPROTOCOL_WRITE_MULTI_DOUBLE_WORDS_ERROR:
                    return "PLC Write Multi Double Words Failed";

                // Event Log Of MC Protocol Name And Data
                case EV_MCPROTOCOL_PLC_IS_NOT_CONNECT:
                    return "PLC Is Not Connect.";
                case EV_MCPROTOCOL_DEVICE_NAME_NOT_CORRECT:
                    return "Device Name Input Not Correct";
                case EV_MCPROTOCOL_ADDRESS_NOT_CORRECT:
                    return "Address Not Correct";
                case EV_MCPROTOCOL_DATA_RESPONSE_NOT_CORRECT:
                    return "Data Response Not Correct";
                case EV_MCPROTOCOL_CANNOT_TYPE_WORD_IN_BIT:
                    return "Cannot Type Word In Bit Mode";
                case EV_MCPROTOCOL_WRITE_DATA_FALSE:
                    return "Write Data To PLC Fail";
                case EV_MCPROTOCOL_DATA_TRANS_NOT_CORRECT:
                    return "Data Trans To PLC Not Correct Format";
            }
            return "";
        }
    }
}
