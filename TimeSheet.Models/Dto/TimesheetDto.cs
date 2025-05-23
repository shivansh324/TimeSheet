using System.ComponentModel.DataAnnotations;

namespace TimeSheet.Models.Dto
{
    public class TimesheetDto
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        [Display(Name = "Working Hours")]
        public TimeSpan Hours { get; set; } = new TimeSpan(0, 0, 0);
        public string? Remarks { get; set; }
        public bool IsBillable { get; set; }
    }
}