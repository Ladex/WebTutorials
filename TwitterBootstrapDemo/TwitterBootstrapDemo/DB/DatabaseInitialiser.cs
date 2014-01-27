using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TwitterBootstrapDemo.DB
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<DatabaseContext> 
    {
        private List<Category> categoriesSeedData;

        public DatabaseInitializer(List<Category> categoriesSeedData)
        {
            this.categoriesSeedData = categoriesSeedData;
        }


        protected override void Seed(DatabaseContext context)
        {
            foreach (var category in categoriesSeedData)
            {
                context.Categories.Add(category);
            }
            context.SaveChanges();
        }
    }
}