using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_DataAccess;
using Tangy_Models;
using Tangy_Models.ViewModel;

namespace Tangy_Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<ProductPriceDTO, ProductPrice>().ReverseMap();
            CreateMap<OrderHeaderDTO, OrderHeader>().ReverseMap();
            CreateMap<OrderDetailDTO, OrderDetail>().ReverseMap();
            CreateMap<OrderDTO, Order>().ReverseMap();
        }
    }
}
