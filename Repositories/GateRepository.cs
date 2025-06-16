using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Globalization;

namespace AHTAPI.Repositories
{
    public class GateRepository
    {
        string connectionString;
        public GateRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #region get flight by counter
        public List<AHT_Departures> FlightListByGate(string date, string Ip)
        {
            List<AHT_Departures> aHT_Counters = new List<AHT_Departures>();
            AHT_Departures aHT_Counter;
            var data = GetFlightByCounterAndIp(date, Ip);

            foreach (DataRow row in data.Rows)
            {
                string chedule = ConvertToFormattedDate(row["Schedule"].ToString());
                string counterStartFormatted = ConvertToFormattedDate(row["CounterStart"].ToString());
                string counterEndFormatted = ConvertToFormattedDate(row["CounterEnd"].ToString());
                string gateStartFormatted = ConvertToFormattedDate(row["GateStart"].ToString());
                string gateEndFormatted = ConvertToFormattedDate(row["GateEnd"].ToString());
                var departure = new AHT_Departures()
                {
                    Id = row["Id"].ToString(),
                    ScheduledDate = row["ScheduledDate"].ToString(),
                    Schedule = row["Schedule"].ToString(),
                    Estimated = row["Estimated"].ToString(),
                    Actual = row["Actual"].ToString(),
                    LineCode = row["LineCode"].ToString(),
                    Flight = row["Flight"].ToString(),
                    City = row["City"].ToString(),
                    Gate = row["Gate"].ToString(),
                    Remark = row["Remark"].ToString(),
                    Status = row["Status"].ToString(),
                    RowFrom = row["RowFrom"].ToString(),
                    RowTo = row["RowTo"].ToString(),
                    CheckInCounters = row["CheckInCounters"].ToString(),
                    CounterStart = counterStartFormatted,
                    CounterEnd = counterEndFormatted,
                    GateStart = gateStartFormatted,
                    GateEnd = gateEndFormatted,
                    Mcdt = row["Mcdt"].ToString()
                };
                aHT_Counters.Add(departure);
            }

            return aHT_Counters;
        }

        private string ConvertToFormattedDate(string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime date))
            {
                return date.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            }
            return null;
        }

        public DataTable GetFlightByCounterAndIp(string date, string ip)
        {
            string query = "SELECT F.Id, F.ScheduledDate,F.Schedule, F.Estimated, F.Actual, CONCAT(F.LineCode, F.Number) AS Flight, F.Mcdt, "+
                           "F.LineCode, F.City, F.Gate, F.Remark, F.Status, F.RowFrom, F.RowTo, F.CheckInCounters, F.CounterStart, F.CounterEnd, F.GateStart, F.GateEnd "+
                           "FROM AHT_FidsLocation C LEFT JOIN AHT_FlightInformation F ON C.Name = F.Gate " +
                           "AND F.Schedule BETWEEN DATEADD(HOUR, 5, CAST(CAST(@Date AS DATE) AS DATETIME)) "+
                           "AND DATEADD(HOUR, 28, CAST(CAST(@Date AS DATE) AS DATETIME)) and F.Remark<> 'Departed' and F.Remark<> 'Gate closed' " +
                           "AND F.Status<> '' AND F.Status<> 'Cancelled' AND F.Adi = 'D' WHERE C.Ip = @local Order by Schedule ASC ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@local", ip);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);
                            return dataTable;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        #endregion
    }
}
