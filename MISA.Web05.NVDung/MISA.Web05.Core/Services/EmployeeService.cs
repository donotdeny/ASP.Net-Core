using MISA.Web05.Core.Models;
using MISA.Web05.Core.Interfaces.Repository;
using MISA.Web05.Core.Interfaces.Services;
using MISA.Web05.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace MISA.Web05.Core.Services
{
    /// <summary>
    /// Service xử lý Employee
    /// Created by NVDung (26/6/2022)
    /// </summary>
    public class EmployeeService : BaseServices<Employee>, IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        // Khởi tạo đối tượng, gọi hàm khởi tạo của cha và truyền vào repo
        public EmployeeService(IEmployeeRepository repository):base(repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Nhập khẩu dữ liệu
        /// Created by NVDung (11/7/2022)
        /// </summary>
        /// <param name="fileImport"></param>
        /// <returns>Danh sách nhân viên thêm thành công</returns>
        /// <exception cref="ValidateException"></exception>
        public async Task<IEnumerable<Employee>> Import(IFormFile fileImport)
        {
            if (!Path.GetExtension(fileImport.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                throw new ValidateException("Tệp không đúng định dạng");
            }

            var employees = new List<Employee>();

            using (var stream = new MemoryStream())
            {
                fileImport.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var employeeCode = ConvertToString(worksheet.Cells[row, 2].Value); 
                        var employeeName = ConvertToString(worksheet.Cells[row, 3].Value);
                        var gender = ConvertToString(worksheet.Cells[row, 4].Value); 
                        var dateOfBirth = ConvertToDate(worksheet.Cells[row, 5].Value);
                        var employeePosition = ConvertToString(worksheet.Cells[row, 6].Value);  
                        var departmentName = ConvertToString(worksheet.Cells[row, 7].Value);   
                        var bankAccountNumber = ConvertToString(worksheet.Cells[row, 8].Value);
                        var bankName = ConvertToString(worksheet.Cells[row, 9].Value); 
                        var bankBranchName = ConvertToString(worksheet.Cells[row, 11].Value);
                        employees.Add(new Employee
                        {
                            EmployeeCode = employeeCode,
                            EmployeeName = employeeName,
                            DateOfBirth = dateOfBirth,
                            EmployeePosition = employeePosition,
                            BankAccountNumber = bankAccountNumber,
                            BankName = bankName,
                            BankBranchName = bankBranchName
                        });
                    }
                }
            }
            return employees;
        }

        /// <summary>
        /// Override phương thức Validate của cha
        /// Created by NVDung (11/7/2022)
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>true or false</returns>
        /// <exception cref="ValidateException"></exception>
        protected override bool Validate(Employee employee)
        {
            // Validate dữ liệu
            // 1. Mã nhân viên, tên nhân viên, phòng ban không được phép để trống
            if (string.IsNullOrEmpty(employee.EmployeeCode) ||
                string.IsNullOrEmpty(employee.EmployeeName) ||
                string.IsNullOrEmpty(employee.DepartmentId.ToString()))
            {
                throw new ValidateException("Mã nhân viên, tên nhân viên, phòng ban không được phép để trống!");
            }
            // 2. Ngày sinh, ngày cấp CCCD 
            var toDay = DateTime.Now;
            if (!string.IsNullOrEmpty(employee.DateOfBirth.ToString()) &&
                employee.DateOfBirth > toDay)
            {
                throw new ValidateException("Ngày sinh không hợp lệ!");
            }
            if (!string.IsNullOrEmpty(employee.IdentityDate.ToString()) &&
                employee.IdentityDate > toDay)
            {
                throw new ValidateException("Ngày cấp CCCD không hợp lệ!");
            }
            // Nếu hợp lệ thì gọi yêu cầu thực hiện thêm mới dữ liệu vào CSDL
            return true;
        }
        /// <summary>
        /// Xử lý dữ liệu chuỗi 
        /// Created by NVDung (13/7/2022)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string? ConvertToString(object? value)
        {
            if (value != null)
            {
                return value.ToString();
            }
            else return null;
        }
        /// <summary>
        /// Xử lý ngày tháng
        /// Created by NVDung (13/7/2022)
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Ngày tháng</returns>
        private DateTime? ConvertToDate(object? value)
        {
            value = "2/15/2000";
            DateTime dateConvert;
            if (value != null && DateTime.TryParse(value.ToString(), out dateConvert))
            {
                return dateConvert;
            }
            return null;
        }
        /// <summary>
        /// Service thực hiện xuất khẩu dữ liệu
        /// Created by NVDung (18/7/2022)
        /// </summary>
        /// <returns>MemoryStream</returns>
        public async Task<MemoryStream> ExportAsync()
        {
            // query data from database  
            await Task.Yield();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            var list = await _repository.ExportData();
            var stream = new MemoryStream();
            var length = list.Count;
            var cellLastBorder = "J" + length.ToString();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                for (int i = 1; i <= length; i++)
                {
                    workSheet.Cells[$"A{i + 1}"].Value = i;
                }
                workSheet.Cells[$"B1:{cellLastBorder}"].LoadFromCollection(list, true);
                workSheet.InsertRow(1, 2);
                workSheet.Cells["A1:K1"].Merge = true;
                //Make all text fit the cells
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                workSheet.Cells["A1"].Value = "DANH SÁCH NHÂN VIÊN";
                workSheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells["A3"].Value = "STT";
                workSheet.Cells["B3"].Value = "Mã nhân viên";
                workSheet.Cells["C3"].Value = "Tên nhân viên";
                workSheet.Cells["D3"].Value = "Giới tính";
                workSheet.Cells["E3"].Value = "Ngày sinh";
                workSheet.Cells["F3"].Value = "Phòng ban";
                workSheet.Cells["G3"].Value = "Chức danh";
                workSheet.Cells["H3"].Value = "Số tài khoản";
                workSheet.Cells["I3"].Value = "Trạng thái";
                workSheet.Cells["J3"].Value = "Tên ngân hàng";
                workSheet.Cells["K3"].Value = "Chi nhánh";
                workSheet.Columns[5].Style.Numberformat.Format = "dd/mm/yyyy"; ;
                workSheet.Cells["A3:K3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells["A3:K3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["A3:K3"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ccc"));
                workSheet.Cells["A3:K3"].Style.Font.Bold = true;
                workSheet.Cells["A3:K3"].Style.Font.Size = 10;
                workSheet.Cells["A3:K3"].Style.Font.Name = "Arial";
                workSheet.Cells["A1"].Style.Font.Bold = true;
                workSheet.Cells["A1"].Style.Font.Size = 16;
                workSheet.Cells["A1"].Style.Font.Name = "Arial";
                workSheet.Cells[$"A3:{cellLastBorder}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[$"A3:{cellLastBorder}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[$"A3:{cellLastBorder}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[$"A3:{cellLastBorder}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[$"A3:{cellLastBorder}"].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[$"A3:{cellLastBorder}"].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[$"A3:{cellLastBorder}"].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[$"A3:{cellLastBorder}"].Style.Border.Bottom.Color.SetColor(Color.Black);
                //Autofit with minimum size for the column.
                double minimumSize = 10;
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns(minimumSize);

                //Autofit with minimum and maximum size for the column.
                double maximumSize = 50;
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns(minimumSize, maximumSize);

                //optional use this to make all columms just a bit wider, text would sometimes still overflow after AutoFitColumns().
                for (int col = 1; col <= workSheet.Dimension.End.Column; col++)
                {
                    workSheet.Column(col).Width = workSheet.Column(col).Width + 1;
                }
                package.Save();
            }
            stream.Position = 0;
            //return Ok(list);  
            return stream;
        }
    }
}
