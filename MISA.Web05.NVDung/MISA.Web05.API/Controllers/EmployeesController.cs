using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web05.Core.Models;
using MISA.Web05.Core;
using MISA.Web05.Core.Interfaces.Repository;
using MISA.Web05.Core.Interfaces.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace MISA.Web05.API.Controllers
{
    /// <summary>
    /// API nhân viên
    /// Created by NVDung (26/6/2022)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseControllers<Employee>
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="service">Đối tượng được implement IBaseService</param>
        /// <param name="repository">Đối tượng được implement IBaseRepository</param>
        /// 
        private readonly IEmployeeRepository _repository;
        private readonly IEmployeeService _service;
        public EmployeesController(IBaseServices<Employee> service, IBaseRepository<Employee> repository, IEmployeeRepository employeeRepository, IEmployeeService employeeService) : base(service, repository)
        {
            _repository = employeeRepository;
            _service = employeeService;
        }
        /// <summary>
        /// Lấy mã mới
        /// Created by NVDung (11/7/2022)
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        [HttpGet($"NewEmployeeCode")]
        public async Task<IActionResult> GetNewEmployeeCode()
        {
            try
            {
                var employeeCode = await _repository.GetNewEmployeeCodeAsync();
                return Ok(employeeCode);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.Web05.Core.Resources.Resource.ResourceManager.GetString($"ErrorException_VN")

                };
                return StatusCode(500, response);
            }
        }
        /// <summary>
        /// Kiểm tra mã nhân viên đã tồn tại chưa
        /// Created by NVDung (15/7/2022)s
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>true or false</returns>
        [HttpGet("exist")]
        public async Task<IActionResult> CheckExistEmployeeCode(string employeeCode)
        {
            try
            {
                var isExist = await _repository.CheckExistEmployeeCodeAsync(employeeCode);
                return Ok(isExist);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.Web05.Core.Resources.Resource.ResourceManager.GetString($"ErrorException_VN")

                };
                return StatusCode(500, response);
            }
        }
        /// <summary>
        /// Phân trang, lọc
        /// Created by NVDung (11/7/2022)
        /// </summary>
        /// <param name="pageIndex">Số thứ tự trang</param>
        /// <param name="pageSize">Số bản ghi trên trang</param>
        /// <param name="filter">filter tìm kiếm</param>
        /// <returns>Danh sách object</returns>
        [HttpGet("filter")]
        public async Task<IActionResult> GetPaging(int pageIndex, int pageSize, string? filter)
        {
            try
            {
                var res = await _repository.GetPagingAsync(pageIndex, pageSize, filter);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.Web05.Core.Resources.Resource.ResourceManager.GetString($"ErrorException_VN")

                };
                return StatusCode(500, response);
            }
        }
        /// <summary>
        /// Nhập khẩu dữ liệu
        /// Created by NVDung (27/7/2022)
        /// </summary>
        /// <param name="fileImport">File xlsx</param>
        /// <returns></returns>
        [HttpPost("import")]
        public async Task<IActionResult> Import(IFormFile fileImport)
        {
            try
            {
                var employee = await _service.Import(fileImport);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// API xuất khẩu
        /// Created by NVDung (18/7/2022)
        /// </summary>
        /// <returns>File xlsx</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportV2()
        {
            string excelName = $"EmployeeList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            var stream = await _service.ExportAsync();
            //return Ok(list);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
