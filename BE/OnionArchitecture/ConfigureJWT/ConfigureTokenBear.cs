

using IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OnionArchitecture.ConfigureJWT
{
    public static class ConfigureTokenBear
    {
        public static void RegisterTokenBear(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(options =>
                    {
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = configuration["TokenBear:Issuer"],
                            ValidateIssuer = false,
                            ValidAudience = configuration["TokenBear:Audience"],
                            ValidateAudience = false,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenBear:SignatureKey"]??string.Empty)),
                            ValidateIssuerSigningKey = true,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero
                        };
                        options.Events = new JwtBearerEvents()
                        {
                            OnTokenValidated = context =>
                            {
                                var tokenHandler = context.HttpContext.RequestServices.GetRequiredService<ITokenIdentityServices>();
                                return tokenHandler.ValidateToken(context);

                            },
                            OnAuthenticationFailed = context =>
                            {
                                // Record log
                                return Task.CompletedTask;
                            },
                            OnMessageReceived = context =>
                            {
                                return Task.CompletedTask;
                            },
                            OnChallenge = context =>
                            {
                                string error = context.ErrorDescription??string.Empty;
                                // Record log

                                return Task.CompletedTask;
                            }
                        };
                    });



        }
    }
}
