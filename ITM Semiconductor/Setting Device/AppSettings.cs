using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MvCamCtrl.NET;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using static ITM_Semiconductor.MesSettings;

namespace ITM_Semiconductor
{
    class AppSettings
    {
        public const string SETTING_FILE_NAME = "appsetting.json";
        private const String DEFAULT_PASSWORD = "itm";
        public String PassWordEN { get; set; }
        public String PassWordADM { get; set; }
        public String UseName { get; set; }
        public const String DEFAULT_USER_NAME = "Operator";
        public string Operator { get; set; }
        public ConnectionSettings connection { get; set; }

        // Setting Scanner
        private SettingScanner settingScanner;

        private SettingPLCCom settingPLCCom;

        private Setting_PLCTcp setting_PLCTcp;

        private SettingScannerTCP settingScannerTCP;

        private ModelSetting settingModel;

        private MesSettings MesSettings;

        // Setting Scanner
        public SettingScanner SettingScanner { get => settingScanner; set => settingScanner = value; }
        public SettingPLCCom SettingPLCCom { get => settingPLCCom; set => settingPLCCom = value; }
        public Setting_PLCTcp Setting_PLCTcp { get => setting_PLCTcp; set => setting_PLCTcp = value; }
        public SettingScannerTCP SettingScannerTCP { get => settingScannerTCP; set => settingScannerTCP = value; }



        public RunSettings run { get; set; }
        public ModelSetting SettingModel { get => settingModel; set => settingModel = value; }
        public MesSettings MesSettings1 { get => MesSettings; set => MesSettings = value; }

        public LotInData lotData { get; set; }

        public FTPClientSettings FTPClientSettings { get; set; }






        public MotorParameter Robot { get; set; }


        public Model M01_CH1 { get; set; }
        public Model M02_CH1 { get; set; }
        public Model M03_CH1 { get; set; }

        public Model M01_CH2 { get; set; }
        public Model M02_CH2 { get; set; }
        public Model M03_CH2 { get; set; }
        public ROIProperty Property { get; set; }
        public bool caseShowDataMatrixRT { get; set; } = false;


