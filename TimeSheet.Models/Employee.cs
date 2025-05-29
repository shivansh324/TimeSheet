using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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

        public string Role { get; set; } = "Employee";

        [JsonIgnore]
        [Required]
        public string? Password { get; set; }

        public string? Status { get; set; } = "Active"; //Active, Inactive

        public ICollection<WorkingHours>? WorkingHours { get; set; } = [];
        public ICollection<Timesheet>? Timesheets { get; set; } = [];

    }
}
