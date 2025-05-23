using System.ComponentModel.DataAnnotations;

namespace TimeSheet.Models.Dto
{
    public class ProjectMilestoneDto
    {
        public int Id { get; set; }

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
        public List<TimesheetDto>? Timesheets { get; set; }
    }
}