        public AppSettings()
        {
            this.lotData = new LotInData();
            this.UseName = DEFAULT_USER_NAME;
            this.Operator = DEFAULT_USER_NAME;

            this.settingScanner = new SettingScanner();
            this.settingPLCCom = new SettingPLCCom();

            this.connection = new ConnectionSettings();
            
            this.setting_PLCTcp = new Setting_PLCTcp();

            this.settingScannerTCP = new SettingScannerTCP();

            this.SettingModel = new ModelSetting();

            this.run = new RunSettings();

            this.MesSettings1 = new MesSettings();

            this.FTPClientSettings = new FTPClientSettings();





            this.Robot = new MotorParameter();

            this.M01_CH1 = new Model();
            this.M02_CH1 = new Model();
            this.M03_CH1 = new Model();
            this.M01_CH2 = new Model();
            this.M02_CH2 = new Model();
            this.M03_CH2 = new Model();
            this.Property = new ROIProperty();


        }
        public string TOJSON()
        {
            string retValue = "";
            retValue = JsonConvert.SerializeObject(this);
            return retValue;
        }
        public static AppSettings FromJSON(String json)
        {

            var _appSettings = JsonConvert.DeserializeObject<AppSettings>(json);

            if (String.IsNullOrEmpty(_appSettings.PassWordEN))
            {
                _appSettings.PassWordEN = DEFAULT_PASSWORD;
            }
            if (String.IsNullOrEmpty(_appSettings.PassWordADM))
            {
                _appSettings.PassWordADM = DEFAULT_PASSWORD;
            }
            if (_appSettings.SettingScanner == null)
            {
                _appSettings.SettingScanner = new SettingScanner();
            }
            if (_appSettings.SettingPLCCom == null)
            {
                _appSettings.SettingPLCCom = new SettingPLCCom();
            }
            if (_appSettings.connection == null)
            {
                _appSettings.connection = new ConnectionSettings();
            }
            if (_appSettings.Setting_PLCTcp == null)
            {
                _appSettings.Setting_PLCTcp = new Setting_PLCTcp();
            }
            if (_appSettings.SettingScannerTCP == null)
            {
                _appSettings.SettingScannerTCP = new SettingScannerTCP();
            }
            if (_appSettings.run == null)
            {
                _appSettings.run = new RunSettings();
            }
            if(_appSettings.SettingModel == null)
            {
                _appSettings.SettingModel = new ModelSetting();
            }
            if (_appSettings.MesSettings1 == null)
            {
                _appSettings.MesSettings1 = new MesSettings();
            }
            if (_appSettings.lotData == null)
            {
                _appSettings.lotData = new LotInData();
            }
            if (_appSettings.FTPClientSettings == null)
            {
                _appSettings.FTPClientSettings = new FTPClientSettings();
            }
            if (_appSettings.M01_CH1 == null)
            {
                _appSettings.M01_CH1 = new Model();
            }
            if (_appSettings.M02_CH1 == null)
            {
                _appSettings.M02_CH1 = new Model();
            }
            if (_appSettings.M03_CH1 == null)
            {
                _appSettings.M03_CH1 = new Model();
            }
            if (_appSettings.M01_CH2 == null)
            {
                _appSettings.M01_CH2 = new Model();
            }
            if (_appSettings.M02_CH2 == null)
            {
                _appSettings.M02_CH2 = new Model();
            }
            if (_appSettings.M03_CH2 == null)
            {
                _appSettings.M03_CH2 = new Model();
            }           
            if(_appSettings.Property == null)
            {
                _appSettings.Property = new ROIProperty();
            }
            return _appSettings;
        }      
    }
    #region MotorParameter
    class MotorParameter
    {
        public int XaxisJogSpeed { get; set; }
        public int ZaxisJogSpeed { get; set; }
        public TeachDataXaxis TeachDataXaxis { get; set; }
        public TeachDataYaxis TeachDataYaxis { get; set; }
        public TeachDataYaxis1 TeachDataYaxis1 { get; set; }
        public TeachSpeedYaxis TeachSpeedYaxis { get; set; }
        public TeachSpeedYaxis1 TeachSpeedYaxis1 { get; set; }
        public TeachDataXaxis1 TeachDataXaxis1 { get; set; }
        public TeachDataZaxis TeachDataZaxis { get; set; }
        public TeachDataZaxis1 TeachDataZaxis1 { get; set; }
        public MotorParameter()
        {
            this.XaxisJogSpeed = 1000;
            this.ZaxisJogSpeed = 1000;
            this.TeachDataXaxis = new TeachDataXaxis();
            this.TeachDataZaxis = new TeachDataZaxis();
            this.TeachDataXaxis1 = new TeachDataXaxis1();
            this.TeachDataZaxis1 = new TeachDataZaxis1();
            this.TeachDataYaxis = new TeachDataYaxis();
            this.TeachDataYaxis1 = new TeachDataYaxis1();
            this.TeachSpeedYaxis = new TeachSpeedYaxis();
            this.TeachSpeedYaxis1 = new TeachSpeedYaxis1();
        }
        public MotorParameter Clone()
        {
            return new MotorParameter()
            {
                XaxisJogSpeed = this.XaxisJogSpeed,
                ZaxisJogSpeed = this.ZaxisJogSpeed,
                TeachDataXaxis = this.TeachDataXaxis.Clone(),
                TeachDataZaxis = this.TeachDataZaxis.Clone(),
                TeachDataXaxis1 = this.TeachDataXaxis1.Clone(),
                TeachDataZaxis1 = this.TeachDataZaxis1.Clone(),
                TeachDataYaxis = this.TeachDataYaxis.Clone(),
                TeachDataYaxis1 = this.TeachDataYaxis1.Clone(),
                TeachSpeedYaxis = this.TeachSpeedYaxis.Clone(),
                TeachSpeedYaxis1 = this.TeachSpeedYaxis1.Clone()
            };
        }

    }
    class TeachDataXaxis
    {

