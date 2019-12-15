using AM.Domain.ViewModel;
using AM.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleManagement.Controllers.BaseController
{
    public abstract class BaseControllers : Controller
    {
        protected readonly ILoggerService _logService;
        public BaseControllers(ILoggerService logService)
        {
            _logService = logService;
        }
        protected JsonResult JsonResult<T>(T data, bool status, bool ignorProp = false, bool dataTable = false)
        {
            JsonResult response = null;
            if (!status)
            {
                // var exception = Convert.ToString(data);
                // convert data obj to json string then pass to logdebug
                _logService.LogDebug(data);
                //logs
            }
            if (dataTable)
            {
                response = new JsonResult(data);
            }
            else
            {
                response = new JsonResult(new { Data = data, Status = status });
            }
            if (ignorProp)
            {
                //ignor property contain null
                response.SerializerSettings = IgnorNullProp();
                //CamelCasePropertyNamesContractResolver use to send respose in camcel case
                response.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }
            return response;
        }

        protected JsonResult JsonResult(object data, bool status, bool ignorProp = false)
        {
            if (!status)
            {
                //  _logService.LogDebug(data);
                //logs
            }
            if (ignorProp)
            {
                return new JsonResult(data, IgnorNullProp());
            }
            return new JsonResult(data, IgnorNullProp());
        }

        protected FilterModel FilterFormData()
        {
            FilterModel filterModel = new FilterModel();

            try
            {
                if (Request?.Form != null)
                {
                    filterModel = new FilterModel
                    {
                        Draw = Convert.ToInt32(Request.Form["draw"].FirstOrDefault() ?? "10"),
                        Length = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "2147483647"),
                        Start = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0"),
                        SortColumn = Request.Form["columns[" +
                                Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault()
                                ?? Request.Form["columnsName"].FirstOrDefault() ?? "Id",
                        SortColumnAscDesc = Request.Form["order[0][dir]"].FirstOrDefault()
                                ?? Request.Form["orderBy"].FirstOrDefault() ?? "asc",
                        SearchValue = Request.Form["search[value]"].FirstOrDefault()
                                ?? Request.Form["search"].FirstOrDefault() ?? ""
                    };
                }
            }
            catch (Exception)
            {

               // throw;
            }

           
            return filterModel;
        }

        protected JsonSerializerSettings IgnorNullProp()
        {

            return new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        protected bool CheckPropExist<T>(string PropName)
        {
            if (typeof(T).GetProperty(PropName) == null)
            {
                return false;
            }
            return true;
        }

        protected string CheckNDefaultPropName<T>(string propName)
        {
            if (CheckPropExist<T>(propName))
            {
                return propName;
            }
            return typeof(T).GetProperties()?.FirstOrDefault().Name;
        }
    }
}
