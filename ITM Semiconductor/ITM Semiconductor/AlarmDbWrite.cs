using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM_Semiconductor
{
    class AlarmDbWrite
    {
        private static MyLogger logger = new MyLogger("DbWrite");
        public static bool createAlarmImage(int id, string NameAlarmimage)
        {
            var ret = false;
            using (var conn = AlarmSQLDba.GetConnection())
            {
                var sql = @"INSERT INTO AlarmImage_log (id, NameImage) VALUES (@id, @solution) ON CONFLICT(id) DO UPDATE SET NameImage = excluded.NameImage;";
                //var sql = "INSERT INTO AlarmImage_log (NameImage) VALUES (@solution)";
                using (var sqlCmd = conn.CreateCommand())
                {
                    try
                    {
                        sqlCmd.CommandText = sql;
                        sqlCmd.Parameters.AddWithValue("@id", id);
                        sqlCmd.Parameters.AddWithValue("@solution", NameAlarmimage.ToString());

                        conn.Open();
                        ret = sqlCmd.ExecuteNonQuery() > 0;
                    }
                    catch (Exception ex)
                    {

                        logger.Create("CreateAlarm Error: " + ex.Message, LogLevel.Error);

                    }
                }
            }
            return ret;
        }
    }
}
