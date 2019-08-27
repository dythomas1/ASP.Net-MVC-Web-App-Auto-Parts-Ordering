using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11Project.ViewModels
{
    //Model to hold all the data retreived when creating the campaign goal table and charts
    public class CampaignGoalStatusModel
    {
        public int CampaignModelID { get; set; }
        public string Name { get; set; }
        public string GoalName { get; set; }
        public decimal CPC { get; set; }
        public decimal CTR { get; set; }
        public decimal CVR { get; set; }
        public decimal TargetCPC { get; set; }
        public decimal TargetCTR { get; set; }
        public decimal TargetCVR { get; set; }
    }
}