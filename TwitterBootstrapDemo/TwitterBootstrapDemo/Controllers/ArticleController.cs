using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TwitterBootstrapDemo.DB;

namespace TwitterBootstrapDemo.Controllers
{
    public class ArticleController : ApiController
    {
        // GETALL api/article/GetAll/5
        public HttpResponseMessage GetAll(int categoryId)
        {
            IEnumerable<Article> articles;
            using (var context = new DatabaseContext())
            {
                articles = context.Articles.Where(x => x.CategoryId == categoryId).ToList();
                var slimArticles = articles.Select(x => new
                    {
                        x.CategoryId,
                        x.Id,
                        x.Title, 
                        x.ImageUrl,
                        x.ShortDescription,
                        x.LongDescription,
                        x.ArticleUrl
                    }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, slimArticles,
                        Configuration.Formatters.JsonFormatter);
            }
        }
    }
}