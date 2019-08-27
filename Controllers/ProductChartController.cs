using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11Project.Models;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;

namespace Team11Project.Controllers
{
    public class ProductChartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProductChart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductSalesTable()
        {
            //set the query string to query the database to retreive the sales data. 
            //The query will return the product name and how many of that product have been sold
            string query = "SELECT P.ProductName, Sum(OI.Quantity) AS Quantity "
                            + "FROM OrderItemModels AS OI "
                            + "JOIN ProductModels AS P ON OI.ProductID = P.ProductID "
                            + "GROUP BY P.ProductName "
                            + "ORDER BY Quantity DESC ";
            //Run the query and save the results as a order stats object(model specifically designed to hold the product and quantity
            IEnumerable<OrderStats> dataset = db.Database.SqlQuery<OrderStats>(query);

            //Return the view and send the data that was gathered from the db in the form of a list
            return View(dataset.ToList());
        }

        public ActionResult ProductSalesChart()
        {
            //set the query string to query the database to retreive the sales data. 
            //The query will return the product name and how many of that product have been sold
            string query = "SELECT TOP(10) P.ProductName, Sum(OI.Quantity) AS Quantity "
                            + "FROM OrderItemModels AS OI "
                            + "JOIN ProductModels AS P ON OI.ProductID = P.ProductID "
                            + "GROUP BY P.ProductName "
                            + "ORDER BY Quantity DESC ";

            //Run the query and save the results as a order stats object(model specifically designed to hold the product and quantity
            IEnumerable<OrderStats> dataset = db.Database.SqlQuery<OrderStats>(query);

            //Pull the x axis (product names) from the data set and place them in an array b/c the chart will only accept array objects
            var xDataProducts = dataset.Select(i => i.ProductName).ToArray();
            //Pull the y asix (quantity sold) from the data set and place them in an array
            var yDataQuantity = dataset.Select(i => i.Quantity).ToArray();

            //The chart plugin will plot chart objects from 'Series' objects.
            //Declare a list of series objects to hold the chart data
            List<Series> dataSeries = new List<Series>();
            //Declare a holder to hold each products data until it is added to the list of all products
            Series holder = new Series();

            //Place each products data into a list of all products data
            foreach (var i in xDataProducts)
            {
                //Pull the data and place it in a temporary placeholder of the 'Series' type object
                holder = new Series { Name = i, Data = new Data(new object[] { yDataQuantity.ElementAt(Array.IndexOf(xDataProducts, i)) }) };
                //Add the data to a list with all the other data gathered thus far
                dataSeries.Add(holder);
            }

            var chart = new Highcharts("chart")
                //define the type of chart
                .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Column })
                //overall title of the chart
                .SetTitle(new Title { Text = "Best Selling Products" })
                //small label below the main title
                .SetSubtitle(new Subtitle { Text = "Products by Quantity Sold" })
                //load the X axis values
                .SetXAxis(new XAxis { Categories = new[] { "Products" } })
                //set the y title
                .SetYAxis(new YAxis { Title = new YAxisTitle { Text = "Quantity Sold" } })
                //Set the tool tip, which displays info about the column when the user hovers over it.
                .SetTooltip(new Tooltip
                {
                    Enabled = true,
                    Formatter = @"function() {return '<b>' + this.series.name + '</b><br/>' + 'Quantity' + ': ' + this.y; }"
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
                .SetSeries( dataSeries.ToArray() );

            return View(chart);
        }
    }
}