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
    public interface ITokenServices
    {
        Task<string> CreateRefreshTokenOptimize(UserModel user);
        Task<string> CreateTokenOptimize(UserModel user);
        Task SaveToken(UserTokenModel userTokenModel);
        Task<JwtModel> ValidateRefreshToken(string refreshToken);
        Task ValidateToken(TokenValidatedContext context);
    }
}
