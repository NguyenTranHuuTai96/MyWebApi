using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public MyDbContext _myDbContext;
        public CategoryRepository(MyDbContext myDbContext) {
            _myDbContext = myDbContext;
        }
        public IQueryable<Category> GetAll()
        {
            return _myDbContext.Categorys.AsNoTracking();
        }
    }
}
