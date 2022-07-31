using MISA.Web05.Core.Models;
using MISA.Web05.Core.Interfaces.Repository;
using Dapper;
using MySqlConnector;

namespace MISA.Web05.Infrastructure.Repository
{
    /// <summary>
    /// Truy vấn CSDL Nhân viên
    /// Created by NVDung (26/6/2022)
    /// </summary>
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        // Khởi tạo đối tượng
        public EmployeeRepository()
        {
           
        }
        /// <summary>
        /// Kiểm tra mã nhân viên đã tồn tại chưa
        /// Created by NVDung (15/7/2022)
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>true or false</returns>
        public async Task<bool> CheckExistEmployeeCodeAsync(string employeeCode)
        {
            try
            {
                using (mySqlConnection = new MySqlConnection(connectionString))
                {
                    var sqlQuery = $"SELECT EmployeeCode FROM Employee WHERE EmployeeCode = @EmployeeCode";
                    var parameters = new DynamicParameters();
                    parameters.Add($"@EmployeeCode", employeeCode);
                    var isExist = await mySqlConnection.QueryFirstOrDefaultAsync(sqlQuery, param: parameters);
                    if (isExist == null)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Lấy mã mới
        /// Creadted by NVDung (12/7/2022)
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        public async Task<string> GetNewEmployeeCodeAsync()
        {
            try
            {
                // Khởi tạo kết nối
                using (mySqlConnection = new MySqlConnection(connectionString))
                {
                    // Lấy dữ liệu
                    var sqlQuery = $"Proc_Get{tableName}CodeMax";
                    string employeeCode = await mySqlConnection.QueryFirstOrDefaultAsync<string>(sqlQuery,
                        commandType: System.Data.CommandType.StoredProcedure);
                    string a = employeeCode;
                    string b = string.Empty;
                    int val;
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (Char.IsDigit(a[i]))
                            b += a[i];
                    }
                    if (b.Length > 0)
                    {
                        val = int.Parse(b);
                        employeeCode = employeeCode.Substring(0, 2) + "-" + (val).ToString();
                    }
                    // Trả về dữ liệu
                    return employeeCode;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Nhập khẩu
        /// </summary>
        /// <param name="employees">Danh sách đối tựợng insert</param>
        /// <returns>Danh sách thêm thành công</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> ImportAsync(IEnumerable<Employee> employees)
        {
            try
            {
                // Khởi tạo kết nối
                using (mySqlConnection = new MySqlConnection(connectionString))
                {
                    var employeeesInserted = 0;
                    foreach(var employee in employees)
                    {
                        var rowInsert = await mySqlConnection.ExecuteAsync(
                            "Proc_InsertEmployee", employee, 
                            commandType:System.Data.CommandType.StoredProcedure);
                        if (rowInsert != null)
                        {
                            employeeesInserted++;
                        }
                    }
                    return employeeesInserted; 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Repo yêu cầu xuất khẩu dữ liệu
        /// Created by NVDung (18/7/2022)
        /// </summary>
        /// <returns>Danh sách nhân viên trả về</returns>
        public async Task<List<EmployeeExport>> ExportData()
        {
            try
            {
                using (mySqlConnection = new MySqlConnection(connectionString))
                {
                    var sqlQuery = "Proc_ExportEmployee";
                    var employees = await mySqlConnection.QueryAsync<EmployeeExport>(sqlQuery,
                        commandType: System.Data.CommandType.StoredProcedure);
                    return employees.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
