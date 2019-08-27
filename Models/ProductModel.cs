using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Team11Project.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        [ForeignKey("MarketSegment")]
        public int MarketSegmentID { get; set; }
        public double Price { get; set; }
        public double Cost { get; set; }

        public virtual MarketSegmentModel MarketSegment { get; set; }
    }
}