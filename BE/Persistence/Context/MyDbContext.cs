using Domain.Entities;
using Domain.Optimize;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;


namespace Persistence.Context
{
    public class MyDbContext : IdentityDbContext<UserIdentity, IdentityRole, string>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        public MyDbContext(DbContextOptions<MyDbContext> options, IServiceProvider serviceProvider, IConfiguration configuration) : base(options)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }
        public virtual DbSet<Category> Categorys { get; set; }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Seed(modelBuilder);
        }
        private void Seed(ModelBuilder modelBuilder)
        {

            string roleId0 = Guid.NewGuid().ToString();
            string roleId1 = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole {
                Id = roleId0,
                Name = Enum.GetName(typeof(SetRoles), 0),
                NormalizedName = (Enum.GetName(typeof(SetRoles), 0)??string.Empty).ToString().ToUpper(),
            },
            new IdentityRole
            {
                Id = roleId1,
                Name = Enum.GetName(typeof(SetRoles), 1),
                NormalizedName = (Enum.GetName(typeof(SetRoles), 1) ?? string.Empty).ToString().ToUpper(),
            });

            string userId = Guid.NewGuid().ToString();
            using var scope = _serviceProvider.CreateScope();
            var passwordHasherService = scope.ServiceProvider.GetService<PasswordHasher<UserIdentity>>();
            modelBuilder.Entity<UserIdentity>().HasData(
                new UserIdentity
                {
                    Id = userId,
                    UserName = (_configuration.GetSection("DefaultUser:Username")?.Value ?? string.Empty).ToString().ToLower(),
                    NormalizedUserName = (_configuration.GetSection("DefaultUser:Username")?.Value ?? string.Empty).ToString().ToUpper(),
                    Email = (_configuration.GetSection("DefaultUser:Email")?.Value ?? string.Empty).ToString().ToLower(),
                    NormalizedEmail = (_configuration.GetSection("DefaultUser:Email")?.Value ?? string.Empty).ToString().ToUpper(),
                    EmailConfirmed = Convert.ToBoolean( _configuration.GetSection("DefaultUser:EmailConfirmed").Value),
                    PhoneNumberConfirmed = Convert.ToBoolean( _configuration.GetSection("DefaultUser:PhoneNumberConfirmed").Value),
                    TwoFactorEnabled = Convert.ToBoolean( _configuration.GetSection("DefaultUser:TwoFactorEnabled").Value),
                    LockoutEnabled = Convert.ToBoolean( _configuration.GetSection("DefaultUser:LockoutEnabled").Value),
                    AccessFailedCount = Convert.ToInt32(_configuration.GetSection("DefaultUser:AccessFailedCount").Value),
                    PasswordHash = passwordHasherService.HashPassword(new UserIdentity
                    {
                        UserName = (_configuration.GetSection("DefaultUser:Username")?.Value ?? string.Empty).ToString().ToLower(),
                        NormalizedUserName = (_configuration.GetSection("DefaultUser:Username")?.Value ?? string.Empty).ToString().ToUpper(),
                        Email = (_configuration.GetSection("DefaultUser:Email")?.Value ?? string.Empty).ToString().ToUpper(),
                        NormalizedEmail = (_configuration.GetSection("DefaultUser:Email")?.Value ?? string.Empty).ToString().ToUpper(),
                    }, _configuration.GetSection("DefaultUser:Password")?.Value ?? string.Empty).ToString()
                });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId0,
                UserId = userId,
            });
        }
    }
}
