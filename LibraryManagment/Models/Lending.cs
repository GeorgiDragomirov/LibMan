using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagment.Models
{
    public class Lending
    {
        // Class to record the lending process in the college
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Landing Date")]
        public  DateTime LendingDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }
        public bool AmountPaid { get; set; }

        //Navigational Properties
        public Book Book { get; set; }
        public Member Member { get; set; }
    }
}
