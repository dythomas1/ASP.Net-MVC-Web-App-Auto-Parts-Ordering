using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Team11Project.Models
{
    public class OrderItemModel
    {
        [Key]
        public int OrderItemID { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        [ForeignKey("CustomerOrder")]
        public int CustomerOrderID { get; set; }
        public int Quantity { get; set; }

        public virtual ProductModel Product { get; set; }
        public virtual CustomerOrderModel CustomerOrder { get; set; }
    }
}