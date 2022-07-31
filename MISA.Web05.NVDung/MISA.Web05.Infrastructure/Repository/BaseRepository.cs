using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MISA.Web05.Core.Interfaces.Repository;
using MySqlConnector;

namespace MISA.Web05.Infrastructure.Repository
{
    /// <summary>
    /// Base Repository dùng chung
    /// Created by NVDung (26/6/2022)
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    public class BaseRepository<Entity> : IBaseRepository<Entity>
    {
        protected string connectionString;
        protected MySqlConnection mySqlConnection;
        protected string tableName;
        /// <summary>
        /// Khởi tạo đối tượng
        /// Created by NVDung (11/7/2022)
        /// </summary>
        public BaseRepository()
        {
            // Khai báo thông tin CSDL
            connectionString = MISA.Web05.Core.Resources.Resource.ResourceManager.GetString("DatabaseConfig"); ;
            tableName = typeof(Entity).Name;
        }
        /// <summary>
        /// Lấy danh sách đối tượng 
        /// Created by NVDung (11/7/2022)
        /// </summary>
        /// <returns>Danh sách đối tượng Entity</returns>
        public async Task<IEnumerable<Entity>> GetAsync()
        {
            try
            {
                // Khởi tạo kết nối
                using (mySqlConnection = new MySqlConnection(connectionString))
                {
                    // Lấy dữ liệu
                    var sqlQuery = $"Proc_Get{tableName}";
                    var entities = await mySqlConnection.QueryAsync<Entity>(sqlQuery,
                        commandType: System.Data.CommandType.StoredProcedure);
                    // Trả về dữ liệu
                    return entities;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  Phân trang, lọc
        ///  Created by NVDung (11/7/2022)
        /// </summary>
        /// <param name="pageIndex">Số thứ tự trang</param>
        /// <param name="pageSize">Số lượng bản ghi</param>
        /// <param name="filter">filter tìm kiếm</param>
        /// <returns>Đối tượng object</returns>
        public async Task<object> GetPagingAsync(int pageIndex, int pageSize, string? filter)
        {
            try
            {
                // Khởi tạo kết nối
                using (mySqlConnection = new MySqlConnection(connectionString))
                {
                    // Lấy dữ liệu
                    var sqlQuery = $"Proc_Get{tableName}Paging";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@d_Filter", filter);
                    parameters.Add("@d_PageIndex", pageIndex);
                    parameters.Add("@d_PageSize", pageSize);
                    parameters.Add("@d_TotalRecord", direction: System.Data.ParameterDirection.Output);
                    parameters.Add("@d_RecordStart", direction: System.Data.ParameterDirection.Output);
                    parameters.Add("@d_RecordEnd", direction: System.Data.ParameterDirection.Output);
                    var employees = await mySqlConnection.QueryAsync<Entity>(sqlQuery, param: parameters,
                        commandType: System.Data.CommandType.StoredProcedure);
                    int totalRecord = parameters.Get<int>("@d_TotalRecord");
                    int recordStart = parameters.Get<int>("@d_RecordStart");
                    int recordEnd = parameters.Get<int>("@d_RecordEnd");
                    // Trả về dữ liệu
                    return new
                    {
                        TotalRecord = totalRecord,
                        RecordStart = recordStart,
                        RecordEnd = recordEnd,
                        Data = employees
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  Lấy theo Id
        ///  Created by NVDung (11/7/2022)
        /// </summary>
        /// <param name="id">Mã</param>
        /// <returns>Đối tượng Entity</returns>
        public async Task<Entity> GetByIdAsync(Guid id)
        {
            try
            {
                using (mySqlConnection = new MySqlConnection(connectionString))
                {
                    var sqlQuery = $"SELECT * FROM {tableName} WHERE {tableName}Id = @{tableName}Id";
                    var parameters = new DynamicParameters();
                    parameters.Add($"@{tableName}Id", id);
                    var employee = await mySqlConnection.QueryFirstOrDefaultAsync<Entity>(sqlQuery, param: parameters);
                    return employee;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Thêm một đối tượng
        /// Created by NVDung (11/7/2022)
        /// </summary>
        /// <param name="entity">object</param>
        /// <returns>true or false</returns>
        public async Task<int> InsertAsync(Entity entity)
        {
            try
            {
                // Khởi tạo kết nối
                using (mySqlConnection = new MySqlConnection(connectionString))
                {
                    // Khởi tạo truy vấn thêm mới
                    var sqlQuery = $"Proc_Insert{tableName}";
                    var res = await mySqlConnection.ExecuteAsync(sqlQuery, param: entity,
                        commandType: System.Data.CommandType.StoredProcedure);
                    return res;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Sửa một đối tượng
        /// Created by NVDung (11/7/2022)
        /// </summary>
        /// <param name="entity">object</param>
        /// <returns>true or false</returns>
        public async Task<int> UpdateAsync(Entity entity)
        {
            try
            {
                // Khởi tạo kết nối
                using (mySqlConnection = new MySqlConnection(connectionString))
                {
                    // Khởi tạo truy vấn sửa
                    var sqlQuery = $"Proc_Update{tableName}";
                    var res = await mySqlConnection.ExecuteAsync(sqlQuery, param: entity,
                        commandType: System.Data.CommandType.StoredProcedure);
                    return res;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        ///  Xóa một đối tượng
        ///  Created by NVDung (11/7/2022)
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>true or false</returns>
        public async Task<int> DeleteAsync(Guid entityId)
        {
            try
            {
                // Khởi tạo kết nối
                using (mySqlConnection = new MySqlConnection(connectionString))
                {
                    var sqlQuery = $"DELETE FROM {tableName} WHERE {tableName}Id = @{tableName}Id";
                    var parameters = new DynamicParameters();
                    parameters.Add($"@{tableName}Id", entityId);
                    var res = await mySqlConnection.ExecuteAsync(sql: sqlQuery, param: parameters);
                    return res;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
