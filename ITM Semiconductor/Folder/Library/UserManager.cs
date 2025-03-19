using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
    class UserManager
    {
        private static MyLogger logger = new MyLogger("UserManager");

        private static int isLogOn = 0;
        public static Boolean isLogOnSuper = false;
        public static Boolean isLogOnManager = false;
        public static Boolean isLogOnOperater = false;


        public static void createUserLog(UserActions action, String detail = null)
        {
            try
            {
                var log = new UserLog(action);
                if (detail != null)
                {
                    log.Message += ": " + detail;
                }
                DbWrite.createUserLog(log);
            }
            catch (Exception ex)
            {
                logger.Create("createUserLog error:" + ex.Message,LogLevel.Error) ;
            }
        }

        public static int LogOn(String username, String password)
        {
            string EN = "Manager";
            string ADM = "AutoTeams";
            string OP = "Operator";
            try
            {
                logger.Create(String.Format("LogOn: {0}/{1}", username, password) ,LogLevel.Information);

                if (username != null && username.Equals(EN) && password != null && password.Equals(UiManager.appSettings.PassWordEN))
                {
                    isLogOn = 1;
                }
              

               else if (username != null &&  username.Equals(ADM) && password != null && password.Equals(UiManager.appSettings.PassWordADM))
                {
                    isLogOn = 2;
                }
               
               else if (username != null && username.Equals(OP))
                {
                    isLogOn = 3;
                }
                else
                {
                    isLogOn = 0; 
                }

            }
            catch (Exception ex)
            {
                logger.Create("LogOn error:" + ex.Message, LogLevel.Error);
            }
            return isLogOn;
        }

        public static void LogOut()
        {
            logger.Create("LogOut", LogLevel.Information);
            isLogOn = 0;
        }

        public static int IsLogOn()
        {
            return isLogOn;
        }

        public static Boolean ChangePassword(string UserName ,String passOld, String passNew)
        {
           
            try
            {
                logger.Create(String.Format("ChangePassword: Old={0}, New={1}" , passOld, passNew) + String.Format("  ChangeUserName:" + UserName), LogLevel.Information);

                if (UserName != null && UserName.Equals(UiManager.appSettings.UseName) && passOld != null && passNew != null && passNew.Length > 0 && passOld.Equals(UiManager.appSettings.PassWordEN))
                {
                    UiManager.appSettings.PassWordEN = String.Copy(passNew);
                    UiManager.SaveAppSetting();
                    return true;
                }
                if (UserName != null && UserName.Equals(UiManager.appSettings.UseName) && passOld != null && passNew != null && passNew.Length > 0 && passOld.Equals(UiManager.appSettings.PassWordADM))
                {
                    UiManager.appSettings.PassWordADM = String.Copy(passNew);
                    UiManager.SaveAppSetting();
                    return true;
                }
            }
            catch (Exception ex)
            {
                logger.Create("ChangePassword error:" + ex.Message,LogLevel.Error);
            }
            return false;
        }
    }
}
