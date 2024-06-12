using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        [StringLength(300)]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
