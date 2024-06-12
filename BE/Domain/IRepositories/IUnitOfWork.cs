using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IUnitOfWork
    {
        IProductRepository _productRepository { get; }
        ITokenRepository _tokenRepository { get;  }
        IUserRepository _userRepository { get;  }
        IUserIdentityRepository _userIdentityRepository { get; }

        void Commit();
        Task CommitAsync();
    }
}
