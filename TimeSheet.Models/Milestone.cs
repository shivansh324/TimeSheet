using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TimeSheet.Models
{
    public class Milestone
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public int? DepartmentId { get; set; }
        [ValidateNever]
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
        public ICollection<Timesheet>? Timesheets { get; set; } = [];
    }
}
