using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AHTAPI.Repositories
{
    public class EmployeeBLERepository
    {
        string connectionString;
        public EmployeeBLERepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #region Get EmployeeBLE
        public List<AHT_EmployeeBLE> CountriesList()
        {
            List<AHT_EmployeeBLE> aHT_EmployeeBLEs = new List<AHT_EmployeeBLE>();
            AHT_EmployeeBLE aHT_EmployeeBLE;
            var data = GetCountriesList();
            foreach (DataRow row in data.Rows)
            {
                aHT_EmployeeBLE = new AHT_EmployeeBLE
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    DeviceMac = row["DeviceMac"].ToString(),                                        
                    TeamName = row["TeamName"].ToString(),
                    Rank = row["Rank"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Mail = row["Mail"].ToString()
                };
                aHT_EmployeeBLEs.Add(aHT_EmployeeBLE);
            }
            return aHT_EmployeeBLEs;
        }
        public DataTable GetCountriesList()
        {
            string query = "SELECT * FROM [MSMQFLIGHT].[dbo].[AHT_EmployeeBLE]";
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
