using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AM.Data.Model;
using AM.Reopsitory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleE>>> GetArticle()
        {
            IEnumerable<ArticleE> a = await articleRepository.FindAllAsync();
            return a.ToList();
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleE>> GetArticle(int id)
        {
            var articleEs = await articleRepository.FindByConditionAsync(x=>x.Id == id);

            if (articleEs == null)
            {
                return NotFound();
            }

            return articleEs.FirstOrDefault();
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, ArticleE articleE)
        {
            if (id != articleE.Id)
            {
                return BadRequest();
            }

            articleRepository.Update(articleE);
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
        public async Task<ActionResult<Category>> PostCategory(ArticleE articleE)
        {
            articleRepository.Create(articleE);
            //_context.Category.Add(category);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = articleE.Id }, articleE);
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

    }
}