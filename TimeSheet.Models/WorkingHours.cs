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
    public class WorkingHours
    {
        [Key]
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        [ValidateNever]
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
        public DateOnly Date { get; set; }
        [Display(Name = "Working Hours")]
        public long Hours { get; set; } = 0;
        public long HoursLeft { get; set; } = 0;
    }
}
