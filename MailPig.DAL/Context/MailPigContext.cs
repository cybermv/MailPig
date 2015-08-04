namespace MailPig.DAL.Context
{
    using Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Entities;
    using System.Data.Entity;

    public class MailPigContext : IdentityDbContext<MailPigUser>
    {
        public MailPigContext()
            : base("MailPigContext")
        {
        }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<GroupSubscription> GroupSubscriptions { get; set; }

        public DbSet<Email> Emails { get; set; }

        public DbSet<SentEmail> SentEmails { get; set; }

        public DbSet<EmailRead> EmailReads { get; set; }
    }
}