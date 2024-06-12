using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.DataBase;

namespace IServices
{
    public interface IUserServices
    {
        UserModel CheckLogin(string Username, string Password);
       UserModel CheckLogin(string Username);
    }
}
