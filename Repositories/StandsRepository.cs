using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;

namespace AHTAPI.Repositories
{
    public class StandsRepository
    {
        string connectionString;
        public StandsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public bool PostStandByName(string standname, string devices)
        {
            Console.WriteLine("'" + standname + "'");
            var alLocation = GetStand(standname);
            if (alLocation == null)
            {
                Console.WriteLine("Stand null");
                return false; // Không tìm thấy stand thì không insert/update
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Kiểm tra StandName đã tồn tại trong AHT_Stands hay chưa
                    string checkQuery = "SELECT COUNT(*) FROM [MSMQFLIGHT].[dbo].[AHT_Stands] WHERE StandName = @StandName";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@StandName", alLocation.ARR_STAND);
                        int count = (int)checkCommand.ExecuteScalar();

                        if (count > 0)
                        {
                            // Nếu đã tồn tại, thực hiện UPDATE
                            string updateQuery = "UPDATE [MSMQFLIGHT].[dbo].[AHT_Stands] SET Flight = @Flight, Schedule =@Schedule, Status = @Status, StandStart = @StandStart, StandEnd =@StandEnd, DevicesBle = @DevicesBle, Note = @No, TimeUpdate = @TimeUpdate WHERE StandName = @StandName";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@TimeUpdate", DateTime.Now);
                                updateCommand.Parameters.AddWithValue("@Flight", alLocation.ARR_FLIGHT+"-"+alLocation.DEP_FLIGHT);
                                updateCommand.Parameters.AddWithValue("@Schedule", alLocation.ETA.HasValue ? (object)alLocation.ETA.Value : DBNull.Value);
                                updateCommand.Parameters.AddWithValue("@Status", alLocation.STATUS);
                                updateCommand.Parameters.AddWithValue("@StandStart", alLocation.STA.HasValue ? (object)alLocation.STA.Value : DBNull.Value);
                                updateCommand.Parameters.AddWithValue("@StandEnd", alLocation.ETD.HasValue ? (object)alLocation.ETD.Value : DBNull.Value);
                                updateCommand.Parameters.AddWithValue("@DevicesBle", devices);
                                updateCommand.Parameters.AddWithValue("@No", alLocation.No);
                                updateCommand.Parameters.AddWithValue("@StandName", alLocation.ARR_STAND);
                                updateCommand.ExecuteNonQuery();
                            }
                            Console.WriteLine("✅ Update thành công cho StandName: " + alLocation.ARR_STAND);
                        }
                        else
                        {
                            string insertQuery = "INSERT INTO [MSMQFLIGHT].[dbo].[AHT_Stands] (StandName,  Flight, Schedule, Status, StandStart, StandEnd, DevicesBle, Note) VALUES (@StandName,  @Flight, @Schedule, @Status, @StandStart, @StandEnd, @DevicesBle, @No)";
                            using (SqlCommand updateCommand = new SqlCommand(insertQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@StandName", alLocation.ARR_STAND);
                                updateCommand.Parameters.AddWithValue("@Flight", alLocation.ARR_FLIGHT + "-" + alLocation.DEP_FLIGHT);
                                updateCommand.Parameters.AddWithValue("@Schedule", alLocation.ETA.HasValue ? (object)alLocation.ETA.Value : DBNull.Value);
                                updateCommand.Parameters.AddWithValue("@Status", alLocation.STATUS);
                                updateCommand.Parameters.AddWithValue("@StandStart", alLocation.STA.HasValue ? (object)alLocation.STA.Value : DBNull.Value);
                                updateCommand.Parameters.AddWithValue("@StandEnd", alLocation.ETD.HasValue ? (object)alLocation.ETD.Value : DBNull.Value);
                                updateCommand.Parameters.AddWithValue("@DevicesBle", devices);
                                updateCommand.Parameters.AddWithValue("@No", alLocation.No);
                                updateCommand.ExecuteNonQuery();
                            }
                            Console.WriteLine("✅ Insert thành công cho StandName: " + alLocation.ARR_STAND);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi PostStandByName: {ex.Message}");
                return false;
            }
        }
        public ALLOCATION? GetStand(string standName)
        {
            Console.Write(standName);
            string query = @" SELECT TOP (1) * FROM [MSMQFLIGHT].[dbo].[RESOURCE_ALLOCATION] WHERE ARR_STAND = @StandName AND STATUS <> 'Departed' AND STATUS <> 'Gate closed'";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StandName", standName);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ALLOCATION
                            {
                                No = reader.GetInt32(reader.GetOrdinal("No")),
                                AC_TYPE = reader.GetString(reader.GetOrdinal("AC_TYPE")),
                                ARR_FLIGHT = reader.GetString(reader.GetOrdinal("ARR_FLIGHT")),
                                ETA = reader.IsDBNull(reader.GetOrdinal("ETA")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ETA")),
                                STA = reader.IsDBNull(reader.GetOrdinal("STA")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("STA")),
                                FLIGHT_FROM = reader.GetString(reader.GetOrdinal("FLIGHT_FROM")),
                                ARR_STAND = reader.GetString(reader.GetOrdinal("ARR_STAND")),
                                DEP_FLIGHT = reader.GetString(reader.GetOrdinal("DEP_FLIGHT")),
                                STD = reader.IsDBNull(reader.GetOrdinal("STD")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("STD")),
                                ETD = reader.IsDBNull(reader.GetOrdinal("ETD")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ETD")),
                                FLIGHT_TO = reader.GetString(reader.GetOrdinal("FLIGHT_TO")),
                                DEP_STAND = reader.GetString(reader.GetOrdinal("DEP_STAND")),
                                STATUS = reader.GetString(reader.GetOrdinal("STATUS")),
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving stand: {ex.Message}");
            }

            return null; // Không tìm thấy
        }
    }
}
