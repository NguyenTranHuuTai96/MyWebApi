using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.DataBase;

namespace ViewModels.DataBase
{
    public class UserTokenModel : BaseViewModelExtend<int>
    {
        //public int UserId { get; set; }
        //public string AccessToken { get; set; }
        //public DateTime ExpiredDateAccessToken { get; set; }
        //[StringLength(50)]
        //public string CodeRefreshToken { get; set; }
        //public string RefreshToken { get; set; }
        //public DateTime ExpiredDateRefreshToken { get; set; }
        //public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }
        public string AccessToken { get; set; } = null!;
        public DateTime? ExpiredDateAccessToken { get; set; }
        [StringLength(50)]
        public string? CodeRefreshToken { get; set; }
        public string RefreshToken { get; set; } = null!;
        public DateTime? ExpiredDateRefreshToken { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
