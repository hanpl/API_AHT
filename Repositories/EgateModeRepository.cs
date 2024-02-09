using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace AHTAPI.Repositories
{
    public class EgateModeRepository
    {
        string connectionString;
        public EgateModeRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #region Get By ID
        public List<AHT_EGateForFlight> GetEGateByName(string name)
        {
            List<AHT_EGateForFlight> aHT_EGateForFlights = new List<AHT_EGateForFlight>();
            AHT_EGateForFlight aHT_EGateForFlight;
            var data = getEGateByName(name);
            foreach (DataRow row in data.Rows)
            {
                aHT_EGateForFlight = new AHT_EGateForFlight
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    GateNumber = row["GateNumber"].ToString(),
                    IsEgate = row["IsEgate"].ToString(),
                };
                aHT_EGateForFlights.Add(aHT_EGateForFlight); 
            }
            return aHT_EGateForFlights;
        }
        public DataTable getEGateByName(string name)
        {
            string query = "SELECT * FROM [MSMQFLIGHT].[dbo].[AHT_EGateForFlight] WHERE Name = '"+name+"' ORDER BY Id ASC";
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
        public List<AHT_EGateForFlight> GetEgateById(int id)
        {
            List<AHT_EGateForFlight> aHT_EGateForFlights = new List<AHT_EGateForFlight>();
            AHT_EGateForFlight aHT_EGateForFlight;
            var data = getEgateById(id);
            foreach (DataRow row in data.Rows)
            {
                aHT_EGateForFlight = new AHT_EGateForFlight
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    GateNumber = row["GateNumber"].ToString(),
                    IsEgate = row["IsEgate"].ToString(),
                };
                aHT_EGateForFlights.Add(aHT_EGateForFlight); 
            }
            return aHT_EGateForFlights;
        }
        public DataTable getEgateById(int id)
        {
            string query = "SELECT * FROM [MSMQFLIGHT].[dbo].[AHT_EGateForFlight] WHERE Id = '"+id+"' ORDER BY Id ASC";
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
        #region Get All Name
        public List<AHT_EGATE> GetAllName()
        {
            List<AHT_EGATE> aHT_EGATEs = new List<AHT_EGATE>();
            AHT_EGATE aHT_EGATE;
            var data = getAllName();
            foreach (DataRow row in data.Rows)
            {
                aHT_EGATE = new AHT_EGATE
                {
                    Name = row["Name"].ToString(),
                };
                aHT_EGATEs.Add(aHT_EGATE); 
            }
            return aHT_EGATEs;
        }
        public DataTable getAllName()
        {
            string query = "SELECT Name FROM [MSMQFLIGHT].[dbo].[AHT_EGateForFlight] GROUP BY Name ORDER BY Name ASC";
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
        //HttpPost
        #region HttpPost
        public bool AddEgate(AHT_EGateForFlight newEgate)
        {
            string query = "INSERT INTO [MSMQFLIGHT].[dbo].[AHT_EGateForFlight] (Name, GateNumber, IsEgate) VALUES (@Name,@GateNumber,@IsEgate) ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", newEgate.Name);
                        command.Parameters.AddWithValue("@GateNumber", newEgate.GateNumber);
                        command.Parameters.AddWithValue("@IsEgate", newEgate.IsEgate);
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
        
        public bool PostEGateByName(string name)
        {
            
                string query = "INSERT INTO [MSMQFLIGHT].[dbo].[AHT_EGateForFlight] (Name, GateNumber, IsEgate) VALUES (@Name,@GateNumber,@IsEgate) ";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        for (int i = 1; i < 11; i++)
                        {
                          using (SqlCommand command = new SqlCommand(query, connection))
                          {
                            command.Parameters.AddWithValue("@Name", name);
                            command.Parameters.AddWithValue("@GateNumber", i.ToString());
                            command.Parameters.AddWithValue("@IsEgate", "No");
                            command.ExecuteNonQuery();
                          }
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


        //End posst
        #endregion

        #region Http put
        public bool PutEgateById(int id, string mode)
        {
            string query = "UPDATE [MSMQFLIGHT].[dbo].[AHT_EGateForFlight] SET IsEgate =@IsEgate WHERE Id = @Id";
            DataTable dataTable = new DataTable();
            //string connectionString = "Data Source=172.17.2.38;Initial Catalog=MSMQFLIGHT;Persist Security Info=True;User ID=sa;Password=AHT@2019";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IsEgate", mode);
                        command.Parameters.AddWithValue("@Id", id);
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
        //End put
        #endregion


    }
}
