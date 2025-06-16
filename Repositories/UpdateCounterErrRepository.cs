using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AHTAPI.Repositories
{
    public class UpdateCounterErrRepository
    {
        string connectionString;
        public UpdateCounterErrRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<bool> UpdateDataToDB(UpdateCounterErr data)
        {
            bool result = UpdateFlight(data);
            if (data == null) return false;
            return result;
        }

        public bool UpdateFlight(UpdateCounterErr data)
        {
            //Console.WriteLine(rabbitMQ.FieldValue);
            string query = "UPDATE [MSMQFLIGHT].[dbo].[AHT_FlightInformation] SET Gate = @Gate, CheckinCounters = @CheckinCounters," +
                " RowFrom = @RowFrom , RowTo = @RowTo  " +
                "WHERE Id = @Id";
            DataTable dataTable = new DataTable();
            //string connectionString = "Data Source=172.17.2.38;Initial Catalog=MSMQFLIGHT;Persist Security Info=True;User ID=sa;Password=AHT@2019";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Gate", data.Gate);
                        command.Parameters.AddWithValue("@CheckinCounters", data.CheckinCounters);
                        command.Parameters.AddWithValue("@RowFrom", data.RowFrom);
                        command.Parameters.AddWithValue("@RowTo", data.RowTo);
                        command.Parameters.AddWithValue("@Id", data.Id);
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
