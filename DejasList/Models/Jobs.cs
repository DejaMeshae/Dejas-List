using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace DejasList.Models
{
    public class Jobs
    {
        [Key]
        public int JobsId { get; set; }
        public string TypeOfProject { get; set; }
        public string SizeOfProject { get; set; }
        public string Budget { get; set; }
        public string Summary { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }


        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int ContractorId { get; set; }

    }
}