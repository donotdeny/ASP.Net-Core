using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web05.Core.Interfaces.Repository;
using MISA.Web05.Core.Interfaces.Services;
using MISA.Web05.Core.Models;

namespace MISA.Web05.API.Controllers
{
    /// <summary>
    /// API phòng ban
    /// Created by NVDung (12/07/2022)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseControllers<Department>
    {
        public DepartmentsController(IBaseServices<Department> service, IBaseRepository<Department> repository) : base(service, repository)
        {

        }
    }
}
