using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AHTAPI.Repositories
{
    public class UpdateModeCounterRepository
    {
        string connectionString;
        public UpdateModeCounterRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IActionResult UpdateMode(string name, string mode)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query để cập nhật mode dựa trên name
                    var query = "UPDATE [MSMQFLIGHT].[dbo].[AHT_CountersInformation] SET Mode = @Mode WHERE Name = @Name";

                    using (var command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số cho câu lệnh SQL
                        command.Parameters.AddWithValue("@Mode", mode);
                        command.Parameters.AddWithValue("@Name", name);

                        // Thực thi câu lệnh SQL
                        var rowsAffected = command.ExecuteNonQuery();

                        // Kiểm tra số hàng bị ảnh hưởng
                        if (rowsAffected > 0)
                        {
                            return new OkObjectResult(new { Message = "Mode updated successfully." });
                        }
                        else
                        {
                            return new NotFoundObjectResult(new { Message = "No record found with the given Name." });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi hoặc log (tùy thuộc vào hệ thống logging bạn sử dụng)
                return new ObjectResult(new { Message = "An error occurred while updating mode.", Details = ex.Message })
                {
                    StatusCode = 500
                };
            }
        }

        public IActionResult UpdateAuto(string name, string auto)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query để cập nhật mode dựa trên name
                    var query = "UPDATE [MSMQFLIGHT].[dbo].[AHT_CountersInformation] SET Auto = @Auto WHERE Name = @Name";

                    using (var command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số cho câu lệnh SQL
                        command.Parameters.AddWithValue("@Auto", auto);
                        command.Parameters.AddWithValue("@Name", name);

                        // Thực thi câu lệnh SQL
                        var rowsAffected = command.ExecuteNonQuery();

                        // Kiểm tra số hàng bị ảnh hưởng
                        if (rowsAffected > 0)
                        {
                            return new OkObjectResult(new { Message = "Mode updated successfully." });
                        }
                        else
                        {
                            return new NotFoundObjectResult(new { Message = "No record found with the given Name." });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi hoặc log (tùy thuộc vào hệ thống logging bạn sử dụng)
                return new ObjectResult(new { Message = "An error occurred while updating mode.", Details = ex.Message })
                {
                    StatusCode = 500
                };
            }
        }


        public IActionResult UpdateNomal(string name, string nomal)
        {
            var flight = getFlightByName(name);
            if (flight == null || flight.Rows.Count == 0)
            {
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Query để cập nhật mode dựa trên name
                        var query = "INSERT INTO [MSMQFLIGHT].[dbo].[AHT_ImageForFlight] (Name, Nomal, Eco, Bus, Manual) VALUES (@Name,@Nomal,@Eco, @Bus, @Manual) ";

                        using (var command = new SqlCommand(query, connection))
                        {
                            // Thêm tham số cho câu lệnh SQL
                            command.Parameters.AddWithValue("@Nomal", nomal);
                            command.Parameters.AddWithValue("@Name", name);
                            command.Parameters.AddWithValue("@Eco", "");
                            command.Parameters.AddWithValue("@Bus", "");
                            command.Parameters.AddWithValue("@Manual", "");

                            // Thực thi câu lệnh SQL
                            var rowsAffected = command.ExecuteNonQuery();

                            // Kiểm tra số hàng bị ảnh hưởng
                            if (rowsAffected > 0)
                            {
                                return new OkObjectResult(new { Message = "Mode Insert successfully." });
                            }
                            else
                            {
                                return new NotFoundObjectResult(new { Message = "No record found with the given Name. insert error" });
                            }
                        }


                    }
                }
                catch (Exception ex)
                {
                    // Ghi lại lỗi hoặc log (tùy thuộc vào hệ thống logging bạn sử dụng)
                    return new ObjectResult(new { Message = "An error occurred while updating mode.", Details = ex.Message })
                    {
                        StatusCode = 500
                    };
                }
            }
            else {
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Query để cập nhật mode dựa trên name
                        var query = "UPDATE [MSMQFLIGHT].[dbo].[AHT_ImageForFlight] SET Nomal = @Nomal WHERE Name = @Name";

                        using (var command = new SqlCommand(query, connection))
                        {
                            // Thêm tham số cho câu lệnh SQL
                            command.Parameters.AddWithValue("@Nomal", nomal);
                            command.Parameters.AddWithValue("@Name", name);

                            // Thực thi câu lệnh SQL
                            var rowsAffected = command.ExecuteNonQuery();

                            // Kiểm tra số hàng bị ảnh hưởng
                            if (rowsAffected > 0)
                            {
                                return new OkObjectResult(new { Message = "Mode updated successfully." });
                            }
                            else
                            {
                                return new NotFoundObjectResult(new { Message = "No record found with the given Name." });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Ghi lại lỗi hoặc log (tùy thuộc vào hệ thống logging bạn sử dụng)
                    return new ObjectResult(new { Message = "An error occurred while updating mode.", Details = ex.Message })
                    {
                        StatusCode = 500
                    };
                }
            }
            
        }

        public IActionResult UpdateEco(string name, string eco)
        {
            var flight = getFlightByName(name);
            if (flight == null || flight.Rows.Count == 0)
            {
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Query để cập nhật mode dựa trên name
                        var query = "INSERT INTO [MSMQFLIGHT].[dbo].[AHT_ImageForFlight] (Name, Nomal, Eco, Bus, Manual) VALUES (@Name,@Nomal,@Eco, @Bus, @Manual) ";

                        using (var command = new SqlCommand(query, connection))
                        {
                            // Thêm tham số cho câu lệnh SQL
                            command.Parameters.AddWithValue("@Nomal", "");
                            command.Parameters.AddWithValue("@Name", name);
                            command.Parameters.AddWithValue("@Eco", eco);
                            command.Parameters.AddWithValue("@Bus", "");
                            command.Parameters.AddWithValue("@Manual", "");

                            // Thực thi câu lệnh SQL
                            var rowsAffected = command.ExecuteNonQuery();

                            // Kiểm tra số hàng bị ảnh hưởng
                            if (rowsAffected > 0)
                            {
                                return new OkObjectResult(new { Message = "Mode Insert successfully." });
                            }
                            else
                            {
                                return new NotFoundObjectResult(new { Message = "No record found with the given Name." });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Ghi lại lỗi hoặc log (tùy thuộc vào hệ thống logging bạn sử dụng)
                    return new ObjectResult(new { Message = "An error occurred while updating mode.", Details = ex.Message })
                    {
                        StatusCode = 500
                    };
                }
            }
            else
            {
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Query để cập nhật mode dựa trên name
                        var query = "UPDATE [MSMQFLIGHT].[dbo].[AHT_ImageForFlight] SET Eco = @Eco WHERE Name = @Name";

                        using (var command = new SqlCommand(query, connection))
                        {
                            // Thêm tham số cho câu lệnh SQL
                            command.Parameters.AddWithValue("@Eco", eco);
                            command.Parameters.AddWithValue("@Name", name);

                            // Thực thi câu lệnh SQL
                            var rowsAffected = command.ExecuteNonQuery();

                            // Kiểm tra số hàng bị ảnh hưởng
                            if (rowsAffected > 0)
                            {
                                return new OkObjectResult(new { Message = "Mode updated successfully." });
                            }
                            else
                            {
                                return new NotFoundObjectResult(new { Message = "No record found with the given Name." });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Ghi lại lỗi hoặc log (tùy thuộc vào hệ thống logging bạn sử dụng)
                    return new ObjectResult(new { Message = "An error occurred while updating mode.", Details = ex.Message })
                    {
                        StatusCode = 500
                    };
                }
            }

        }

        public IActionResult UpdateBus(string name, string Bus)
        {
            var flight = getFlightByName(name);
            if (flight == null || flight.Rows.Count == 0)
            {
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Query để cập nhật mode dựa trên name
                        var query = "INSERT INTO [MSMQFLIGHT].[dbo].[AHT_ImageForFlight] (Name, Nomal, Eco, Bus, Manual) VALUES (@Name,@Nomal,@Eco, @Bus, @Manual) ";

                        using (var command = new SqlCommand(query, connection))
                        {
                            // Thêm tham số cho câu lệnh SQL
                            command.Parameters.AddWithValue("@Nomal", "");
                            command.Parameters.AddWithValue("@Name", name);
                            command.Parameters.AddWithValue("@Eco", "");
                            command.Parameters.AddWithValue("@Bus", Bus);
                            command.Parameters.AddWithValue("@Manual", "");

                            // Thực thi câu lệnh SQL
                            var rowsAffected = command.ExecuteNonQuery();

                            // Kiểm tra số hàng bị ảnh hưởng
                            if (rowsAffected > 0)
                            {
                                return new OkObjectResult(new { Message = "Mode Insert successfully." });
                            }
                            else
                            {
                                return new NotFoundObjectResult(new { Message = "No record found with the given Name." });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Ghi lại lỗi hoặc log (tùy thuộc vào hệ thống logging bạn sử dụng)
                    return new ObjectResult(new { Message = "An error occurred while updating mode.", Details = ex.Message })
                    {
                        StatusCode = 500
                    };
                }
            }
            else
            {
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Query để cập nhật mode dựa trên name
                        var query = "UPDATE [MSMQFLIGHT].[dbo].[AHT_ImageForFlight] SET Bus = @Nomal WHERE Name = @Name";

                        using (var command = new SqlCommand(query, connection))
                        {
                            // Thêm tham số cho câu lệnh SQL
                            command.Parameters.AddWithValue("@Nomal", Bus);
                            command.Parameters.AddWithValue("@Name", name);

                            // Thực thi câu lệnh SQL
                            var rowsAffected = command.ExecuteNonQuery();

                            // Kiểm tra số hàng bị ảnh hưởng
                            if (rowsAffected > 0)
                            {
                                return new OkObjectResult(new { Message = "Mode updated successfully." });
                            }
                            else
                            {
                                return new NotFoundObjectResult(new { Message = "No record found with the given Name." });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Ghi lại lỗi hoặc log (tùy thuộc vào hệ thống logging bạn sử dụng)
                    return new ObjectResult(new { Message = "An error occurred while updating mode.", Details = ex.Message })
                    {
                        StatusCode = 500
                    };
                }
            }

        }


        public IActionResult UpdateManual(string name, string manual)
        {
            var flight = getFlightByName(name);
            if (flight == null || flight.Rows.Count == 0)
            {
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Query để cập nhật mode dựa trên name
                        var query = "INSERT INTO [MSMQFLIGHT].[dbo].[AHT_ImageForFlight] (Name, Nomal, Eco, Bus, Manual) VALUES (@Name,@Nomal,@Eco, @Bus, @Manual) ";

                        using (var command = new SqlCommand(query, connection))
                        {
                            // Thêm tham số cho câu lệnh SQL
                            command.Parameters.AddWithValue("@Nomal", "");
                            command.Parameters.AddWithValue("@Name", name);
                            command.Parameters.AddWithValue("@Eco", "");
                            command.Parameters.AddWithValue("@Bus", "");
                            command.Parameters.AddWithValue("@Manual", manual);

                            // Thực thi câu lệnh SQL
                            var rowsAffected = command.ExecuteNonQuery();

                            // Kiểm tra số hàng bị ảnh hưởng
                            if (rowsAffected > 0)
                            {
                                return new OkObjectResult(new { Message = "Mode Insert successfully." });
                            }
                            else
                            {
                                return new NotFoundObjectResult(new { Message = "No record found with the given Name." });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Ghi lại lỗi hoặc log (tùy thuộc vào hệ thống logging bạn sử dụng)
                    return new ObjectResult(new { Message = "An error occurred while updating mode.", Details = ex.Message })
                    {
                        StatusCode = 500
                    };
                }
            }
            else
            {
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Query để cập nhật mode dựa trên name
                        var query = "UPDATE [MSMQFLIGHT].[dbo].[AHT_ImageForFlight] SET Manual = @Nomal WHERE Name = @Name";

                        using (var command = new SqlCommand(query, connection))
                        {
                            // Thêm tham số cho câu lệnh SQL
                            command.Parameters.AddWithValue("@Nomal", manual);
                            command.Parameters.AddWithValue("@Name", name);

                            // Thực thi câu lệnh SQL
                            var rowsAffected = command.ExecuteNonQuery();

                            // Kiểm tra số hàng bị ảnh hưởng
                            if (rowsAffected > 0)
                            {
                                return new OkObjectResult(new { Message = "Mode updated successfully." });
                            }
                            else
                            {
                                return new NotFoundObjectResult(new { Message = "No record found with the given Name." });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Ghi lại lỗi hoặc log (tùy thuộc vào hệ thống logging bạn sử dụng)
                    return new ObjectResult(new { Message = "An error occurred while updating mode.", Details = ex.Message })
                    {
                        StatusCode = 500
                    };
                }
            }

        }



        public DataTable getFlightByName(string name)
        {
            string query = "SELECT * FROM [MSMQFLIGHT].[dbo].[AHT_ImageForFlight] WHERE Name = '" + name + "' ";
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


    }
}
