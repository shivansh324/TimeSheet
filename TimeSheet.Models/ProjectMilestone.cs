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
    public class ProjectMilestone
    {
        [Key]
        public int Id { get; set; }

        public int? ProjectId { get; set; }
        [ValidateNever]
        [ForeignKey("ProjectId")]
        public Project? Project { get; set; }

        [Display(Name = "Time Sheet Number")]
        public string? TimeSheetNumber { get; set; }
        [Display(Name = "Time Sheet Line Number")]
        public int TimeSheetLineNumber { get; set; }
        public string? WbsId { get; set; }

        [Display(Name = "Milestone Code")]
        public string? MilestoneCode { get; set; }
        [Display(Name = "Milestone Description")]
        public string? MilestoneDescription { get; set; }
        [Display(Name = "Task Code")]
        public string? TaskCode { get; set; }
        [Display(Name = "Task Description")]
        public string? TaskDescription { get; set; }

        [Display(Name = "Assigned Hours")] //Total Assigned Hours in Ticks
        public long AssignedHours { get; set; } = 0;
        [Display(Name = "Total Working Hours")] //Total Hours Worked in Ticks
        public long TotalWorkingHours { get; set; } = 0;
        [Display(Name = "Pending Working Hours")] //Total Hours Pending in Ticks
        public long PendingWorkingHours { get; set; } = 0;
        public string? Status { get; set; } = "Open";

        [Display(Name = "Rejection Remarks")]
        public string? RejectionRemarks { get; set; }

        [Display(Name = "Milestone Start Date")]
        public DateOnly StartDate { get; set; }

        [Display(Name = "Milestone End Date")]
        public DateOnly EndDate { get; set; }

        public ICollection<Timesheet>? Timesheets { get; set; } = [];
    }
}
