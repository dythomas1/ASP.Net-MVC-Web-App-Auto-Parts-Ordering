using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Team11Project.Models
{
    public class CustomerOrderModel
    {
        [Key]
        public int CustomerOrderID { get; set; }
        //[ForeignKey("OrderItem")]
        //public int OrderItemID { get; set; }
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual CustomerModel Customer { get; set; }
        public virtual List<OrderItemModel> OrderItem { get; set; }
    }
}