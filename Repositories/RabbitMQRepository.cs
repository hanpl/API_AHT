using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Threading;

namespace AHTAPI.Repositories
{
    public class RabbitMQRepository
    {
        string connectionString;
        public RabbitMQRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //Http Get All
        #region Get All # Done
        public List<AHT_T1_FlightInformation> GetAllRabbitMQ()
        {
            List<AHT_T1_FlightInformation> aHT_T1_FlightInformations = new List<AHT_T1_FlightInformation>();
            AHT_T1_FlightInformation aHT_T1_FlightInformation;
            var data = GetRabbitMQList();
            foreach (DataRow row in data.Rows)
            {
                aHT_T1_FlightInformation = new AHT_T1_FlightInformation
                {
                    Id = row["Id"].ToString(),
                    Adi = row["Adi"].ToString(),
                    LineCode = row["LineCode"].ToString(),
                    FlightNo = row["FlightNo"].ToString(),
                    Status = row["Status"].ToString(),
                    ScheduledDate = row["ScheduledDate"].ToString(),
                    Route = row["Route"].ToString(),
                    Terminal = row["Terminal"].ToString(),
                    City = row["City"].ToString(),
                    SIBT = row["SIBT"].ToString(),
                    SOBT = row["SOBT"].ToString(),
                    EIBT = row["EIBT"].ToString(),
                };
                aHT_T1_FlightInformations.Add(aHT_T1_FlightInformation);
            }
            return aHT_T1_FlightInformations;
        }
        public DataTable GetRabbitMQList()
        {
            string query = "SELECT * FROM [RABBITMQFLIGHT].[dbo].[AHT_T1_FlightInformation]";
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

        #region Get Flight By Id
        public AHT_T1_FlightInformation GetFlightInformation(string Id)
        {
            AHT_T1_FlightInformation aHT_T1_FlightInformation = new AHT_T1_FlightInformation();
            var data = GetFlight(Id);
            foreach (DataRow row in data.Rows)
            {
                aHT_T1_FlightInformation = new AHT_T1_FlightInformation
                {
                    Id = row["Id"].ToString(),
                    Adi = row["Adi"].ToString(),
                    LineCode = row["LineCode"].ToString(),
                    FlightNo = row["FlightNo"].ToString(),
                    Status = row["Status"].ToString(),
                    ScheduledDate = row["ScheduledDate"].ToString(),
                    Route = row["Route"].ToString(),
                    Terminal = row["Terminal"].ToString(),
                    City = row["City"].ToString(),
                    SIBT = row["SIBT"].ToString(),
                    SOBT = row["SOBT"].ToString(),
                    EIBT = row["EIBT"].ToString(),
                };
            }
            return aHT_T1_FlightInformation;
        }
        public DataTable GetFlight(string Id)
        {
            string query = "SELECT * FROM [RABBITMQFLIGHT].[dbo].[AHT_T1_FlightInformation] Where Route = '" + Id+"'";
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
        public bool AddRabbitMQ(List<RabbitMQ> newRabbitMQList)
        {
            bool result = true; // Biến để theo dõi trạng thái tổng quát

            foreach (var newRabbitMQ in newRabbitMQList)
            {
                string id = "FlightNo: " + newRabbitMQ.FlightNo + " FlightDate: " + newRabbitMQ.FlightDate + " Route: " + newRabbitMQ.Route;
                //Console.WriteLine(id);
                //Console.WriteLine(newRabbitMQ.FlightData.Count);

                if (!ExistingFlight(id))
                {
                    // Tạo FlightInformation và chèn nếu không tồn tại flight
                    FlightInformation flightInformation = new FlightInformation
                    {
                        FlightNo = newRabbitMQ.FlightNo,
                        FlightDate = newRabbitMQ.FlightDate,
                        Route = newRabbitMQ.Route,
                        FieldName = newRabbitMQ.FlightData[0].FieldName,
                        FieldValue = newRabbitMQ.FlightData[0].FieldValue,
                        ValueOLD = newRabbitMQ.FlightData[0].ValueOLD,
                        InputSource = newRabbitMQ.FlightData[0].InputSource,
                        InputTime = newRabbitMQ.FlightData[0].InputTime
                    };

                    if (InsertFlight(flightInformation))
                    {
                        // Chạy vòng lặp cập nhật cho các phần tử FlightData
                        for (int i = 0; i < newRabbitMQ.FlightData.Count; i++)
                        {
                            FlightInformation flightInformation2 = new FlightInformation
                            {
                                FlightNo = newRabbitMQ.FlightNo,
                                FlightDate = newRabbitMQ.FlightDate,
                                Route = newRabbitMQ.Route,
                                FieldName = newRabbitMQ.FlightData[i].FieldName,
                                FieldValue = newRabbitMQ.FlightData[i].FieldValue,
                                ValueOLD = newRabbitMQ.FlightData[i].ValueOLD,
                                InputSource = newRabbitMQ.FlightData[i].InputSource,
                                InputTime = newRabbitMQ.FlightData[i].InputTime
                            };
                            if (!UpdateFlight(flightInformation2, id))
                            {
                                result = false; // Nếu thất bại, ghi nhận và tiếp tục
                                Console.WriteLine("Update failed for FlightNo: " + newRabbitMQ.FlightNo);
                            }
                        }
                    }
                    else
                    {
                        result = false; // Nếu chèn thất bại
                        Console.WriteLine("Insert failed for FlightNo: " + newRabbitMQ.FlightNo);
                    }
                }
                else
                {
                    // Nếu flight đã tồn tại, chỉ cần cập nhật
                    for (int i = 0; i < newRabbitMQ.FlightData.Count; i++)
                    {
                        FlightInformation flightInformation = new FlightInformation
                        {
                            FlightNo = newRabbitMQ.FlightNo,
                            FlightDate = newRabbitMQ.FlightDate,
                            Route = newRabbitMQ.Route,
                            FieldName = newRabbitMQ.FlightData[i].FieldName,
                            FieldValue = newRabbitMQ.FlightData[i].FieldValue,
                            ValueOLD = newRabbitMQ.FlightData[i].ValueOLD,
                            InputSource = newRabbitMQ.FlightData[i].InputSource,
                            InputTime = newRabbitMQ.FlightData[i].InputTime
                        };
                        if (!UpdateFlight(flightInformation, id))
                        {
                            result = false; // Ghi nhận lỗi cập nhật
                            Console.WriteLine("Update failed for FlightNo: " + newRabbitMQ.FlightNo);
                        }
                    }
                }
            }

            return result; // Trả về trạng thái tổng quát sau khi đã xử lý tất cả
        }

        public bool ExistingFlight(string id)
        {
            string query = "SELECT COUNT(1) FROM [RABBITMQFLIGHT].[dbo].[AHT_T1_FlightInformation] WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0; 
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ nếu có
                    Console.WriteLine("Error1: " + ex.Message);
                    return false;
                }
            }
        }
        //End posst
        #endregion



