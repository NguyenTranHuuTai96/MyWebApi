
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence.Context;

namespace Persistence.Configure.ConfigureDbContext
{
    public static class ConfigureDbContext
    {
        public static void RegisterDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<MyDbContext>(options => options.UseSqlServer(configuration["ConnectionStrings:MySQLServerConnection"],
               options => options.MigrationsAssembly(typeof(MyDbContext).Assembly.FullName)));
            service.AddDbContext<MyDbContext>(options => options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));


            service.AddIdentityCore<UserIdentity>().AddRoles<IdentityRole>().AddEntityFrameworkStores<MyDbContext>().AddTokenProvider<DataProtectorTokenProvider<UserIdentity>>(TokenOptions.DefaultProvider);
            service.Configure<IdentityOptions>(config => {
                config.Password.RequireNonAlphanumeric = true;
                config.Password.RequireDigit = true;
                config.Password.RequireLowercase = true;   
                config.Password.RequireUppercase = true;
                config.Password.RequiredLength = 6;
            }); //PasswordValidator


            //service.Configure<DataProtectionTokenProviderOptions>(options =>
            //{
            //    options.TokenLifespan = TimeSpan.FromHours(10);
            //});

        }
    }
}
