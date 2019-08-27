using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team11Project.Models
{
    public class CampaignGoalModel
    {
        [Key]
        public int CampaignGoalID {get; set;}
        [DisplayName("Campaign Goal Name")]
        public string GoalName { get; set; }
        [DisplayName("Description of Goal")]
        public string Description { get; set; }
        [DisplayName("Target CPC of Goal")]
        public decimal TargetCPC { get; set; }
        [DisplayName("Target CTR of Goal")]
        public decimal TargetCTR { get; set; }
        [DisplayName("Target CVR of Goal")]
        public decimal TargetCVR { get; set; }

        public virtual List<CampaignModel> CampaignModels { get; set; }
    }
}