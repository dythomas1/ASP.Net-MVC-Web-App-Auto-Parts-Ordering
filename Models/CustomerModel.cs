using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Team11Project.Models
{
    public class CustomerModel
    {
        [Key]
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [DisplayName("Last Login Date")]
        public DateTime? LastLogin { get; set; }
        [DisplayName("Sign-Up Date")]
        public DateTime? SignupDate { get; set; }
        [DisplayName("Home Address")]
        public string MailingAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        [DisplayName("Market Segment")]
        [ForeignKey("MarketSegmentModel")]
        public int? MarketSegmentID { get; set; }
        [DisplayName("Campaign Model")]
        [ForeignKey("CampaignModel")]
        public int? CampaignModelID { get; set; }

        public virtual MarketSegmentModel MarketSegmentModel { get; set; }
        public virtual CampaignModel CampaignModel { get; set; }
    }
}