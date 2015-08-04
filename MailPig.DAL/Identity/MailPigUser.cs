namespace MailPig.DAL.Identity
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class MailPigUser : IdentityUser
    {
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Oib mora biti ispravne dužine.")]
        public string Oib { get; set; }

        public string OrganisationName { get; set; }

        public bool UsedForCommercialPurposes { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<MailPigUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}