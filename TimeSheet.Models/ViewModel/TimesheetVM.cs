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
        public List<Timesheet> Timesheets { get; set; } = [];

        public List<ProjectDto> Projects { get; set; } = [];
    }
}
