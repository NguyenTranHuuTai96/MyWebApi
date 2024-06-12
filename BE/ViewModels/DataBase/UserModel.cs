using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.DataBase
{
    public class UserModel : BaseViewModelExtend<int>
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
    }
}
