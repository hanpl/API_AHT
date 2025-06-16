using AHTAPI.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AHTAPI.Repositories
{
    public class CheckinCounterRepository
    {
        string connectionString;
        public CheckinCounterRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        #region Get counterlist
        public List<AHT_Departures> FlightList(string date)
        {
            List<AHT_Departures> aHT_Departuress = new List<AHT_Departures>();
            AHT_Departures aHT_Departures;
            var data = GetFlightList(date);
            foreach (DataRow item in data.Rows)
            {
                string chedule = ConvertToFormattedDate(item["Schedule"].ToString());
                string estimated = ConvertToFormattedDate(item["Estimated"].ToString());
                string actual = ConvertToFormattedDate(item["Actual"].ToString());
                string counterStartFormatted = ConvertToFormattedDate(item["CounterStart"].ToString());
                string counterEndFormatted = ConvertToFormattedDate(item["CounterEnd"].ToString());
                string gateStartFormatted = ConvertToFormattedDate(item["GateStart"].ToString());
                string gateEndFormatted = ConvertToFormattedDate(item["GateEnd"].ToString());
                aHT_Departures = new AHT_Departures()
                {
                    Id = item["Id"].ToString(),
                    ScheduledDate = item["ScheduledDate"].ToString(),
                    Schedule = chedule,
                    Estimated = estimated,
                    Actual = actual,
                    LineCode = item["LineCode"].ToString(),
                    Flight = item["Flight"].ToString(),
                    City = item["City"].ToString(),
                    Gate = item["Gate"].ToString(),
                    RowFrom = item["RowFrom"].ToString(),
                    RowTo = item["RowTo"].ToString(),
                    CheckInCounters = item["CheckInCounters"].ToString(),
                    CounterStart = counterStartFormatted,
                    CounterEnd = counterEndFormatted,
                    GateStart = gateStartFormatted,
                    GateEnd = gateEndFormatted,
                    Status = item["Status"].ToString(),
                    Remark = item["Remark"].ToString(),
                };
                aHT_Departuress.Add(aHT_Departures);
            }
            return aHT_Departuress;
        }

        public DataTable GetFlightList(string date)
        {
            //string query = "SELECT Id, ScheduledDate,Schedule, Estimated, Actual, CONCAT(LineCode, Number) AS Flight, "+
            //    "    LineCode, City, Gate, Remark, Status, RowFrom, RowTo, CheckInCounters, CounterStart, CounterEnd, GateStart, GateEnd "+
            //    "    FROM AHT_FlightInformation where CheckInCounters ='' and Schedule BETWEEN DATEADD(HOUR, 5, CAST(CAST(@Date AS DATE) AS DATETIME)) " +
            //    "    AND DATEADD(HOUR, 28, CAST(CAST(@Date AS DATE) AS DATETIME)) AND Status <> '' AND Status <> 'Cancelled' AND Adi = 'D' ORDER BY Schedule ASC ";
            string query = "SELECT Id, ScheduledDate,Schedule, Estimated, Actual, CONCAT(LineCode, Number) AS Flight, " +
                "    LineCode, City, Gate, Remark, Status, RowFrom, RowTo, CheckInCounters, CounterStart, CounterEnd, GateStart, GateEnd " +
                "    FROM AHT_FlightInformation where Schedule BETWEEN DATEADD(HOUR, 5, CAST(CAST(@Date AS DATE) AS DATETIME)) " +
                "    AND DATEADD(HOUR, 28, CAST(CAST(@Date AS DATE) AS DATETIME)) AND Status <> '' AND Status <> 'Cancelled' AND Adi = 'D' ORDER BY Schedule ASC ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", date);
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
        #region get flight by counter
        public List<AHT_Departures> FlightListByC(string date, string Ip)
        {
            List<AHT_Departures> aHT_Counters = new List<AHT_Departures>();
            AHT_Departures aHT_Counter;
            var data = GetFlightByCounterAndIp(date, Ip);
            
            foreach (DataRow row in data.Rows)
            {
                string chedule = ConvertToFormattedDate(row["Schedule"].ToString());
                string estimated = ConvertToFormattedDate(row["Estimated"].ToString());
                string actual = ConvertToFormattedDate(row["Actual"].ToString());
                string counterStartFormatted = ConvertToFormattedDate(row["CounterStart"].ToString());
                string counterEndFormatted = ConvertToFormattedDate(row["CounterEnd"].ToString());
                string gateStartFormatted = ConvertToFormattedDate(row["GateStart"].ToString());
                string gateEndFormatted = ConvertToFormattedDate(row["GateEnd"].ToString());
                var departure = new AHT_Departures()
                {
                    Id = row["Id"].ToString(),
                    ScheduledDate = row["ScheduledDate"].ToString(),
                    Schedule = chedule,
                    Estimated = estimated,
                    Actual = actual,
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
                };
                aHT_Counters.Add(departure);
            }

            return aHT_Counters;
        }

        public DataTable GetFlightByCounterAndIp(string date, string ip)
        {
            string query = "SELECT " +
                           "F.Id, F.ScheduledDate,F.Schedule, F.Estimated, F.Actual, CONCAT(F.LineCode, F.Number) AS Flight, " +
                           "F.LineCode, F.City, F.Gate, F.Remark, F.Status, F.RowFrom, F.RowTo, F.CheckInCounters, F.CounterStart, F.CounterEnd, F.GateStart, F.GateEnd " +
                           "FROM AHT_FidsInformation C LEFT JOIN AHT_FlightInformation F ON " +
                           "SUBSTRING(C.Name, 2, LEN(C.Name) - 1) IN(SELECT value FROM STRING_SPLIT(F.CheckInCounters, ',')) " +
                           "AND F.Schedule BETWEEN DATEADD(HOUR, 5, CAST(CAST(@Date AS DATE) AS DATETIME)) " +
                           "AND DATEADD(HOUR, 28, CAST(CAST(@Date AS DATE) AS " +
                           "DATETIME)) " +
                           "AND F.Status<> '' AND F.Status<> 'Cancelled' AND F.Adi = 'D' " +
                           "WHERE C.Ip = @local ORDER BY C.Id ASC";

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

        public List<AHT_Counter> CountersList(string date, string local)
        {
            List<AHT_Counter> aHT_Counters = new List<AHT_Counter>();
            AHT_Counter aHT_Counter;
            var data = GetFlightByCounterName(date, local);
            Dictionary<string, AHT_Counter> countersDict = new Dictionary<string, AHT_Counter>();

            foreach (DataRow row in data.Rows)
            {
                string counterName = row["Name"].ToString();

                // Kiểm tra nếu CounterName đã tồn tại trong từ điển
                if (!countersDict.ContainsKey(counterName))
                {
                    countersDict[counterName] = new AHT_Counter()
                    {
                        Name = counterName,
                        Flights = new List<AHT_Departures>()
                    };
                }
                string chedule = ConvertToFormattedDate(row["Schedule"].ToString());
                string actual = ConvertToFormattedDate(row["Actual"].ToString());
                string estimated = ConvertToFormattedDate(row["Estimated"].ToString());
                string counterStartFormatted = ConvertToFormattedDate(row["CounterStart"].ToString());
                string counterEndFormatted = ConvertToFormattedDate(row["CounterEnd"].ToString());
                string gateStartFormatted = ConvertToFormattedDate(row["GateStart"].ToString());
                string gateEndFormatted = ConvertToFormattedDate(row["GateEnd"].ToString());
                // Tạo đối tượng AHT_Departures từ DataRow
                var departure = new AHT_Departures()
                {
                    Id = row["Id"].ToString(),
                    ScheduledDate = row["ScheduledDate"].ToString(),
                    Schedule = chedule,
                    Estimated = estimated,
                    Actual = actual,
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
                    Mcdt = row["Mcdt"].ToString() == null ? "NULL" : row["Mcdt"].ToString()

                };

                countersDict[counterName].Flights.Add(departure);
            }

            // Chuyển từ Dictionary sang List<AHT_Counter>
            aHT_Counters = countersDict.Values.ToList();

            return aHT_Counters;
        }
        private string ConvertToFormattedDate(string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime date))
            {
                return date.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            }
            return ""; 
        }
        public DataTable GetFlightByCounterName(string date, string id)
        {
            string query = "SELECT " +
                           "C.Name,F.Id, F.ScheduledDate,F.Schedule, F.Estimated, F.Actual, CONCAT(F.LineCode, F.Number) AS Flight, F.Mcdt, " +
                           "F.LineCode, F.City, F.Gate, F.Remark, F.Status, F.RowFrom, F.RowTo, F.CheckInCounters, F.CounterStart, F.CounterEnd, F.GateStart, F.GateEnd " +
                           "FROM AHT_FidsInformation C LEFT JOIN AHT_FlightInformation F ON " +
                           "SUBSTRING(C.Name, 2, LEN(C.Name) - 1) IN(SELECT value FROM STRING_SPLIT(F.CheckInCounters, ',')) " +
                           "AND F.Schedule BETWEEN DATEADD(HOUR, 5, CAST(CAST(@Date AS DATE) AS DATETIME)) " +
                           "AND DATEADD(HOUR, 28, CAST(CAST(@Date AS DATE) AS " +
                           "DATETIME)) " +
                           "AND F.Status<> '' AND F.Status<> 'Cancelled' AND F.Adi = 'D' " +
                           "WHERE C.Location = @local ORDER BY C.Id ASC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@local", id);
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
        public DataTable GetCountersList(string local)
        {
            string query = "  SELECT Name FROM [MSMQFLIGHT].[dbo].[AHT_FidsLocation] WHERE Location = '" + local + "' ORDER BY CAST(SUBSTRING(Name, 2, LEN(Name) - 1) AS INT) ASC";
            DataTable dataTable = new DataTable();
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
        #endregion

        public string converttime(string date)
        {
            try
            {
                // Định dạng ban đầu của ngày giờ (MM/dd/yyyy h:mm:ss tt)
                string inputFormat = "dd/MM/yyyy h:mm:ss tt";
                // Phân tích chuỗi ngày giờ đầu vào thành đối tượng DateTime
                DateTime dateTime = DateTime.ParseExact(date, inputFormat, CultureInfo.InvariantCulture);
                // Định dạng lại đối tượng DateTime thành chuỗi "yyyy-MM-dd HH:mm:ss.fff"
                string outputFormat = "yyyy-MM-ddTHH:mm:ss";
                return dateTime.ToString(outputFormat);
            }
            catch (FormatException)
            {
                // Xử lý trường hợp chuỗi ngày giờ đầu vào không hợp lệ
                return "Input date format is incorrect. Please use 'MM/dd/yyyy h:mm:ss tt'.";
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi không mong đợi khác
                return $"An error occurred: {ex.Message}";
                
            }
        }
        public string converttimeMcdt(string date)
        {
            try
            {
                // Định dạng ban đầu của ngày giờ (dd/MM/yyyy h:mm:ss tt)
                string inputFormat = "dd/MM/yyyy h:mm:ss tt";
                // Phân tích chuỗi ngày giờ đầu vào thành đối tượng DateTime
                DateTime dateTime = DateTime.ParseExact(date, inputFormat, CultureInfo.InvariantCulture);
                // Định dạng lại đối tượng DateTime thành chuỗi "MMM d yyyy h:mmtt"
                string outputFormat = "MMM d yyyy h:mmtt";
                return dateTime.ToString(outputFormat, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                // Xử lý trường hợp chuỗi ngày giờ đầu vào không hợp lệ
                return "Input date format is incorrect. Please use 'dd/MM/yyyy h:mm:ss tt'.";
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi không mong đợi khác
                return $"An error occurred: {ex.Message}";
            }
        }

        public string getRowFrom(string checkincounter)
        {
            if (checkincounter.Contains(","))
            {
                string[] chuoi = checkincounter.Split(',');
                string rowFrom = chuoi[0];
                return rowFrom;
            }
            else {
                return checkincounter;
            }
            
        }
        public string getRowTo(string checkincounter)
        {
            if (checkincounter.Contains(","))
            {
                string[] chuoi = checkincounter.Split(',');
                string rowTo = chuoi[^1];
                return rowTo;
            }
            else
            {
                return checkincounter;
            }
        }

        public bool UpdateFlight(AHT_Departures flightData)
        {
            string query = "UPDATE [MSMQFLIGHT].[dbo].[AHT_FlightInformation] SET Actual = @Actual, Gate = @Gate, Status = @Status, Mcdt = @Mcdt, " +
                           "CheckInCounters = @CheckInCuonters, RowFrom = @rowFrom, RowTo = @rowTo, CounterStart = @CounterStart, CounterEnd = @CounterEnd, GateStart = @GateStart, "+
                           " GateEnd = @GateEnd, Estimated = @Estimated WHERE Id = @Id";
            DataTable dataTable = new DataTable();
            //string connectionString = "Data Source=172.17.2.38;Initial Catalog=MSMQFLIGHT;Persist Security Info=True;User ID=sa;Password=AHT@2019";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Actual", string.IsNullOrEmpty(flightData.Actual) ? DBNull.Value : converttime(flightData.Actual));
                        command.Parameters.AddWithValue("@Estimated", string.IsNullOrEmpty(flightData.Estimated) ? DBNull.Value : converttime(flightData.Estimated));
                        command.Parameters.AddWithValue("@Gate", string.IsNullOrEmpty(flightData.Gate) ? "" : flightData.Gate);
                        command.Parameters.AddWithValue("@Status", flightData.Status);
                        command.Parameters.AddWithValue("@rowFrom", getRowFrom(flightData.CheckInCounters));
                        command.Parameters.AddWithValue("@rowTo", getRowTo(flightData.CheckInCounters));
                        command.Parameters.AddWithValue("@CheckInCuonters", string.IsNullOrEmpty(flightData.CheckInCounters) ? "" : flightData.CheckInCounters);
                        command.Parameters.AddWithValue("@CounterStart", string.IsNullOrEmpty(flightData.CounterStart) ? DBNull.Value : converttime(flightData.CounterStart));
                        command.Parameters.AddWithValue("@CounterEnd", string.IsNullOrEmpty(flightData.CounterEnd) ? DBNull.Value : converttime(flightData.CounterEnd));
                        command.Parameters.AddWithValue("@GateStart", string.IsNullOrEmpty(flightData.GateStart) ? DBNull.Value : converttime(flightData.GateStart));
                        command.Parameters.AddWithValue("@GateEnd", string.IsNullOrEmpty(flightData.GateEnd) ? DBNull.Value : converttime(flightData.GateEnd));
                        command.Parameters.AddWithValue("@Mcdt", string.IsNullOrEmpty(flightData.GateEnd) ? DBNull.Value : converttimeMcdt(flightData.GateEnd));
                        command.Parameters.AddWithValue("@Id", flightData.Id);
                        command.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error2: " + ex.Message);
                    return false;
                }
                finally { connection.Close(); }
            }
        }

        public bool UpdateFlightToGateInformation(AHT_Departures flightData)
        {
            string query = "UPDATE [MSMQFLIGHT].[dbo].[AHT_GateInformation] SET Status =@Status, Remark = @Status, Mcdt = @Mcdt Where Id = @Id";
            DataTable dataTable = new DataTable();
            //string connectionString = "Data Source=172.17.2.38;Initial Catalog=MSMQFLIGHT;Persist Security Info=True;User ID=sa;Password=AHT@2019";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Status", flightData.Status);
                        command.Parameters.AddWithValue("@Mcdt", string.IsNullOrEmpty(flightData.GateEnd) ? DBNull.Value : converttimeMcdt(flightData.GateEnd));
                        command.Parameters.AddWithValue("@Id", flightData.Id);
                        command.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error2: " + ex.Message);
                    return false;
                }
                finally { connection.Close(); }
            }
        }

    }
}
