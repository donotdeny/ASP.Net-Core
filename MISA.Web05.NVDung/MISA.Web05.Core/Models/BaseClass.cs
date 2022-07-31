namespace MISA.Web05.Core.Models
{
    /// <summary>
    /// Class với các prop dùng chung
    /// Created by NVDung (26/6/2022)
    /// </summary>
    public class BaseClass
    {
        // Ngày tạo
        public DateTime? CreatedDate { get; set; }
        // Người tạo
        public string? CreatedBy { get; set; }
        // Ngày chỉnh sửa
        public DateTime? ModifiedDate { get; set; }
        // Người chỉnh sửa
        public string? ModifiedBy { get; set; }
    }
}
