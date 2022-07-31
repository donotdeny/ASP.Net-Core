using MISA.Web05.Core.Exceptions;
using MISA.Web05.Core.Interfaces.Repository;
using MISA.Web05.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Core.Services
{
    /// <summary>
    /// Service implement Interface Services
    /// Created By NVDung (26/6/2022)
    /// </summary>
    public class BaseServices<Entity> : IBaseServices<Entity>
    {
        private readonly IBaseRepository<Entity> _repository;
        /// <summary>
        /// Created by NVDung (11/7/2022)
        /// Hàm khởi tạo 
        /// </summary>
        /// <param name="repository"></param>
        public BaseServices(IBaseRepository<Entity> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Service thực hiện chức năng thêm đối tượng
        /// Created by NVDung (11/7/2022)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ValidateException"></exception>
        public async Task<int> InsertService(Entity entity)
        {
            // Validate dữ liệu
            var isValid = Validate(entity);
            // Thêm mới
            if(isValid == true)
            {
                var res = await _repository.InsertAsync(entity);
                return res;
            }
            else
            {
                throw new ValidateException("Có lỗi xảy ra!");
            }
        }
        /// <summary>
        /// Validate đối tượng
        /// Created by NVDung (11/7/2022)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true</returns>
        protected virtual bool Validate(Entity entity)
        {
            return true;
        }

        /// <summary>
        /// Service thực hiện chức năng sửa đối tượng
        /// Created by NVDung (11/7/2022)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ValidateException"></exception>
        public async Task<int> UpdateService(Entity entity)
        {
            // Validate dữ liệu
            var isValid = Validate(entity);
            // Sửa
            if (isValid == true)
            {
                var res = await _repository.UpdateAsync(entity);
                return res;
            }
            else
            {
                throw new ValidateException("Có lỗi xảy ra!");
            }
        }
    }
}
