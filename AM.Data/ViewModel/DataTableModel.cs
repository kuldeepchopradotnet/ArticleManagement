using System;
using System.Collections.Generic;
using System.Text;

namespace AM.Domain.ViewModel
{
    public class DataTableModel<T>
    {
        public int Draw { get; set; }
        public T Data { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
    }
}
