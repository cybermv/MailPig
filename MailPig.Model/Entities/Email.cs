namespace MailPig.Model.Entities
{
    using Core;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Emails")]
    public class Email : EntityBase
    {
        [StringLength(250)]
        public string Subject { get; set; }

        [StringLength(5000)]
        public string Body { get; set; }

        public virtual ICollection<SentEmail> Sends { get; set; }
    }
}