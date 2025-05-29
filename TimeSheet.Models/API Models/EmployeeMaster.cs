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
        public List<EmployeeZing> Employees { get; set; }
    }

    public class EmployeeZing
    {
        public string? EmployeeCode { get; set; }
        public string? EmployeeID { get; set; }
        public string? EmployeeName { get; set; }
        public string? Email { get; set; }
        //public Approver? ApproverDetails { get; set; }
    }

    //public class Approver
    //{
    //    public string? ApproverName { get; set; }
    //    public string? ApproverEmployeeCode { get; set; }
    //}
}
