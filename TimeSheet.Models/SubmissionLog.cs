using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheet.Models
{
    public class SubmissionLog
    {
        [Key]
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public DateOnly TimesheetDate { get; set; }
        public long Hours { get; set; }
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
        public string? RejectionRemarks { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected
        public DateTime? ApprovedDate { get; set; }
    }
}
