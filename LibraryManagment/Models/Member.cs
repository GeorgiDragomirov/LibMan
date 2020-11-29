using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagment.Models
{
    public class Member
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Forename { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        //Another approach to display Wherever is necessary
        public string FullName
        {
            get
            { 
                return Forename + " " + Surname; 
            }
        }
    }
}
