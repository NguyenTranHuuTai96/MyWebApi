using Domain.Entities;
using Domain.IRepositories;
using Persistence.Context;

namespace Persistence.Repository
{
    public class TokenRepository : ITokenRepository
    {
        public MyDbContext _myDbContext;
        public TokenRepository(MyDbContext myDbContext) {
            _myDbContext = myDbContext;
        }
        public  void Insert(UserToken model)
        {
            _myDbContext.UserTokens.Add(model).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        }

        public async Task InsertAsync(UserToken model)
        {
            await _myDbContext.UserTokens.AddAsync(model);
        }
        public async Task CommitAsync()
        {
            await _myDbContext.SaveChangesAsync();
        }

    }
}
