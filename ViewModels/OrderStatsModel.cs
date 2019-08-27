using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team11Project.Models
{
    //Model used when creating the product sales chart and table
    //The product sales chart will show which items have sold the greatest quantity
    public class OrderStats
    {
        public string ProductName { get; set;}
        public int Quantity { get; set; }
    }


}