using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Core.Interfaces.Services
{
    /// <summary>
    /// Service Base dùng chung
    /// Created by NVDung (26/6/2022)
    /// </summary>
    public interface IBaseServices<Entity>
    {
        // Thêm service
        Task<int> InsertService(Entity entity);
        // Sửa service
        Task<int> UpdateService(Entity entity);
    }
}
