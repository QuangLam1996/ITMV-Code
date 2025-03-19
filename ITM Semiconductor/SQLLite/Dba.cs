using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using DTO;
namespace ITM_Semiconductor
{
    class Dba
    {
        private static MyLogger logger = new MyLogger("Dba");
        private const String dbFileName = "SIP_AutoLoading.db";

        public static SQLiteConnection GetConnection()
        {
            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), dbFileName);
            var dbConnectionString = String.Format("Data Source={0};Mode=ReadWrite;", dbPath);
            var conn = new SQLiteConnection(dbConnectionString);
            conn.DefaultTimeout = 10;
            return conn;
        }
        public static void createDatabaseIfNotExisted()
        {
            try
            {
                var dbPath = Path.Combine(Directory.GetCurrentDirectory(), dbFileName);
                if (!File.Exists(dbPath))
                {
                    logger.Create(" -> Database File SQLite Not Existed -> Create New!",LogLevel.Information);
                    SQLiteConnection.CreateFile(dbPath);

                    createTableUserLog();// Table User Log

                    createTableQr1Status();// Table Tack time

                    createTableEventLog(); // Table Event Log

                    createTableAlarmLog(); // Table Alarm Log

                    createTableAlarmImage();
                }
                else
                {
                    logger.Create(" ->  Database File SQLite Already Existed!",LogLevel.Information);
                }
            }
            catch (Exception ex)
            {
                logger.Create("CreateDatabaseIfNotExisted Error: " + ex.Message,LogLevel.Error);
            }
        }
        private static void createTableAlarmImage()
        {
            try
            {
                logger.Create("Create TableAlarmImage: ", LogLevel.Information);
                using (var conn = Dba.GetConnection())
                {
                    //string sql = "CREATE TABLE IF NOT EXISTS AlarmImage_log (id INTEGER NOT NULL, NameImage TEXT NOT NULL,PRIMARY KEY(id AUTOINCREMENT))";
                    var sql = $"CREATE TABLE IF NOT EXISTS AlarmImage_log (id INTEGER NOT NULL, NameImage TEXT NOT NULL, PRIMARY KEY(id AUTOINCREMENT))";
                    using (var sqlCmd = conn.CreateCommand())
                    {
                        sqlCmd.CommandText = sql;
                        conn.Open();
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Create("Create TableUserLog Error: " + ex.Message, LogLevel.Error);
            }
        }

        private static void createTableUserLog()
        {
            try
            {
                logger.Create("Create TableUserLog: ",LogLevel.Information);
                using (var conn = Dba.GetConnection())
                {
                    var sql = $"CREATE TABLE IF NOT EXISTS user_log (id INTEGER NOT NULL, username TEXT NOT NULL, action INTEGER NOT NULL, created_time TEXT, message TEXT, PRIMARY KEY(id AUTOINCREMENT))";
                    using (var sqlCmd = conn.CreateCommand())
                    {
                        sqlCmd.CommandText = sql;
                        conn.Open();
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Create("Create TableUserLog Error: " + ex.Message, LogLevel.Error);
            }
        }

        private static void createTableQr1Status()
        {
            try
            {
                logger.Create("createTableQr1Status:",LogLevel.Information);
                using (var conn = Dba.GetConnection())
                {
                    var sql = $"CREATE TABLE IF NOT EXISTS qr1_status (key TEXT NOT NULL UNIQUE, value TEXT NOT NULL, PRIMARY KEY(key))";
                    using (var sqlCmd = conn.CreateCommand())
                    {
                        sqlCmd.CommandText = sql;
                        conn.Open();
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Create("Create TableQr1Status ex:" + ex.Message, LogLevel.Error);
            }
        }

        private static void createTableEventLog()
        {
            try
            {
                logger.Create("createTableEventLog:", LogLevel.Information);
                using (var conn = Dba.GetConnection())
                {
                    var sql = $"CREATE TABLE IF NOT EXISTS event_log (id INTEGER NOT NULL, created_time TEXT NOT NULL, message TEXT NOT NULL, event_type INTEGER DEFAULT 0, PRIMARY KEY(id AUTOINCREMENT))";
                    using (var sqlCmd = conn.CreateCommand())
                    {
                        sqlCmd.CommandText = sql;
                        conn.Open();
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Create("createTableEventLog ex:" + ex.Message, LogLevel.Error);
            }
        }

        private static void createTableAlarmLog()
        {
            try
            {
                logger.Create("createTableAlarmImageLog:", LogLevel.Information);
                using (var conn = Dba.GetConnection())
                {
                    var sql = $"CREATE TABLE IF NOT EXISTS alarm_log (id INTEGER, created_time TEXT NOT NULL, alarm_code INTEGER NOT NULL, message TEXT, solution TEXT, mode INTEGER, PRIMARY KEY(id AUTOINCREMENT))";
                    using (var sqlCmd = conn.CreateCommand())
                    {
                        sqlCmd.CommandText = sql;
                        conn.Open();
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Create("createTableAlarmLog ex:" + ex.Message, LogLevel.Error);
            }
        }

    }
}
