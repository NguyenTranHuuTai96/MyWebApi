using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public  class ModelConfirmMail
    {
        public string IdUser { get; set; } = null!;
        public string TokenConfirmMail { get; set; } = null!;
    }
}
