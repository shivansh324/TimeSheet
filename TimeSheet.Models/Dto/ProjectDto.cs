using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheet.Models.Dto
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        [Display(Name = "Project Code")]
        public string? ProjectCode { get; set; }
        [Display(Name = "Project Description")]
        public string? ProjectDescription { get; set; }

        [Display(Name = "Project Start Date")]
        public DateOnly StartDate { get; set; }
        [Display(Name = "Project End Date")]
        public DateOnly EndDate { get; set; }
        public List<ProjectMilestoneDto>? ProjectMilestones { get; set; }
    }
}
