using AutoMapper;
using GrpcService2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcService2.Infrastructure.ViewModel.Category;
using GrpcService2.Infrastructure.ViewModel.Product;
using GrpcService2.Infrastructure.ViewModel.Detail;

namespace GrpcService2.Domain.Extensions
{
    public class MapperInitializer:Profile
    {
        public MapperInitializer()
        {
            CreateMap<Category, CategoryReponse>().ReverseMap();
            CreateMap<Category, CategoryResquest>().ReverseMap();
            CreateMap<Product, ProductReponse>().ReverseMap();
            CreateMap<Product, ProductResquest>().ReverseMap();
            CreateMap<Detail, DetailReponse>().ReverseMap();
            CreateMap<Detail, DetailResquest>().ReverseMap();
        }
    }
}
