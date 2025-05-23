using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TimeSheet.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? EmployeeCode { get; set; }
        [Required]
        public string? EmployeeID { get; set; } //Zing Id
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; } = 15; //Default to 15 (Project Team Members)
        [ForeignKey("DepartmentId")]
        [ValidateNever]
        public Department? Department { get; set; }        
        
        public int? ApproverId { get; set; }
        [ForeignKey("ApproverId")]
        [ValidateNever]
        public Employee? Approver { get; set; }

        public ICollection<WorkingHours>? WorkingHours { get; set; } = [];
        public ICollection<Timesheet>? Timesheets { get; set; } = [];

    }
}
