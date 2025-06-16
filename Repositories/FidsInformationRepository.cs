using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AHTAPI.Repositories
{
    public class FidsInformationRepository
    {
        string connectionString;
        public FidsInformationRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #region Get Device By Ip
        public AHT_FidsInformation? GetDeviceByIp(string ip)
        {
            var data = GetDevice(ip);

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
                MaxPages = Convert.ToInt32(row["MaxPages"]),
                PageInterval = Convert.ToInt32(row["PageInterval"]),
                ReloadInterval = Convert.ToInt32(row["ReloadInterval"]),
                Mobilities = row["Mobilities"].ToString(),
                ConnectionId = row["ConnectionId"].ToString()
            };
        }
        public DataTable GetDevice(string ip)
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
