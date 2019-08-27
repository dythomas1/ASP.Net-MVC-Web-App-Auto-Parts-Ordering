using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11Project.ViewModels
{
    //Model used to hold all the queried data when creating the campaign stats chart
    public class CampaignStatsModel
    {
        public int CampaignModelID { get; set; }
        public string Name { get; set; }
        public decimal CPC { get; set; }
        public decimal CTR { get; set; }
        public decimal CVR { get; set; }
    }
}