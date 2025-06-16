using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace AHTAPI.Repositories
{
    public class DepArrRepository
    {
        string connectionString;
        public DepArrRepository (string connectionString)
        {
            this.connectionString = connectionString;
        }

        #region Get ArrivalsList
        public List<AHT_DepArr> DepArrList(string date, string adi)
        {
            List<AHT_DepArr> aHT_DepArrs = new List<AHT_DepArr>();
            AHT_DepArr aHT_DepArr;
            var data = GetDepArrList(date, adi);
            foreach (DataRow row in data.Rows)
            {
                string chedule = ConvertToFormattedDate(row["Schedule"].ToString());
                aHT_DepArr = new AHT_DepArr
                {
                    Id = row["Id"].ToString(),
                    Flight = row["Flight"].ToString(),
                    City = row["City"].ToString(),
                    ScheduledDate = row["ScheduledDate"].ToString(),
                    Schedule = chedule,
                    Estimated = row["Estimated"].ToString(),
                    Actual = row["Actual"].ToString(),
                    Belt = row["Belt"].ToString(),
                    Gate = row["Gate"].ToString(),
                    RowFrom = row["RowFrom"].ToString(),
                    RowTo = row["RowTo"].ToString(),
                    Checkincounters = row["Checkincounters"].ToString(),
                    Mcat = row["Mcat"].ToString(),
                    Mcdt = row["Mcdt"].ToString(),
                    Status = row["Status"].ToString(),
                    Remark = row["Remark"].ToString(),

                };
                aHT_DepArrs.Add(aHT_DepArr);
            }
            return aHT_DepArrs;
        }

        private string ConvertToFormattedDate(string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime date))
            {
                return date.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            }
            return null;
        }
        public DataTable GetDepArrList(string date, string adi)
        {
            string query = "SELECT Id,CONCAT(LineCode, Number) AS Flight,  ScheduledDate,Schedule, Estimated, Actual, Status, Remark, City, RowFrom, Rowto, Gate,Belt, Mcdt, Mcat, CheckInCounters "+
                           "FROM AHT_FlightInformation WHERE Schedule BETWEEN DATEADD(HOUR, 5, CAST(CAST('"+date+"' AS DATE) AS DATETIME))"+
                           "AND DATEADD(HOUR, 28, CAST(CAST('"+date+"' AS DATE) AS DATETIME)) AND Adi = '"+adi+"' ORDER BY Schedule ASC";
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }
                    return dataTable;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally { connection.Close(); }
            }
        }
        #endregion
    }
}