        public int BendingPos { get; set; }
        public int readyPos { get; set; }
        public int QrPos { get; set; }
        public int QrScapPos { get; set; }
        public int MesScapPos { get; set; }
        public int MatingPos { get; set; }
        public int BendingPosSpeed { get; set; }
        public int readyPosSpeed { get; set; }
        public int QrPosSpeed { get; set; }
        public int QrScapPosSpeed { get; set; }
        public int MesScapPosSpeed { get; set; }
        public int MatingPosSpeed { get; set; }
        public TeachDataXaxis()
        {
            this.BendingPos = 0;
            this.readyPos = 0;
            this.QrPos = 0;
            this.QrScapPos = 0;
            this.MesScapPos = 0;
            this.MatingPos = 0;
            this.BendingPosSpeed = 0;
            this.readyPosSpeed = 0;
            this.QrPosSpeed = 0;
            this.QrScapPosSpeed = 0;
            this.MesScapPosSpeed = 0;
            this.MatingPosSpeed = 0;
        }
        public TeachDataXaxis Clone()
        {
            return new TeachDataXaxis
            {
                BendingPos = this.BendingPos,
                readyPos = this.readyPos,
                QrPos = this.QrPos,
                QrScapPos = this.QrScapPos,
                MesScapPos = this.MesScapPos,
                MatingPos = this.MatingPos
            };
        }
    }
    class TeachDataYaxis
    {

        public int BendingPos { get; set; }
        public int readyPos { get; set; }
        public int QrPos { get; set; }
        public int QrScapPos { get; set; }
        public int MesScapPos { get; set; }
        public int MatingPos { get; set; }
        public int BendingPosSpeed { get; set; }
        public int readyPosSpeed { get; set; }
        public int QrPosSpeed { get; set; }
        public int QrScapPosSpeed { get; set; }
        public int MesScapPosSpeed { get; set; }
        public int MatingPosSpeed { get; set; }
        public TeachDataYaxis()
        {
            this.BendingPos = 0;
            this.readyPos = 0;
            this.QrPos = 0;
            this.QrScapPos = 0;
            this.MesScapPos = 0;
            this.MatingPos = 0;
            this.BendingPosSpeed = 0;
            this.readyPosSpeed = 0;
            this.QrPosSpeed = 0;
            this.QrScapPosSpeed = 0;
            this.MesScapPosSpeed = 0;
            this.MatingPosSpeed = 0;
        }
        public TeachDataYaxis Clone()
        {
            return new TeachDataYaxis
            {
                BendingPos = this.BendingPos,
                readyPos = this.readyPos,
                QrPos = this.QrPos,
                QrScapPos = this.QrScapPos,
                MesScapPos = this.MesScapPos,
                MatingPos = this.MatingPos
            };
        }
    }
    class TeachDataYaxis1
    {

        public int BendingPos { get; set; }
        public int readyPos { get; set; }
        public int QrPos { get; set; }
        public int QrScapPos { get; set; }
        public int MesScapPos { get; set; }
        public int MatingPos { get; set; }
        public int BendingPosSpeed { get; set; }
        public int readyPosSpeed { get; set; }
        public int QrPosSpeed { get; set; }
        public int QrScapPosSpeed { get; set; }
        public int MesScapPosSpeed { get; set; }
        public int MatingPosSpeed { get; set; }
        public TeachDataYaxis1()
        {
            this.BendingPos = 0;
            this.readyPos = 0;
            this.QrPos = 0;
            this.QrScapPos = 0;
            this.MesScapPos = 0;
            this.MatingPos = 0;
            this.BendingPosSpeed = 0;
            this.readyPosSpeed = 0;
            this.QrPosSpeed = 0;
            this.QrScapPosSpeed = 0;
            this.MesScapPosSpeed = 0;
            this.MatingPosSpeed = 0;
        }
        public TeachDataYaxis1 Clone()
        {
            return new TeachDataYaxis1
            {
                BendingPos = this.BendingPos,
                readyPos = this.readyPos,
                QrPos = this.QrPos,
                QrScapPos = this.QrScapPos,
                MesScapPos = this.MesScapPos,
                MatingPos = this.MatingPos
            };
        }
    }
    class TeachDataXaxis1
    {

