using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheet.Models.API_Models
{
    public class Attendance
    {
        public int Code { get; set; }
        public string? Message { get; set; }
        public List<AttendanceDetails>? AttendanceDetails { get; set; }
    }

    public class AttendanceDetails
    {
        public string? Employeecode { get; set; }
        public string? Shiftname { get; set; }
        public string? Shiftdate { get; set; }
        public string? Shiftstartdate { get; set; }
        public string? Shiftenddate { get; set; }
        public string? Actualintime { get; set; }
        public string? Actualouttime { get; set; }
        public string? Attendancestatus { get; set; }
    }
}
