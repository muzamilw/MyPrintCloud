using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MPC.Webstore.Models
{
    public class AccountViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress()]
        [Display(Name = "Email id")]

        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Password")]
        public string Password { get; set; }



        [Display(Name = "KeepMeLoggedIn")]
        public bool KeepMeLoggedIn { get; set; }

        public string ReturnURL { get; set; }


    }
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class ItemCartViewModel
    {
        public string ItemId { get; set; }

        public string OrderId { get; set; }

        public string JsonPriceMatrix { get; set; }

        public string JsonAddOnsPrice { get; set; }

        public string isTaxApplied { get; set; }

        public string TaxRate { get; set; }

        public string StockId { get; set; }
    }


    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string ReturnURL { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class ContactViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress()]
        [Display(Name = "Email id")]

        public string Email { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "Your name")]
        public string YourName { get; set; }




        [DataType(DataType.Text)]
        [Display(Name = "Company name")]
        public string CompanyName { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "Enquiry")]
        public string YourEnquiry { get; set; }


    }
 
}
