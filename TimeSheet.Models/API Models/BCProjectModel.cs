using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TimeSheet.Models.API_Models
{
    public class BCProjectModel
    {
        [JsonPropertyName("@odata.context")]
        public string? ODataContext { get; set; }
        public List<BCProject>? value { get; set; }
        [JsonPropertyName("@odata.nextLink")]
        public string? ODataNextLink { get; set; }
    }

    public class BCProject
    {
        public string? Time_Sheet_No { get; set; }
        public int Time_Sheet_Line_No { get; set; }
        public string? employeeCode { get; set; }
        public string? employeeName { get; set; }
        public string? projectCode { get; set; }
        public string? projectName { get; set; }
        public string? milestone { get; set; }
        public string? milestoneDescription { get; set; }
        public string? ProjectStartDate { get; set; }
        public string? ProjectEndDate { get; set; }
        public string? MileStonStartDt { get; set; }
        public string? MileStonEndDt { get; set; }
        public string? wbsId { get; set; }
        public int totalDaysAssigned { get; set; }
        public string? taskCode { get; set; }
        public string? taskDescription { get; set; }
    }
}
