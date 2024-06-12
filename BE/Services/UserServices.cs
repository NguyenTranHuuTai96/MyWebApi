using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using FluentValidation;
using IServices;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.DataBase;

namespace Services
{
    public class UserServices : IUserServices
    {
        //public IUserRepository _userepository;
        public IMapper _mapper;
        public IUnitOfWork _unitOfWork;

        public UserServices(
            IMapper mapper,
            IUnitOfWork unitOfWork

            )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
     
        }
        public UserModel CheckLogin(string Username, string Password)
        {
            var data = _unitOfWork._userRepository.GetEntityUser(Username,Password);
            return _mapper.Map<UserModel>(data);
          
        }
        public UserModel CheckLogin(string Username)
        {
            var data = _unitOfWork._userRepository.GetEntityUser(Username);
            return _mapper.Map<UserModel>(data);

        }
 
    }
}
