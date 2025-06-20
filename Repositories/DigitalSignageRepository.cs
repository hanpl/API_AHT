﻿using AHTAPI.Models;
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
                    Work = row["Work"].ToString(),
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
        
        //Http Get All
        #region Get All DigitalSignages
        public List<AHT_DigitalSignage> GetAllDigitalSignalges()
        {
            List<AHT_DigitalSignage> aHT_DigitalSignages = new List<AHT_DigitalSignage>();
            AHT_DigitalSignage aHT_DigitalSignage;
            var data = getAllDigitalSignalges();
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
                    Work = row["Work"].ToString(),
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
        public DataTable getAllDigitalSignalges()
        {
            string query = "SELECT * FROM [MSMQFLIGHT].[dbo].[AHT_DigitalSignage] where Name LIKE '%AHTBG%' order by Location ASC";
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
        //Http Get All by name 
        #region Get All DigitalSignages by name = name
        public List<AHT_DigitalSignage> GetDigitalByName(string name)
        {
            List<AHT_DigitalSignage> aHT_DigitalSignages = new List<AHT_DigitalSignage>();
            AHT_DigitalSignage aHT_DigitalSignage;
            var data = getDigitalByName(name);
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
                    Work = row["Work"].ToString(),
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
        public DataTable getDigitalByName(string name)
        {
            string query = "SELECT * FROM [MSMQFLIGHT].[dbo].[AHT_DigitalSignage] where Name LIKE '%" +name+ "%' order by Location ASC";
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
        //Http Get By Gate Number
        #region Get By Name
        public List<DigitalSignage> GetDigitalByGateNumber( string gate, string leftright)
        {
            List<DigitalSignage> digitalSignages = new List<DigitalSignage>();
            DigitalSignage digitalSignage;
            var data = getDigitalByGateNumber(gate, leftright);
            Console.WriteLine(data.Rows.Count.ToString());
            if (data.Rows.Count == 0)
            {
                if (UpdateGateToAHT(gate, leftright, "AHT", "", "No", "", "", ""))
                {
                    var data1 = getOnlyDigitalByGateNumber(gate, leftright);
                    foreach (DataRow row in data1.Rows)
                    {
                        List<GateVideos> gateVideos = new List<GateVideos>();
                        var code = GetGateVideosByName(gate, leftright);
                        GateVideos _gateVideos;
                        foreach (DataRow item in code.Rows)
                        {
                            _gateVideos = new GateVideos()
                            {
                                VideoName = item["VideoName"].ToString(),
                            };
                            gateVideos.Add(_gateVideos);
                        }
                        digitalSignage = new DigitalSignage
                        {
                            LineCode = "",
                            FlightNo = "",
                            RemarkNo = "",
                            Mcdt = "",
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
                            Work = row["Work"].ToString(),
                            City = row["City"].ToString(),
                            MixCity = row["MixCity"].ToString(),
                            MixVideos = row["MixVideos"].ToString(),
                            ModeNow = "",
                            ListVideoGate = gateVideos,
                        };
                        digitalSignages.Add(digitalSignage);
                    }
                        return digitalSignages;

                }
                return digitalSignages;

            }
            else
            {
                foreach (DataRow row in data.Rows)
                {
                    var Mode = (CheckModeEgate(row["LineCode"].ToString(), row["Gate"].ToString()) == true ? "Yes" : "No");
                    var Live = row["LineCode"].ToString() + "_" + (Mode == "Yes" ? "EGATE" : "NOEGATE") + "_" + (((row["RemarkNo"].ToString() == "On time") || (row["RemarkNo"].ToString() == "Delayed")) ? "PRE" : "BOARD") + "_" + row["LeftRight"].ToString();
                    Console.WriteLine("Gate Doing..." + row["Name"].ToString() + " - " + row["LineCode"].ToString() + " - " + row["RemarkNo"].ToString() + " - " + Mode + "  " + Live);
                    if ((row["Live"].ToString() == Live) && (row["Mode"].ToString() == Mode) && (row["Mcdt"].ToString() == row["TimeMcdt"].ToString()))
                    {
                        Console.WriteLine("Gate not yet change...");
                        //Bổ sung code gửi signalr đến  client tương ứng
                    }
                    else
                    {
                        if (UpdateGateToDoing(Convert.ToInt32(row["Id"]), Live, row["RemarkNo"].ToString(), Mode, row["LineCode"].ToString(), row["FlightNo"].ToString(), row["Mcdt"].ToString()))
                        {
                            Console.WriteLine("UpdateGateToDoing");
                            //Bổ sung code gửi signalr đến  client tương ứng
                        }
                    }
                    List<GateVideos> gateVideos = new List<GateVideos>();
                    if ((row["Work"].ToString() == "2") || (row["Work"].ToString() == "3"))
                    {
                        var code = GetGateVideosByName(row["Name"].ToString(), row["LeftRight"].ToString());
                        GateVideos _gateVideos;
                        foreach (DataRow item in code.Rows)
                        {
                            _gateVideos = new GateVideos()
                            {
                                VideoName = item["VideoName"].ToString(),
                            };
                            gateVideos.Add(_gateVideos);
                        }
                    }
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
                        Work = row["Work"].ToString(),
                        City = row["CityTo"].ToString(),
                        MixCity = row["MixCity"].ToString(),
                        MixVideos = row["MixVideos"].ToString(),
                        ModeNow = Mode,
                        ListVideoGate = gateVideos,
                    };
                    digitalSignages.Add(digitalSignage);
                }
                return digitalSignages;
            }
            
        }

        public DataTable GetGateVideosByName(string name, string location)
        {
            string query = "SELECT VideoName FROM [MSMQFLIGHT].[dbo].[AHT_GateVideos] where Name = '"+name+"' and LeftRight = '"+location+"'"; 
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
            string query = "select TOP (1) A.LineCode, CONCAT (A.LineCode,A.Number) as FlightNo,A.Remark as RemarkNo, A.Mcdt, A.Gate, B.*, C.City as CityTo " +
                "from [MSMQFLIGHT].[dbo].[AHT_GateInformation] AS A JOIN [MSMQFLIGHT].[dbo].[AHT_DigitalSignage] AS B ON CONCAT ('AHTBG',A.Gate) = B.Name " +
                "JOIN [MSMQFLIGHT].[dbo].[AHT_FlightInformation] AS C ON CONCAT ('AHTBG',C.Gate) = B.Name where CONVERT(Datetime ,A.Mcdt) " +
                "between DATEADD(Mi, -20,getdate()) and DATEADD(Mi, 150,getdate()) AND A.Status<>'' and A.Status<> 'Cancelled'  and A.Adi = 'D' " +
                "AND B.Name = '"+gate+"' AND B.LeftRight = '"+leftright+"' AND A.Remark != 'Gate closed' AND A.Remark != 'Departed' Order by CONVERT(Datetime ,A.Mcdt) ASC";
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
        public DataTable getOnlyDigitalByGateNumber(string gate, string leftright)
        {
            string query = "select * from [MSMQFLIGHT].[dbo].[AHT_DigitalSignage] where Name = '"+gate+"' AND LeftRight = '"+leftright+"'";
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

        #region Update Data Gate
        public bool UpdateGateToDoing(int id, string live, string remark, string mode, string iata, string namelinecode, string timemcdt)
        {
            string query = "UPDATE [MSMQFLIGHT].[dbo].[AHT_DigitalSignage] SET Live =@Live, Remark =@Remark, " +
                "Mode = @Mode, Iata =@Iata, NameLineCode = @NameLineCode, TimeMcdt =@TimeMcdt WHERE Id = @Id";
            DataTable dataTable = new DataTable();
            //string connectionString = "Data Source=172.17.2.38;Initial Catalog=MSMQFLIGHT;Persist Security Info=True;User ID=sa;Password=AHT@2019";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Live", live);
                        command.Parameters.AddWithValue("@Remark", remark);
                        command.Parameters.AddWithValue("@Mode", mode);
                        command.Parameters.AddWithValue("@Iata", iata);
                        command.Parameters.AddWithValue("@NameLineCode", namelinecode);
                        command.Parameters.AddWithValue("@TimeMcdt", timemcdt);
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
        }public bool UpdateGateToAHT(string gate, string leftright, string live, string remark, string mode, string iata, string namelinecode, string timemcdt)
        {
            string query = "UPDATE [MSMQFLIGHT].[dbo].[AHT_DigitalSignage] SET Live =@Live, Remark =@Remark, " +
                "Mode = @Mode, Iata =@Iata, NameLineCode = @NameLineCode, TimeMcdt =@TimeMcdt WHERE Name = @Id AND LeftRight = @LeftRight";
            DataTable dataTable = new DataTable();
            //string connectionString = "Data Source=172.17.2.38;Initial Catalog=MSMQFLIGHT;Persist Security Info=True;User ID=sa;Password=AHT@2019";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Live", live);
                        command.Parameters.AddWithValue("@Remark", remark);
                        command.Parameters.AddWithValue("@Mode", mode);
                        command.Parameters.AddWithValue("@Iata", iata);
                        command.Parameters.AddWithValue("@NameLineCode", namelinecode);
                        command.Parameters.AddWithValue("@TimeMcdt", timemcdt);
                        command.Parameters.AddWithValue("@Id", gate);
                        command.Parameters.AddWithValue("@LeftRight", leftright);
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
        #endregion#region Update Data Gate

        #region Update Digital By column Name
        public bool UpdateDigitalByColumnName(string Name, string leftRight, string columnName, string des)
        {
            string query = "UPDATE [MSMQFLIGHT].[dbo].[AHT_DigitalSignage] SET "+ columnName + " = '"+des+"', Work = 'Yes' WHERE Name = @Name AND LeftRight = @LeftRight ";
            DataTable dataTable = new DataTable();
            //string connectionString = "Data Source=172.17.2.38;Initial Catalog=MSMQFLIGHT;Persist Security Info=True;User ID=sa;Password=AHT@2019";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@LeftRight", leftRight);
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
        #endregion


        #region Get Flight for DigSig2
        public FlightForComple GetFlightByGateNumber(string name)
        {
            FlightForComple? flightForComple = null;
            var data = getConnectionIdClinets(name);
            foreach (DataRow row in data.Rows)
            {
                // Kiểm tra nếu Location trong dòng hiện tại khớp với locationToSearch
                if ("AHTBG"+row["Gate"]?.ToString() == name)
                {
                    flightForComple = new FlightForComple
                    {
                        iata = row["LineCode"]?.ToString(),
                        name = "AHTBG" + row["Gate"]?.ToString(),
                        nameLineCode = row["LineCode"]?.ToString() + row["Number"]?.ToString(),
                        remark = row["Remark"]?.ToString(),
                        timeMcdt = row["Mcdt"]?.ToString()
                    };
                    break;
                }
            }
            return flightForComple;
        }

        public DataTable getConnectionIdClinets(string name)
        {
            string query = @"
             SELECT TOP (1) g.*, e.IsEgate
             FROM [MSMQFLIGHT].[dbo].[AHT_GateInformation] g
             LEFT JOIN [MSMQFLIGHT].[dbo].[AHT_EGateForFlight] e 
             ON g.LineCode = e.Name
             WHERE ISNULL(Remark, '') NOT IN ('Departed', 'Cancelled', 'Gate closed', '') 
             AND Gate = REPLACE(@gate, 'AHTBG', '')
             AND DATEADD(MINUTE, -20, GETDATE()) < TRY_CAST(Mcdt AS DATETIME)
             ORDER BY TRY_CAST(Mcdt AS datetime) ASC";
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@gate", name);
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                }
            }

            return dataTable;
        }
        #endregion
    }
}