        public int BendingPos1 { get; set; }
        public int readyPos1 { get; set; }
        public int QrPos1 { get; set; }
        public int QrScapPos1 { get; set; }
        public int MesScapPos1 { get; set; }
        public int MatingPos1 { get; set; }
        public int BendingPosSpeed1 { get; set; }
        public int readyPosSpeed1 { get; set; }
        public int QrPosSpeed1 { get; set; }
        public int QrScapPosSpeed1 { get; set; }
        public int MesScapPosSpeed1 { get; set; }
        public int MatingPosSpeed1 { get; set; }
        public TeachDataXaxis1()
        {
            this.BendingPos1 = 0;
            this.readyPos1 = 0;
            this.QrPos1 = 0;
            this.QrScapPos1 = 0;
            this.MesScapPos1 = 0;
            this.MatingPos1 = 0;
            this.BendingPosSpeed1 = 0;
            this.readyPosSpeed1 = 0;
            this.QrPosSpeed1 = 0;
            this.QrScapPosSpeed1 = 0;
            this.MesScapPosSpeed1 = 0;
            this.MatingPosSpeed1 = 0;
        }
        public TeachDataXaxis1 Clone()
        {
            return new TeachDataXaxis1
            {
                BendingPos1 = this.BendingPos1,
                readyPos1 = this.readyPos1,
                QrPos1 = this.QrPos1,
                QrScapPos1 = this.QrScapPos1,
                MesScapPos1 = this.MesScapPos1,
                MatingPos1 = this.MatingPos1
            };
        }
    }
    class TeachDataZaxis
    {
        public int BendingPos { get; set; }
        public int readyPos { get; set; }
        public int QrPos { get; set; }
        public int QrScapPos { get; set; }
        public int MesScapPos { get; set; }
        public int MatingPos { get; set; }
        public int BendingPosSpeed { get; set; }
        public int readyPosSpeed { get; set; }
        public int QrPosSpeed { get; set; }
        public int QrScapPosSpeed { get; set; }
        public int MesScapPosSpeed { get; set; }
        public int MatingPosSpeed { get; set; }
        public TeachDataZaxis()
        {
            this.BendingPos = 0;
            this.readyPos = 0;
            this.QrPos = 0;
            this.QrScapPos = 0;
            this.MesScapPos = 0;
            this.MatingPos = 0;
            this.BendingPosSpeed = 0;
            this.readyPosSpeed = 0;
            this.QrPosSpeed = 0;
            this.QrScapPosSpeed = 0;
            this.MesScapPosSpeed = 0;
            this.MatingPosSpeed = 0;
        }
        public TeachDataZaxis Clone()
        {
            return new TeachDataZaxis
            {
                BendingPos = this.BendingPos,
                readyPos = this.readyPos,
                QrPos = this.QrPos,
                QrScapPos = this.QrScapPos,
                MesScapPos = this.MesScapPos,
                MatingPos = this.MatingPos
            };
        }
    }
    class TeachDataZaxis1
    {
        public int BendingPos1 { get; set; }
        public int readyPos1 { get; set; }
        public int QrPos1 { get; set; }
        public int QrScapPos1 { get; set; }
        public int MesScapPos1 { get; set; }
        public int MatingPos1 { get; set; }
        public int BendingPosSpeed1 { get; set; }
        public int readyPosSpeed1 { get; set; }
        public int QrPosSpeed1 { get; set; }
        public int QrScapPosSpeed1 { get; set; }
        public int MesScapPosSpeed1 { get; set; }
        public int MatingPosSpeed1 { get; set; }
        public TeachDataZaxis1()
        {
            this.BendingPos1 = 0;
            this.readyPos1 = 0;
            this.QrPos1 = 0;
            this.QrScapPos1 = 0;
            this.MesScapPos1 = 0;
            this.MatingPos1 = 0;
            this.BendingPosSpeed1 = 0;
            this.readyPosSpeed1 = 0;
            this.QrPosSpeed1 = 0;
            this.QrScapPosSpeed1 = 0;
            this.MesScapPosSpeed1 = 0;
            this.MatingPosSpeed1 = 0;
        }
        public TeachDataZaxis1 Clone()
        {
            return new TeachDataZaxis1
            {
                BendingPos1 = this.BendingPos1,
                readyPos1 = this.readyPos1,
                QrPos1 = this.QrPos1,
                QrScapPos1 = this.QrScapPos1,
                MesScapPos1 = this.MesScapPos1,
                MatingPos1 = this.MatingPos1
            };
        }
    }
    class TeachSpeedYaxis
    {
        public int BendingPos { get; set; }
        public int readyPos { get; set; }
        public int QrPos { get; set; }
        public int QrScapPos { get; set; }
        public int MesScapPos { get; set; }
        public int MatingPos { get; set; }
        public int BendingPosSpeed { get; set; }
        public int readyPosSpeed { get; set; }
        public int QrPosSpeed { get; set; }
        public int QrScapPosSpeed { get; set; }
        public int MesScapPosSpeed { get; set; }
        public int MatingPosSpeed { get; set; }
        public TeachSpeedYaxis()
        {
            this.BendingPos = 0;
            this.readyPos = 0;
            this.QrPos = 0;
            this.QrScapPos = 0;
            this.MesScapPos = 0;
            this.MatingPos = 0;
            this.BendingPosSpeed = 0;
            this.readyPosSpeed = 0;
            this.QrPosSpeed = 0;
            this.QrScapPosSpeed = 0;
            this.MesScapPosSpeed = 0;
            this.MatingPosSpeed = 0;
        }
        public TeachSpeedYaxis Clone()
        {
            return new TeachSpeedYaxis
            {
                BendingPos = this.BendingPos,
                readyPos = this.readyPos,
                QrPos = this.QrPos,
                QrScapPos = this.QrScapPos,
                MesScapPos = this.MesScapPos,
                MatingPos = this.MatingPos
            };
        }
    }
    class TeachSpeedYaxis1
    {
        public int BendingPos { get; set; }
        public int readyPos { get; set; }
        public int QrPos { get; set; }
        public int QrScapPos { get; set; }
        public int MesScapPos { get; set; }
        public int MatingPos { get; set; }
        public int BendingPosSpeed { get; set; }
        public int readyPosSpeed { get; set; }
        public int QrPosSpeed { get; set; }
        public int QrScapPosSpeed { get; set; }
        public int MesScapPosSpeed { get; set; }
        public int MatingPosSpeed { get; set; }
        public TeachSpeedYaxis1()
        {
            this.BendingPos = 0;
            this.readyPos = 0;
            this.QrPos = 0;
            this.QrScapPos = 0;
            this.MesScapPos = 0;
            this.MatingPos = 0;
            this.BendingPosSpeed = 0;
            this.readyPosSpeed = 0;
            this.QrPosSpeed = 0;
            this.QrScapPosSpeed = 0;
            this.MesScapPosSpeed = 0;
            this.MatingPosSpeed = 0;
        }
        public TeachSpeedYaxis1 Clone()
        {
            return new TeachSpeedYaxis1
            {
                BendingPos = this.BendingPos,
                readyPos = this.readyPos,
                QrPos = this.QrPos,
                QrScapPos = this.QrScapPos,
                MesScapPos = this.MesScapPos,
                MatingPos = this.MatingPos
            };
        }
    }
    #endregion
    class CamSettings
    {
        public String name { get; set; }
        public String fileConf { get; set; }

