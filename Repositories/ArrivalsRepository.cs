using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace AHTAPI.Repositories
{
    public class ArrivalsRepository
    {
        string connectionString;
        public ArrivalsRepository(string connectionString)
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

        //Http Get DeparturesList
        #region Get ArrivalsList
        public List<AHT_Arrivals> ArrivalsList()
        {
            List<AHT_Arrivals> aHT_Arrivalss = new List<AHT_Arrivals>();
            AHT_Arrivals aHT_Arrivals;
            var data = GetArrivalsList();
            foreach (DataRow row in data.Rows)
            {
                string schedule = ConvertToFormattedDate(row["Schedule"].ToString());
                string estimated = ConvertToFormattedDate(row["Estimated"].ToString());
                string actual = ConvertToFormattedDate(row["Actual"].ToString());
                aHT_Arrivals = new AHT_Arrivals
                {
                    Id = row["Id"].ToString(),
                    ScheduledDate = row["ScheduledDate"].ToString(),
                    Schedule = schedule,
                    Estimated = estimated,
                    Actual = actual,
                    LineCode = row["LineCode"].ToString(),
                    Flight = row["Flight"].ToString(),
                    City = row["City"].ToString(),
                    Belt = row["Belt"].ToString(),
                    Remark = row["Remark"].ToString(),
                };
                aHT_Arrivalss.Add(aHT_Arrivals);
            }
            return aHT_Arrivalss;
        }


        public DataTable GetArrivalsList()
        {
            string query = "Select top 20 id, ScheduledDate, Schedule, Estimated, Actual , LineCode , CONCAT(LineCode, Number) AS Flight, City , Belt, Remark "+
            "from AHT_FlightInformation WHERE Mcat BETWEEN DATEADD(Mi, -65, GETDATE()) AND DATEADD(Mi, 720, GETDATE()) "+
            "AND Status<> '' AND Adi = 'A' ORDER BY Schedule ASC";
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

        //Http Get DeparturesList
        #region Get ArrivalsList
        public async Task<List<AHT_Arrivals>> GetFlightFidsArr(int rollOn, int rollOff)
        {
            var flights = new List<AHT_Arrivals>();

            string query = $@"Select top 20 id, ScheduledDate, Schedule, Estimated, Actual , LineCode , CONCAT(LineCode, Number) AS Flight, City , Belt, Remark 
            from AHT_FlightInformation WHERE Mcat BETWEEN DATEADD(Mi, -@RollOn, GETDATE()) AND DATEADD(Mi, @RollOff, GETDATE()) 
            AND Status<> '' AND Adi = 'A' ORDER BY Schedule ASC";

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
                        flights.Add(new AHT_Arrivals
                        {
                            Id = reader["Id"].ToString(),
                            ScheduledDate = reader["Scheduleddate"]?.ToString(),
                            Schedule = ConvertToFormattedDate(reader["Schedule"]?.ToString()),
                            Estimated = ConvertToFormattedDate(reader["Estimated"]?.ToString()),
                            Actual = ConvertToFormattedDate(reader["Actual"]?.ToString()),
                            LineCode = reader["LineCode"]?.ToString(),
                            Flight = reader["Flight"].ToString(),
                            City = reader["City"].ToString(),
                            Belt = reader["Belt"].ToString(),
                            Remark = reader["Remark"].ToString(),
                        });
                    }
                }
            }
            return flights;
        }


        public DataTable GetFlightList()
        {
            string query = "Select top 20 id, ScheduledDate, Schedule, Estimated, Actual , LineCode , CONCAT(LineCode, Number) AS Flight, City , Belt, Remark " +
            "from AHT_FlightInformation WHERE Mcat BETWEEN DATEADD(Mi, -65, GETDATE()) AND DATEADD(Mi, 720, GETDATE()) " +
            "AND Status<> '' AND Adi = 'A' ORDER BY Schedule ASC";
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
