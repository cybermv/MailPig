using System.ComponentModel.DataAnnotations;

namespace MailPig.Web.Models
{
    using DAL.Identity;

    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "{0} je obavezno polje.")]
        [EmailAddress(ErrorMessage = "Nije unesena valjana e-mail adresa.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string Action { get; set; }

        public string ReturnUrl { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required(ErrorMessage = "{0} je obavezno polje.")]
        [DataType(DataType.Password)]
        [Display(Name = "Trenutna lozinka")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "{0} je obavezno polje.")]
        [StringLength(100, ErrorMessage = "{0} mora imati minimalno {2} znakova.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova lozinka")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrda nove lozinke")]
        [Compare("NewPassword", ErrorMessage = "Nove lozinke se moraju podudarati.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} je obavezno polje.")]
        [EmailAddress(ErrorMessage = "Nije unesena valjana e-mail adresa.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} je obavezno polje.")]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [Display(Name = "Zapamti me")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "{0} je obavezno polje.")]
        [EmailAddress(ErrorMessage = "Nije unesena valjana e-mail adresa.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} je obavezno polje.")]
        [StringLength(100, ErrorMessage = "{0} mora imati minimalno {2} znakova.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrda nove lozinke")]
        [Compare("Password", ErrorMessage = "Lozinke se moraju podudarati.")]
        public string ConfirmPassword { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "Oib mora biti ispravne dužine.")]
        public string Oib { get; set; }

        [Display(Name = "Za komercijalnu uporabu")]
        public bool UsedForCommercialPurposes { get; set; }

        [StringLength(50, ErrorMessage = "Ime organizacije nesmije biti duže od 50 znakova.")]
        [Display(Name = "Ime organizacije")]
        public string OrganisationName { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "{0} je obavezno polje.")]
        [EmailAddress(ErrorMessage = "Nije unesena valjana e-mail adresa.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} je obavezno polje.")]
        [StringLength(100, ErrorMessage = "{0} mora imati minimalno {2} znakova.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrda nove lozinke")]
        [Compare("Password", ErrorMessage = "Nove lozinke se moraju podudarati.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "{0} je obavezno polje.")]
        [EmailAddress(ErrorMessage = "Nije unesena valjana e-mail adresa.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}