using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.DataBase
{
    public class CategoriesModel : BaseViewModelExtend<int>
    {

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

    }
}
