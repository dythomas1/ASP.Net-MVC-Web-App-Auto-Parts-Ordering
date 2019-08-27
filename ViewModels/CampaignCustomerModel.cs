using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11Project.Models
{
    //Class that is used as a placeholder between the customer and campaign classes
    //Needed this class so that I could hold the CampaignID and Customer ID and pass it through to a view and back from a view
    public class CampaignCustomerModel
    {
        public int CustomerID { get; set; }
        public int? CampaignID { get; set; }
        public string CustomerName { get; set; }

        public bool Check { get; set; }
        




    }
}