using System;
using System.Collections.Generic;
using System.Text;

namespace AM.Domain.ViewModel
{
    public class FilterModel
    {
        public int Draw { get; set; } = 1;
        public int Start { get; set; } = 0;
        public string SortColumn { get; set; } = "Id";
        public string SortColumnAscDesc { get; set; } = "asc";
        public string SearchValue { get; set; } = "";
        public int Length { get; set; } = 2147483647;
    }
}
