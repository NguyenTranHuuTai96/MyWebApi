using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.DataBase;

namespace Services
{
    public class TokenIdentityServices : ITokenIdentityServices
    {
        private IConfiguration _configuration;
        public IUserIdentityServices _userIdentityServices;
        public IUnitOfWork _unitOfWork;
        public IMapper _mapper;
        private readonly UserManager<UserIdentity> _userManager;
        public TokenIdentityServices(IConfiguration configuration, IUserIdentityServices userIdentityServices, IUnitOfWork unitOfWork, IMapper mapper, UserManager<UserIdentity> userManager) {
            _configuration = configuration;
            _userIdentityServices = userIdentityServices;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<string> CreateAccessToken(UserIdentity dataUserIdentity)
        {
            string issuer = _configuration["TokenBear:Issuer"] ?? string.Empty;
            string audience = _configuration["TokenBear:Audience"] ?? string.Empty;
            string signatureKey = _configuration["TokenBear:SignatureKey"] ?? string.Empty;
            int tokenExpirationMinutes = int.Parse(_configuration["TokenBear:AccessTokenExpiredByMinutes"] ?? string.Empty);
            DateTime expiresAt = DateTime.Now.AddMinutes(tokenExpirationMinutes);

            IList<string> roles = await _userManager.GetRolesAsync(dataUserIdentity);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, issuer),
                new Claim(JwtRegisteredClaimNames.Iss, issuer, ClaimValueTypes.String, issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString(), ClaimValueTypes.DateTime, issuer),
                new Claim(JwtRegisteredClaimNames.Aud, audience, ClaimValueTypes.String, issuer),
                new Claim(JwtRegisteredClaimNames.Exp, expiresAt.ToString("yyyy/MM/dd hh:mm:ss"), ClaimValueTypes.String, issuer),
                new Claim(ClaimTypes.Name, dataUserIdentity.UserName??string.Empty, ClaimValueTypes.String, issuer)
            };
             claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signatureKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.Now,
                Expires = expiresAt,
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken accessToken = tokenHandler.CreateToken(tokenDescriptor);
          

            var dataUserTokens = await _userManager.SetAuthenticationTokenAsync(dataUserIdentity, "AccessTokenProvider", "AccessToken", tokenHandler.WriteToken(accessToken));
            if (!dataUserTokens.Succeeded) return string.Empty;
            return tokenHandler.WriteToken(accessToken);
        }

        public async Task<string> CreateRefreshToken(UserIdentity dataUserIdentity)
        {
            string issuer = _configuration["TokenBear:Issuer"] ?? string.Empty;
            string audience = _configuration["TokenBear:Audience"] ?? string.Empty;
            string signatureKey = _configuration["TokenBear:SignatureKey"] ?? string.Empty;
            int refreshTokenExpirationHours = int.Parse(_configuration["TokenBear:RefreshTokenExpiredByHours"] ?? string.Empty);
            DateTime expiresAt = DateTime.Now.AddHours(refreshTokenExpirationHours);
        
            string refreshTokenCode = Guid.NewGuid().ToString();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, issuer),
                new Claim(JwtRegisteredClaimNames.Iss, issuer, ClaimValueTypes.String, issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString(), ClaimValueTypes.DateTime, issuer),
                new Claim(JwtRegisteredClaimNames.Exp, expiresAt.ToString("yyyy/MM/dd hh:mm:ss"), ClaimValueTypes.String, issuer),
                new Claim(ClaimTypes.SerialNumber, refreshTokenCode, ClaimValueTypes.String, issuer),
                new Claim(ClaimTypes.Name, dataUserIdentity.UserName??string.Empty, ClaimValueTypes.String, issuer)
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

            var dataUserTokens = await _userManager.SetAuthenticationTokenAsync(dataUserIdentity, "RefreshTokenProvider", "RefreshToken", tokenHandler.WriteToken(refreshToken));
            if (!dataUserTokens.Succeeded) return string.Empty;
            return tokenHandler.WriteToken(refreshToken);
        }
        public async Task<JwtModel> CreateJwtModel(AccountModel accountModel)
        {
            var dataUserIdentity = await _unitOfWork._userIdentityRepository.CheckLoginUserIdentityEF(accountModel.UserName, accountModel.Password);
            if (dataUserIdentity is null) return null;
            if (!dataUserIdentity.EmailConfirmed) return null;
            var accessToken = await CreateAccessToken(dataUserIdentity);
            var refreshToken = await CreateRefreshToken(dataUserIdentity);
            var jwtModel = new JwtModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return jwtModel;

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

            if (identity.FindFirst("Username") != null)
            {
                string username = identity?.FindFirst("Username")?.Value??string.Empty;

                var user = await _userManager.FindByNameAsync(username);

                if (user == null)
                {
                    context.Fail("This token is invalid for user");
                    return;
                }
            }

        }
        public async Task<JwtModel> ValidateRefreshToken(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(refreshToken, GetTokenValidationParameters(), out _);

                string username = principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value??string.Empty;

                var user = await _userManager.FindByNameAsync(username);

                if (user != null)
                {
                    var existingAccessToken = await _userManager.GetAuthenticationTokenAsync(user, "AccessTokenProvider", "AccessToken");

                    if (!string.IsNullOrEmpty(existingAccessToken))
                    {
                      //  var userIdentityModel = _mapper.Map<UserIdentityModel>(user);
                        string newAccessToken = await CreateAccessToken(user);
                        string newRefreshToken = await CreateRefreshToken(user);

                        return new JwtModel
                        {
                            AccessToken = newAccessToken,
                            RefreshToken = newRefreshToken
                        };
                    }
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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenBear:SignatureKey"] ?? string.Empty)),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
