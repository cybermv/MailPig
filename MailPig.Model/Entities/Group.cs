namespace MailPig.Model.Entities
{
    using Core;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Groups")]
    public class Group : EntityBase
    {
        [StringLength(250)]
        public string Name { get; set; }

        public virtual ICollection<GroupSubscription> GroupSubscriptions { get; set; }
    }
}