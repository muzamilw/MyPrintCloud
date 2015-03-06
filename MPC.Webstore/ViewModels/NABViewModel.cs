﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MPC.Webstore.ViewModels
{
    public class NABViewModel
    {
       
        [Required(ErrorMessage = "Card Number  Can't be blank")]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }
         [MaxLength(3, ErrorMessage = "CVV Number cannot be longer than 3 characters.")]
         [Display(Name = "CVV Number")]
        public string CVVNumber { get; set; }
        [Display(Name = "Name on the Card")]
         public string NameOnCard { get; set; }
         
         [EmailAddress]
         [Display(Name = "Email Address(optional)")]
         public string EmailAddress { get; set; }
        [Display(Name = "Card Type  ")]
         public int SelectedCardType { get; set; }
        [Display(Name = "Expiry")]
        public string SelectedDate { get; set; }
        public string SelectedYear { get; set; }
        public IEnumerable<SelectListItem> ListCardType { get; set; }
        public IEnumerable<SelectListItem> ListDate { get; set; }
        public IEnumerable<SelectListItem> ListYear { get; set; }
        
    }
    public class DropdownType
    {
        //public DropdownType(int val, string )
        public String value { get; set; }
        public String Text { get; set; }
    }
    public class DropdownCardType
    {
       
        public int value { get; set; }
        public String Text { get; set; }
    }
}