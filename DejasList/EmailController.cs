
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using DejasList.Models;
=======
﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using SendGrid;
//using SendGrid.Helpers.Mail;
//using System.Threading.Tasks;
>>>>>>> 9da3866906828e06be80aa6f33c86a76c6845629



//namespace DejasList
//{
//    public class EmailController : ApiController
//    {

<<<<<<< HEAD
        //private static void Main()
        //{
        //    Execute().Wait();
        //}

        //static async Task Execute()
        //{
        //    var UserLoggedIn= User.Identity.GetUserId();
        //    var Email = UserLoggedIn.Email;
        //    var apiKey = Environment.GetEnvironmentVariable(APIKeys.SendGridKey);
        //    var client = new SendGridClient(apiKey);
        //    var from = new EmailAddress(Email, "Example User");
        //    var subject = "Sending with SendGrid is Fun";
        //    var to = new EmailAddress("test@example.com", "Example User");
        //    var plainTextContent = "and easy to do anywhere, even with C#";
        //    var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    var response = await client.SendEmailAsync(msg);
        //}
=======
//        private static void Main()
//        {
//            Execute().Wait();
//        }

//        static async Task Execute()
//        {
//            var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
//            var client = new SendGridClient(apiKey);
//            var from = new EmailAddress("test@example.com", "Example User");
//            var subject = "Sending with SendGrid is Fun";
//            var to = new EmailAddress("test@example.com", "Example User");
//            var plainTextContent = "and easy to do anywhere, even with C#";
//            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
//            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
//            var response = await client.SendEmailAsync(msg);
//        }
>>>>>>> 9da3866906828e06be80aa6f33c86a76c6845629




//        //// GET api/<controller>
//        //public IEnumerable<string> Get()
//        //{
//        //    return new string[] { "value1", "value2" };
//        //}

//        //// GET api/<controller>/5
//        //public string Get(int id)
//        //{
//        //    return "value";
//        //}

//        //// POST api/<controller>
//        //public void Post([FromBody]string value)
//        //{
//        //}

//        //// PUT api/<controller>/5
//        //public void Put(int id, [FromBody]string value)
//        //{
//        //}

//        //// DELETE api/<controller>/5
//        //public void Delete(int id)
//        //{
//        //}
//    }
//}