using AutoMapper;
using Domain.IRepositories;
using Domain.Optimize;
using IServices;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.DataBase;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services
{
    public class CategoriesServices : ICategoriesServices
    {
        public IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public CategoriesServices(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagingQuery<Dictionary<string, object>>> GetDataService(QueryModel queryModel)
        {
            var DictRs = new List<Dictionary<string, object>>();
            var data = await Task.FromResult(_unitOfWork._categoryRepository.GetAll());
            IQueryable<CategoriesModel>  categoriesModel =(from d in data
                     select new CategoriesModel()
                     {
                         Id = d.Id,
                         Description = d.Description,
                         IsActive = d.IsActive,
                         Name = d.Name??string.Empty,
                     });
            foreach (var item in categoriesModel)
            {
                var objDict = MethodConvert.ToDictionary(item);
                DictRs.Add(objDict);
            }
            return new PagingQuery<Dictionary<string, object>>(queryModel.pq_curpage, queryModel.pq_rpp, DictRs);
        }
    }
}
