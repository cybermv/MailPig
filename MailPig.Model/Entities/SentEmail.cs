namespace MailPig.Model.Entities
{
    using Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SentEmails")]
    public class SentEmail : EntityBase
    {
        public int RecipientId { get; set; }

        [ForeignKey("RecipientId")]
        public GroupSubscription Recipient { get; set; }

        public int EmailId { get; set; }

        [ForeignKey("EmailId")]
        public Email Email { get; set; }

        public DateTime DateSent { get; set; }

        public virtual ICollection<EmailRead> Reads { get; set; }
    }
}