using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TimeSheet.Models
{
    public class Project
    {

        [Key]
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        [ValidateNever]
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        [Display(Name = "Project Code")]
        public string? ProjectCode { get; set; }
        [Display(Name = "Project Description")]
        public string? ProjectDescription { get; set; }
        
        [Display(Name = "Project Start Date")]
        public DateOnly StartDate { get; set; }
        [Display(Name = "Project End Date")]
        public DateOnly EndDate { get; set; }

        public ICollection<ProjectMilestone>? ProjectMilestones { get; set; } = [];
    }
}
