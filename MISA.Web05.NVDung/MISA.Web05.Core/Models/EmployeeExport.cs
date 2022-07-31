using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Core.Models
{
    /// <summary>
    /// // Nhân viên
    /// Created by NVDung (18/7/2022)
    /// </summary>
    public class EmployeeExport
    {
        #region Constructor
        // Constructor
        public EmployeeExport()
        {
        }
        #endregion
        #region Properties
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
        // Số tài khoản ngân hàng
        public string? BankAccountNumber { get; set; }
        // Trạng thái
        public string? Status { get; set; } 
        // Tên ngân hàng
        public string? BankName { get; set; }
        // Chi nhánh ngân hàng
        public string? BankBranchName { get; set; }
        #endregion
        #region Method
        #endregion

    }
}
