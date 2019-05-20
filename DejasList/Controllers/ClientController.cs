using DejasList.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DejasList.Controllers
{
    public class ClientController : Controller
    {
        ///string currentlyLoggedInUserId = User.Identity.GetUserId();

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: 
        public ActionResult Index()
        {
            var ClientLoggedIn = User.Identity.GetUserId();
            var client = db.Clients.Where(e => e.ApplicationUserId == ClientLoggedIn).Include(c => c.ApplicationUser).FirstOrDefault();            
            JobsViewModel model = new JobsViewModel();
            model.JobList = db.Jobs.Where(j => j.ClientId == client.ClientId).ToList();
            return View(model);
        }


        public IQueryable<Client> GetClients()
        {
            var clients = from w in db.Clients
                              select new Client()
                              {
                                  ApplicationUserId = w.ApplicationUserId,
                                  FirstName = w.FirstName,
                                  LastName = w.LastName,
                                  Address = w.Address,
                                  City = w.City,
                                  State = w.State,
                                  ClientId = w.ClientId,
                                  Zipcode = w.Zipcode
                              };
            return clients;
        }




        // GET:/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client clients = db.Clients.Where(c => c.ClientId == id).Include(a => a.ApplicationUser).FirstOrDefault();
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        //GET:/Create
        public ActionResult Create()
        {

            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

        // POST:/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,FirstName,LastName,Address,City,State,ZipCode,Email,AboutMe")] Client client)
        {
            if (ModelState.IsValid)
            {
                //Add image to folder
                // To convert the user uploaded Photo as Byte Array before save to DB
                byte[] imageData = null;
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase poImgFile = Request.Files["ProfilePhoto"];

                    using (var binary = new BinaryReader(poImgFile.InputStream))
                    {
                        imageData = binary.ReadBytes(poImgFile.ContentLength);
                    }
                }
                client.ApplicationUserId = User.Identity.GetUserId();
                client.ProfilePhoto = imageData;
                string address = (client.Address + "+" + client.City + "+" + client.State + "+" + client.Zipcode);
                GeocodeController geocode = new GeocodeController();
                geocode.SendRequest(address);
                client.Lat = geocode.latitude;
                client.Lng = geocode.longitude;
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = client.ClientId });
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", client.ApplicationUserId);
            return View(client);
        }
        
        // GET:
        public ActionResult ContractorList()
        {
            
            List<Contractor> contractors = new List<Contractor>();
            contractors = db.Contractors.ToList();
            ViewBag.contractorList = contractors;        

            return View();
        }

        // GET:Edit/5
        public ActionResult Edit()
        {
            string id = User.Identity.GetUserId();
            Client model = db.Clients.Where(m => m.ApplicationUserId == id).FirstOrDefault();
            return View(model);
        }

        // POST:Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,FirstName,LastName,Address,City,State,ZipCode,ApplicationUserId,AboutMe")] Client client)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase poImgFile = Request.Files["ProfilePhoto"];

                    using (var binary = new BinaryReader(poImgFile.InputStream))
                    {
                        imageData = binary.ReadBytes(poImgFile.ContentLength);
                    }
                }
                db.Entry(client).State = EntityState.Modified;
                client.ProfilePhoto = imageData;
                string address = (client.Address + "+" + client.City + "+" + client.State + "+" + client.Zipcode);
                GeocodeController geocode = new GeocodeController();
                geocode.SendRequest(address);
                client.Lat = geocode.latitude;
                client.Lng = geocode.longitude;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", client.ApplicationUserId);
            return View(client);
        }

        // GET: Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST:Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       //public ActionResult CreateJob(int? id)
       // {
       //     //var jobs = DependencyResolver.Current.GetService<JobsController>();
       //     //jobs.ControllerContext = new ControllerContext(this.Request.RequestContext, jobs);
       //     return RedirectToAction("Create", "JobsController");

       //     //return jobs.Create();
       //     //var job = new JobsController().Create();
       //     //return jobs;
       // }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public FileContentResult UserPhotos()
        {
            if (User.Identity.IsAuthenticated)
            {
                String userId = User.Identity.GetUserId();

                if (userId == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");

                }
                // to get the user details to load user Image
                var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var user = db.Users.Where(x => x.Id == userId).FirstOrDefault();
                var client = db.Clients.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault();

                return new FileContentResult(client.ProfilePhoto, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");

            }
        }
    }
}