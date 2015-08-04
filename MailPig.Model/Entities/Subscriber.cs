namespace MailPig.Model.Entities
{
    using Core;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Subscribers")]
    public class Subscriber : EntityBase
    {
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        public virtual ICollection<GroupSubscription> GroupSubscriptions { get; set; }

        public virtual ICollection<SentEmail> SentEmails { get; set; }
    }
}