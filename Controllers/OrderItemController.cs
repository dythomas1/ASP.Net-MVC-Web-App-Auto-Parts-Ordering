﻿using System;
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
    public class OrderItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderItem
        public ActionResult Index()
        {
            var orderItemModels = db.OrderItemModels.Include(o => o.CustomerOrder).Include(o => o.Product);
            return View(orderItemModels.ToList());
        }

        // GET: OrderItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItemModel orderItemModel = db.OrderItemModels.Find(id);
            if (orderItemModel == null)
            {
                return HttpNotFound();
            }
            return View(orderItemModel);
        }

        // GET: OrderItem/Create
        public ActionResult Create()
        {
            ViewBag.CustomerOrderID = new SelectList(db.CustomerOrderModels, "CustomerOrderID", "CustomerOrderID");
            ViewBag.ProductID = new SelectList(db.ProductModels, "ProductID", "ProductName");
            return View();
        }

        // POST: OrderItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderItemID,ProductID,CustomerOrderID,Quantity")] OrderItemModel orderItemModel)
        {
            if (ModelState.IsValid)
            {
                db.OrderItemModels.Add(orderItemModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerOrderID = new SelectList(db.CustomerOrderModels, "CustomerOrderID", "CustomerOrderID", orderItemModel.CustomerOrderID);
            ViewBag.ProductID = new SelectList(db.ProductModels, "ProductID", "ProductName", orderItemModel.ProductID);
            return View(orderItemModel);
        }

        // GET: OrderItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItemModel orderItemModel = db.OrderItemModels.Find(id);
            if (orderItemModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerOrderID = new SelectList(db.CustomerOrderModels, "CustomerOrderID", "CustomerOrderID", orderItemModel.CustomerOrderID);
            ViewBag.ProductID = new SelectList(db.ProductModels, "ProductID", "ProductName", orderItemModel.ProductID);
            return View(orderItemModel);
        }

        // POST: OrderItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderItemID,ProductID,CustomerOrderID,Quantity")] OrderItemModel orderItemModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderItemModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerOrderID = new SelectList(db.CustomerOrderModels, "CustomerOrderID", "CustomerOrderID", orderItemModel.CustomerOrderID);
            ViewBag.ProductID = new SelectList(db.ProductModels, "ProductID", "ProductName", orderItemModel.ProductID);
            return View(orderItemModel);
        }

        // GET: OrderItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItemModel orderItemModel = db.OrderItemModels.Find(id);
            if (orderItemModel == null)
            {
                return HttpNotFound();
            }
            return View(orderItemModel);
        }

        // POST: OrderItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderItemModel orderItemModel = db.OrderItemModels.Find(id);
            db.OrderItemModels.Remove(orderItemModel);
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
