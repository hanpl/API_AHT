using AHTAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AHTAPI.Repositories
{
    public class EmployeeWorkRepository
    {
        string connectionString;
        public EmployeeWorkRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #region Get All Name
        public List<AHT_EmployeeWorkDaily> GetAllEmployee()
        {
            List<AHT_EmployeeWorkDaily> aHT_EmployeeWorkDailys = new List<AHT_EmployeeWorkDaily>();
            AHT_EmployeeWorkDaily aHT_EmployeeWorkDaily;
            var data = getAllName();
            foreach (DataRow row in data.Rows)
            {
                aHT_EmployeeWorkDaily = new AHT_EmployeeWorkDaily
                {
                    Department = row["Department"].ToString(),
                    FullName = row["FullName"].ToString(),
                    ShiftCode = row["ShiftCode"].ToString(),
                    ShiftCodeV = row["ShiftCodeV"].ToString(),
                    ShiftDate = row["ShiftDate"].ToString(),
                    ChucDanh = row["ChucDanh"].ToString(),
                };
                aHT_EmployeeWorkDailys.Add(aHT_EmployeeWorkDaily);
            }
            return aHT_EmployeeWorkDailys;
        }
        public DataTable getAllName()
        {
            string query = "SELECT Department, FullName, ShiftCode, ShiftCodeV, ShiftDate, ChucDanh " +
                "FROM AHT_EmployeeWorkDaily WHERE ShiftDate = CONVERT(DATE, GETDATE()) " +
                "AND LTRIM(RTRIM(Department)) COLLATE SQL_Latin1_General_CP1_CI_AI NOT LIKE N'%Phòng Điều Hành%' " +
                "ORDER BY hienthi, Department, stt";
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
