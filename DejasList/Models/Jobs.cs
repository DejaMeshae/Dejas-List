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

        [Display(Name = "Type of Project:")]
        public string TypeOfProject { get; set; }

        [Display(Name = "Size of Project:")]
        public string SizeOfProject { get; set; }

        public string Budget { get; set; }

        public string Summary { get; set; }

        public string Lat { get; set; }

        public string Lng { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        public Client Client { get; set; }

        public int ContractorId { get; set; }

        public List<Message> Messages { get; set; }

        public class Message
        {
            [Key]
            public int Id { get; set; }

            public string _Message { get; set; }

            [ForeignKey("Contractor")]
            public int ContractorId { get; set; }

            public Contractor Contractor { get; set; }
        }

        //[ForeignKey("Emails")]
        //public int EmailId { get; set; }
        //public Emails Emails { get; set; }



    }
}