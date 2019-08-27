using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team11Project.Models;
using Team11Project.ViewModels;

namespace Team11Project.Controllers
{
    public class CampaignGoalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CampaignGoal
        public ActionResult Index()
        {
            return View(db.CampaignGoalModels.ToList());
        }

        // GET: CampaignGoal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CampaignGoalModel campaignGoalModel = db.CampaignGoalModels.Find(id);
            if (campaignGoalModel == null)
            {
                return HttpNotFound();
            }
            return View(campaignGoalModel);
        }

        // GET: CampaignGoal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CampaignGoal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CampaignGoalID,GoalName,Description,TargetCPC,TargetCTR,TargetCVR")] CampaignGoalModel campaignGoalModel)
        {
            if (ModelState.IsValid)
            {
                db.CampaignGoalModels.Add(campaignGoalModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(campaignGoalModel);
        }

        // GET: CampaignGoal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CampaignGoalModel campaignGoalModel = db.CampaignGoalModels.Find(id);
            if (campaignGoalModel == null)
            {
                return HttpNotFound();
            }
            return View(campaignGoalModel);
        }

        // POST: CampaignGoal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CampaignGoalID,GoalName,Description,TargetCPC,TargetCTR,TargetCVR")] CampaignGoalModel campaignGoalModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campaignGoalModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(campaignGoalModel);
        }

        // GET: CampaignGoal/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CampaignGoalModel campaignGoalModel = db.CampaignGoalModels.Find(id);
            if (campaignGoalModel == null)
            {
                return HttpNotFound();
            }
            return View(campaignGoalModel);
        }

        // POST: CampaignGoal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CampaignGoalModel campaignGoalModel = db.CampaignGoalModels.Find(id);
            db.CampaignGoalModels.Remove(campaignGoalModel);
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


        //Method to create a table showing the statistics of each goal
        //Returns a view that compares the goal and the current stats
        public ActionResult GoalStatsTable()
        {
            //Query database to find necessary data, then save it to a model object (CampaignGoalStatusModel) designed to hold all the data
            string query = "SELECT C.CampaignModelID, C.Name, CG.GoalName, C.CPC, C.CTR, C.CVR, CG.TargetCPC, CG.TargetCTR, CG.TargetCVR "
            + "FROM CampaignModels AS C "
            + "JOIN CampaignGoalModels AS CG ON C.CampaignGoalID = CG.CampaignGoalID ";

            //Run the query and save it to dataset
            IEnumerable<CampaignGoalStatusModel> dataset = db.Database.SqlQuery<CampaignGoalStatusModel>(query);

            //Return the view
            return View(dataset.ToList());
        }

        public ActionResult GoalStatsChart(int? id)
        {
            //If the campaign id is not passed properly the app will throw a bad request error
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //set the query string to query the database to retreive the sales data. 
            //The query will return the product name and how many of that product have been sold
            string query = "SELECT C.CampaignModelID, C.Name, CG.GoalName, C.CPC, C.CTR, C.CVR, CG.TargetCPC, CG.TargetCTR, CG.TargetCVR "
            + "FROM CampaignModels AS C "
            + "JOIN CampaignGoalModels AS CG ON C.CampaignGoalID = CG.CampaignGoalID "
            + "WHERE CampaignModelID = " + id;

            //Run the query and save the results as a order stats object(model specifically designed to campaign name and statistics
            IEnumerable<CampaignGoalStatusModel> dataset = db.Database.SqlQuery<CampaignGoalStatusModel>(query);

            //Pull the x axis (campaign names and stats) from the data set and place them in an array b/c the chart will only accept array objects
            //var xDataCampaign = dataset.Select(i => i.).ToArray();
            //Pull the y asix (campaign stats) from the data set and place them in an array
            var yDataCPC = dataset.Select(i => i.CPC).ToArray();
            var yDataTargetCPC = dataset.Select(i => i.TargetCPC).ToArray();
            var yDataCTR = dataset.Select(i => i.CTR).ToArray();
            var yDataTargetCTR = dataset.Select(i => i.TargetCTR).ToArray();
            var yDataCVR = dataset.Select(i => i.CVR).ToArray();
            var yDataTargetCVR = dataset.Select(i => i.TargetCVR).ToArray();

            //The chart plugin will plot chart objects from 'Series' objects.
            //Declare a list of series objects to hold the chart data
            List<Series> dataSeries = new List<Series>();

            //Declare a holder to hold each products data until it is added to the list of all products
            Series holder = new Series();

            //Place the current stats data in a temporary placeholder of the 'Series' type object
            holder = new Series
            {
                Name = "Current Stats",
                Data = new Data(new object[] { yDataCPC.ElementAt(0),
                    yDataCTR.ElementAt(0),
                    yDataCVR.ElementAt(0)
            })
            };

            //Add the data to a list with all the other data gathered thus far
            dataSeries.Add(holder);

            //Place the current stats data in a temporary placeholder of the 'Series' type object
            holder = new Series
            {
                Name = "Target Stats",
                Data = new Data(new object[] { yDataTargetCPC.ElementAt(0),
                    yDataTargetCTR.ElementAt(0),
                    yDataTargetCVR.ElementAt(0)
            })
            };

            //Add the data to a list with all the other data gathered thus far
            dataSeries.Add(holder);



            var chart = new Highcharts("chart")
                //define the type of chart
                .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Column })
                //overall title of the chart
                .SetTitle(new Title { Text = "Campaign Statistics" })
                //small label below the main title
                //.SetSubtitle(new Subtitle { Text = "" })
                //load the X axis values
                .SetXAxis(new XAxis { Categories = new[] { "CPC", "CTR", "CVR" } })
                //set the y title
                .SetYAxis(new YAxis { Title = new YAxisTitle { Text = "Value" } })
                //Set the tool tip, which displays info about the column when the user hovers over it.
                .SetTooltip(new Tooltip
                {
                    Enabled = true,
                    Formatter = @"function() {return '<b>' + this.series.name + '</b><br/>' + this.x + ': ' + this.y; }"
                })
                .SetPlotOptions(new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        DataLabels = new PlotOptionsBarDataLabels
                        {
                            Enabled = true
                        },
                        EnableMouseTracking = false
                    }
                })
                //load the y values
                //The data series must be converted from a list to an array before it can be used in a chart
                .SetSeries(dataSeries.ToArray());

            return View(chart);
        }

    }
}
