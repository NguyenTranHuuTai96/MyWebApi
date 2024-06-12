using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IUserIdentityRepository
    {
        Task<UserIdentity> CheckLoginUserIdentityEF(string username, string password);
        Task<IdentityResult> CheckAndCreatePassword(UserIdentity user, string password);
        Task<IdentityResult> AddRoleForUser(UserIdentity user);
        Task<string> GenerateEmailConfirmationToken(UserIdentity user);
        Task<UserIdentity> GetObjectUserIdentity(string idUser);
        Task<IdentityResult> ConfirmMail(UserIdentity user, string TokenConfirmMail);
    }
}
