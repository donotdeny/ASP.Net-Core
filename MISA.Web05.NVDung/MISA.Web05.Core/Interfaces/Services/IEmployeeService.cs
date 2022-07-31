using Microsoft.AspNetCore.Http;
using MISA.Web05.Core.Models;

namespace MISA.Web05.Core.Interfaces.Services
{
    /// <summary>
    /// Interface giao tiếp với API 
    /// Created by NVDung (26/6/2022)
    /// </summary>
    public interface IEmployeeService : IBaseServices<Employee>
    {
        /// <summary>
        /// Thực hiện nhập khẩu dữ liệu
        /// Created by NVDung (13/7/2022)
        /// </summary>
        /// <param name="fileImport"></param>
        /// <returns>Danh sách nhân viên kèm theo trạng thái đã được nhập khẩu chưa</returns>
        Task<IEnumerable<Employee>> Import(IFormFile fileImport);
        /// <summary>
        /// Thực hiện xuất khẩu dữ liệu
        /// Created by NVDung (18/7/2022)
        /// </summary>
        /// <returns></returns>
        Task<MemoryStream> ExportAsync();   
    }
}
