using System;
using System.Collections.Generic;
using System.Text;

namespace AM.Service.AutoMapperService
{
    public interface IAutoMapperService
    {
        TReturn Map<TReturn, TInput>(TInput obj);
    }
}
