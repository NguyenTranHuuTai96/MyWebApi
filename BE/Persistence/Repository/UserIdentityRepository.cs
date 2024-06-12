using Domain.Entities;
using Domain.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class UserIdentityRepository : IUserIdentityRepository
    {
        MyDbContext _myDbContext;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly PasswordHasher<UserIdentity> _passwordHasher;
        private readonly PasswordValidator<UserIdentity> _passwordValidator;
        public UserIdentityRepository(MyDbContext myDbContext,
            UserManager<UserIdentity> userManager,
             PasswordHasher<UserIdentity> passwordHasher,
              PasswordValidator<UserIdentity> passwordValidator
            ) {
            _myDbContext = myDbContext;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
        }
        
        public async Task<UserIdentity> CheckLoginUserIdentityEF(string username,string password)
        {
            var rsUserIdentityByUserName = await _userManager.FindByNameAsync(username);
            if (rsUserIdentityByUserName == null) return null;
            var checkUserIdentityByPassword = await _userManager.CheckPasswordAsync(rsUserIdentityByUserName, password);
            if (!checkUserIdentityByPassword) return null;
            return rsUserIdentityByUserName;
        }
        public async Task<IdentityResult> CheckAndCreatePassword(UserIdentity user,string password)
        {
            var rsIdentity = new IdentityResult();
            var validPassword = await _passwordValidator.ValidateAsync(_userManager, user, user.PasswordHash);
            if (!validPassword.Succeeded) return rsIdentity;
            user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash ?? string.Empty);
            rsIdentity = await _userManager.CreateAsync(user);
            return rsIdentity;
        }
        public async Task<IdentityResult> AddRoleForUser(UserIdentity user)
        {
            var rsIdentity = new IdentityResult();
            rsIdentity = await _userManager.AddToRoleAsync(user, "User");
            return rsIdentity;
        }
        public async Task<string> GenerateEmailConfirmationToken(UserIdentity user)
        {
            string rs = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return rs;
        }
        public async Task<UserIdentity> GetObjectUserIdentity(string idUser)
        {
            var userIdentity = await _userManager.FindByIdAsync(idUser);
            return userIdentity;

        }
        public async Task<IdentityResult> ConfirmMail(UserIdentity user, string TokenConfirmMail)
        {
            IdentityResult userIdentity = await _userManager.ConfirmEmailAsync(user, TokenConfirmMail);
            return userIdentity;

        }
    }
}
