namespace MailPig.Model.Entities
{
    using Core;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EmailReads")]
    public class EmailRead : EntityBase
    {
        public int SentEmailId { get; set; }

        [ForeignKey("SentEmailId")]
        public SentEmail SentEmail { get; set; }

        public DateTime ReadDateTime { get; set; }

        [StringLength(39)] // IPv6 length
        public string IpAddress { get; set; }
    }
}