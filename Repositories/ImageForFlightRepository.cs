using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AHTAPI.Repositories
{
    public class ImageForFlightRepository
    {
        string connectionString;
        public ImageForFlightRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #region Get By Name
        public AHT_ImageForFlight? GetImgByName(string name)
        {
            AHT_ImageForFlight? aHT_ImageForFlight = null; 
            var data = getImgByName(name);
            foreach (DataRow row in data.Rows)
            {
                // Kiểm tra nếu Location trong dòng hiện tại khớp với locationToSearch
                if (row["Name"]?.ToString() == name)
                {
                    aHT_ImageForFlight = new AHT_ImageForFlight
                    {
                        Name = row["Name"].ToString(),
                        Nomal = row["Nomal"].ToString(),
                        Eco = row["Eco"].ToString(),
                        Bus = row["Bus"].ToString(),
                        Manual = row["Manual"].ToString(),
                    };
                    // Thoát khỏi vòng lặp sau khi tìm thấy bản ghi đầu tiên
                    break;
                }
            }
            return aHT_ImageForFlight;
        }
        public DataTable getImgByName(string name)
        {
            string query = "SELECT * FROM [MSMQFLIGHT].[dbo].[AHT_ImageForFlight] WHERE Name = '" + name + "' ORDER BY Id ASC";
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
        #region Get All Name
        public List<AHT_ImageForFlight> GetAllName()
        {
            List<AHT_ImageForFlight> aHT_ImageForFlights = new List<AHT_ImageForFlight>();
            AHT_ImageForFlight aHT_ImageForFlight;
            var data = getAllName();
            foreach (DataRow row in data.Rows)
            {
                aHT_ImageForFlight = new AHT_ImageForFlight
                {
                    Name = row["Name"].ToString(),
                };
                aHT_ImageForFlights.Add(aHT_ImageForFlight);
            }
            return aHT_ImageForFlights;
        }
        public DataTable getAllName()
        {
            string query = "SELECT Name FROM [MSMQFLIGHT].[dbo].[AHT_ImageForFlight] ORDER BY Name ASC";
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

        public bool PostImgByName(string name)
        {

            string query = "INSERT INTO [MSMQFLIGHT].[dbo].[AHT_ImageForFlight] (Name, Nomal) VALUES (@Name,@Nomal) ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Nomal", name + "_1920x480.png");
                        command.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally { connection.Close(); }

            }
        }


    }
}
