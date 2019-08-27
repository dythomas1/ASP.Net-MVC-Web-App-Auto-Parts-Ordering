using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team11Project.Models
{
    public class MarketSegmentModel
    {
        //Properties
        [Key]
        public int MarketSegmentID { get; set; }
        //Market Segments are divided by the car manufacturer the customer purchses (Basically the customers car brand)
        public string Manufacturer { get; set; }

        public virtual List<CustomerModel> SegmentCustomers { get; set; }
    }
}