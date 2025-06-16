using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AHTAPI.Repositories
{
    public class RateUserRepository
    {
        string connectionString;
        public RateUserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        //Http Get All
        #region Get All # Done
        public List<AlertStateZalo> GetAlert()
        {
            List<AlertStateZalo> alertStateZalos = new List<AlertStateZalo>();
            AlertStateZalo alertStateZalo;
            var data = GetRabbitMQList();
            foreach (DataRow row in data.Rows)
            {
                alertStateZalo = new AlertStateZalo
                {
                    MessageId = Convert.ToInt32(row["MessageId"]),
                    MessageText = row["MessageText"].ToString(),
                    SentTime = Convert.ToDateTime(row["SentTime"]),
                    Confirmed = Convert.ToBoolean(row["Confirmed"]),
                    RetryCount = Convert.ToInt32(row["RetryCount"]),
                };
                alertStateZalos.Add(alertStateZalo);
            }
            return alertStateZalos;
        }
        public DataTable GetRabbitMQList()
        {
            string query = "SELECT TOP (10) * FROM [NOTIFYDB].[dbo].[AlertStateZalo]";
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