        public int ExposeTime { get; set; }

        public MyCamera.MV_CC_DEVICE_INFO device { get; set; }

        public int OffsetAlignJigX { get; set; }
        public int OffsetAlignJigY { get; set; }
        public int mediumGrayVal { get; set; }
        public double scale { get; set; }



        public CamSettings()
        {
            this.name = "";
            this.fileConf = "";
            this.ExposeTime = 5000;
            this.OffsetAlignJigX = 10;
            this.OffsetAlignJigY = 10;
            this.mediumGrayVal = 10;
            this.scale = 0.12;
            this.device = new MyCamera.MV_CC_DEVICE_INFO();

        }

        public CamSettings Clone()
        {
            return new CamSettings
            {
                OffsetAlignJigX = this.OffsetAlignJigX,
                OffsetAlignJigY = this.OffsetAlignJigY,
                mediumGrayVal = this.mediumGrayVal,
                name = String.Copy(this.name),
                fileConf = String.Copy(this.fileConf),
                scale = this.scale,
                device = this.device,
                ExposeTime = this.ExposeTime

            };
        }

    }
    class ImageSettings
    {
        public String CH1_path { get; set; }
        public String CH2_path { get; set; }

        public ImageSettings()
        {
            this.CH1_path = "CH1Path";
            this.CH2_path = "CH2Path";
        }

