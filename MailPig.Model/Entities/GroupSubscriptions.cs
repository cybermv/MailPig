namespace MailPig.Model.Entities
{
    using Core;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("GroupSubscriptions")]
    public class GroupSubscription : EntityBase
    {
        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        public Group Group { get; set; }

        public int SubscriberId { get; set; }

        [ForeignKey("SubscriberId")]
        public Subscriber Subscriber { get; set; }

        public DateTime DateJoined { get; set; }

        public DateTime? DateLeft { get; set; }
    }
}