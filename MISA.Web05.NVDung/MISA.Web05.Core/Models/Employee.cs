namespace MISA.Web05.Core.Models
{
    /// <summary>
    /// // Nhân viên
    /// Created by NVDung (26/6/2022)
    /// </summary>
    public class Employee : BaseClass
    {
        #region Constructor
        // Constructor
        public Employee()
        {
            this.EmployeeId = new Guid();
        }
        #endregion
        #region Properties
        // Khóa chính
        public Guid EmployeeId { get; set; }
        // Mã nhân viên
        public string EmployeeCode { get; set; }
        // Tên nhân viên
        public string EmployeeName { get; set; }
        // Giới tính
        public int? Gender { get; set; }
        // Ngày sinh
        public DateTime? DateOfBirth { get; set; }
        // Thuộc phòng ban nào?
        public Guid DepartmentId { get; set; }
        // Chức danh 
        public string? EmployeePosition { get; set; }
        // Số Căn  cước công dân
        public string? IdentityNumber { get; set; }
        // Nơi cấp CCCD
        public string? IdentityPlace { get; set; }
        // Ngày cấp CCCD
        public DateTime? IdentityDate { get; set; }
        // Địa chỉ nhà 
        public string? Address { get; set; }
        // Số điện thoại di động
        public string? PhoneNumber { get; set; }
        // Số điện thoại cố định
        public string? TelephoneNumber { get; set; }
        // Email 
        public string? Email { get; set; }
        // Số tài khoản ngân hàng
        public string? BankAccountNumber { get; set; }
        // Tên ngân hàng
        public string? BankName { get; set; }
        // Chi nhánh ngân hàng
        public string? BankBranchName { get; set; }
        // Tỉnh/TP ngân hàng
        public string? BankProvinceName { get; set; }
        #endregion
        #region Method
        #endregion
    }
}
