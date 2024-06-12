using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class ProductRepository : IProductRepository
    {
        public MyDbContext _myDbContext;
        public ProductRepository(MyDbContext myDbContext) {
            _myDbContext = myDbContext;
        }
        public void Delete(Product model)
        {
            _myDbContext.Products.Add(model).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public IQueryable<Product> GetAll()
        {
            return _myDbContext.Products.AsNoTracking();
        }

        public Product GetById(int id)
        {
            return _myDbContext.Products.AsNoTracking().FirstOrDefault(x => x.Id == id)??new Product();
        }

        public void Insert(Product model)
        {
            _myDbContext.Products.Add(model).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        }

        public void UpDate(Product model)
        {
            _myDbContext.Products.Add(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public Product isExist(int id)
        {
            var query = _myDbContext.Products.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return query;
        }
        public void Commit() {
            _myDbContext.SaveChanges();
        }
    }
}
