using AutoMapper;
using StockManagementSystem.BusinessObject;
using StockManagementSystem.Models;
using CatagoryBo = StockManagementSystem.BusinessObject.Category;
using CatagoryEo = StockManagementSystem.Models.Category;
using CompanyBo =  StockManagementSystem.BusinessObject.Company;
using CompanyEo = StockManagementSystem.Models.Company;
using ItemBo = StockManagementSystem.BusinessObject.Item;
using ItemEo = StockManagementSystem.Models.Item;
namespace StockManagementSystem.Profiles
{
    public class TrainingProfile : Profile
    {
        public TrainingProfile()
        {
            CreateMap<CatagoryBo, CatagoryEo>().ReverseMap();
            CreateMap<CompanyBo, CompanyEo>().ReverseMap();
            CreateMap<ItemBo, ItemEo>().ReverseMap();
        }
    }
}
