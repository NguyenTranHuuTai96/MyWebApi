using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.DataBase
{
    public class ProductsModel : BaseViewModelExtend<int>
    {
        public string Name { get; set; } = null!;

        public int? QuatityPerUnit { get; set; }
        public double? UnitPrice { get; set; }
        public int? UnitInStock { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UnitOnOrder { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string? StringDate { get; set; }

    }
}
