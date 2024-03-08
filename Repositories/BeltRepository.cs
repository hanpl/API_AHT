using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AHTAPI.Repositories
{
    public class BeltRepository
    {
        string connectionString;
        public BeltRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #region Get All DigitalSignages by name = name and location
        public List<AHT_DigitalSignage> GetBeltById(string name, string leftright)
        {
            List<AHT_DigitalSignage> aHT_DigitalSignages = new List<AHT_DigitalSignage>();
            AHT_DigitalSignage aHT_DigitalSignage;
            var data = getBeltById(name, leftright);
            foreach (DataRow row in data.Rows)
            {
                aHT_DigitalSignage = new AHT_DigitalSignage
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    Ip = row["Ip"].ToString(),
                    Location = row["Location"].ToString(),
                    Live = row["Live"].ToString(),
                    Remark = row["Remark"].ToString(),
                    Status = row["Status"].ToString(),
                    LeftRight = row["LeftRight"].ToString(),
                    GateChange = row["GateChange"].ToString(),
                    Mode = row["Mode"].ToString(),
                    Auto = row["Auto"].ToString(),
                    Iata = row["Iata"].ToString(),
                    NameLineCode = row["NameLineCode"].ToString(),
                    TimeMcdt = row["TimeMcdt"].ToString(),
                    ConnectionId = row["ConnectionId"].ToString(),
                    LiveAuto = row["LiveAuto"].ToString(),
                };
                aHT_DigitalSignages.Add(aHT_DigitalSignage);
            }
            return aHT_DigitalSignages;
        }
        public DataTable getBeltById(string name, string leftright)
        {
            string query = "SELECT * FROM [MSMQFLIGHT].[dbo].[AHT_DigitalSignage] where Name LIKE '%" + name + "%' AND LeftRight LIKE '%" + leftright + "%' order by Location ASC";
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
        #region Get All DigitalSignages by name = name
        public List<AHT_DigitalSignage> GetBeltByName(string name)
        {
            List<AHT_DigitalSignage> aHT_DigitalSignages = new List<AHT_DigitalSignage>();
            AHT_DigitalSignage aHT_DigitalSignage;
            var data = getBeltByName(name);
            foreach (DataRow row in data.Rows)
            {
                aHT_DigitalSignage = new AHT_DigitalSignage
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    Ip = row["Ip"].ToString(),
                    Location = row["Location"].ToString(),
                    Live = row["Live"].ToString(),
                    Remark = row["Remark"].ToString(),
                    Status = row["Status"].ToString(),
                    LeftRight = row["LeftRight"].ToString(),
                    GateChange = row["GateChange"].ToString(),
                    Mode = row["Mode"].ToString(),
                    Auto = row["Auto"].ToString(),
                    Iata = row["Iata"].ToString(),
                    NameLineCode = row["NameLineCode"].ToString(),
                    TimeMcdt = row["TimeMcdt"].ToString(),
                    ConnectionId = row["ConnectionId"].ToString(),
                    LiveAuto = row["LiveAuto"].ToString(),
                };
                aHT_DigitalSignages.Add(aHT_DigitalSignage);
            }
            return aHT_DigitalSignages;
        }
        public DataTable getBeltByName(string name )
        {
            string query = "SELECT * FROM [MSMQFLIGHT].[dbo].[AHT_DigitalSignage] where Name LIKE '%" + name + "%' order by Location ASC";
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
    }
}
