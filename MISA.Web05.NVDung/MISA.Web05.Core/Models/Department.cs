namespace MISA.Web05.Core.Models
{
    /// <summary>
    /// Phòng ban
    /// Created by NVDung (26/6/2022)
    /// </summary>
    public class Department : BaseClass
    {
        #region Constructor
        // Hàm khởi tạo
        public Department()
        {
            this.DepartmentId = new Guid();
        }
        #endregion
        #region Properties
        // Khóa chính
        public Guid DepartmentId { get; set; }
        // Tên phòng ban
        public string? DepartmentName { get; set; }
        #endregion
        #region Method
        #endregion
    }
}
