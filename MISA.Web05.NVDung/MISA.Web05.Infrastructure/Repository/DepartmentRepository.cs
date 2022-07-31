using MISA.Web05.Core.Interfaces.Repository;
using MISA.Web05.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Infrastructure.Repository
{
    /// <summary>
    /// Truy vấn CSDL Phòng ban
    /// Created by NVDung (12/7/2022)
    /// </summary>
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        // Hàm khởi tạo
        public DepartmentRepository()
        {

        }
    }
}
