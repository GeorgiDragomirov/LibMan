using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagment.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        [Display(Name = "Cost Of The Book")]
        public decimal CostOfTheBook { get; set; }

        [Display(Name = "Location In The Library")]
        public string LocationInLibrary { get; set; }
        public int BookCategoryId { get; set; }

        //Navigation properties
        public BookCategory BookCategory { get; set; }
    }
}
