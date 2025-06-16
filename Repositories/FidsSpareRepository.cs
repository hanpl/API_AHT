using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AHTAPI.Repositories
{
    public class FidsSpareRepository
    {
        string connectionString;
        public FidsSpareRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //Http Get DeparturesList
        #region Get DeparturesList
        public List<AHT_Departures> DeparturesList()
        {
            List<AHT_Departures> aHT_Departuress = new List<AHT_Departures>();
            AHT_Departures aHT_Departures;
            var data = GetDeparturesList();
            foreach (DataRow row in data.Rows)
            {
                string chedule = ConvertToFormattedDate(row["Schedule"].ToString());
                string estimated = ConvertToFormattedDate(row["Estimated"].ToString());
                string actual = ConvertToFormattedDate(row["Actual"].ToString());
                string counterStart = ConvertToFormattedDate(row["CounterStart"].ToString());
                aHT_Departures = new AHT_Departures
                {
                    Id = row["Id"].ToString(),
                    ScheduledDate = row["ScheduledDate"].ToString(),
                    Schedule = chedule,
                    Actual = actual,
                    Estimated = estimated,
                    LineCode = row["LineCode"].ToString(),
                    Flight = row["Flight"].ToString(),
                    City = row["City"].ToString(),
                    Gate = row["Gate"].ToString(),
                    CheckInCounters = row["Counter"].ToString(),
                    CounterStart = counterStart,
                    Remark = row["Remark"].ToString(),
                    

                };
                aHT_Departuress.Add(aHT_Departures);
            }
            return aHT_Departuress;
        }

        private string ConvertToFormattedDate(string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime date))
            {
                return date.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            }
            return "";
        }
        public DataTable GetDeparturesList()
        {
            string query = "SELECT TOP 30 FI.Id, FI.ScheduledDate, FI.Schedule,FI.Estimated,FI.LineCode, FI.Actual, " +
                            "CONCAT(FI.LineCode, FI.Number) AS Flight, FI.City, FI.Gate, "+
                            "CONCAT(FI.RowFrom, '-', FI.Rowto) AS Counter, FI.Remark, FI.CounterStart, "+
                            "STRING_AGG(CONCAT(CS.LineCode, CS.Number), ', ') AS Codeshare "+
                            "FROM AHT_FlightInformation FI LEFT JOIN AHT_Codeshare CS ON FI.Id = CS.IdFlightInformation "+
                            "WHERE FI.Mcdt BETWEEN DATEADD(Mi, -20, GETDATE()) AND DATEADD(hh, 24, GETDATE()) "+
                            "AND FI.Status<> '' AND FI.Status<> 'Cancelled'AND FI.Adi = 'D' "+
                            "GROUP BY FI.Id, FI.ScheduledDate, FI.Schedule, FI.Estimated, FI.LineCode,FI.Actual, " +
                            "FI.Number, FI.City, FI.Gate, FI.RowFrom, FI.Rowto, FI.Remark, FI.CounterStart "+
                            "ORDER BY FI.Schedule ASC";
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


        public async Task<List<AHT_Departures>> GetFlightFidsDep(int rollOn, int rollOff)
        {
            var flights = new List<AHT_Departures>();


            string query = $@"SELECT TOP 30 FI.Id, FI.ScheduledDate, FI.Schedule,FI.Estimated,FI.LineCode, FI.Actual, 
                            CONCAT(FI.LineCode, FI.Number) AS Flight, FI.City, FI.Gate, 
                            CONCAT(FI.RowFrom, '-', FI.Rowto) AS Counter, FI.Remark, FI.CounterStart, FI.CounterEnd,
                            STRING_AGG(CONCAT(CS.LineCode, CS.Number), ', ') AS Codeshare
                            FROM AHT_FlightInformation FI LEFT JOIN AHT_Codeshare CS ON FI.Id = CS.IdFlightInformation 
                            WHERE FI.Mcdt BETWEEN DATEADD(Mi, -@RollOn, GETDATE()) AND DATEADD(Mi, @RollOff, GETDATE()) 
                            AND FI.Status<> '' AND FI.Status<> 'Cancelled'AND FI.Adi = 'D' 
                            GROUP BY FI.Id, FI.ScheduledDate, FI.Schedule, FI.Estimated, FI.LineCode,FI.Actual, 
                            FI.Number, FI.City, FI.Gate, FI.RowFrom, FI.Rowto, FI.Remark, FI.CounterStart , FI.CounterEnd
                            ORDER BY FI.Schedule ASC";
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@RollOn", rollOn);
                cmd.Parameters.AddWithValue("@RollOff", rollOff);

                await con.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        flights.Add(new AHT_Departures
                        {
                            Id = reader["Id"].ToString(),
                            ScheduledDate = reader["Scheduleddate"]?.ToString(),
                            Schedule = ConvertToFormattedDate(reader["Schedule"]?.ToString()),
                            Estimated = ConvertToFormattedDate(reader["Estimated"]?.ToString()),
                            Actual = ConvertToFormattedDate(reader["Actual"]?.ToString()),
                            CounterStart = ConvertToFormattedDate(reader["CounterStart"]?.ToString()),
                            CounterEnd = ConvertToFormattedDate(reader["CounterEnd"]?.ToString()),
                            CheckInCounters = reader["Counter"]?.ToString(),
                            LineCode = reader["LineCode"]?.ToString(),
                            Flight = reader["Flight"].ToString(),
                            City = reader["City"].ToString(),
                            Gate = reader["Gate"].ToString(),
                            Remark = reader["Remark"].ToString(),
                            CodeShare = reader["CodeShare"].ToString(),
                        });
                    }
                }
            }
            return flights;
        }


    }



}
