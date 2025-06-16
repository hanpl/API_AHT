using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AHTAPI.Repositories
{
    public class FidsLocationRepository
    {

        string connectionString;
        public FidsLocationRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //Http Get DeparturesList
        #region Get DeparturesList
        public List<AHT_FidsInformation> GetLocationByIp(string ip)
        {
            List<AHT_FidsInformation> aHT_FidsLocations = new List<AHT_FidsInformation>();
            AHT_FidsInformation aHT_FidsLocation;
            var data = GetDeparturesList(ip);
            foreach (DataRow row in data.Rows)
            {
                aHT_FidsLocation = new AHT_FidsInformation
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Location = row["Location"].ToString(),
                    Name = row["Name"].ToString(),
                    Description = row["Description"].ToString(),
                    ConnectionId = row["ConnectionId"].ToString()
                };
                aHT_FidsLocations.Add(aHT_FidsLocation);
            }
            return aHT_FidsLocations;
        }
        public DataTable GetDeparturesList(string ip)
        {
            string query = "SELECT * FROM [MSMQFLIGHT].[dbo].[AHT_FidsInformation] WHERE Ip = '" + ip+"'";
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
