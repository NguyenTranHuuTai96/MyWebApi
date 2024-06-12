using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.DataBase;

namespace IServices
{
    public interface ITokenIdentityServices
    {
        //<string> CreateAccessToken(UserIdentityModel userIdentityModel);
        Task<JwtModel> CreateJwtModel(AccountModel accountModel);
       // Task<string> CreateRefreshToken(UserIdentityModel userIdentityModel);
        Task<JwtModel> ValidateRefreshToken(string refreshToken);
        Task ValidateToken(TokenValidatedContext context);
    }
}
