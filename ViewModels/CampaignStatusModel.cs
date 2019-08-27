using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11Project.ViewModels
{
    //Model used to hold the data used for the campaign stats table
    public class CampaignStatusModel
    {        
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime EndCampaign { get; set; }
        public decimal Budget { get; set; }
        public decimal FundingExpenditures { get; set; }
    }
}