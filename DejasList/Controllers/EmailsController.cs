using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using DejasList.Models;
using Microsoft.AspNet.Identity;

namespace DejasList.Controllers
{
    public class EmailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Emails
        public ActionResult Index()
        {
            return View(db.Emails.ToList());
        }

        // GET: Emails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emails emails = db.Emails.Find(id);
            if (emails == null)
            {
                return HttpNotFound();
            }
            return View(emails);
        }

        // GET: Emails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Emails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmailId,From,To,Subject,Body")] Emails emails)
        {
            if (ModelState.IsValid)
            {
                var id= User.Identity.GetUserId();
                emails.From = db.Users.Where(d => d.Id == id).Select(s => s.Email).FirstOrDefault();
                emails.Subject = "You have a message from Deja's List.";
                db.Emails.Add(emails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emails);
        }

        // GET: Emails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emails emails = db.Emails.Find(id);
            if (emails == null)
            {
                return HttpNotFound();
            }
            return View(emails);
        }

        // POST: Emails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmailId,From,To,Subject,Body")] Emails emails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emails);
        }

        // GET: Emails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emails emails = db.Emails.Find(id);
            if (emails == null)
            {
                return HttpNotFound();
            }
            return View(emails);
        }

        // POST: Emails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emails emails = db.Emails.Find(id);
            db.Emails.Remove(emails);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult SendEmail(Emails emails)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(emails.To);
            mail.To.Add(emails.To);
            mail.From = new MailAddress(emails.From);
            mail.Subject = emails.Subject;

            mail.Body = emails.Body;

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "DejasList@gmail.com"; 
            smtp.Credentials = new System.Net.NetworkCredential
                 ("DejasList@gmail.com", APIKeys.GmailPassword); 
            smtp.Port = 587;
            
            smtp.EnableSsl = true;
            smtp.Send(mail);
            return View("Index");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
