using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using Domain.Optimize;
using IServices;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ViewModels.DataBase;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Services
{
    public class ProductServices : IProductServices
    {
        //public IProductRepository _productRepository;
        public IUnitOfWork _unitOfWork;
        private IMapper _mapper;
     
        public ProductServices(IUnitOfWork unitOfWork,
            IMapper mapper  
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<ProductsModel>> GetDataService()
        {
            var data = await Task.FromResult(_unitOfWork._productRepository.GetAll());
            var rs = from d in data
                       select new ProductsModel()
                       {
                          Id = d.Id,
                          Name = d.Name,
                          CategoryId = d.CategoryId,
                          QuatityPerUnit = d.QuatityPerUnit,
                          UnitInStock = d.UnitInStock,
                          UnitOnOrder = d.UnitOnOrder,
                          UnitPrice = d.UnitPrice,
                          CreatedDate = d.CreatedDate,
                          IsActive = d.IsActive,
                          StringDate = MethodConvert.ConvertDateToStringEight(d.CreatedDate),
                       };
            return rs.ToList();
        }
        public  ProductsModel GetDataByIdService(int id)
        {
            var data = _unitOfWork._productRepository.GetById(id);
            var rs = _mapper.Map<ProductsModel>(data);
            rs.StringDate = MethodConvert.ConvertDateToStringEight(data.CreatedDate);
            return rs;

        }
        public UpdateResultExtend<int> SaveProductByIdService(ProductsModel model)
        {
            var objToSave = _mapper.Map<Product>(model);
            objToSave.CreatedDate = MethodConvert.ConvertStingEightToDate(model.StringDate);
            var isExist = _unitOfWork._productRepository.isExist(model.Id);
            //try
            //{
                switch (model.Working)
                {
                    case WorkingStatus.AddNew:
                        if (isExist != null)
                        {
                            return new UpdateResultExtend<int> { UpdateStatus = SaveStatus.Fail, Message = "ID Product is Exist" };
                        }
                        _unitOfWork._productRepository.Insert(objToSave);
                        break;
                    case WorkingStatus.Update:
                        if (isExist == null)
                        {
                            return new UpdateResultExtend<int> { UpdateStatus = SaveStatus.Fail, Message = "ID Product is not Exist" };
                        }
                        _unitOfWork._productRepository.UpDate(objToSave);
                        break;
                    case WorkingStatus.Delete:
                        if (isExist == null)
                        {
                            return new UpdateResultExtend<int> { UpdateStatus = SaveStatus.Fail, Message = "ID Product is not Exist" };
                        }
                        _unitOfWork._productRepository.Delete(objToSave);
                        break;
                }
            //  Db contexr Repo diff
            _unitOfWork.Commit();
                return new UpdateResultExtend<int>() { UpdateStatus = SaveStatus.Success, Message = "OK" };
            //}
            //catch (Exception ex) {
            //    return new UpdateResultExtend<int>() { UpdateStatus = SaveStatus.Fail, Message = ex.Message };
            //}
 
        }
        public UpdateResultExtend<int> SaveListProduct(IList<ProductsModel> models)
        {
            var savesResult = new List<UpdateResultExtend<long>>();
            try
            {
                foreach (var obj in models)
            {
                var EfProduct = _mapper.Map<Product>(obj);
                EfProduct.CreatedDate = MethodConvert.ConvertStingEightToDate(obj.StringDate??string.Empty);
                var objSave = new UpdateResultExtend<long>();              
                var isExist = _unitOfWork._productRepository.isExist(EfProduct.Id);
          
                    switch (obj.Working)
                    {
                        case WorkingStatus.AddNew:
                            if (isExist != null)
                            {
                                return new UpdateResultExtend<int> { UpdateStatus = SaveStatus.Fail, Message = "ID Product is Exist" };
                            }
                            _unitOfWork._productRepository.Insert(EfProduct);
                            break;
                        case WorkingStatus.Update:
                            if (isExist == null)
                            {
                                return new UpdateResultExtend<int> { UpdateStatus = SaveStatus.Fail, Message = "ID Product is not Exist" };
                                
                            }
                            _unitOfWork._productRepository.UpDate(EfProduct);
                            break;
                        case WorkingStatus.Delete:
                            if (isExist == null)
                            {
                                return new UpdateResultExtend<int> { UpdateStatus = SaveStatus.Fail, Message = "ID Product is not Exist" };
                            }
                            _unitOfWork._productRepository.Delete(EfProduct);
                            break;
                    }
    
            }
                _unitOfWork._productRepository.Commit();
                return new UpdateResultExtend<int>() { UpdateStatus = SaveStatus.Success, Message = "OK" };

            }
            catch (Exception ex)
            {
                return new UpdateResultExtend<int>() { UpdateStatus = SaveStatus.Fail, Message = ex.Message };

            }

        }
        //public async Task<string> GetProductNameByIdAsync(int id)
        //{
        //    string resultCache = await _distributedCacheService.Get<string>($"cache_product_{id}");

        //    if (!string.IsNullOrEmpty(resultCache))
        //    {
        //        return resultCache;
        //    }

        //    string name = _unitOfWork._productRepository.GetById(id)?.Name ?? string.Empty;

        //    await _distributedCacheService.Set($"cache_product_{id}", name);

        //    return name;
        //}
        public async Task<List<string>> GetProductWithCancel(CancellationToken cancellation)
        {
            List<string> ls = new();
            try
            {
                var pd = await Task.FromResult(_unitOfWork._productRepository.GetAll().ToList());

                foreach (var item in pd)
                {
                    if (cancellation.IsCancellationRequested) //CancellationTokenSource
                        cancellation.ThrowIfCancellationRequested();

                    ls.Add(item.Name);

                    Thread.Sleep(3000);
                }
            }
            catch (Exception )
            {
                //record log
                throw;
            }

            return ls;
        }

    }
}
