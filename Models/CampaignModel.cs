using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Team11Project.Models
{
    public class CampaignModel
    {
        //Properties
        [Key]
        public int CampaignModelID { get; set; }
        [DisplayName("Campaign Name")]
        public string Name { get; set; }
        [DisplayName("Active Status")]
        [DefaultValue(true)]
        public bool IsActive { get; set; }
        [ForeignKey("CampaignGoal")]
        public int? CampaignGoalID { get; set; }
        [DisplayName("Campaign End Date")]
        public DateTime? EndCampaign { get; set; }
        [DisplayName("Campaign Budget")]
        public decimal Budget { get; set; }
        [DisplayName("Current Campaign Cost")]
        [DefaultValue(0)]
        public decimal FundingExpenditures { get; set; }
        [DisplayName("Subscriber Status")]
        [DefaultValue(false)]
        public bool IsSubscriber { get; set; }
        [DisplayName("Cost Per Click")]
        [DefaultValue(0)]
        public decimal CPC { get; set; }
        [DisplayName("Click Through Rate")]
        [DefaultValue(0)]
        public decimal CTR { get; set; }
        [DisplayName("Cost Per Conversion")]
        [DefaultValue(0)]
        public decimal CVR { get; set; }

        //Navigational proptery to the customers in the campaign
        public virtual List<CustomerModel> CampaignCustomers { get; set; }
        public virtual CampaignGoalModel CampaignGoal { get; set; }
    }
}