using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AM.Api.Controllers.BaseController;
using AM.Data;
using AM.Data.Model;
using AM.Domain.ViewModel;
using AM.Reopsitory;
using AM.Service;
using AM.Service.AutoMapperService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : BaseControllers
    {
        private readonly IArticleRepository articleRepository;
        protected readonly ILoggerService _logService;
        protected readonly IAutoMapperService _mapperService;

        public ArticleController(IArticleRepository articleRepository, ILoggerService logService,
            IAutoMapperService mapperService
            ) : base(logService)
        {
            this.articleRepository = articleRepository;
            _logService = logService;
            _mapperService = mapperService;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetArticle()
        {
            try
            {
                FilterModel filterModel = FilterFormData();
                var data = await filter(filterModel);
                var result = _mapperService.Map<IEnumerable<ArticleModel>, IEnumerable<ArticleDto>>(data);
                return JsonResult(result, true);

            }
            catch (Exception ex)
            {
                return JsonResult(ex, false);
            }


        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> GetArticle(int id)
        {
            var ArticleDtos = await articleRepository.FindByConditionAsync(x=>x.Id == id);

            if (ArticleDtos == null)
            {
                return NotFound();
            }

            return ArticleDtos.FirstOrDefault();
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, ArticleDto ArticleDto)
        {
            if (id != ArticleDto.Id)
            {
                return BadRequest();
            }

            articleRepository.Update(ArticleDto);
            //_context.Entry(category).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!CategoryExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(ArticleDto articleDto)
        {
            articleRepository.Create(articleDto);
            //_context.Category.Add(category);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = articleDto.Id }, articleDto);
        }

        // DELETE: api/Category/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Category>> DeleteCategory(int id)
        //{
        //    //var category = await _context.Category.FindAsync(id);
        //    //if (category == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //_context.Category.Remove(category);
        //    //await _context.SaveChangesAsync();

        //    //return category;
        //}

        //private bool CategoryExists(int id)
        //{
        //    //return _context.Category.Any(e => e.Id == id);
        //}

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


    }
}