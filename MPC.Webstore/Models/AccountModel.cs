
using System.ComponentModel.DataAnnotations;

namespace MPC.Webstore.Models
{
    public class AccountModel
    {
        [Required(ErrorMessage = "Invalid Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress()]
        [Display(Name = "Email id")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Invalid Password")]
        [DataType(DataType.Text)]
        [Display(Name = "Password")]
        public string Password { get; set; }



        [Display(Name = "KeepMeLoggedIn")]
        public bool KeepMeLoggedIn { get; set; }


    }
}