using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TwitterBootstrapDemo.DB
{
    public static class XmlParser
    {
        public static List<Category> ObtainSeedData()
        {
            var appPath = HttpRuntime.AppDomainAppPath;
            var doc = XElement.Load(string.Format(@"{0}\App_Data\SeedData.xml",appPath));
            var serverUtility = HttpContext.Current.Server;

            var categories = doc.Descendants("category").Select(x =>
            {
                Category category = new Category
                {
                    SlideOrder = int.Parse(x.Element("slideOrder").Value),
                    Title = x.Element("title").Value.Trim(),
                    Description = x.Element("description").Value.Trim()
                };


                var articles = x.Descendants("articles").Descendants("article").Select(y =>
                    {
                        return new Article
                            {
                                Title = y.Element("title").Value.Trim(),
                                ShortDescription = y.Element("shortDescription").Value.Trim(),
                                LongDescription = y.Element("longDescription").Value.Trim(),
                                ArticleUrl = y.Element("articleUrl").Value,
                                ImageUrl = y.Element("imageUrl").Value,
                                Category = category
                            };
                    });

                category.Articles = articles.ToList();
                return category;

            }).ToList();

            return categories;
        }
    }
}