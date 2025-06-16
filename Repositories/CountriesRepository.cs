using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AHTAPI.Repositories
{
    public class CountriesRepository
    {
        string connectionString;
        public CountriesRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #region Get CountriesList
        public List<AHT_Countries> CountriesList()
        {
            List<AHT_Countries> aHT_Countriess = new List<AHT_Countries>();
            AHT_Countries aHT_Countries;
            var data = GetCountriesList();
            foreach (DataRow row in data.Rows)
            {
                aHT_Countries = new AHT_Countries
                {
                    CodeAirport = row["CodeAirport"].ToString(),
                    NameAirport = row["NameAirport"].ToString(),
                    Countries = row["Countries"].ToString(),
                };
                aHT_Countriess.Add(aHT_Countries);
            }
            return aHT_Countriess;
        }
        public DataTable GetCountriesList()
        {
            string query = "SELECT [CodeAirport], UPPER([NameAirport]) AS NameAirport, [Countries]FROM [MSMQFLIGHT].[dbo].[AHT_Countries]";
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
