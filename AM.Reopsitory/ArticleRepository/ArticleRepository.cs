using AM.Api.Model;
using AM.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AM.Reopsitory
{
    public class ArticleRepository : RepositoryBase<ArticleDto>, IArticleRepository
    {
        public ArticleRepository(ArticleManagementContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
