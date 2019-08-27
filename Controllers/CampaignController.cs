using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team11Project.Models;
using System.Net.Mail;
using SendGrid;
using System.Net.Http;
using Team11Project.ViewModels;
using System.Collections;
//using SendGrid;

namespace Team11Project.Controllers
{
    public class CampaignController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Campaign
        //Action result that returns all the customers within the database to the view
        public ActionResult Index()
        {
            var campaignModels = db.CampaignModels.Include(c => c.CampaignGoal);
            return View(campaignModels.ToList());
        }

        // GET: Campaign/Details/5
        //Action result that reads in the id for the campaign that is wanted to be viewed
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //finds id in the Campaign model
            CampaignModel campaignModel = db.CampaignModels.Find(id);
            if (campaignModel == null)
            {
                return HttpNotFound();
            }
            return View(campaignModel);
        }

        // GET: Campaign/Create
        public ActionResult Create()
        {
            ViewBag.CampaignGoalID = new SelectList(db.CampaignGoalModels, "CampaignGoalID", "GoalName");
            return View();
        }

        // POST: Campaign/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        //Action result that read in information and saves the new campaign to the database
        public ActionResult Create([Bind(Include = "CampaignModelID,Name,IsActive,CampaignGoalID,EndCampaign,Budget,FundingExpenditures,IsSubscriber,CPC,CTR,CVR")] CampaignModel campaignModel)
        {
            if (ModelState.IsValid)
            {
                db.CampaignModels.Add(campaignModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampaignGoalID = new SelectList(db.CampaignGoalModels, "CampaignGoalID", "GoalName", campaignModel.CampaignGoalID);
            return View(campaignModel);
        }

        // GET: Campaign/Edit/5
        //Action result that returns a view but is first read in an id for the campaign that is wanted to be editted
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CampaignModel campaignModel = db.CampaignModels.Find(id);
            if (campaignModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampaignGoalID = new SelectList(db.CampaignGoalModels, "CampaignGoalID", "GoalName", campaignModel.CampaignGoalID);
            return View(campaignModel);
        }

        // POST: Campaign/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

            //Http post method that actually edits the information within the database and saves the changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CampaignModelID,Name,IsActive,CampaignGoalID,EndCampaign,Budget,FundingExpenditures,IsSubscriber,CPC,CTR,CVR")] CampaignModel campaignModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campaignModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampaignGoalID = new SelectList(db.CampaignGoalModels, "CampaignGoalID", "GoalName", campaignModel.CampaignGoalID);
            return View(campaignModel);
        }

        // GET: Campaign/Delete/5

            //Action result that is called as a result of a redirect to action and directs to a view
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CampaignModel campaignModel = db.CampaignModels.Find(id);
            if (campaignModel == null)
            {
                return HttpNotFound();
            }
            return View(campaignModel);
        }

        // POST: Campaign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //An action result that actually deletes the information from the database
        public ActionResult DeleteConfirmed(int id)
        {
            CampaignModel campaignModel = db.CampaignModels.Find(id);
            db.CampaignModels.Remove(campaignModel);
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
        //action result method that returns a view
        public ActionResult SendEmail()
        {
            return View("SendEmail");
        }
        //reads in data and sends email
        public ActionResult SendEmailSubmit(int segment, string subject, string content)
        {
            int Segment = segment;
            //searches for desired customers and adds them to list
            var recipientList = new List<string>();
            foreach (var i in db.CustomerModels)
            {
                if (i.CampaignModelID == Segment)
                {
                    recipientList.Add(i.Email);
                }

            }
            var campaignEmail = new SendGrid.SendGridMessage();
            //new email
            campaignEmail.From = new MailAddress("bwatts1@crimson.ua.edu");
            //email account we will use
            campaignEmail.AddTo(recipientList);
            //adds recipients to be sent to
            campaignEmail.Subject = subject;
            //adds subject of the email
            campaignEmail.Text = content;
            //content of email is added
            var credential = new NetworkCredential("bwatts1@crimson.ua.edu", "Bretts@uto1");
            //email credentials we will use to send email
            var transportWeb = new Web(credential);
            transportWeb.DeliverAsync(campaignEmail);
            return RedirectToAction("Index");
        }

        //Action result method reads in a id for the campaign that customers will be added to
        public ActionResult EditCustomerCampaign(int? id, string customerSegment, string searchString)
        {
            var customerModels = db.CustomerModels.Include(c => c.CampaignModel).Include(c => c.MarketSegmentModel);
            var SegmentLst = new List<string>();

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
            //creates a list for reading in the customers that are wanted to be checked
            List<CampaignCustomerModel> customerCampaigns = new List<CampaignCustomerModel>();

            foreach (var a in customerModels)
            {
                var customerCampaign1 = new CampaignCustomerModel();
                customerCampaign1.CustomerID = a.CustomerID;
                customerCampaign1.CampaignID = id;
                customerCampaign1.CustomerName = a.Name;
                customerCampaign1.Check = false;
                customerCampaigns.Add(customerCampaign1);
            }
            return View(customerCampaigns);
        }
        //Action result method that takes the full list of campaigns and is saved under the customer model
        [HttpPost]
        public ActionResult EditCustomerCampaign(List<CampaignCustomerModel> camp)
        {
            if (ModelState.IsValid)
            {
                foreach (var a in camp)
                {
                    //IF the check is true then the customer is given the speicifed campaign
                    if (a.Check == true)
                    {

                        CustomerModel cust = db.CustomerModels.Find(a.CustomerID);
                        cust.CampaignModelID = a.CampaignID;
                        db.SaveChanges();
                    }
                    //if false then it wont give the customer any Campaign ID
                    else if (a.Check == false)
                    {
                        CustomerModel cust = db.CustomerModels.Find(a.CustomerID);
                        a.CampaignID = null;
                        db.SaveChanges();
                    }
                }

            }
            else if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        //This is the data that will be querried to create the campaign table
        public ActionResult CreateCampaignStatusTable()
        {
            string query = "SELECT Name, IsActive, EndCampaign, Budget, FundingExpenditures "
                + "FROM CampaignModels ";

            IEnumerable<CampaignStatusModel> dataset = db.Database.SqlQuery<CampaignStatusModel>(query);

            foreach (var x in dataset)
            {
                x.EndCampaign.ToShortDateString();
            }

            return View(dataset.ToList());
        }
    }
}
