

using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ViewModels;
using ViewModels.DataBase;

namespace IServices
{
    public  class TokenServices : ITokenServices
    {
        private IConfiguration _configuration;
        public IUserServices _userServices;
        public IUnitOfWork _unitOfWork;
        public IMapper _mapper;

        public TokenServices(IConfiguration configuration, IUserServices userServices, IUnitOfWork unitOfWork, IMapper mapper) {
            _configuration = configuration;
            _userServices = userServices;
            _unitOfWork = unitOfWork;
             _mapper = mapper;
        }
        public async Task<string> CreateTokenOptimize(UserModel userModel)
        {
            string issuer = _configuration["TokenBear:Issuer"] ??string.Empty ;
            string audience = _configuration["TokenBear:Audience"] ?? string.Empty;
            string signatureKey = _configuration["TokenBear:SignatureKey"] ?? string.Empty;
            int tokenExpirationMinutes = int.Parse(_configuration["TokenBear:AccessTokenExpiredByMinutes"] ?? string.Empty);
            DateTime expiresAt = DateTime.Now.AddMinutes(tokenExpirationMinutes);

            var user = _mapper.Map<User>(userModel);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, issuer),
                new Claim(JwtRegisteredClaimNames.Iss, issuer, ClaimValueTypes.String, issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString(), ClaimValueTypes.DateTime, issuer),
                new Claim(JwtRegisteredClaimNames.Aud, audience, ClaimValueTypes.String, issuer),
                new Claim(JwtRegisteredClaimNames.Exp, expiresAt.ToString("yyyy/MM/dd hh:mm:ss"), ClaimValueTypes.String, issuer),
                new Claim(ClaimTypes.Name, user.Username, ClaimValueTypes.String, issuer)
            };
            // claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signatureKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.Now,
                Expires = expiresAt,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var accessToken = tokenHandler.CreateToken(tokenDescriptor);

          

            return await Task.FromResult(tokenHandler.WriteToken(accessToken));
        }

        public async Task<string> CreateRefreshTokenOptimize(UserModel userModel)
        {
            string issuer = _configuration["TokenBear:Issuer"] ?? string.Empty;
            string audience = _configuration["TokenBear:Audience"] ?? string.Empty;
            string signatureKey = _configuration["TokenBear:SignatureKey"] ?? string.Empty;
            int refreshTokenExpirationHours = int.Parse(_configuration["TokenBear:RefreshTokenExpiredByHours"] ?? string.Empty);
            DateTime expiresAt = DateTime.Now.AddHours(refreshTokenExpirationHours);

            string refreshTokenCode = Guid.NewGuid().ToString();
            var user = _mapper.Map<User>(userModel);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, issuer),
                new Claim(JwtRegisteredClaimNames.Iss, issuer, ClaimValueTypes.String, issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString(), ClaimValueTypes.DateTime, issuer),
                new Claim(JwtRegisteredClaimNames.Exp, expiresAt.ToString("yyyy/MM/dd hh:mm:ss"), ClaimValueTypes.String, issuer),
                new Claim(ClaimTypes.SerialNumber, refreshTokenCode, ClaimValueTypes.String, issuer),
                new Claim(ClaimTypes.Name, user.Username, ClaimValueTypes.String, issuer)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signatureKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.Now,
                Expires = expiresAt,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var refreshToken = tokenHandler.CreateToken(tokenDescriptor);

         

            return tokenHandler.WriteToken(refreshToken);
        }


        public async Task ValidateToken(TokenValidatedContext context)
        {
            var claims = context.Principal?.Claims.ToList();

            if (claims?.Count == 0)
            {
                context.Fail("This token contains no information");
                return;
            }

            var identity = context.Principal?.Identity as ClaimsIdentity;

            if (identity?.FindFirst(JwtRegisteredClaimNames.Iss) == null)
            {
                context.Fail("This token is not issued by point entry");
                return;
            }

            if (identity.FindFirst(ClaimTypes.Name) != null)
            {
                string username = identity.FindFirst(ClaimTypes.Name)?.Value??string.Empty;

                var user = await Task.FromResult(_userServices.CheckLogin(username));

                if (user == null)
                {
                    context.Fail("This token is invalid for user");
                    return;
                }
            }
        }

        public async Task<JwtModel> ValidateRefreshToken(string refreshToken)
        {
            try
            {
                var principal = new JwtSecurityTokenHandler().ValidateToken(refreshToken, GetTokenValidationParameters(), out _);

                string username = principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty;


                UserModel userModel = _userServices.CheckLogin(username);
                if (userModel != null) {
                    return new JwtModel
                    {
                        AccessToken = await CreateTokenOptimize(userModel),
                        RefreshToken = await CreateRefreshTokenOptimize(userModel),
                    };
                }    

            }
            catch (SecurityTokenException)
            {
                // Token validation failed
            }

            return new JwtModel();
        }
      

        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenBear:SignatureKey"]??string.Empty)),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }

        public async Task SaveToken(UserTokenModel userTokenModel)
        {
            var userToken = _mapper.Map<UserToken>(userTokenModel);
            await _unitOfWork._tokenRepository.InsertAsync(userToken);
            await _unitOfWork._tokenRepository.CommitAsync();
        }

    }
}
