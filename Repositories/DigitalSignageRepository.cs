using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AHTAPI.Repositories
{
    public class DigitalSignageRepository
    {
        string connectionString;
        public DigitalSignageRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //Http Get All
        #region Get All # Done 
        public List<DigitalSignage> GetGateInfor()
        {
            List<DigitalSignage> digitalSignages = new List<DigitalSignage>();
            DigitalSignage digitalSignage;
            var data = GetWorkOrderDetailsFromDb();
            foreach (DataRow row in data.Rows)
            {
                digitalSignage = new DigitalSignage
                {
                    LineCode = row["LineCode"].ToString(),
                    FlightNo = row["FlightNo"].ToString(),
                    RemarkNo = row["RemarkNo"].ToString(),
                    Mcdt = row["Mcdt"].ToString(),
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
                digitalSignages.Add(digitalSignage);
            }
            return digitalSignages;
        }
        public DataTable GetWorkOrderDetailsFromDb()
        {
            string query = "select TOP (200) A.LineCode, CONCAT (A.LineCode,A.Number) as FlightNo,A.Remark as RemarkNo, A.Mcdt, B.* " +
                "from [MSMQFLIGHT].[dbo].[AHT_GateInformation] AS A JOIN AHT_DigitalSignage AS B ON CONCAT ('AHTBG',A.Gate) = B.Name "+
                "where CONVERT(Datetime ,A.Mcdt) between DATEADD(Mi, -20,getdate()) and DATEADD(Mi, 150,getdate()) "+
                "AND A.Status<>'' and A.Status<> 'Cancelled'  and A.Adi = 'D' AND A.Gate != '' AND A.Remark != 'Gate closed' "+
                "AND A.Remark != 'Departed' Order by CONVERT(Datetime ,A.Mcdt) ASC";
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
        //Http Get By Gate Number
        #region Get All # Done 
        public List<DigitalSignage> GetDigitalByGateNumber( string gate, string leftright)
        {
            List<DigitalSignage> digitalSignages = new List<DigitalSignage>();
            DigitalSignage digitalSignage;
            var data = getDigitalByGateNumber(gate, leftright);
            foreach (DataRow row in data.Rows)
            {
                var Mode = (CheckModeEgate(row["LineCode"].ToString(), row["Gate"].ToString()) == true ? "Yes" : "No");
                digitalSignage = new DigitalSignage
                {
                    LineCode = row["LineCode"].ToString(),
                    FlightNo = row["FlightNo"].ToString(),
                    RemarkNo = row["RemarkNo"].ToString(),
                    Mcdt = row["Mcdt"].ToString(),
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
                    ModeNow = Mode,
                };
                digitalSignages.Add(digitalSignage);
            }
            return digitalSignages;
        }
        public bool CheckModeEgate(string linecode, string gate)
        {
            var data = GetModeEgate(linecode, gate);
            if (data.Rows.Count != 0)
            {
                if (data.Rows[0]["IsEgate"].ToString() == "Yes")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public DataTable GetModeEgate(string line, string gate)
        {
            string query = "SELECT IsEgate FROM [MSMQFLIGHT].[dbo].[AHT_EGateForFlight] WHERE Name = '" + line + "' AND GateNumber = '" + gate + "'";
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
        public DataTable getDigitalByGateNumber(string gate, string leftright)
        {
            string query = "select TOP (1) A.LineCode, CONCAT (A.LineCode,A.Number) as FlightNo,A.Remark as RemarkNo, A.Mcdt,A.Gate, B.* " +
                "from [MSMQFLIGHT].[dbo].[AHT_GateInformation] AS A JOIN AHT_DigitalSignage AS B ON CONCAT ('AHTBG',A.Gate) = B.Name " +
                "where CONVERT(Datetime ,A.Mcdt) between DATEADD(Mi, -20,getdate()) and DATEADD(Mi, 150,getdate()) " +
                "AND A.Status<>'' and A.Status<> 'Cancelled'  and A.Adi = 'D' AND B.Name = '" + gate+ "' AND B.LeftRight = '"+leftright+"' AND A.Remark != 'Gate closed' " +
                "AND A.Remark != 'Departed' Order by CONVERT(Datetime ,A.Mcdt) ASC";
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
