using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11Project.Models;
using Team11Project.ViewModels;

namespace Team11Project.Controllers
{
    public class CampaignChartController : Controller
    {
            private ApplicationDbContext db = new ApplicationDbContext();

            // GET: ProductChart
            public ActionResult Index()
            {
                return View();
            }

            public ActionResult CampaignStatsTable()
            {
                //set the query string to query the database to retreive the campaign data. 
                //The query will return the campaign id, name, and all of its statistics
                string query = "SELECT CampaignModelID, Name, CPC, CTR, CVR "
                                + "FROM CampaignModels "
                                + "ORDER BY Name DESC ";
                //Run the query and save the results as a campaign stats object(model specifically designed to hold the campaign statistics)
                IEnumerable<CampaignStatsModel> dataset = db.Database.SqlQuery<CampaignStatsModel>(query);

                //Return the view and send the data that was gathered from the db in the form of a list
                return View(dataset.ToList());
            }

            public ActionResult CampaignStatsChart()
            {
                //set the query string to query the database to retreive the campaign data. 
                //The query will return the campaign name and all of its stats
                string query = "SELECT  Name, CPC, CTR, CVR "
                                + "FROM CampaignModels "
                                + "WHERE IsActive = 1 "
                                + "ORDER BY Name DESC ";

                //Run the query and save the results as a campaign stats model(model specifically designed to hold the campaign and its statistics)
                IEnumerable<CampaignStatsModel> dataset = db.Database.SqlQuery<CampaignStatsModel>(query);

                //Pull the x axis (campaign name) from the data set and place them in an array b/c the chart will only accept array objects
                var xDataCampaigns = dataset.Select(i => i.Name).ToArray();
                //Pull the y asix (CPC, CTR, CVR) from the data set and place them in an array
                var yDataCPC = dataset.Select(i => i.CPC).ToArray();
                var yDataCTR = dataset.Select(i => i.CTR).ToArray();
                var yDataCVR = dataset.Select(i => i.CVR).ToArray();

                //The chart plugin will plot chart objects from 'Series' objects.
                //Declare a list of series objects to hold the chart data
                List<Series> dataSeries = new List<Series>();

                //Declare a holder to hold each products data until it is added to the list of all products
                Series holder = new Series();

                //Place each statistic into its proper series
                foreach (var i in xDataCampaigns)
                {
                    //Pull the data and place it in a temporary placeholder of the 'Series' type object
                    holder = new Series { Name = i, Data = new Data(new object[] { yDataCPC.ElementAt(Array.IndexOf(xDataCampaigns, i)),
                        yDataCTR.ElementAt(Array.IndexOf(xDataCampaigns, i)),
                        yDataCVR.ElementAt(Array.IndexOf(xDataCampaigns, i))}) };

                    //Add the data to a list with all the other data gathered thus far
                    dataSeries.Add(holder);
                }

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