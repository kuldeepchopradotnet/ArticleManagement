using AM.Data;
using AM.Data.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM.Service.AutoMapperService
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            ConfigureMappings();
        }

        private void ConfigureMappings()
        {
            CreateMap<ArticleModel, ArticleDto>().ReverseMap();
        }

    }
}
