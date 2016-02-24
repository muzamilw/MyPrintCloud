using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MPC.Webstore.ViewModels
{
    public class StripeViewModel
    {
        public string PublishableKey { get; set; }

        public int OrderId { get; set; }
       
        [Required(ErrorMessage = "Card Number Can't be blank")]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }
         [MaxLength(4, ErrorMessage = "CVV Number cannot be longer than 3 characters.")]
         [Display(Name = "CVV Number")]
        public string CVVNumber { get; set; }

        public string stripeToken { get; set; }

         public double OrderTotal { get; set; }
         public string Currency { get; set; }

         [Display(Name = "Expiry")]
         public string SelectedDate { get; set; }
         public string SelectedYear { get; set; }
  
        public IEnumerable<SelectListItem> ListCardType { get; set; }
        public IEnumerable<SelectListItem> ListDate { get; set; }
        public IEnumerable<SelectListItem> ListYear { get; set; }
      
        
    }

}