using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web05.Core.Interfaces.Repository;
using MISA.Web05.Core.Interfaces.Services;

namespace MISA.Web05.API.Controllers
{
    /// <summary>
    /// Controller Base 
    /// Created by NVDung (11/7/2022)
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseControllers<Entity> : ControllerBase
    {
        private readonly IBaseServices<Entity> _service;
        private readonly IBaseRepository<Entity> _repository;
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="service">Đối tượng được implement IBaseService</param>
        /// <param name="repository">Đối tượng được implement IBaseRepository</param>
        public BaseControllers(IBaseServices<Entity> service, IBaseRepository<Entity> repository)
        {
            _service = service;
            _repository = repository;   
        }

        /// <summary>
        /// Lấy toàn bộ danh sách
        /// Created by NVDung (11/7/2022)
        /// </summary>
        /// <returns>Danh sách đối tượng Entity</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var entities = await _repository.GetAsync();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                // Trả về thông tin lỗi
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.Web05.Core.Resources.Resource.ResourceManager.GetString($"ErrorException_VN")

                };
                return StatusCode(500, response);
            }
        }



        /// <summary>
        /// Lấy theo mã 
        /// Created by NVDung (11/7/2022)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Đối tượng Entity</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                // Trả về thông tin lỗi
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.Web05.Core.Resources.Resource.ResourceManager.GetString($"ErrorException_VN")

                };
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Thêm một đối tượng
        /// Created by NVDung (11/7/2022)
        /// </summary>
        /// <param name="entity">đối tượng insert</param>
        /// <returns>status code</returns>
        [HttpPost]
        public async Task<IActionResult> Post(Entity entity)
        {
            try
            {
                // Trả kết quả về client
                var res = await _service.InsertService(entity);
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {
                // Trả về thông tin lỗi
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.Web05.Core.Resources.Resource.ResourceManager.GetString($"ErrorException_VN")

                };
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Xóa một đối tượng
        /// Created by NVDung (11/7/2022)
        /// </summary>
        /// <param name="employeeId">Mã đối tượng cần xóa</param>
        /// <returns>status code</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var res = await _repository.DeleteAsync(id);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                // Trả về thông tin lỗi
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.Web05.Core.Resources.Resource.ResourceManager.GetString($"ErrorException_VN")

                };
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Sửa một đối tượng
        /// Created by NVDung (11/7/2022)
        /// </summary>
        /// <param name="entity">Đối tượng cần sửa</param>
        /// <returns>status code</returns>
        [HttpPut]
        public async Task<IActionResult> Update(Entity entity)
        {
            try
            {
                var res = await _service.UpdateService(entity);
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
    }
}
