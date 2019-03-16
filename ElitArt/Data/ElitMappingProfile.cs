using AutoMapper;
using ElitArt.Data.Entities;
using ElitArt.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElitArt.Data
{
    public class ElitMappingProfile : Profile 
    {
        public ElitMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(o=>o.OrderId,ex=>ex.MapFrom(o=>o.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap();
        }
    }
}
