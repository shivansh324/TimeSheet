using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheet.Models.API_Models
{
    public class BCPostModelList
    {
        public string? timeSheetNo { get; set; }
        public List<BCPostModel>? timeSheetLines { get; set; }
    }

    public class BCPostModel
    {
        public string? timeSheetNo { get; set; }
        public int timeSheetLineNo { get; set; }
        public string? date { get; set; }
        public double workingHours { get; set; }
        public string? remarks { get; set; }

        public string? TaskCode { get; set; }
        public string? WBSId { get; set; }
        public string? EmployeeCode { get; set; }
        public string? ProjectCode { get; set; }
        public string? Milestone { get; set; } //milestone code

    }
}
