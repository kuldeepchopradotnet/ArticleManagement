using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM.Service.AutoMapperService
{
    public class AutoMapperService : IAutoMapperService
    {
        private readonly IMapper _mapper;

        public AutoMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TReturn Map<TReturn, TInput>(TInput obj)
        {
            return _mapper.Map<TReturn>(obj);
        }
    }
}
