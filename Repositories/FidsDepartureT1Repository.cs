using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Globalization;

namespace AHTAPI.Repositories
{
    public class FidsDepartureT1Repository
    {
        string connectionString;
        public FidsDepartureT1Repository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #region Get DeparturesList
        public List<AHT_T1_Departures> DeparturesList()
        {
            List<AHT_T1_Departures> aHT_Departuress = new List<AHT_T1_Departures>();
            AHT_T1_Departures aHT_Departures;
            var data = GetDeparturesList();
            foreach (DataRow row in data.Rows)
            {
                string SOBT = ConvertToFormattedDate(row["SOBT"].ToString());
                string EOBT = ConvertToFormattedDate(row["EOBT"].ToString());
                string Mcdt = ConvertToFormattedDate(row["Mcdt"].ToString());
                aHT_Departures = new AHT_T1_Departures
                {
                    Id = row["Id"].ToString(),
                    ScheduledDate = row["ScheduledDate"].ToString(),
                    SOBT = SOBT,
                    EOBT = EOBT,
                    Mcdt = Mcdt,
                    LineCode = row["LineCode"].ToString(),
                    FlightNo = row["FlightNo"].ToString(),
                    Status = row["Status"].ToString(),
                    CkiRow = row["CkiRow"].ToString(),
                    City = row["City"].ToString(),
                    Route = row["Route"].ToString(),
                    DGATE = row["DGATE"].ToString()
                };
                aHT_Departuress.Add(aHT_Departures);
            }
            return aHT_Departuress;
        }

        private string ConvertToFormattedDate(string input)
        {
            // Kiểm tra độ dài chuỗi phải là 4 ký tự
            if (string.IsNullOrWhiteSpace(input) || input.Length < 4)
            {
                return "";
            }

            // Kiểm tra toàn bộ chuỗi phải là số
            if (!int.TryParse(input, out _))
            {
                return "";
            }

            // Lấy giá trị giờ và phút
            string hours = input.Substring(0, 2);
            string minutes = input.Substring(2, 2);

            // Kiểm tra giờ và phút hợp lệ
            if (!int.TryParse(hours, out int hourValue) || hourValue < 0 || hourValue > 23)
            {
                return "";
            }

            if (!int.TryParse(minutes, out int minuteValue) || minuteValue < 0 || minuteValue > 59)
            {
                return "";
            }

            // Chuyển đổi sang định dạng HH:mm
            return $"{hours}:{minutes}";
        }
        public DataTable GetDeparturesList()
        {
            string query = "SELECT Top 30 Id,ScheduledDate, LineCode, FlightNo, Route, City, SOBT, EOBT, Mcdt, Status, CkiRow, DGATE " +
                           "FROM[RABBITMQFLIGHT].[dbo].[AHT_T1_FlightInformation] "+ 
                           "WHERE Terminal = 'D' and ISNUMERIC(SOBT) = 1 AND LEN(SOBT) = 4 AND Status<>'XXX' "+
                           "AND CONCAT(CONVERT(VARCHAR(10), ScheduledDate, 120), ' ', STUFF(SOBT, 3, 0, ':')) "+
                           "BETWEEN DATEADD(MINUTE, -50, GETDATE()) AND DATEADD(HOUR, 24, GETDATE()) "+
                           "ORDER BY ScheduledDate ASC, SOBT ASC";
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
