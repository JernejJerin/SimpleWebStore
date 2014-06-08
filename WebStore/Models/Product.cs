using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebStore.Models
{
    public class Product
    {
        public int ID { get; set; }

        [Required, StringLength(100), Display(Name = "Product name")]
        public string ProductName { get; set; }

        [Required, StringLength(100), Display(Name = "Short description"), DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }

        [Required, StringLength(10000), Display(Name = "Description"), DataType(DataType.MultilineText)]
        public string FullDescription { get; set; }

        [DataType(DataType.Url), Display(Name = "Image")]
        public string ImageURL { get; set; }

        [Display(Name = "Price"), DataType(DataType.Currency), DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}", ConvertEmptyStringToNull = true)]
        public Decimal? UnitPrice { get; set; }

        [Display(Name = "Category")]
        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }
    }
}