using AHTAPI.Models;
using Microsoft.Data.SqlClient;

namespace AHTAPI.Repositories
{
    public class WaitingTimeNewRepository
    {
        string connectionString;
        public WaitingTimeNewRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public async Task<bool> AddDataToDB(WaitingTimeFlight data)
        {
            bool result = true;
            if (data == null) return false;
            DateTime now = DateTime.Now;
            // Save Checkin Area data
            foreach (var checkinArea in data.CheckinArea)
            {
                foreach (var flight in checkinArea.Flights)
                {
                    if (flight.Data != null)
                    {
                        foreach (var flightData in flight.Data) 
                        {
                            result = await InsertIntoDatabase(checkinArea.AreaName,
                                                              flight.Name, flight.Airline, flight.Number, flight.FlightCounters,
                                                                 flightData.InPeople, flightData.WaitingTime, flightData.LaneIdOfFlight, flightData.AverageProcessTime, flightData.LaneCounters,
                                                              flight.ScheduleDate, flight.DepartureTime, flight.CounterStart, flight.CounterEnd, now);
                            if (!result) return false;
                        }
                        
                    }


                }
            }

            // Save Other Area data
            foreach (var otherArea in data.OtherArea)
            {
                if (otherArea.Datas != null)
                {
                    foreach (var lane in otherArea.Datas)
                    {
                        result = await InsertOtherAreaintodatabase(otherArea.AreaName, 
                                                                   lane.InPeople, lane.LaneId, lane.WaitingTime, lane.AverageProcessTime, now);
                        if (!result) return false;

                    }
                }    
                
            }

            return result;
        }

        private async Task<bool> InsertIntoDatabase(string AreaName, 
                                                    string Name, string Airline, string Number, List<int> FlightCounters,
                                                      int InPeople, double WaitingTime, string LaneIdOfFlight, double AverageProcessTime, List<string> LaneCounters,
                                                    string ScheduleDate, string DepartureTime, string CounterStart, string CounterEnd, DateTime now)
        {
            string query = @"
                           INSERT INTO AHT_CheckinArea
                           (AreaName, Airline, LineCode, Number, CheckInCounters, ScheduleDate, InPeople, WaitingTime, AverageProcessTime, 
                           LaneIdOfFlight, LaneCounters, DepartureTime, CounterStart, CounterEnd, CreatedAt)
                           VALUES
                           (@AreaName, @Airline, @LineCode, @Number, @CheckInCounters, @ScheduleDate, @InPeople, @WaitingTime, @AverageProcessTime, 
                           @LaneIdOfFlight, @LaneCounters, @DepartureTime, @CounterStart, @CounterEnd, @CreatedAt)";


            string flightCountersString = FlightCounters != null && FlightCounters.Count > 0 ? string.Join(",", FlightCounters) : null;
            string laneCountersString = LaneCounters != null && LaneCounters.Count > 0 ? string.Join(",", LaneCounters) : null;


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@AreaName", AreaName);
                        command.Parameters.AddWithValue("@Airline", Name);
                        command.Parameters.AddWithValue("@LineCode", Airline);
                        command.Parameters.AddWithValue("@Number", Number);
                        command.Parameters.AddWithValue("@CheckInCounters", flightCountersString ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ScheduleDate", ScheduleDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@InPeople", InPeople);
                        command.Parameters.AddWithValue("@WaitingTime", WaitingTime);
                        command.Parameters.AddWithValue("@AverageProcessTime", AverageProcessTime);
                        command.Parameters.AddWithValue("@LaneIdOfFlight", LaneIdOfFlight ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@LaneCounters", laneCountersString ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DepartureTime", DepartureTime);
                        command.Parameters.AddWithValue("@CounterStart", CounterStart);
                        command.Parameters.AddWithValue("@CounterEnd", CounterEnd);
                        command.Parameters.AddWithValue("@CreatedAt", now);

                        // Execute the query
                        await command.ExecuteNonQueryAsync();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }


        private async Task<bool> InsertOtherAreaintodatabase(string AreaName, 
                                                             int InPeople, string LaneId, double WaitingTime, double AverageProcessTime, DateTime now)
        {
            string query = @"
                           INSERT INTO AHT_OtherArea
                           (AreaName, InPeople, LaneId, WaitingTime, AverageProcessTime, CreatedAt)
                           VALUES
                           (@AreaName, @InPeople, @LaneId, @WaitingTime, @AverageProcessTime, @CreatedAt)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@AreaName", AreaName);
                        command.Parameters.AddWithValue("@InPeople", InPeople);
                        command.Parameters.AddWithValue("@LaneId", LaneId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@WaitingTime", WaitingTime);
                        command.Parameters.AddWithValue("@AverageProcessTime", AverageProcessTime);
                        command.Parameters.AddWithValue("@CreatedAt", now);

                        // Execute the query
                        await command.ExecuteNonQueryAsync();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
    }
}
