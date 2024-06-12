using Domain.Entities;
using Domain.IRepositories;
using Microsoft.AspNetCore.Identity;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        MyDbContext _myDbContext;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly PasswordHasher<UserIdentity> _passwordHasher;
        private readonly PasswordValidator<UserIdentity> _passwordValidator;
        public IProductRepository _productRepository { get; private set; }
        public IUserRepository _userRepository { get; private set; }
        public ITokenRepository _tokenRepository { get; private set; }
        public IUserIdentityRepository _userIdentityRepository { get; private set; }

        private bool _disposedValue;

        public UnitOfWork(MyDbContext myDbContext,
               UserManager<UserIdentity> userManager,
             PasswordHasher<UserIdentity> passwordHasher,
             PasswordValidator<UserIdentity> passwordValidator,
             IProductRepository productRepository,
             IUserRepository userRepository,
             ITokenRepository tokenRepository,
             IUserIdentityRepository userIdentityRepository
             )
        {
            _myDbContext = myDbContext;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;

            //this._userRepository = _userRepository ??= new UserRepository(_myDbContext);
            //this._productRepository = _productRepository ??= new ProductRepository(_myDbContext);
            //this._tokenRepository = _tokenRepository ??= new TokenRepository(_myDbContext);
            //this._userIdentityRepository = _userIdentityRepository ??= new UserIdentityRepository(_myDbContext, _userManager, _passwordHasher, _passwordValidator);
            _productRepository = _productRepository ?? productRepository;
            _userRepository = _userRepository ?? userRepository;
            _tokenRepository = _tokenRepository ?? tokenRepository;
            _userIdentityRepository = _userIdentityRepository ?? userIdentityRepository;

        }

        public async Task CommitAsync()
        {
              await _myDbContext.SaveChangesAsync();
        }
        public void  Commit()
        {
             _myDbContext.SaveChanges();
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _myDbContext.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
