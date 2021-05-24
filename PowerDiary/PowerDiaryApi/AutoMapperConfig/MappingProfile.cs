using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PowerDiaryApi.ViewModels;
using PowerDiaryBusiness.BusinessViewModels;

namespace PowerDiaryApi.AutoMapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VrChat, ChatViewModel>();
            CreateMap<VrHourlyChat, HourlyChatViewModel>();
        }
        
        
    }
}
