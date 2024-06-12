using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IProductRepository
    {
        void Commit();
        void Delete(Product model);
        IQueryable<Product> GetAll();
        Product GetById(int id);
        void Insert(Product model);
        void UpDate(Product model);
        Product isExist(int id);
    }
}
