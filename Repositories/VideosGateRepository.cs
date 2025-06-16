using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AHTAPI.Repositories
{
    public class VideosGateRepository
    {
        string connectionString;
        public VideosGateRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #region Get All DigitalSignages by name = name and location
        public List<GateVideos> GetBeltById(string name, string leftright)
        {
            List<GateVideos> gateVideos = new List<GateVideos>();
            GateVideos gateVideo;
            var data = getBeltById(name, leftright);
            foreach (DataRow row in data.Rows)
            {
                gateVideo = new GateVideos
                {
                    VideoName = row["VideoName"].ToString(),
                };
                gateVideos.Add(gateVideo);
            }
            return gateVideos;
        }
        public DataTable getBeltById(string name, string leftright)
        {
            string query = "SELECT VideoName FROM [MSMQFLIGHT].[dbo].[AHT_GateVideos] where Name LIKE '%" + name + "%' AND LeftRight LIKE '%" + leftright + "%' order by VideoName ASC";
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
        public List<GateVideos> GetBeltByName(string name)
        {
            List<GateVideos> gateVideos = new List<GateVideos>();
            GateVideos gateVideo;
            var data = getBeltByName(name);
            foreach (DataRow row in data.Rows)
            {
                gateVideo = new GateVideos
                {
                    VideoName = row["VideoName"].ToString(),
                };
                gateVideos.Add(gateVideo);
            }
            return gateVideos;
        }

        public DataTable getBeltByName(string name)
        {
            string query = "SELECT VideoName FROM [MSMQFLIGHT].[dbo].[AHT_GateVideos] where Name = '" + name + "'";
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

        public bool AddVideosToGate(GateVideos newWorkOrder)
        {
            string query = " INSERT INTO [MSMQFLIGHT].[dbo].[AHT_GateVideos] (Name, LeftRight, VideoName ) VALUES (@name,@lefrRight,@videoName) ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", newWorkOrder.Name);
                        command.Parameters.AddWithValue("@lefrRight", newWorkOrder.LeftRight);
                        command.Parameters.AddWithValue("@videoName", newWorkOrder.VideoName);

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
        public bool RemoveVideosFromGate(string name, string leftRight)
        {
            string query = " Delete [MSMQFLIGHT].[dbo].[AHT_GateVideos] where Name = @name And LeftRight = @leftRight";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@leftRight", leftRight);
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
