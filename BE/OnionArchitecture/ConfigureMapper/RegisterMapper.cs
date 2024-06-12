
using AutoMapper;
using Domain.Entities;
using ViewModels.DataBase;
using static Dapper.SqlMapper;

namespace OnionArchitecture.ConfigureMapper
{
    public class RegisterMapper : Profile
    { 
        public RegisterMapper() {
            CreateMap<Product, ProductsModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<UserToken, UserTokenModel>().ReverseMap();
            CreateMap<UserIdentity, UserIdentityModel>()
            .ForMember(identity =>
            identity.Password, y => y.MapFrom(identityModel => identityModel.PasswordHash)).ReverseMap();
        }
        
    }
}
