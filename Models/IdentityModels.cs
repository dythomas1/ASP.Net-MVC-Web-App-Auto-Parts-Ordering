using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;

namespace Team11Project.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        //Add extra user properties here
        //Instructions on how to add: http://aspmvp.com/archives/37
        public string Name { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Team11Project.Models.MarketSegmentModel> MarketSegmentModels { get; set; }

        public System.Data.Entity.DbSet<Team11Project.Models.CampaignModel> CampaignModels { get; set; }

        public System.Data.Entity.DbSet<Team11Project.Models.CustomerModel> CustomerModels { get; set; }

        public System.Data.Entity.DbSet<Team11Project.Models.OrderItemModel> OrderItemModels { get; set; }

        public System.Data.Entity.DbSet<Team11Project.Models.CustomerOrderModel> CustomerOrderModels { get; set; }

        public System.Data.Entity.DbSet<Team11Project.Models.ProductModel> ProductModels { get; set; }

        public System.Data.Entity.DbSet<Team11Project.Models.CampaignGoalModel> CampaignGoalModels { get; set; }

        //public System.Data.Entity.DbSet<Team11Project.Models.OrderStatsModel> OrderStatsModels { get; set; }
    }
}