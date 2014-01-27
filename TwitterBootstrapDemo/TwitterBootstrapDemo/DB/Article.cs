using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TwitterBootstrapDemo.DB
{
    [Table("Articles")]
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is a required field.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "ShortDescription is a required field.")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "LongDescription is a required field.")]
        public string LongDescription { get; set; }

        [Required(ErrorMessage = "ImageUrl is a required field.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "ArticleUrl is a required field.")]
        public string ArticleUrl { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

    }
}