        public ImageSettings Clone()
        {
            return new ImageSettings
            {
                CH1_path = String.Copy(this.CH1_path),
                CH2_path = String.Copy(this.CH2_path)
            };
        }
    }
    class ConnectionSettings
    {
        public String VerUpDate { get; set; }
        public string DateUpdate { get; set; }
        public String modelName { get; set; }
        public String Recipe { get; set; }
        public String model { get; set; }
        public String model1 { get; set; }
        public String model2 { get; set; }
        public String model3 { get; set; }



        public CamSettings camera1 { get; set; }
        public CamSettings camera2 { get; set; }
        public CamSettings camera3 { get; set; }
        public CamSettings camera4 { get; set; }
        public ImageSettings image { get; set; }

        public string EquipmentName { get; set; }
        public ComSettings scanner { get; set; }
        public bool SelectModeCOM { get; set; }
        public ConnectionSettings()
        {
            this.DateUpdate = "2024-11-12";
            this.VerUpDate = "0";
            this.modelName = "Model Current";
            this.Recipe = "Machine";
            this.model = "X2833";
            this.model1 = "Model 01";
            this.model2 = "Model 02";
            this.model3 = "Model 03";

            this.EquipmentName = "MESITM001";
            this.scanner = new ComSettings();
            this.SelectModeCOM = false;

            this.camera1 = new CamSettings();
            this.camera2 = new CamSettings();
            this.camera3 = new CamSettings();
            this.camera4 = new CamSettings();
            this.image = new ImageSettings();

        }
       
        public ConnectionSettings Clone()
        {
            return new ConnectionSettings
            {
                modelName = String.Copy(this.modelName),
                model = String.Copy(this.model),

                camera1 = this.camera1.Clone(),
                camera2 = this.camera2.Clone(),
                camera3 = this.camera3.Clone(),
                camera4 = this.camera4.Clone(),
                image = this.image.Clone(),

                EquipmentName = string.Copy(EquipmentName),
                scanner = this.scanner.Clone(),
                SelectModeCOM = this.SelectModeCOM,
            };
        }
    }
    class RunSettings
    {
        public bool jamAction { get; set; } = true;
        public bool autoJigEnd { get; set; } = true;
        public bool qrCrossCheck { get; set; } = true;
        //public bool lotCheck { get; set; } = true;

        public bool mesOnline { get; set; } = true;
        public bool PackingOnline { get; set; } = false;
        public bool SortingMode { get; set; } = false;
        public bool PackingEnd { get; set; } = false;
        public bool PackingQROnline { get; set; } = false;
        //public bool testerOnline { get; set; } = true;
        public bool scannerOnline { get; set; } = true;
        public bool MachineQR { get; set; } = true;
        public int scannerNumberTrigger { get; set; } = 0;

        public RunSettings Clone()
        {
            return new RunSettings
            {
                MachineQR = this.MachineQR,
                jamAction = this.jamAction,
                autoJigEnd = this.autoJigEnd,
                PackingOnline = this.PackingOnline,
                PackingEnd = this.PackingEnd,
                qrCrossCheck = this.qrCrossCheck,
                mesOnline = this.mesOnline,
                scannerOnline = this.scannerOnline,           
                scannerNumberTrigger = this.scannerNumberTrigger,
            };
        }
        

    }
    class MesSettings
    {
        public String localIp { get; set; }
        public int localPort { get; set; }

        public String MESName { get; set; }
        public bool Is_Enable_Log { get; set; } // Enable Write Log

        public MesSettings()
        {
            this.localIp = "192.168.1.2";
            this.localPort = 5010;
            this.MESName = "";
            this.Is_Enable_Log = true;
        }

        public MesSettings Clone()
        {
            return new MesSettings
            {
                localIp = String.Copy(this.localIp),
                localPort = this.localPort,
                MESName = string.Copy(this.MESName),
                Is_Enable_Log = this.Is_Enable_Log
            };
        }
       
    }
    public class LotInData
    {
        public String workGroup { get; set; } = "ITM";
        public String LotId { get; set; } = "";
        public String Config { get; set; } = "";
        public int lotQty { get; set; }



