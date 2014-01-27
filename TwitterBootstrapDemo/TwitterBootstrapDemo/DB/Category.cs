using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TwitterBootstrapDemo.DB
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "SlideOrder is a required field.")]
        public int SlideOrder { get; set; }

        [Required(ErrorMessage = "Title is a required field.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        public string Description { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

    }
}