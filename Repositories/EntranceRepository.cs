using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Data;
using System.Globalization;
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
                        Flight = item["Flight"].ToString(),
                    };
                    codeShares.Add(_codeShare);
                }
                string chedule = ConvertToFormattedDate(row["Schedule"].ToString());
                aHT_Entrance = new AHT_Entrance()
                {
                    LineCode = row["LineCode"].ToString() == "VZ" ? "VJ" : row["LineCode"].ToString() == "FD" ? "AK" : row["LineCode"].ToString(),
                    Schedule = chedule,
                    Flight = row["Flight"].ToString(),
                    Mcdt = row["Mcdt"].ToString(),
                    RowFrom = row["RowFrom"].ToString(),
                    RowTo = row["RowTo"].ToString(),
                    Code = codeShares
                };
                aHT_Entrances.Add(aHT_Entrance);
            }
            return aHT_Entrances.Distinct().ToList();
        }

        public DataTable GetCodeShareById(string id)
        {
            //string query = "select B.LineCode from AHT_FlightInformation AS A JOIN AHT_CodeShare AS B ON A.Id = B.IdFlightInformation "+ 
            //    "where A.Mcdt between DATEADD(Mi, -20, getdate()) and DATEADD(hh, 6,getdate()) AND A.Status<>'' and A.Status<> 'Cancelled' " + 
            //    "and A.Adi = 'D' and SUBSTRING(A.CheckInCounters, 0, CHARINDEX(',', A.CheckInCounters)) <= 27 and A.Id = '"+id+"' Order by A.Mcdt ASC"
            string query = "select B.LineCode, CONCAT(B.LineCode, B.Number) AS Flight from AHT_FlightInformation AS A JOIN AHT_CodeShare AS B ON A.Id = B.IdFlightInformation " + 
            "where A.Mcdt between DATEADD(Mi, -20, getdate()) and DATEADD(hh, 6,getdate()) AND A.Status<>'' and A.Status<> 'Cancelled' " +
            "and A.Adi = 'D' AND RowFrom NOT LIKE 'M%' AND ISNUMERIC(A.RowFrom) = 1 and A.RowFrom <= 27 and A.Id = '" + id + "' Order by A.Mcdt ASC";
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
            string query = @"SELECT LineCode, CONCAT(LineCode, Number) AS Flight, Mcdt, Schedule, RowFrom, RowTo, Id FROM AHT_FlightInformation 
                             WHERE Mcdt BETWEEN DATEADD(Mi, -20, GETDATE()) AND DATEADD(hh, 6, GETDATE()) AND Status<> '' AND Status<> 'Cancelled' 
                             AND Adi = 'D' AND RowFrom NOT LIKE 'M%' AND ISNUMERIC(RowFrom) = 1 AND CAST(RowFrom AS INT) <= 27 ORDER BY Mcdt ASC";
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
                    Flight = row["Flight"].ToString(),
                };
            codeShares.Add(codeShare);
            }
            return codeShares;
        }
        public DataTable GetHashCodeA()
        {
            string query = "select B.LineCode, CONCAT(B.LineCode, B.Number) AS Flight from AHT_FlightInformation AS A JOIN AHT_CodeShare AS B ON A.Id = B.IdFlightInformation " +
                " where A.Mcdt between DATEADD(Mi, -20,getdate()) and DATEADD(hh, 6,getdate()) AND A.Status<>'' and A.Status<> 'Cancelled' " +
                " and A.Adi = 'D' AND RowFrom NOT LIKE 'M%' AND ISNUMERIC(RowFrom) = 1 and A.RowFrom >= 27 Order by A.Mcdt ASC";
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
                        Flight = item["Flight"].ToString(),
                    };
                    codeShares.Add(_codeShare);
                }
                string chedule = ConvertToFormattedDate(row["Schedule"].ToString());
                aHT_Entrance = new AHT_Entrance()
                {
                    LineCode = row["LineCode"].ToString() == "VZ" ? "VJ" : row["LineCode"].ToString() == "FD" ? "AK" : row["LineCode"].ToString(),
                    Schedule = chedule,
                    Flight = row["Flight"].ToString(),
                    Mcdt = row["Mcdt"].ToString(),
                    RowFrom = row["RowFrom"].ToString(),
                    RowTo = row["RowTo"].ToString(),
                    Code = codeShares
                };
                aHT_Entrances.Add(aHT_Entrance);
            }
            return aHT_Entrances.Distinct().ToList();
        }

        public DataTable GetCodeShareByIdB(string id)
        {
            string query = "select B.LineCode, CONCAT(B.LineCode, B.Number) AS Flight from AHT_FlightInformation AS A JOIN AHT_CodeShare AS B ON A.Id = B.IdFlightInformation " +
                "where A.Mcdt between DATEADD(Mi, -20, getdate()) and DATEADD(hh, 6,getdate()) AND A.Status<>'' and A.Status<> 'Cancelled' " +
                "and A.Adi = 'D' and A.RowFrom NOT LIKE 'M%' AND ISNUMERIC(A.RowFrom) = 1 and A.RowFrom >= 27 and A.Id = '" + id + "' Order by A.Mcdt ASC";
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
            string query = "SELECT LineCode,CONCAT(LineCode, Number) AS Flight, Mcdt, Schedule, RowFrom, RowTo, Id FROM AHT_FlightInformation WHERE Mcdt BETWEEN DATEADD(Mi, -20, GETDATE()) AND DATEADD(hh, 6, GETDATE()) " +
              " AND Status<> '' " +
              " AND Status<> 'Cancelled' " +
              " AND Adi = 'D' " +
              " AND RowFrom NOT LIKE 'M%' " +
              " AND ISNUMERIC(RowFrom) = 1 " +
              " AND CAST(RowFrom AS INT) > 27 " +
              " ORDER BY Mcdt ASC";
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

        private string ConvertToFormattedDate(string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime date))
            {
                return date.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            }
            return "";
        }
    }
}
