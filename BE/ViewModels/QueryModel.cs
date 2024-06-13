using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class QueryModel
    {
        public int? pq_curpage { get; set; }        // Current Page
        public int? pq_rpp { get; set; }        // Page size 
        public string? pq_filter { get; set; }       // filter term
    }
}
