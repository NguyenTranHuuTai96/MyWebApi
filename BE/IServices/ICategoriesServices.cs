using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace IServices
{
    public interface ICategoriesServices
    {
        Task<PagingQuery<Dictionary<string, object>>> GetDataService(QueryModel queryModel);
    }
}
