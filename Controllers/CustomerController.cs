using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team11Project.Models;

namespace Team11Project.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customer
        public ActionResult Index(string customerSegment, string searchString)
        {
            var customerModels = db.CustomerModels.Include(c => c.CampaignModel).Include(c => c.MarketSegmentModel);

            var SegmentLst = new List<string>();

            // gets the list of market segments
            var SegmentQry = from d in db.CustomerModels
                           orderby d.MarketSegmentModel.Manufacturer
                           select d.MarketSegmentModel.Manufacturer;

            //makes sure no segments are repeated in the list
            SegmentLst.AddRange(SegmentQry.Distinct());
            ViewBag.customerSegment = new SelectList(SegmentLst);

            var segments = from m in db.CustomerModels
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                segments = segments.Where(s => s.MarketSegmentModel.Manufacturer.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(customerSegment))
            {
                segments = segments.Where(x => x.MarketSegmentModel.Manufacturer == customerSegment);
                return View(segments.ToList());
            }

            return View(customerModels.ToList());
        }


        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerModel customerModel = db.CustomerModels.Find(id);
            if (customerModel == null)
            {
                return HttpNotFound();
            }
            return View(customerModel);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            ViewBag.CampaignModelID = new SelectList(db.CampaignModels, "CampaignModelID", "Name");
            ViewBag.MarketSegmentID = new SelectList(db.MarketSegmentModels, "MarketSegmentID", "Manufacturer");
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,Name,Email,LastLogin,SignupDate,MailingAddress,City,State,ZipCode,MarketSegmentID,CampaignModelID")] CustomerModel customerModel)
        {
            if (ModelState.IsValid)
            {
                db.CustomerModels.Add(customerModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampaignModelID = new SelectList(db.CampaignModels, "CampaignModelID", "Name", customerModel.CampaignModelID);
            ViewBag.MarketSegmentID = new SelectList(db.MarketSegmentModels, "MarketSegmentID", "Manufacturer", customerModel.MarketSegmentID);
            return View(customerModel);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerModel customerModel = db.CustomerModels.Find(id);
            if (customerModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampaignModelID = new SelectList(db.CampaignModels, "CampaignModelID", "Name", customerModel.CampaignModelID);
            ViewBag.MarketSegmentID = new SelectList(db.MarketSegmentModels, "MarketSegmentID", "Manufacturer", customerModel.MarketSegmentID);
            return View(customerModel);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,Name,Email,LastLogin,SignupDate,MailingAddress,City,State,ZipCode,MarketSegmentID,CampaignModelID")] CustomerModel customerModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampaignModelID = new SelectList(db.CampaignModels, "CampaignModelID", "Name", customerModel.CampaignModelID);
            ViewBag.MarketSegmentID = new SelectList(db.MarketSegmentModels, "MarketSegmentID", "Manufacturer", customerModel.MarketSegmentID);
            return View(customerModel);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerModel customerModel = db.CustomerModels.Find(id);
            if (customerModel == null)
            {
                return HttpNotFound();
            }
            return View(customerModel);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerModel customerModel = db.CustomerModels.Find(id);
            db.CustomerModels.Remove(customerModel);
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
