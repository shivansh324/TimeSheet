using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TimeSheet.Models
{
    public class DataTableRequest
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public DataTableSearch? Search { get; set; }
        public List<DataTableOrder>? Order { get; set; }
        public int WeekOffset { get; set; }
    }

    public class DataTableSearch
    {
        public string? Value { get; set; }
        public bool Regex { get; set; }
    }
    public class DataTableOrder
    {
        public string ColumnName { get; set; } = "No";
        public string Dir { get; set; } = "asc"; // "asc" or "desc"
    }
}
