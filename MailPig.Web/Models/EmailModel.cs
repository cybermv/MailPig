namespace MailPig.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class EmailEditCreateModel
    {
        public int Id { get; set; }

        [Display(Name = "Subjekt")]
        [Required(ErrorMessage = "Subjekt je obavezan podatak")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Subjekt mora biti između 5 i 50 znakova")]
        public string Subject { get; set; }

        [AllowHtml]
        [Display(Name = "Poruka")]
        public string Body { get; set; }
    }
}