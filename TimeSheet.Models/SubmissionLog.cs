using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TimeSheet.Models
{
    public class SubmissionLog
    {
        [Key]
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        [ValidateNever]
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
        public DateOnly TimesheetDate { get; set; }
        public long Hours { get; set; }
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
        public string? RejectionRemarks { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected
        public DateTime? ApprovedDate { get; set; }

        public int? ApproverId { get; set; }
        [ValidateNever]
        [ForeignKey("ApproverId")]
        public Employee? Approver { get; set; }

        public bool IsClosed { get; set; } = false; //either closed or not after rejection, default is false
    } 
}