        public DateTime LotStart { get; set; }

        public String deviceId { get; set; } = "";
        public String lotId { get; set; } = "";

        public int lotCount { get; set; }
        public int QRNG { get; set; }

        public int MESNG { get; set; }

        public LotInData Clone()
        {
            return new LotInData
            {
                workGroup = String.Copy(this.workGroup),
                LotId = String.Copy(this.LotId),
                Config = String.Copy(this.Config),
                lotQty = this.lotQty,

              
                deviceId = String.Copy(this.deviceId),
                lotId = String.Copy(this.lotId),
               
                lotCount = this.lotCount,
                QRNG = this.QRNG,
                MESNG = this.MESNG,
                LotStart = this.LotStart
            };
        }
    }
    public class FTPClientSettings
    {
        public string Image { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public String UserID { get; set; }
        public String PassWord { get; set; }
        public string FolderServer { get; set; }
        public FTPClientSettings()
        {
            this.Host = "192.168.54.217";
            this.Port = 38;
            this.UserID = "AUTOMATION_ITM";
            this.PassWord = "1";
            this.FolderServer = "/Project Hao";
            this.Image = "1.jpg";
        }
        public FTPClientSettings Clone()
        {
            return new FTPClientSettings()
            {
                Host = string.Copy(this.Host),
                Port = this.Port,
                UserID = string.Copy(this.UserID),
                PassWord = string.Copy(this.PassWord),
                FolderServer = string.Copy(this.FolderServer),
                Image = string.Copy(this.Image)
            };
        }
    }
    public class Model
    {
        public String Name { get; set; }
        public ROISettings ROI { get; set; }
        public int WhitePixels { get; set; }
        public int BlackPixels { get; set; }
        public int MatchingRate { get; set; }
        public int MatchingRateMin { get; set; }
        public int Threshol { get; set; }
        public int ThresholBl { get; set; }
        public bool CirWhCntEnb { get; set; }
        public bool RoiWhCntEnb { get; set; }
        public OpenCvSharp.Point BarCodeOffSet { get; set; }
        public Boolean OffSetJigEnb { get; set; }

        public Model()
        {
            this.Name = "2833";
            this.ROI = new ROISettings();
            this.MatchingRate = 100;
            this.MatchingRateMin = 70;
            this.BarCodeOffSet = new OpenCvSharp.Point { };
            this.WhitePixels = 100;
            this.BlackPixels = 10;
            this.Threshol = 127;
            this.ThresholBl = 40;
            this.OffSetJigEnb = false;
            this.CirWhCntEnb = true;
            this.RoiWhCntEnb = false;
            
           
        }

        public Model Clone()
        {
            return new Model()
            {
                Name = String.Copy(this.Name),
                ROI = this.ROI,
                WhitePixels = this.WhitePixels,
                BlackPixels = this.BlackPixels,
                MatchingRate = this.MatchingRate,
                MatchingRateMin = this.MatchingRateMin,
                BarCodeOffSet = this.BarCodeOffSet,
                OffSetJigEnb = this.OffSetJigEnb,
                Threshol = this.Threshol,
                ThresholBl = this.ThresholBl,
                CirWhCntEnb = this.CirWhCntEnb,
                RoiWhCntEnb = this.RoiWhCntEnb
            };
        }
    }
    



    public class ROISettings
    {
        public List<OpenCvSharp.Rect> listRectangle { get; set; }
        
        public ROISettings()
        {
           listRectangle = new List<OpenCvSharp.Rect>() { };

        }
        public ROISettings Clone()
        {
            return new ROISettings
            {
                listRectangle = this.listRectangle,

            };
        }
    }

    public class ROIProperty
    {
        public int StrokeThickness { get; set; }
        public int labelFontSize { get; set; }
        public OpenCvSharp.Size rectSize { get; set; }

        public ROIProperty() 
        {
            StrokeThickness = 7;
            labelFontSize = 25;
            rectSize = new OpenCvSharp.Size(10,10);
        
        }
        public ROIProperty Clone() 
        {
            return new ROIProperty
            {
                StrokeThickness = this.StrokeThickness,
                labelFontSize = this.labelFontSize,
                rectSize = this.rectSize,
            };
        }

    }
}
