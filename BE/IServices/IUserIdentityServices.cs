using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.DataBase;

namespace IServices
{
    public interface IUserIdentityServices
    {
        Task<IdentityResult> AddRoles(UserIdentityModel userIdentityModel);
       // Task<UserIdentityModel> CheckLoginUserIdentityServices(string username, string password);
        Task<IdentityResult> RegisterUserIdentityServices(UserIdentityModel userIdentityModel);
        Task SendMailForUserConfirm(UserIdentityModel userIdentityModel, string url, CancellationToken cancellationToken);
        Task<ModelConfirmMail> SetDataModelConfirmMail(UserIdentityModel userIdentityModel);
        Task<UserIdentityModel> GetObjectByID(string userId);
        Task<IdentityResult> UpdateConfirmMail(UserIdentityModel userIdentityModel, string TokenConfirmMail);
        //Task<string> TokenMailConfirm(UserIdentityModel userIdentityModel);
    }
}
