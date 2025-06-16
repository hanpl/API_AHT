using AHTAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace AHTAPI.Repositories
{
    public class SortingRepository
    {
        string connectionString;
        public SortingRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private string ConvertToFormattedDate(string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime date))
            {
                return date.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            }
            return "";
        }

        public async Task<List<SortingDep>> GetFlightSortingDep( int top, int timeStart, int timeEnd, string location,  int numberM)
        {
            Console.WriteLine("chami"+ top +" "+timeStart +" " + timeEnd +"  " +location  +"  "+numberM);
            var flights = new List<SortingDep>();
            // Xác định toán tử từ tham số 'location'
            Console.WriteLine("timeEnd: " + timeEnd);
            string comparisonOperator = location == "CKA" ? "<=" : ">";
            string comparisonOperatorM = location == "CKA" ? "<=" : ">";
            string query = $@"
            SELECT TOP (@Top)
                LineCode, CONCAT(LineCode, Number) AS Flight, City, Mcdt, Schedule, Estimated, 
                CounterStart, CounterEnd, RowFrom, RowTo, CheckInCounters, Remark, Status, Id
            FROM AHT_FlightInformation
            WHERE 
                Mcdt BETWEEN DATEADD(MINUTE, -@TimeStart, GETDATE()) 
                    AND DATEADD(HOUR, @TimeEnd, GETDATE())
                AND Status <> '' AND Status <> 'Cancelled' AND Adi = 'D'
                AND (
                    (ISNUMERIC(RowFrom) = 1 AND CAST(RowFrom AS INT) {comparisonOperator} 27)
                    OR (RowFrom LIKE 'M%' 
                        AND ISNUMERIC(SUBSTRING(RowFrom, 2, LEN(RowFrom) - 1)) = 1
                        AND CAST(SUBSTRING(RowFrom, 2, LEN(RowFrom) - 1) AS INT) {comparisonOperatorM} @NumberM)
                )
            ORDER BY CAST(Mcdt AS DATETIME) ASC";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Top", top);
                cmd.Parameters.AddWithValue("@TimeStart", timeStart);
                cmd.Parameters.AddWithValue("@TimeEnd", 24);
                cmd.Parameters.AddWithValue("@NumberM", numberM >0 ? numberM : 0);

                await con.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        flights.Add(new SortingDep
                        {
                            LineCode = reader["LineCode"]?.ToString(),
                            Flight = reader["Flight"]?.ToString(),
                            City = reader["City"]?.ToString(),
                            Mcdt = reader["Mcdt"]?.ToString(),
                            Schedule = ConvertToFormattedDate(reader["Schedule"]?.ToString()),
                            Estimated = ConvertToFormattedDate(reader["Estimated"]?.ToString()),
                            CounterStart = reader["CounterStart"]?.ToString(),
                            CounterEnd = reader["CounterEnd"]?.ToString(),
                            RowFrom = reader["RowFrom"]?.ToString(),
                            RowTo = reader["RowTo"]?.ToString(),
                            CheckInCounters = reader["CheckInCounters"]?.ToString(),
                            Remark = reader["Remark"]?.ToString(),
                            Status = reader["Status"]?.ToString(),
                            Id = reader["Id"]?.ToString(),
                        });
                    }
                }
            }
            return flights;
        }

        #region GetFlightSortingArr
        public async Task<List<AHT_SortingArr>> GetFlightSortingArr(string belt, int timeStart, int timeEnd)
        {
            var flights = new List<AHT_SortingArr>();
            // Xác định toán tử từ tham số 'location'

            string query = $@"
            SELECT TOP (5) LineCode, CONCAT(LineCode, Number) AS Flight, Belt, City,  Schedule, Estimated,  Actual, Mcat, Remark, Status, Id
            FROM [MSMQFLIGHT].[dbo].[AHT_FlightInformation]
            WHERE CAST(Mcat AS DATETIME) BETWEEN DATEADD(MINUTE, - @TimeStart, GETDATE()) 
                AND DATEADD(MINUTE, @TimeEnd, GETDATE())
                AND Status <> '' AND Status <> 'Cancelled' AND Adi = 'A' and Belt = @belt
            ORDER BY CAST(Mcat AS DATETIME) ASC";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@belt", belt);
                cmd.Parameters.AddWithValue("@TimeStart", timeStart);
                cmd.Parameters.AddWithValue("@TimeEnd", timeEnd);

                await con.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        flights.Add(new AHT_SortingArr
                        {
                            LineCode = reader["LineCode"]?.ToString(),
                            Flight = reader["Flight"]?.ToString(),
                            City = reader["City"]?.ToString(),
                            Mcat = reader["Mcat"]?.ToString(),
                            Schedule = ConvertToFormattedDate(reader["Schedule"]?.ToString()),
                            Estimated = ConvertToFormattedDate(reader["Estimated"]?.ToString()),
                            Actual = ConvertToFormattedDate(reader["Actual"]?.ToString()),
                            Belt = reader["Belt"]?.ToString(),
                            Remark = reader["Remark"]?.ToString(),
                            Status = reader["Status"]?.ToString(),
                            Id = reader["Id"]?.ToString(),
                        });
                    }
                }
            }
            return flights;
        }
        #endregion

        #region Get Device By Ip
        public AHT_FidsInformation? GetDeviceByIp(string ip)
        {
            var data = GetDeparturesList(ip);

            if (data.Rows.Count == 0)
                return null; // Không tìm thấy IP

            DataRow row = data.Rows[0];

            return new AHT_FidsInformation
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString(),
                Location = row["Location"].ToString(),
                Ip = row["Ip"].ToString(),
                Description = row["Description"].ToString(),
                RollOn = Convert.ToInt32(row["RollOn"]),
                RollOff = Convert.ToInt32(row["RollOff"]),
                PageSize = Convert.ToInt32(row["PageSize"]),
                ConnectionId = row["ConnectionId"].ToString()
            };
        }
        public DataTable GetDeparturesList(string ip)
        {
            string query = "SELECT * FROM [MSMQFLIGHT].[dbo].[AHT_FidsInformation] WHERE Ip = '" + ip + "'";
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
