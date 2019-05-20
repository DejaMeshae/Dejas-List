﻿using System;
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
    public class JobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Jobs
        public ActionResult Index()
        {
            //if (!User.IsInRole("Client"))
            //{
               
            //    return RedirectToAction("ClientJobList");
            //}
            //else if (!User.IsInRole("Contractor"))
            //{
            //    return RedirectToAction("ContractorJobList");
            //}
            return View();
        }


        public ActionResult ClientJobList()
        {
            var id = db.Jobs.Select(h => h.ClientId).FirstOrDefault();
            var jobs = db.Jobs.Select(j => j.ClientId==id);
            return View(jobs.ToList());
        }

        public ActionResult ContractorJobList()
        {
            var jobs = db.Jobs;
            return View(jobs.ToList());
        }



        // GET: Jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobs jobs = db.Jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            return View(jobs);
        }

        // GET: Jobs/Details/5
        public ActionResult ContractorDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobs jobs = db.Jobs.Where(j => j.JobsId == id).Include(m => m.Messages).FirstOrDefault();
            if (jobs == null)
            {
                return HttpNotFound();
            }
            return View(jobs);
        }

        // GET: Jobs/Create
        public ActionResult Message()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FirstName");
            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "FirstName");
            return View();
        }



        // GET: Jobs/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FirstName");
            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "FirstName");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobsId,TypeOfProject,SizeOfProject,Budget,Summary,Name")] Jobs jobs)
        {
            if (ModelState.IsValid)
            {
                var id = User.Identity.GetUserId();
                var job = db.Clients.Where(k => k.ApplicationUserId == id).Select(s => s.ClientId).FirstOrDefault();
                jobs.ClientId = job;
                var lat = db.Clients.Where(c => c.ClientId == jobs.ClientId).Select(f => f.Lat).FirstOrDefault();
                var lng = db.Clients.Where(c => c.ClientId == jobs.ClientId).Select(f => f.Lng).FirstOrDefault();
                jobs.Lat = lat;
                jobs.Lng = lng;
                //db.Jobs.Add(job);
                db.Jobs.Add(jobs);
                db.SaveChanges();
                JobsViewModel model = new JobsViewModel();
                model.JobList = db.Jobs.ToList();
                return RedirectToAction("Index", "Client", model);
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FirstName", jobs.ClientId);
            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "FirstName", jobs.ContractorId);
            return View(jobs);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobs jobs = db.Jobs.Where(j => j.JobsId == id).FirstOrDefault();
            if (jobs == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FirstName", jobs.ClientId);
            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "FirstName", jobs.ContractorId);
            return View(jobs);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobsId,TypeOfProject,SizeOfProject,Budget,Summary,ClientId,Name,Lat,Lng")] Jobs jobs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobs).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index", "Client");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FirstName", jobs.ClientId);
            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "FirstName", jobs.ContractorId);
            return View(jobs);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobs jobs = db.Jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            return View(jobs);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jobs jobs = db.Jobs.Find(id);
            db.Jobs.Remove(jobs);
            db.SaveChanges();
            return RedirectToAction("Index", "Client");
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
