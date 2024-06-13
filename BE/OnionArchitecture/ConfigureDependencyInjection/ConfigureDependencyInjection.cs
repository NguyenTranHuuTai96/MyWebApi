

using Domain.Entities;
using Domain.IRepositories;
using FluentValidation;
using IServices;
using Microsoft.AspNetCore.Identity;
using Persistence.BaseDapper.Dapper;
using Persistence.BaseDapper.IDapper;
using Persistence.Repository;
using Services;
using ViewModels;

namespace OnionArchitecture.ConfigureDependencyInjection
{
    public static class ConfigureDependencyInjection
    {
        public static void AddRegisterDependencyInjection(this IServiceCollection service)
        {
            //DI Application Repo
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<ITokenRepository, TokenRepository>();
            service.AddScoped<IUserIdentityRepository, UserIdentityRepository>();
   

            //DI Application Services
            service.AddScoped<IProductServices, ProductServices>();
            service.AddScoped<ICategoriesServices, CategoriesServices>();
            service.AddScoped<IUserServices, UserServices>();
            service.AddScoped<ITokenServices, TokenServices>();
            service.AddScoped<IUserIdentityServices, UserIdentityServices>();
            service.AddScoped<ITokenIdentityServices, TokenIdentityServices>();
            service.AddScoped<IEmailExtend, EmailExtend>();
            
            //DI Unit of work
            service.AddScoped<IUnitOfWork, UnitOfWork>();

            //DI fluent Valid model
            service.AddScoped<IValidator<AccountModel>, AccountModelValidator>();

            //DI Identity User
            service.AddScoped<PasswordHasher<UserIdentity>>();
            service.AddScoped<PasswordValidator<UserIdentity>>();
            service.AddScoped<UserManager<UserIdentity>>();

            //DI Dapper
            service.AddSingleton<IDapperHelper, DapperHelper>();
        }

    }
}
