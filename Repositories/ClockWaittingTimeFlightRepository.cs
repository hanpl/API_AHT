using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml.Linq;

namespace AHTAPI.Repositories
{
    public class ClockWaittingTimeFlightRepository
    {
        string connectionString;
        public ClockWaittingTimeFlightRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<ClockWTTFlight> GetWaitingTimeFlights(string name)
        {
            List<ClockWTTFlight> clockWTTFlights = new List<ClockWTTFlight>();
            ClockWTTFlight clockWTTFlight;
            var data = GetWaitingTimeFlightsFromDb(name);
            foreach (DataRow row in data.Rows)
            {
                clockWTTFlight = new ClockWTTFlight
                {
                    AreaName = row["AreaName"].ToString(),
                    Airline = row["Airline"].ToString(),
                    Linecode = row["Linecode"].ToString(),
                    Number = row["Number"].ToString(),
                    ScheduleDate = row["ScheduleDate"].ToString(),
                    MaxWaitingTime = row["MaxWaitingTime"] != DBNull.Value ? Convert.ToDouble(row["MaxWaitingTime"]) : 0.0,
                    DepartureTime = row["DepartureTime"].ToString(),
                    CounterStart = row["CounterStart"].ToString(),
                    CounterEnd = row["CounterEnd"].ToString(),
                    CreatedAt = row["CreatedAt"].ToString(),
                };
                clockWTTFlights.Add(clockWTTFlight);
            }
            return clockWTTFlights;
        }

        public DataTable GetWaitingTimeFlightsFromDb(string bien)
        {
            string query = "SELECT AreaName, Airline,Linecode, Number,ScheduleDate, MAX(WaitingTime) AS MaxWaitingTime, " +
                            " DepartureTime, CounterStart, CounterEnd, CreatedAt "+
                            " FROM   [WAITINGTIME].[dbo].[AHT_CheckinArea] "+
                            " WHERE GETDATE() BETWEEN [CounterStart] AND [CounterEnd] "+
                            " AND [CreatedAt] = (SELECT MAX([CreatedAt]) FROM [WAITINGTIME].[dbo].[AHT_CheckinArea] "+
                            " WHERE GETDATE() BETWEEN [CounterStart] AND [CounterEnd]) AND LOWER([AreaName]) LIKE "+" '%check-in " + bien + "%' " +
                            " GROUP BY AreaName,Airline,Linecode,Number,ScheduleDate,DepartureTime,CounterStart,CounterEnd,CreatedAt "+
                            " HAVING MAX(WaitingTime) IS NOT NULL;";
            DataTable dataTable = new DataTable();
            Console.WriteLine(query);
            //string connectionString = "Data Source=172.17.2.38;Initial Catalog=MSMQFLIGHT;Persist Security Info=True;User ID=sa;Password=AHT@2019";
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

        
    }
}
