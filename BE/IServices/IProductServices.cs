using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.DataBase;

namespace IServices
{
    public interface IProductServices
    {
        ProductsModel GetDataByIdService(int id);
        Task<List<ProductsModel>> GetDataService();
        Task<List<string>> GetProductWithCancel(CancellationToken cancellation);
        UpdateResultExtend<int> SaveListProduct(IList<ProductsModel> models);
        UpdateResultExtend<int> SaveProductByIdService(ProductsModel model);
    }
}
