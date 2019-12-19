using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ArticleManagement.Models;
using AM.Data;
using ArticleManagement.Controllers.BaseController;
using AM.Data.Model;
using AM.Domain.ViewModel;
using System.Linq.Expressions;
using AM.Reopsitory;
using AM.Service;
using AM.Service.AutoMapperService;

namespace ArticleManagement.Controllers
{
   // [Route("/")]
    public class ArticleController : BaseControllers
    {
        private readonly IArticleRepository articleRepository;
        protected readonly ILoggerService _logService;
        protected readonly IAutoMapperService _mapperService;

        public ArticleController(
            IArticleRepository articleRepository, ILoggerService logService,
            IAutoMapperService mapperService
            ):base(logService)
        {
            this.articleRepository = articleRepository;
            _logService = logService;
            _mapperService = mapperService;
        }
        public IActionResult Index(string query)
        {
            ArticleModel article = new ArticleModel();
            if (query != null) {
                ArticleDto result = articleRepository.FindByConditionAsync(x=>x.Url == query).Result.FirstOrDefault();
                article = _mapperService.Map<ArticleModel, ArticleDto>(result);
            }
            return View(article);
        }


        public IActionResult JDataTable()
        {
      
            try
            {
                FilterModel filterModel = FilterFormData();
                var data = filter(filterModel).Result;
                DataTableModel<IEnumerable<ArticleModel>> dtRes = new DataTableModel<IEnumerable<ArticleModel>>
                {
                    Data = _mapperService.Map<IEnumerable<ArticleModel>, IEnumerable<ArticleDto>>(data),
                    Draw = filterModel.Draw,
                    RecordsTotal = data.Count()//articleRepository.Count()
                    ,RecordsFiltered = articleRepository.Count()
                };

                return JsonResult(dtRes,true,true,true);

            }
            catch (Exception ex)
            {
                //_logService.LogDebug(ex.Message);
                return JsonResult(ex, false);
            }
        }







        private async Task<IEnumerable<ArticleDto>> filter(FilterModel filterModel)
        {

            Expression<Func<ArticleDto, bool>> expression = x => x.Id > 0;
            if (!string.IsNullOrEmpty(filterModel.SearchValue))
            {
                expression = x => x.Article.Contains(filterModel.SearchValue)
                || x.Title.Contains(filterModel.SearchValue);
            }
            var propName = CheckNDefaultPropName<ArticleDto>(filterModel.SortColumn);
            return await articleRepository.Filter(filterModel.SortColumnAscDesc, filterModel.Start, filterModel.Length,
               x => x.GetType().GetProperty(propName).GetValue(x, null),
               expression);
        }

        [HttpGet]
        public IActionResult Publish()
        {
            return View(new ArticleModel());
        }

        [HttpPost]
        public async Task<IActionResult> Publish(ArticleModel articleModel)
        {
            if(ModelState.IsValid)
            {
                articleModel.CreatedBy = 1;
                articleModel.Url = articleModel.Title;
                articleModel.CreatedOn = DateTime.Now;

                var article = _mapperService.Map<ArticleDto, ArticleModel>(articleModel);
                articleRepository.Create(article);
                await articleRepository.SaveAsync();
            }
            return View(new ArticleModel());
        }





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
