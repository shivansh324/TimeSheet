using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheet.Models.API_Models
{
    public class EmployeeMaster
    {
        public int Code { get; set; }
        public string? Message { get; set; }
        public string? EmployeesCount { get; set; }
        public string? TotalEmployeeCount { get; set; }
        public List<EmployeeZing>? EmployeesList { get; set; }
    }

    public class EmployeeZing
    {
        public string? EmployeeCode { get; set; }
        public string? EmployeeID { get; set; }
        public string? EmployeeName { get; set; }
        public string? Email { get; set; }
        public string? RoleName { get; set; }
    }
}
