using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
=======
>>>>>>> 77ea5c61c6e20e5adb6cb63477ac6b71b028fc8b
using System.ComponentModel.DataAnnotations.Schema;

namespace DejasList.Models
{
    public class Jobs
    {
        [Key]
        public int JobsId { get; set; }
        public string TypeOfProject { get; set; }
        public string SizeOfProject { get; set; }
        public double Budget { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [ForeignKey("Contractor")]
        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }

    }
}