        //Http Put
        public bool UpdateFlight(FlightInformation rabbitMQ ,string id)
        {
            //Console.WriteLine(rabbitMQ.FieldValue);
            string query = "UPDATE [RABBITMQFLIGHT].[dbo].[AHT_T1_FlightInformation] SET [" + rabbitMQ.FieldName + "] = @value, ValueOLD = @ValueOLD," +
                " TimeUpdate = @TimeUpdate , InputTime = @InputTime,  " +
                "Mcdt = CASE  " +
                   "WHEN '" + rabbitMQ.FieldName + "' IN('SOBT', 'EOBT', 'AOBT') THEN @value  " +
                   "ELSE MCDT  " +
                "END,  " +
                "Mcat = CASE  " +
                   "WHEN '" + rabbitMQ.FieldName + "' IN('SIBT', 'EIBT', 'AIBT') THEN @value  " +
                   "ELSE MCAT  " +
                "END  " +
                "WHERE Id = @Id";
            DataTable dataTable = new DataTable();
            //string connectionString = "Data Source=172.17.2.38;Initial Catalog=MSMQFLIGHT;Persist Security Info=True;User ID=sa;Password=AHT@2019";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@value", rabbitMQ.FieldValue);
                        command.Parameters.AddWithValue("@ValueOLD", rabbitMQ.ValueOLD);
                        command.Parameters.AddWithValue("@TimeUpdate", DateTime.Now);
                        command.Parameters.AddWithValue("@InputTime", rabbitMQ.InputTime);
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error2: " + ex.Message);
                    return false;
                }
                finally { connection.Close(); }
            }

        }
        //End put

        public string getCityfullName(string name)
        {
            // Kiểm tra nếu tham số 'name' là null hoặc rỗng
            if (IsValidFormat(name))
            {
                string iata = name.Replace("DAD", "");
                iata = iata.Trim('-');

                string query = "SELECT [NameAirport] FROM [MSMQFLIGHT].[dbo].[AHT_Countries] WHERE CodeAirport = @router";
                string connectstring = "Data Source=172.17.2.38;Initial Catalog=MSMQFLIGHT;Persist Security Info=True;User ID=soft;Password=AHT@2019";
                using (SqlConnection connection = new SqlConnection(connectstring))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@router", iata);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            return result.ToString();
                        }
                        else
                        {
                            return "";
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error1: " + ex.Message);
                        return "";
                    }
                }
            }

            // Trả về thông báo chào hỏi với tên đã cho
            return "";
        }
        private static bool IsValidFormat(string input)
        {
            // Định nghĩa pattern cho định dạng "DAD-XXX" hoặc "XXX-DAD"
            string pattern = @"^(DAD-[A-Z]{3}|[A-Z]{3}-DAD)$";

            // Kiểm tra đầu vào với pattern
            return Regex.IsMatch(input, pattern);
        }

        public bool InsertFlight(FlightInformation newRabbitMQ )
        {
            //string query = "INSERT INTO [RABBITMQFLIGHT].[dbo].[AHT_T1_FlightInformation] (Id, Adi, LineCode , Number , FlightNo , ScheduledDate , Route , " +
            //               "Terminal ,City ,CityName ,Aircraft, PaxCount ,SIBT ,EIBT, AIBT, Belt, SOBT, EOBT, AOBT, DGATE, Status, Remark, " +
            //               "CkiRow, CkiOPN, GateStart, GateEnd, CounterStart, CounterEnd, ClaimStart, ClaimEnd, CheckInCounters , " +
            //               "TimeSet, TimeUpdate, ValueOLD, InputSource, InputTime) " +
            //               "VALUES(@Id, dbo.GetDepArr(@Route), @linecode, @number, @FlightNo, @ScheduledDate, @Route, dbo.GetRouteType(@Route), dbo.GetCity(@Route), " +
            //               "'"+ getCityfullName(newRabbitMQ.Route) +"', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', @TimeSet, '', @ValueOLD, @InputSource, @InputTime)";

            string query = "INSERT INTO [RABBITMQFLIGHT].[dbo].[AHT_T1_FlightInformation] (Id, Adi, LineCode , Number , FlightNo , ScheduledDate , Route , " +
                           "Terminal ,City ,CityName ,Aircraft, PaxCount , " +
                           "TimeSet, TimeUpdate, ValueOLD, InputSource, InputTime, Status) " +
                           "VALUES(@Id, dbo.GetDepArr(@Route), @linecode, @number, @FlightNo, @ScheduledDate, @Route, "+
						   "dbo.GetRouteType(@Route), dbo.GetCity(@Route),'"+ getCityfullName(newRabbitMQ.Route) +"','','', "+
                           " @TimeSet, '', @ValueOLD, @InputSource, @InputTime, @Status)";  
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", "FlightNo: " + newRabbitMQ.FlightNo + " FlightDate: " + newRabbitMQ.FlightDate + " Route: " + newRabbitMQ.Route);
                        command.Parameters.AddWithValue("@FlightNo", newRabbitMQ.FlightNo);
                        command.Parameters.AddWithValue("@linecode", (newRabbitMQ.FlightNo.Length > 4 ? newRabbitMQ.FlightNo.Substring(0 , 2) : ""));
                        command.Parameters.AddWithValue("@number", (newRabbitMQ.FlightNo.Length > 4 ? newRabbitMQ.FlightNo.Substring(2, newRabbitMQ.FlightNo.Length -2) : ""));
                        command.Parameters.AddWithValue("@ScheduledDate", newRabbitMQ.FlightDate);
                        command.Parameters.AddWithValue("@Route", newRabbitMQ.Route);
                        command.Parameters.AddWithValue("@TimeSet", DateTime.Now);
                        command.Parameters.AddWithValue("@ValueOLD", newRabbitMQ.FieldName + "-" + newRabbitMQ.FieldValue + "-" + newRabbitMQ.ValueOLD);
                        command.Parameters.AddWithValue("@InputSource", newRabbitMQ.InputSource);
                        command.Parameters.AddWithValue("@InputTime", newRabbitMQ.InputTime);
                        command.Parameters.AddWithValue("@Status", "OPN");
                        command.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error3: " + ex.Message);
                    return false;
                }
                finally { connection.Close(); }
            }
        }

    }
}
