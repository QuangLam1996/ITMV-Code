﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
    public enum UserActions
    {
        
        APP_START , 
        APP_BUTTON_MAIN, 
        APP_BUTTON_MENU,
        APP_BUTTON_IO, 
        APP_BUTTON_LASTJAM, 
        APP_BUTTON_SHUTDOWN,
        MAIN_ENTER, 
        MAIN_EXIT, 

        MAIN_BUTTON_START,
        MAIN_BUTTON_PAUSE,
        MAIN_BUTTON_HOME,
        MAIN_BUTTON_STOP,
        MAIN_BUTTON_RESET,
        MAIN_BUTTON_CONVEY,
        MAIN_BUTTON_INIT,
        MAIN_BUTTON_LOT_END,
        MAIN_BUTTON_LOTIN,

        LOTIN_SHOW, 
        LOTIN_BUTTON_OK,
        LOTIN_BUTTON_CANCEL,
        CONFIRM_SHOW_YESNO,
        CONFIRM_SHOW_YES,
        CONFIRM_BUTTON_YES, 
        CONFIRM_BUTTON_NO,
        MENU_ENTER, 
        MENU_EXIT,
        MENU_BUTTON_LOGIN,
        MENU_BUTTON_LOGOUT,
        MENU_BUTTON_TEACHING, 
        MENU_BUTTON_MECHANICAL, 
        MENU_BUTTON_SYSTEM, 
        MENU_BUTTON_MANUAL,
        MENU_BUTTON_STATUS,
        MENU_BUTTON_LOAD_SAVE, 
        MENU_BUTTON_SUPERUSER,
        MENU_BUTTON_MODEL,
        MENU_BUTTON_ASIGN,

        PAGE_MANUAL_OPERATION,
        PAGE_MANUAL_OPERATION1,
        PAGE_MANUAL_OPERATION2,
        PAGE_MANUAL_OPERATION3,

        PAGE_MECHANICAL_MENU,
        PAGE_MECHANICAL_MENU1,
        PAGE_MECHANICAL_MENU2,
        PAGE_MECHANICAL_MENU3,
        PAGE_MECHANICAL_SETUP_TCP_PLC,
        PAGE_MECHANICAL_SETUP_TCP_SCANNER,

        PAGE_SUPER_USER_MENU,
        PAGE_SUPER_USER_MENU_DELAY_MACHINE,
        PAGE_SUPER_USER_MENU_SETTING_ALARM,
        PAGE_SUPER_USER_MENU_SETTING_SERVO,
        PAGE_SUPER_USER_MENU_SETTING_ROBOT,


        PAGE_TEACHING_MENU,
        PAGE_TEACHING_MENU_JIG_SETUP,
        PAGE_TEACHING_MENU_ROBOT_JOG,
        PAGE_TEACHING_MENU_MANUAL,

        MN_TEACHING_JIG_SAVE,

        PAGE_SYSTEM_MENU,
        PAGE_SYSTEM_MENU_SYSTEM_MACHINE,


        PAGE_CAMERA_SETTING,










        PAGE_STATUS_LOG_BUTTON_ALARM_FIRST,
        PAGE_STATUS_LOG_BUTTON_ALARM_PRE_PAGE,
        PAGE_STATUS_LOG_BUTTON_ALARM_PREVIOUS,
        PAGE_STATUS_LOG_BUTTON_ALARM_CURRENT,
        PAGE_STATUS_LOG_BUTTON_ALARM_NEXT,
        PAGE_STATUS_LOG_BUTTON_ALARM_NEXT_PAGE,
        PAGE_STATUS_LOG_BUTTON_ALARM_LAST,
        PAGE_STATUS_LOG_BUTTON_EVENT_PREVIOUS,
        PAGE_STATUS_LOG_BUTTON_EVENT_LOG,
        PAGE_STATUS_LOG_BUTTON_TODAY,
        PAGE_STATUS_LOG_BUTTON_PRE_PAGE,
        PAGE_STATUS_LOG_BUTTON_NEXT_PAGE,
        PAGE_STATUS_LOG_CHANGE_DATE,
        PAGE_STATUS_LOG_UNLOAED,
        PAGE_STATUS_LOG_LOADED,

        LOGON_SHOW ,
        LOGON_BUTTON_ENTER, 
        LOGON_BUTTON_CANCEL,
        LOGON_BUTTON_CHANGE_PASSWORD,
        LOGON_CHANGE_PASS_SUCCESS,
        LOGIN_BUTTON_SIGNIN,
        LOGIN_BUTTON_MANAGER,
        LOGIN_BUTTON_AUTOTEAMS,
        LOGIN_BUTTON_OPERATOR,

        CHANGEPASS_SHOW , 
        CHANGEPASS_BUTTON_OK,
        CHANGEPASS_BUTTON_CANCEL,     

        MN_MECHANICAL_JIG_ENTER,
        MN_MECHANICAL_JIG_EXIT,
        MN_MECHANICAL_JIG_SAVE, 
        MN_MECHANICAL_JIG_UNDO,
        MN_MECHANICAL_JIG_UNDO_YES,
        MN_MECHANICAL_JIG_SAVE_ID,
        MN_MECHANICAL_JIG_PATTERN,
        MN_MECHANICAL_DELAY_ENTER,
        MN_MECHANICAL_DELAY_SAVE, 
        MN_MECHANICAL_DELAY_UNDO,
        MN_MECHANICAL_DELAY_UNDO_YES,

        MN_MECHANICAL_BARCODE_ENTER,
        MN_MECHANICAL_BARCODE_EXIT, 
        MN_MECHANICAL_BARCODE_PORT_SETTINGS,
        MN_MECHANICAL_BARCODE_PORT_OPEN, 
        MN_MECHANICAL_BARCODE_PORT_CLOSE, 
        MN_MECHANICAL_BARCODE_FOCUSING,
        MN_MECHANICAL_BARCODE_TUNING,
        MN_MECHANICAL_BARCODE_READING_PKG,
        MN_MECHANICAL_BARCODE_READING_JIG,
        MN_MECHANICAL_BARCODE_CLEAR_LOG,
        MN_MECHANICAL_BARCODE_SAVE,

        MN_MECHANICAL_SPEED_ENTER,
        MN_MECHANICAL_SPEED_EXIT,
        MN_MECHANICAL_SPEED_SAVE,
        MN_MECHANICAL_SPEED_X, 
        MN_MECHANICAL_SPEED_Y,
        MN_MECHANICAL_SPEED_PORT_SETTINGS,
        MN_MECHANICAL_SPEED_PORT_OPEN,
        MN_MECHANICAL_SPEED_PORT_CLOSE,
        MN_MECHANICAL_SPEED_CLEAR_LOG,
        MN_MECHANICAL_SPEED_ADD_CRC16, 
        MN_MECHANICAL_SPEED_SEND,

        MN_SYSTEM_RUN_ENTER,
        MN_SYSTEM_RUN_EXIT,
        MN_SYSTEM_RUN_UNDO, 
        MN_SYSTEM_RUN_UNDO_YES,
        MN_SYSTEM_RUN_JAM_ACTION,
        MN_SYSTEM_RUN_SKIP_ACTION,
        MN_SYSTEM_RUN_AUTO_JIG_END, 
        MN_SYSTEM_RUN_MANUAL_JIG_END,
        MN_SYSTEM_RUN_MES_ONLINE,
        MN_SYSTEM_RUN_MES_OFFLINE, 
        MN_SYSTEM_RUN_QR_CROSS_CHECK_ON,
        MN_SYSTEM_RUN_QR_CROSS_CHECK_OFF,
        MN_SYSTEM_RUN_TESTER_ONLINE, 
        MN_SYSTEM_RUN_TESTER_OFFLINE,
        MN_SYSTEM_RUN_LOTID_QR_CHECK_ON, 
        MN_SYSTEM_RUN_LOTID_QR_CHECK_OFF,
        MN_SYSTEM_RUN_2D_ONLINE,
        MN_SYSTEM_RUN_2D_OFFLINE,
        MN_SYSTEM_RUN_2D_OFFLINE_SAVE,
        MN_SYSTEM_RUN_2D_CYCLE_RESET, 
        MN_SYSTEM_RUN_2D_CALIB_RESET,

        MN_SYSTEM_TESTER_ENTER, 
        MN_SYSTEM_TESTER_EXIT,
        MN_SYSTEM_TESTER_PORT_SETTINGS,
        MN_SYSTEM_TESTER_PORT_OPEN,
        MN_SYSTEM_TESTER_PORT_CLOSE, 
        MN_SYSTEM_TESTER_CLEAR_LOG,
        MN_SYSTEM_TESTER_SELECT_TESTER,
        MN_SYSTEM_TESTER_ONLINE, 
        MN_SYSTEM_TESTER_OFFLINE,

        MN_SYSTEM_MES_ENTER, 
        MN_SYSTEM_MES_EXIT, 
        MN_SYSTEM_MES_OPEN,
        MN_SYSTEM_MES_CLOSE,
        MN_SYSTEM_MES_SAVE_IP,
        MN_SYSTEM_MES_SEND_M001, 
        MN_SYSTEM_MES_SEND_M031,

        MN_MANUAL_ENTER,
        MN_MANUAL_EXIT,
        MN_MANUAL_LOADER_UP, 
        MN_MANUAL_LOADER_DOWN,
        MN_MANUAL_MIDDLE_UP,
        MN_MANUAL_MIDDLE_DOWN,
        MN_MANUAL_CONVEYOR_ON,
        MN_MANUAL_CONVEYOR_OFF,
        MN_MANUAL_RETURN_CONVEYOR_ON,
        MN_MANUAL_RETURN_CONVEYOR_OFF,

        MN_STATUS_ENTER,
        MN_STATUS_EXIT,
        MN_SAVE_ENTER, 
        MN_SAVE_EXIT, 
        MN_SAVE_SAVE, 
        MN_SAVE_DELETE_PKG,
        MN_SAVE_DELETE_PKG_YES,
        MN_SAVE_DELETE_ALL,
        MN_SAVE_DELETE_ALL_YES,

        MN_LOAD_ENTER, 
        MN_LOAD_LOAD, 
        MN_LOAD_LOAD_YES,

        MN_SUPER_AXIS_ENTER,
        MN_SUPER_AXIS_EXIT,
        MN_SUPER_AXIS_SAVE,
        MN_SUPER_AXIS_UNDO, 
        MN_SUPER_AXIS_UNDO_YES,
        MN_SUPER_AXIS_X,
        MN_SUPER_AXIS_Y,

        MN_SUPER_OPTIONS_ENTER,
        MN_SUPER_OPTIONS_EXIT,
        MN_SUPER_OPTIONS_SAVE,

        IO_ENTER, 
        IO_EXIT, 
        IO_INPUT, 
        IO_OUTPUT,
        LASTJAM_ENTER, 
        LASTJAM_EXIT,
        LASTJAM_SELECT,

        WND_MAIN,
        WND_MENU,
        WND_IO,
        WND_ALARM,
        WND_CLOSE
    };
    class UserLog
    {
        public int Id { get; set; }
        public String Username { get; set; }
        public String CreatedTime { get; set; }
        public int Action { get; set; }
        public String Message { get; set; }

        public UserLog() { }

        public UserLog(UserActions action)
        {
            this.Id = 0;
            this.Username = UiManager.appSettings.UseName;
            this.CreatedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff");
            this.Action = (int)action;
            this.Message = getActionMessage(action);
        }

        private static String getActionMessage(UserActions action)
        {
            var ret = action.ToString();

            return ret;
        }
    }
}
