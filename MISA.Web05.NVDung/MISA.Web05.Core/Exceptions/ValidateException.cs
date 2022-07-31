using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Core.Exceptions
{
    public class ValidateException : Exception
    {
        public string? ValidateErrorMsg { get; set; }
        public ValidateException(string errorMsg)
        {
            ValidateErrorMsg = errorMsg;
        }
        public override string Message => this.ValidateErrorMsg;
    }
}
