using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team11Project.Models;

namespace Team11Project
{
    public class MarketSegmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MarketSegment
        public ActionResult Index()
        {
            return View(db.MarketSegmentModels.ToList());
        }

        // GET: MarketSegment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarketSegmentModel marketSegmentModel = db.MarketSegmentModels.Find(id);
            if (marketSegmentModel == null)
            {
                return HttpNotFound();
            }
            return View(marketSegmentModel);
        }

        // GET: MarketSegment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MarketSegment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MarketSegmentID,Manufacturer")] MarketSegmentModel marketSegmentModel)
        {
            if (ModelState.IsValid)
            {
                db.MarketSegmentModels.Add(marketSegmentModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marketSegmentModel);
        }

        // GET: MarketSegment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarketSegmentModel marketSegmentModel = db.MarketSegmentModels.Find(id);
            if (marketSegmentModel == null)
            {
                return HttpNotFound();
            }
            return View(marketSegmentModel);
        }

        // POST: MarketSegment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MarketSegmentID,Manufacturer")] MarketSegmentModel marketSegmentModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marketSegmentModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marketSegmentModel);
        }

        // GET: MarketSegment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarketSegmentModel marketSegmentModel = db.MarketSegmentModels.Find(id);
            if (marketSegmentModel == null)
            {
                return HttpNotFound();
            }
            return View(marketSegmentModel);
        }

        // POST: MarketSegment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MarketSegmentModel marketSegmentModel = db.MarketSegmentModels.Find(id);
            db.MarketSegmentModels.Remove(marketSegmentModel);
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
