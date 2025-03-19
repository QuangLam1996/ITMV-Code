using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

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

        // Setting Scanner
        public SettingScanner SettingScanner { get => settingScanner; set => settingScanner = value; }
        public SettingPLCCom SettingPLCCom { get => settingPLCCom; set => settingPLCCom = value; }
        public Setting_PLCTcp Setting_PLCTcp { get => setting_PLCTcp; set => setting_PLCTcp = value; }
        public SettingScannerTCP SettingScannerTCP { get => settingScannerTCP; set => settingScannerTCP = value; }



        public RunSettings run { get; set; }


        public AppSettings()
        {
            this.UseName = DEFAULT_USER_NAME;
            this.Operator = DEFAULT_USER_NAME;

            this.settingScanner = new SettingScanner();
            this.settingPLCCom = new SettingPLCCom();

            this.connection = new ConnectionSettings();
            
            this.setting_PLCTcp = new Setting_PLCTcp();

            this.settingScannerTCP = new SettingScannerTCP();

            this.run = new RunSettings();

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
            return _appSettings;
        }
       
    }
    class ConnectionSettings
    {
        public ComSettings scanner { get; set; }
        public ConnectionSettings()
        {
            this.scanner = new ComSettings();
        }
        public ConnectionSettings Clone()
        {
            return new ConnectionSettings
            {
                scanner = this.scanner.Clone(),
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
        public bool CheckCountQR { get; set; } = true;

        public bool MachineQR { get; set; } = true;

        public String jigOfflineCode { get; set; } = "";

        public int scannerExchangingPeriod { get; set; } = 0;
        public int scannerCalibPeriod { get; set; } = 0;

        public RunSettings()
        {
        }

        public RunSettings Clone()
        {
            return new RunSettings
            {
                MachineQR = this.MachineQR,
                CheckCountQR = this.CheckCountQR,
                jamAction = this.jamAction,
                autoJigEnd = this.autoJigEnd,
                PackingOnline = this.PackingOnline,
                PackingEnd = this.PackingEnd,
                qrCrossCheck = this.qrCrossCheck,
                //lotCheck = this.lotCheck,
                mesOnline = this.mesOnline,
                //testerOnline = this.testerOnline,
                scannerOnline = this.scannerOnline,
                jigOfflineCode = this.jigOfflineCode,
                scannerExchangingPeriod = this.scannerExchangingPeriod,
                scannerCalibPeriod = this.scannerCalibPeriod
            };
        }
    }

}
