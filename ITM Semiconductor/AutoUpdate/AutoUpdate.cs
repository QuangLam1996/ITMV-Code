using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
    public class AutoUpdateSettings
    {
        public ConnectionSetting Connection { get; set; }

        public AutoUpdateSettings()
        {
            Connection = new ConnectionSetting();
        }
        public AutoUpdateSettings Clone()
        {
            return new AutoUpdateSettings
            {
                Connection = this.Connection.Clone()
            };
        }
        public String ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static AutoUpdateSettings FromJSON(String js)
        {
            var j = JsonConvert.DeserializeObject<AutoUpdateSettings>(js);
            var Ret = j.Clone();
            if (Ret.Connection == null) { Ret.Connection = new ConnectionSetting(); }
            return Ret;
        }
    }
    public class ConnectionSetting
    {
        public FTPClientSetting FTPClient { get; set; }
        public ConnectionSetting()
        {
            this.FTPClient = new FTPClientSetting();
        }
        public ConnectionSetting Clone()
        {
            return new ConnectionSetting
            {
                FTPClient = this.FTPClient.Clone()
            };
        }
    }
    public class FTPClientSetting
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public String UserID { get; set; }
        public String PassWord { get; set; }
        public string FolderServer { get; set; }
        public FTPClientSetting()
        {
            this.Host = "192.168.54.217";
            this.Port = 38;
            this.UserID = "AUTOMATION_ITM";
            this.PassWord = "1";
            this.FolderServer = "/Project Hao";

        }
        public FTPClientSetting Clone()
        {
            return new FTPClientSetting()
            {
                Host = string.Copy(this.Host),
                Port = this.Port,
                UserID = string.Copy(this.UserID),
                PassWord = string.Copy(this.PassWord),
                FolderServer = string.Copy(this.FolderServer)
            };
        }
    }
    public class AutoUpdate
    {
        public string VerName { get; set; }
        public InformationVer InformationVer { get; set; }
    }
    public class InformationVer
    {
        public string HistoryUpdate { get; set; }
        public string DateTimeUpdate { get; set; }
    }
    


}
