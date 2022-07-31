using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Core.Interfaces.Repository
{
    /// <summary>
    /// Base Interface Repository
    /// Created by NVDung (26/6/2022)
    /// </summary>
    public interface IBaseRepository<Entity>
    {
        // Lấy danh sách 
        Task<IEnumerable<Entity>> GetAsync();
        // Lấy theo mã 
        Task<Entity> GetByIdAsync(Guid id);
        // Thêm một đối tượng
        Task<int> InsertAsync(Entity employee);
        // Sửa một đối tượng
        Task<int> UpdateAsync(Entity employee);
        // Xóa một đối tượng
        Task<int> DeleteAsync(Guid entityId);
        // Phân trang
        Task<object> GetPagingAsync(int pageIndex, int pageSize, string? filter = "");
    }
}
