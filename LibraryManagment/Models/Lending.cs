using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagment.Models
{
    public class Lending
    {
        public static DateTime Today { get; }

        public const int AllowedDays = 35;
        // Class to record the lending process in the college
        public int Id { get; set; }

        [Display(Name = "Member Name")]
        public int MemberId { get; set; }

        [Display(Name = "Book Title")]
        public int BookId { get; set; }

        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Landing Date")]
        public  DateTime LendingDate { get; set; }

        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }
        public bool AmountPaid { get; set; }

        //Navigational Properties
        public Book Book { get; set; }
        public Member Member { get; set; }

        [Display(Name = "Amount Owed")]
        public decimal AmountOwed
        {
            get
            {
                decimal amountOwed = 0.0m;
                if (LendingDate.AddDays(AllowedDays) <= ReturnDate)
                {
                    int delayedDays = (ReturnDate - LendingDate.AddDays(AllowedDays)).Days;
                    amountOwed = delayedDays * 0.50m;
                }
                return amountOwed;
            }
        }
    }
}
