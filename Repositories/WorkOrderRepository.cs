using System.Data;
using AHTAPI.Models;
using Microsoft.Data.SqlClient;

namespace AHTAPI.Repositories
{
    public class WorkOrderRepository
    {
        string connectionString;
        public WorkOrderRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //HttpPost
        #region HttpPost
        public bool AddWorkOrder(AHT_WorkOrder newWorkOrder)
        {
            string query = "INSERT INTO [MSMQFLIGHT].[dbo].[AHT_WorkOrder] (GroupName, SystemName, Location, Status, ErrorDevice, Error, TimeOrder, TimeStart,TimeEnd,Description,Complate    )" +
                "VALUES (@GroupName,@SystemName,@Location, @Status, @ErrorDevice, @Error, @TimeOrder,@TimeStart, @TimeEnd, @Description,@Complate) ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GroupName", newWorkOrder.GroupName);
                        command.Parameters.AddWithValue("@SystemName", newWorkOrder.SystemName);
                        command.Parameters.AddWithValue("@Location", newWorkOrder.Location);
                        command.Parameters.AddWithValue("@Status", newWorkOrder.Status);
                        command.Parameters.AddWithValue("@ErrorDevice", newWorkOrder.ErrorDevice);
                        command.Parameters.AddWithValue("@Error", newWorkOrder.Error);
                        command.Parameters.AddWithValue("@TimeOrder", newWorkOrder.TimeOrder);
                        command.Parameters.AddWithValue("@TimeStart", newWorkOrder.TimeStart);
                        command.Parameters.AddWithValue("@TimeEnd", newWorkOrder.TimeEnd);
                        command.Parameters.AddWithValue("@Description", newWorkOrder.Description);
                        command.Parameters.AddWithValue("@Complate", newWorkOrder.Complate);
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
        //End posst
        #endregion
        //Http Put
        #region Http put
        public bool UpdateWorkOrder(AHT_WorkOrder workOrder)
        {
            string query = "UPDATE [MSMQFLIGHT].[dbo].[AHT_WorkOrder] SET Status =@Status, Description = @Description WHERE Id = @Id";
            DataTable dataTable = new DataTable();
            //string connectionString = "Data Source=172.17.2.38;Initial Catalog=MSMQFLIGHT;Persist Security Info=True;User ID=sa;Password=AHT@2019";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Status", workOrder.Status);
                        command.Parameters.AddWithValue("@Description", workOrder.Description);
                        command.Parameters.AddWithValue("@Id", workOrder.Id);
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
        //Http Get by Id
        #region Get By ID
        public AHT_WorkOrder GetWorkOrderById(int id)
        {
            AHT_WorkOrder aHT_WorkOrder = new AHT_WorkOrder();
             var data = GetWorkOrderDetailsFromDbById(id);
            foreach (DataRow row in data.Rows)
            {
                aHT_WorkOrder = new AHT_WorkOrder
                {
                    Id = Convert.ToInt32(row["Id"]),
                    GroupName = row["GroupName"].ToString(),
                    SystemName = row["SystemName"].ToString(),
                    Location = row["Location"].ToString(),
                    Status = row["Status"].ToString(),
                    ErrorDevice = row["ErrorDevice"].ToString(),
                    Error = row["Error"].ToString(),
                    TimeOrder = row["TimeOrder"].ToString(),
                    TimeStart = row["TimeStart"].ToString(),
                    TimeEnd = row["TimeEnd"].ToString(),
                    Description = row["Description"].ToString(),
                    Complate = row["Complate"].ToString(),
                };
            }
            return aHT_WorkOrder;
        }
        public DataTable GetWorkOrderDetailsFromDbById(int id)
        {
            string query = "SELECT * FROM [MSMQFLIGHT].[dbo].[AHT_WorkOrder] WHERE Id = '"+id+"'";
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
        //Http Get All
        #region Get All # Done
        public List<AHT_WorkOrder> GetWorkOrder()
        {
            List<AHT_WorkOrder> aHT_WorkOrders = new List<AHT_WorkOrder>();
            AHT_WorkOrder aHT_WorkOrder;
            var data = GetWorkOrderDetailsFromDb();
            foreach (DataRow row in data.Rows)
            {
                aHT_WorkOrder = new AHT_WorkOrder
                {
                    Id = Convert.ToInt32(row["Id"]),
                    GroupName = row["GroupName"].ToString(),
                    SystemName = row["SystemName"].ToString(),
                    Location = row["Location"].ToString(),
                    Status = row["Status"].ToString(),
                    ErrorDevice = row["ErrorDevice"].ToString(),
                    Error = row["Error"].ToString(),
                    TimeOrder = row["TimeOrder"].ToString(),
                    TimeStart = row["TimeStart"].ToString(),
                    TimeEnd = row["TimeEnd"].ToString(),
                    Description = row["Description"].ToString(),
                    Complate = row["Complate"].ToString(),
                };
                aHT_WorkOrders.Add(aHT_WorkOrder);
            }
            return aHT_WorkOrders;
        }
        public DataTable GetWorkOrderDetailsFromDb()
        {
            string query = "SELECT * FROM [MSMQFLIGHT].[dbo].[AHT_WorkOrder] WHERE Status != 'Done'";
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
