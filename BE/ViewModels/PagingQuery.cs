using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class PagingQuery<Model>
    {
        public int pageCount { get; set; }
        public int pageNumber { get; set; }
        public int total { get; set; }
        public string resultMsg { get; set; }
        public IList<Model> data { get; set; }

        public PagingQuery(int? page, int? pageSize, IList<Model> fullList)
        {
            this.total = fullList?.Count() ?? 0;
            if (this.total > pageSize && page.HasValue && page > 0 && pageSize.HasValue && pageSize > 0)
                this.data = fullList?.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value)?.ToList()?? new List<Model>();
            else
                this.data = fullList ?? new List<Model>();
            this.resultMsg = this.total != 0 ? "Success" : "Nodata";
            if (pageSize.HasValue && pageSize > 0)
                this.pageCount = Convert.ToInt32(Math.Ceiling((decimal)this.total / (decimal)pageSize.Value));
            this.pageNumber = pageCount == 0 ? 0 : pageCount == 1 ? 1 : page ?? 0;
        }
    }
}
