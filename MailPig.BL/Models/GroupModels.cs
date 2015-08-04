namespace MailPig.BL.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class GroupModel
    {
        public int Id { get; set; }

        [Display(Name = "Naziv grupe")]
        public string Name { get; set; }
    }

    public class TableGroupModel : GroupModel
    {
        public int CurrentSubscribers { get; set; }
    }

    public class GroupWithSubscribersModel : GroupModel
    {
        public IEnumerable<SubscriberModel> Subscribers { get; set; }
    }
}