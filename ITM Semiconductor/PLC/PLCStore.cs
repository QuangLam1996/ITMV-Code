using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ITM_Semiconductor
{
    class PLCStore
    {
        
        public const int D_TIME_DELAY_TOOL1 = 300;

        #region PGMain
        public const int L_BUTTON_START = 02;
        public const int L_BUTTON_STOP = 03;
        public const int L_BUTTON_HOME = 05;
        public const int L_BUTTON_RESET = 07;
        public const int L_BUTTON_LOTEND = 06;

        public const int SCANNER_JIG_TRIG_IL_OK = 602;
        public const int SCANNER_JIG_TRIG_IL_NG = 603;
        public const int SCANNER_PCB_TRIG_IL_OK = 604;
        public const int SCANNER_PCB_TRIG_IL_NG = 605;
        public const int SCANNER_MES_IL_OK = 606;
        public const int SCANNER_MES_IL_NG = 607;

        public const int SCANNER_JIG_TRIG_MIX_OK = 612;
        public const int SCANNER_JIG_TRIG_MIX_NG = 613;
        public const int SCANNER_PCB_TRIG_MIX_OK = 614;
        public const int SCANNER_PCB_TRIG_MIX_NG = 615;
        public const int SCANNER_MES_MIX_OK = 616;
        public const int SCANNER_MES_MIX_NG = 617;


        #endregion


        #region PG_TEACHING
        public const int L_AX1_LAMP_SERVO_ON_OFF = 13702;
        public const int L_AX1_LAMP_JOG_UP = 12002;
        public const int L_AX1_LAMP_JOG_DOWN = 12003;
        public const int L_AX1_LAMP_ORG = 12041;
        public const int L_AX1_LAMP_BAKE_ON_OFF = 13742;

        public const int L_AX1_SERVO_ON_OFF = 3702;
        public const int L_AX1_JOG_UP = 2002;
        public const int L_AX1_JOG_DOWN = 2003;
        public const int L_AX1_ORG = 2042;
        public const int L_AX1_BAKE_ON_OFF = 3742;

        public const int X_AX1_LIMIT_UP_SIGNAL = 14;
        public const int X_AX1_LIMIT_DOWN_SIGNAL = 15;
        public const int X_AX1_ORG_SIGNAL = 0;
        public const int X_AX1_ALM_SIGNAL = 10;

        public const int D_AX1_JOG_CURRENT_SPEED = 2542;
        public const int D_AX1_POS_CURRENT = 2502;
        public const int D_AX1_CODE_ERROR = 2582;

       
        
        //TBX POS AX1
        public const int R_AX1_WAIT_POS0 = 2000;
        public const int R_AX1_INPUT_MGZ_HI_POS1 = 2002;
        public const int R_AX1_INPUT_MGZ_LO_POS2 = 2004;
        public const int R_AX1_OUTPUT_MGZ_HIGH_POS3 = 2006;
        public const int R_AX1_OUTPUT_MGZ_LOW_POS4 = 2008;
        public const int R_AX1_FIX_MGZ_POS5 = 2010;
        public const int R_AX1_LOADER_POS6 = 2012;

        public const int R_AX1_WAIT_POS0_SPEED = 12000;
        public const int R_AX1_INPUT_MGZ_HI_POS1_SPEED = 12002;
        public const int R_AX1_INPUT_MGZ_LO_POS2_SPEED = 12004;
        public const int R_AX1_OUTPUT_MGZ_HIGH_POS3_SPEED = 12006;
        public const int R_AX1_OUTPUT_MGZ_LOW_POS4_SPEED = 12008;
        public const int R_AX1_FIX_MGZ_POS5_SPEED = 12010;
        public const int R_AX1_LOADER_POS6_SPEED = 12012;

        public const int L_AX1_WAIT_POS0_SW = 2100;
        public const int L_AX1_INPUT_MGZ_HI_POS1_SW = 2101;
        public const int L_AX1_INPUT_MGZ_LO_POS2_SW = 2102;
        public const int L_AX1_OUTPUT_MGZ_HIGH_POS3_SW = 2103;
        public const int L_AX1_OUTPUT_MGZ_LOW_POS4_SW = 2104;
        public const int L_AX1_FIX_MGZ_POS5_SW = 2105;
        public const int L_AX1_LOADER_POS6_SW = 2106;


        // AX2 ////////////////////////////////////////////
        public const int L_AX2_LAMP_SERVO_ON_OFF = 13704;
        public const int L_AX2_LAMP_JOG_UP = 12004;
        public const int L_AX2_LAMP_JOG_DOWN = 12005;
        public const int L_AX2_LAMP_ORG = 12042;
        public const int L_AX2_LAMP_BAKE_ON_OFF = 13744;

        public const int L_AX2_SERVO_ON_OFF = 3704;
        public const int L_AX2_JOG_UP = 2004;
        public const int L_AX2_JOG_DOWN = 2005;
        public const int L_AX2_ORG = 2044;
        public const int L_AX2_BAKE_ON_OFF = 3744;

        public const int X_AX2_LIMIT_UP_SIGNAL = 16;
        public const int X_AX2_LIMIT_DOWN_SIGNAL = 17;
        public const int X_AX2_ORG_SIGNAL = 1;
        public const int X_AX2_ALM_SIGNAL = 11;

        public const int D_AX2_JOG_CURRENT_SPEED = 2544;
        public const int D_AX2_POS_CURRENT = 2504;
        public const int D_AX2_CODE_ERROR = 2584;

        public const int D_AX2_LOADER_INDEX_CALL_POINT_MANUAL = 501;
        public const int D_AX2_LOADER_MATRIX_Z_POINT_CH1 = 502;
        public const int D_AX2_LOADER_MATRIX_Z_PICTH_CH1 = 504;






        //TBX POS
        public const int R_AX2_WAIT_POS0 = 2200;
        public const int R_AX2_INPUT_MGZ_UP_POS1 = 2202;
        public const int R_AX2_INPUT_MGZ_DN_POS2 = 2204;
        public const int R_AX2_OUTPUT_MGZ_UP_POS3 = 2206;
        public const int R_AX2_OUTPUT_MGZ_DN_POS4 = 2208;
        public const int R_AX2_FIX_MGZ_POS5 = 2210;
        public const int R_AX2_P0_LOADER_MATRIX_POS6 = 2212;

        public const int R_AX2_WAIT_POS0_SPEED = 12200;
        public const int R_AX2_INPUT_MGZ_UP_POS1_SPEED = 12202;
        public const int R_AX2_INPUT_MGZ_DN_POS2_SPEED = 12204;
        public const int R_AX2_OUTPUT_MGZ_UP_POS3_SPEED = 12206;
        public const int R_AX2_OUTPUT_MGZ_DN_POS4_SPEED = 12208;
        public const int R_AX2_FIX_MGZ_POS5_SPEED = 12210;
        public const int R_AX2_P0_LOADER_MATRIX_POS6_SPEED = 12212;
        public const int R_AX2_LOADER_MATRIX_POS7 = 12214;

        public const int L_AX2_WAIT_POS0_SW = 2200;
        public const int L_AX2_INPUT_MGZ_UP_POS1_SW = 2201;
        public const int L_AX2_INPUT_MGZ_DN_POS2_SW = 2202;
        public const int L_AX2_OUTPUT_MGZ_UP_POS3_SW = 2203;
        public const int L_AX2_OUTPUT_MGZ_DN_POS4_SW = 2204;
        public const int L_AX2_FIX_MGZ_POS5_SW = 2205;
        public const int L_AX2_P0_LOADER_MATRIX_POS6_SW = 2206;
        public const int L_AX2_LOADER_MATRIX_POS7_SW = 2207;
        public const int L_AX2_AUTO_GENERATOR_MGZ_POS_SW = 550;
        #endregion

        #region PG_TEACHINGMANUAL1

        public const int L_CV_LANE1_LCT_STP1_UP = 1002;
        public const int L_CV_LANE1_LCT_STP1_DOWN = 1003;

        public const int L_CV_LANE1_LCT_UP = 1004;
        public const int L_CV_LANE1_LCT_DOWN = 1005;

        public const int L_CV_LANE1_LCT_STP2_UP = 1006;
        public const int L_CV_LANE1_LCT_STP2_DOWN = 1007;



        public const int L_CV_BUFFER_LCT_CLAMP = 1008;
        public const int L_CV_BUFFER_LCT_UNCLAMP = 1009;

        public const int L_CV_BUFFER_LCT_UP = 1010;
        public const int L_CV_BUFFER_LCT_DOWN = 1011;

        public const int L_BUFFER_PUSHER_FWD = 1012;
        public const int L_BUFFER_PUSHER_BWD = 1013;


        public const int L_BOX1_SUS_LOCK_UP = 1014;
        public const int L_BOX1_SUS_LOCK_DOWN = 1015;

        public const int L_BOX2_COVER_LOCK_UP = 1016;
        public const int L_BOX2_COVER_LOCK_DOWN = 1017;

        public const int L_BOX3_COVER_LOCK_UP = 1018;
        public const int L_BOX3_COVER_LOCK_DOWN = 1019;

        public const int L_BOX2_COVER_BLOW_AIR_ON = 1020;
        public const int L_BOX2_COVER_BLOW_AIR_OFF = 1021;

        public const int L_BOX3_COVER_BLOW_AIR_ON = 1022;
        public const int L_BOX3_COVER_BLOW_AIR_OFF = 1023;

        #endregion

        #region PG_TEACHINGMANUAL2
        // LOADER
        public const int L_LOADER_DRAG_MGZ_IN = 1024;
        public const int L_LOADER_DRAG_MGZ_OUT = 1025;

        public const int L_LOADER_CLAMP_MGZ_UP = 1026;
        public const int L_LOADER_CLAMP_MGZ_DOWN = 1027;

        public const int L_LOADER_FIX_MGZ_FWD = 1028;
        public const int L_LOADER_FIX_MGZ_BWD = 1029;

        public const int L_LOADER_PUSHER_FWD = 1030;
        public const int L_LOADER_PUSHER_BWD = 1031;


        // CONVERYER
        public const int L_CONVEYER_IN_LANE1_ON = 1042;
        public const int L_CONVEYER_IN_LANE1_OFF = 1043;

        public const int L_CONVEYER_OUT_LANE1_ON = 1044;
        public const int L_CONVEYER_OUT_LANE1_OFF = 1045;

        public const int L_CONVEYER_LANE2_ON = 1046;
        public const int L_CONVEYER_LANE2_OFF = 1047;

        public const int L_CONVEYER_BUFFER_IN_ON = 1048;
        public const int L_CONVEYER_BUFFER_IN_OFF = 1049;

        public const int L_CONVEYER_BUFFER_OUT_ON = 1050;
        public const int L_CONVEYER_BUFFER_OUT_OFF = 1051;

        public const int L_CONVEYER_IN_MGZ_ON = 1052;
        public const int L_CONVEYER_IN_MGZ_OFF = 1053;

        public const int L_CONVEYER_OUT_MGZ_ON = 1054;
        public const int L_CONVEYER_OUT_MGZ_OFF = 1055;

        //VACUUM
        public const int L_LANE1_LCT_VACUUM_SUCTION = 1062;
        public const int L_LANE1_LCT_VACUUM_BLOW = 1063;

        public const int L_BUFFER_LCT_VACUUM_SUCTION = 1064;
        public const int L_BUFFER_LCT_VACUUM_BLOW = 1065;

        public const int L_PICKUP_TOOL_PCB_VACUUM_SUCTION = 1066;
        public const int L_PICKUP_TOOL_PCB_VACUUM_BLOW = 1067;

        public const int L_PICKUP_TOOL_COVER_VACUUM_SUCTION = 1068;
        public const int L_PICKUP_TOOL_COVER_VACUUM_BLOW = 1069;
        #endregion
        #region PG_MODEL
       

        public const int R_NAME_MODEL_01 = 10700;
        public const int R_NAME_MODEL_02 = 10710;
        public const int R_NAME_MODEL_03 = 10720;
        public const int R_NAME_MODEL_04 = 10730;
        public const int R_NAME_MODEL_05 = 10740;

        public const int R_MODEL_SELECT_NO = 10600;
        public const int R_MODEL_COPY_TO_NO = 10602;
        public const int R_MODEL_COPY_FROM_NO = 10604;
        public const int R_MODEL_RUNNING_NO = 10610;
        public const int R_MODEL_RUNNING_NAME = 10620;


        public const int L_SAVE_MODEL = 800;
        public const int L_LOAD_MODEL = 802;
        public const int L_DELETE_MODEL = 804;
        public const int L_COPY_MODEL = 806;

        #endregion

        #region PG_SUPER_USER_MENU_1
        public const int TIME_1 = 300;
        public const int TIME_2 = 301;
        public const int TIME_3 = 302;
        public const int TIME_4 = 303;
        public const int TIME_5 = 304;

        public const int TIME_6 = 305;
        public const int TIME_7 = 306;
        public const int TIME_8 = 307;
        public const int TIME_9 = 308;
        public const int TIME_10 = 309;

        public const int TIME_11 = 310;
        public const int TIME_12 = 311;
        public const int TIME_13 = 312;
        public const int TIME_14 = 313;
        public const int TIME_15 = 314;

        public const int TIME_16 = 315;
        public const int TIME_17 = 316;
        public const int TIME_18 = 317;
        public const int TIME_19 = 318;
        public const int TIME_20 = 319;

        public const int TIME_21 = 320;
        public const int TIME_22 = 321;
        public const int TIME_23 = 322;
        public const int TIME_24 = 323;
        public const int TIME_25 = 324;



        #endregion


        #region PG_SUPER_USER_MENU_3


        public const int D_AX1_ACC_TIME = 2002;
        public const int D_AX1_DCC_TIME = 2042;
        public const int D_AX1_SPEED_LIMIR_ALL = 2082;
        public const int D_AX1_ORG_SPEED = 2162;
        public const int D_AX1_JOG_SPEED = 2242;


        public const int D_AX2_ACC_TIME = 2004;
        public const int D_AX2_DCC_TIME = 2044;
        public const int D_AX2_SPEED_LIMIR_ALL = 2084;
        public const int D_AX2_ORG_SPEED = 2164;
        public const int D_AX2_JOG_SPEED = 2244;
        #endregion
    }
}
