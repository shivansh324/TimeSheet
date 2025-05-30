using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheet.Models.Dto;

namespace TimeSheet.Models.ViewModel
{
    public class TimesheetVM
    {
        public Employee Employee { get; set; }
        public List<Milestone> Milestones { get; set; } = [];
        public List<Timesheet> Timesheets { get; set; } = [];
        public List<ProjectMilestone> Projects { get; set; } = [];
    }
}
