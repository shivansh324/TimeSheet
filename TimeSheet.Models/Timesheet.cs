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
    public class Timesheet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int? EmployeeId { get; set; }
        [ValidateNever]
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        public int? MilestoneId { get; set; } //Non-tech Milestones
        [ValidateNever]
        [ForeignKey("MilestoneId")]
        public Milestone? Milestone { get; set; }

        public int? ProjectMilestoneId { get; set; } //Project Assigned to the Employee
        [ValidateNever]
        [ForeignKey("ProjectMilestoneId")]
        public ProjectMilestone? ProjectMilestone { get; set; }

        public DateOnly Date { get; set; }
        [Display(Name = "Working Hours")]
        public long Hours { get; set; } = 0;
        public string? Remarks { get; set; }
        public bool IsBillable { get; set; }
    }
}
