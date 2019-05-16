using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DejasList.Models;
using Microsoft.AspNet.Identity;

namespace DejasList.Controllers
{
    public class ContractorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contractors
        public ActionResult Index()
        {
            var ContractorLoggedIn = User.Identity.GetUserId();
            var contractors = db.Contractors.Where(e => e.ApplicationUserId == ContractorLoggedIn).Include(c => c.ApplicationUser).FirstOrDefault();
            var contractor = Details(contractors.ContractorId);
            return View(contractor);
        }

        public IQueryable<Contractor>GetContractors()
            {
            var contractors = from w in db.Contractors
                              select new Contractor()
                              {
                                  ApplicationUserId = w.ApplicationUserId,
                                  FirstName = w.FirstName,
                                  LastName = w.LastName,
                                  Address = w.Address,
                                  City = w.City,
                                  State = w.State,
                                  ContractorId = w.ContractorId,
                                  Zipcode = w.Zipcode
                              };
                               return contractors;
            }

    // GET: Contractors/Details/5
    public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = db.Contractors.Where(c => c.ContractorId == id).Include(a => a.ApplicationUser).FirstOrDefault();
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        // GET: Contractors/Create
        public ActionResult Create()
        {

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Contractors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContractorId,FirstName,LastName,Address,City,Zipcode,State")] Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                contractor.ApplicationUserId = User.Identity.GetUserId();
                string address = (contractor.Address + "+" + contractor.City + "+" + contractor.State + "+" + contractor.Zipcode);
                GeocodeController geocode = new GeocodeController();
                geocode.SendRequest(address);
                contractor.Lat = geocode.latitude;
                contractor.Lng = geocode.longitude;
                db.Contractors.Add(contractor);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = contractor.ContractorId });
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", contractor.ApplicationUserId); //Will likley not need this line of code delete in the end *DA
            return View(contractor);
        }

        // GET: Contractors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", contractor.ApplicationUserId);//Will likley not need this line of code delete in the end *DA
            return View(contractor);
        }

        // POST: Contractors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContractorId,FirstName,LastName,Address,City,Zipcode,State")] Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", contractor.ApplicationUserId);//Will likley not need this line of code delete in the end *DA
            return View(contractor);
        }

        // GET: Contractors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        // POST: Contractors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contractor contractor = db.Contractors.Find(id);
            db.Contractors.Remove(contractor);
            db.SaveChanges();
            return RedirectToAction("Index");
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
