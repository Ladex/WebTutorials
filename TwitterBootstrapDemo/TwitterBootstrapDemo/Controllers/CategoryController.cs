using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TwitterBootstrapDemo.DB;

namespace TwitterBootstrapDemo.Controllers
{
    public class CategoryController : ApiController
    {
        public HttpResponseMessage Get()
        {
            IEnumerable<Category> categories;
            using (var context = new DatabaseContext())
            {
                context.Categories.Include("Articles");
                categories = context.Categories.OrderBy(x => x.SlideOrder).ToList();
                if (categories.Any())
                {

                    var slimCats = categories.Select(x => new
                        {
                            Id = x.Id,
                            Title = x.Title,
                            Description = x.Description,
                            ArticleIds = x.Articles.Select(y => y.Id).ToList()
                        }).ToList();
                    
                    return Request.CreateResponse(HttpStatusCode.OK, slimCats,
                        Configuration.Formatters.JsonFormatter);
                }

                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}