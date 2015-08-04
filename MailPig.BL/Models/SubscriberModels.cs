namespace MailPig.BL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SubscriberModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} je obavezno polje")]
        [Display(Name = "Ime")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Ime mora biti između 3 i 50 znakova")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} je obavezno polje")]
        [Display(Name = "Prezime")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Prezime mora biti između 3 i 50 znakova")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "{0} je obavezno polje")]
        [Display(Name = "E-mail adresa")]
        [EmailAddress(ErrorMessage = "E-mail adresa nije u ispravnom formatu")]
        public string Email { get; set; }
    }

    public class SubscriberGroupsModel : SubscriberModel
    {
        public SubscriberGroupsModel()
        {
            GroupsIn = new List<string>();
        }

        public List<string> GroupsIn { get; set; }
    }

    public class SubscriberWithFirstGroupModel : SubscriberModel
    {
        public int? FirstGroupId { get; set; }
    }

    public class SubscriberHistoryModel
    {
        public int Id { get; set; }

        public string GroupName { get; set; }

        public DateTime DateJoined { get; set; }

        public DateTime? DateLeft { get; set; }
    }
}