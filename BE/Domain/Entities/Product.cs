using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        [StringLength(1000)]
        public string Name { get; set; } = null!;

        public int? QuatityPerUnit { get; set; }
        public double? UnitPrice { get; set; }
        public int? UnitInStock { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UnitOnOrder { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;
    }
}
