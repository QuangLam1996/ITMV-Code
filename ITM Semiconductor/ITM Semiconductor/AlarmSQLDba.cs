using DTO;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
    class AlarmSQLDba
    {
     
        private static MyLogger logger = new MyLogger("AlarmSQLDba");
        private const String dbFileName = "AlarmSQLDba.db";

        public static SQLiteConnection GetConnection()
        {
            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), dbFileName);
            var dbConnectionString = String.Format("Data Source={0};Mode=ReadWrite;", dbPath);
            var conn = new SQLiteConnection(dbConnectionString);
            conn.DefaultTimeout = 10;
            return conn;
        }
        public static void AlarmCreateDatabase()
        {
            try
            {
                var dbPath = Path.Combine(Directory.GetCurrentDirectory(), dbFileName);
                if (!File.Exists(dbPath))
                {
                    logger.Create(" -> Database File SQLite Not Existed -> Create New!", LogLevel.Information);
                    SQLiteConnection.CreateFile(dbPath);

                    createTableAlarm();
                }
                else
                {
                    logger.Create(" ->  Database File SQLite Already Existed!", LogLevel.Information);
                }
            }
            catch (Exception ex)
            {
                logger.Create("CreateDatabaseIfNotExisted Error: " + ex.Message, LogLevel.Error);
            }
        }
        private static void createTableAlarm()
        {
            try
            {
                logger.Create("Create TableUserLog: ", LogLevel.Information);
                using (var conn = AlarmSQLDba.GetConnection())
                {
                    var sql = $"CREATE TABLE IF NOT EXISTS AlarmImage_log (id INTEGER NOT NULL, NameImage TEXT NOT NULL)";
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
    }
}
