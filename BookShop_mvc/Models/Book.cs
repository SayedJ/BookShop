using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookShop_mvc.Models
{
    public class Book
    {
        public int bookId { get; set; }
        public string bookAuthor { get; set; }
        public string bookDescription { get; set; }
        public string bookImageContentType { get; set; }
        public string bookIsbn { get; set; }
        public int bookPages { get; set; }
        public double bookPrice { get; set; }

        [Required(ErrorMessage ="This field is required")]
        public string bookTitle { get; set; }
    }
}