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
    public class CustomerOrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CustomerOrder
        public ActionResult Index()
        {
            var customerOrderModels = db.CustomerOrderModels.Include(c => c.Customer);
            return View(customerOrderModels.ToList());
        }

        // GET: CustomerOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerOrderModel customerOrderModel = db.CustomerOrderModels.Find(id);
            if (customerOrderModel == null)
            {
                return HttpNotFound();
            }
            return View(customerOrderModel);
        }

        // GET: CustomerOrder/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.CustomerModels, "CustomerID", "Name");
            return View();
        }

        // POST: CustomerOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerOrderID,CustomerID,OrderDate")] CustomerOrderModel customerOrderModel)
        {
            if (ModelState.IsValid)
            {
                db.CustomerOrderModels.Add(customerOrderModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.CustomerModels, "CustomerID", "Name", customerOrderModel.CustomerID);
            return View(customerOrderModel);
        }

        // GET: CustomerOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerOrderModel customerOrderModel = db.CustomerOrderModels.Find(id);
            if (customerOrderModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.CustomerModels, "CustomerID", "Name", customerOrderModel.CustomerID);
            return View(customerOrderModel);
        }

        // POST: CustomerOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerOrderID,CustomerID,OrderDate")] CustomerOrderModel customerOrderModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerOrderModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.CustomerModels, "CustomerID", "Name", customerOrderModel.CustomerID);
            return View(customerOrderModel);
        }

        // GET: CustomerOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerOrderModel customerOrderModel = db.CustomerOrderModels.Find(id);
            if (customerOrderModel == null)
            {
                return HttpNotFound();
            }
            return View(customerOrderModel);
        }

        // POST: CustomerOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerOrderModel customerOrderModel = db.CustomerOrderModels.Find(id);
            db.CustomerOrderModels.Remove(customerOrderModel);
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
