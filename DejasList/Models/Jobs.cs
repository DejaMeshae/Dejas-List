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

        [Display(Name = "Type of Job")]
        public string TypeOfProject { get; set; }

        [Display(Name = "Size of Job")]
        public string SizeOfProject { get; set; }

        [Display(Name = "Estimated Budget for Job")]
        public string Budget { get; set; }

        [Display(Name = "Job Name")]
        public string Name { get; set; }

        [Display(Name = "Job Details")]
        [DataType(DataType.MultilineText)]
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

            [DataType(DataType.MultilineText)]
            public string _Message { get; set; }

            [ForeignKey("Contractor")]
            public int ContractorId { get; set; }

            public Contractor Contractor { get; set; }
        }

    }
}