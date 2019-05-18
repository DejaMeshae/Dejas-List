using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace DejasList.Models
{
    public class Contractor
    {
        [Key]
        public int ContractorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Zipcode { get; set; }

        public string State { get; set; }

        [DisplayFormat(DataFormatString = "{0:###-##-####}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"\d{9}", ErrorMessage ="Needs to be nine digits")]
        public string SocialNumber { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode=true)]
        public DateTime DateOfBirth { get; set; }

        public string Lat { get; set; }

        public string Lng { get; set; }

        //public bool Background { get; set; }

        public IEnumerable<Jobs> Jobs { get; set; }


        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}