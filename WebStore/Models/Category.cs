using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebStore.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Required, StringLength(100), Display(Name = "Category name")]
        public string CategoryName { get; set; }

        [Display(Name = "Category description")]
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}