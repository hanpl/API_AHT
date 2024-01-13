using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Data;
using System.Linq;

namespace AHTAPI.Repositories
{
    public class EntranceRepository
    {
        string connectionString;
        public EntranceRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        #region Get Entrance A
        public List<AHT_Entrance> GetEntrance()
        {
            List<AHT_Entrance> aHT_Entrances = new List<AHT_Entrance>();
            AHT_Entrance aHT_Entrance;
            var data = GetWorkOrderDetailsFromDb();
            foreach (DataRow row in data.Rows)
            {
                List<CodeShare> codeShares = new List<CodeShare>();
                var code = GetCodeShareById(row["Id"].ToString());
                CodeShare _codeShare;
                foreach (DataRow item in code.Rows)
                {
                    _codeShare = new CodeShare()
                    {
                        LineCode = item["LineCode"].ToString(),
                    };
                    codeShares.Add(_codeShare);
                }
                    aHT_Entrance = new AHT_Entrance()
                    {
                        LineCode = row["LineCode"].ToString()=="VZ" ? "VJ": row["LineCode"].ToString() =="FD"? "AK": row["LineCode"].ToString(),
                        Code = codeShares
                    };
                aHT_Entrances.Add(aHT_Entrance);
            }
            return aHT_Entrances.Distinct().ToList();
        }

        public DataTable GetCodeShareById(string id)
        {
            string query = "select B.LineCode from AHT_FlightInformation AS A JOIN AHT_CodeShare AS B ON A.Id = B.IdFlightInformation "+ 
                "where A.Mcdt between DATEADD(Mi, -20, getdate()) and DATEADD(hh, 6,getdate()) AND A.Status<>'' and A.Status<> 'Cancelled' " + 
                "and A.Adi = 'D' and SUBSTRING(A.CheckInCounters, 0, CHARINDEX(',', A.CheckInCounters)) <= 27 and A.Id = '"+id+"' Order by A.Mcdt ASC";
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
        public DataTable GetWorkOrderDetailsFromDb()
        {
            string query = "select A.LineCode, A.Id from AHT_FlightInformation AS A " +
                "where A.Mcdt between DATEADD(Mi, -20,getdate()) and DATEADD(hh, 6,getdate()) AND A.Status<>'' and A.Status<> 'Cancelled' "+
                "and A.Adi = 'D' and SUBSTRING(A.CheckInCounters, 0, CHARINDEX(',', A.CheckInCounters)) <= 27 Order by A.Mcdt ASC";
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

        #region Get CodeShare A
        public List<CodeShare> GetCodeShareA()
        {
            List<CodeShare> codeShares = new List<CodeShare>();
            CodeShare codeShare;
            var data = GetHashCodeA();
            foreach (DataRow row in data.Rows)
            {
                codeShare = new CodeShare
                {
                    LineCode = row["LineCode"].ToString(),
                };
            codeShares.Add(codeShare);
            }
            return codeShares;
        }
        public DataTable GetHashCodeA()
        {
            string query = "select B.LineCode from AHT_FlightInformation AS A JOIN AHT_CodeShare AS B ON A.Id = B.IdFlightInformation " +
                " where A.Mcdt between DATEADD(Mi, -20,getdate()) and DATEADD(hh, 6,getdate()) AND A.Status<>'' and A.Status<> 'Cancelled' " +
                " and A.Adi = 'D' and SUBSTRING(A.CheckInCounters, 0, CHARINDEX(',', A.CheckInCounters)) >= 27 Order by A.Mcdt ASC";
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

        #region Get Entrance B
        public List<AHT_Entrance> GetEntranceB()
        {
            List<AHT_Entrance> aHT_Entrances = new List<AHT_Entrance>();
            AHT_Entrance aHT_Entrance;
            var data = GetWorkOrderDetailsFromDbB();
            foreach (DataRow row in data.Rows)
            {
                List<CodeShare> codeShares = new List<CodeShare>();
                var code = GetCodeShareByIdB(row["Id"].ToString());
                CodeShare _codeShare;
                foreach (DataRow item in code.Rows)
                {
                    _codeShare = new CodeShare()
                    {
                        LineCode = item["LineCode"].ToString(),
                    };
                    codeShares.Add(_codeShare);
                }
                aHT_Entrance = new AHT_Entrance()
                {
                    LineCode = row["LineCode"].ToString() == "VZ" ? "VJ" : row["LineCode"].ToString() == "FD" ? "AK" : row["LineCode"].ToString(),
                    Code = codeShares
                };
                aHT_Entrances.Add(aHT_Entrance);
            }
            return aHT_Entrances.Distinct().ToList();
        }

        public DataTable GetCodeShareByIdB(string id)
        {
            string query = "select B.LineCode from AHT_FlightInformation AS A JOIN AHT_CodeShare AS B ON A.Id = B.IdFlightInformation " +
                "where A.Mcdt between DATEADD(Mi, -20, getdate()) and DATEADD(hh, 6,getdate()) AND A.Status<>'' and A.Status<> 'Cancelled' " +
                "and A.Adi = 'D' and SUBSTRING(A.CheckInCounters, 0, CHARINDEX(',', A.CheckInCounters)) >= 27 and A.Id = '" + id + "' Order by A.Mcdt ASC";
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
        public DataTable GetWorkOrderDetailsFromDbB()
        {
            string query = "select A.LineCode, A.Id from AHT_FlightInformation AS A " +
                "where A.Mcdt between DATEADD(Mi, -20,getdate()) and DATEADD(hh, 6,getdate()) AND A.Status<>'' and A.Status<> 'Cancelled' " +
                "and A.Adi = 'D' and SUBSTRING(A.CheckInCounters, 0, CHARINDEX(',', A.CheckInCounters)) >= 27 Order by A.Mcdt ASC";
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
