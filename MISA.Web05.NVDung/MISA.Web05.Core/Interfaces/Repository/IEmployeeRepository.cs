using MISA.Web05.Core.Models;

namespace MISA.Web05.Core.Interfaces.Repository
{
    /// <summary>
    /// Interface giao tiếp với Infrastructure
    /// Created by NVDung (26/6/2022)
    /// </summary>
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        // Lấy mã mới
        Task<string> GetNewEmployeeCodeAsync();
        // Nhập khẩu
        Task<int> ImportAsync(IEnumerable<Employee> employees);
        // Kiểm tra xem mã nhân viên đã tồn tại chưa
        Task<bool> CheckExistEmployeeCodeAsync(string employeeCode);
        // Xuất khẩu dữ liệu
        Task<List<EmployeeExport>> ExportData();
    }
}
