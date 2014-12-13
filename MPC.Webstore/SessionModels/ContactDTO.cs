using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Webstore.SessionModels
{
    public class ContactDTO
    {
        public long ContactId { get; set; }
        public long CompanyId { get; set; }
        public long AddressId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
     
     
        public int? ContactRoleId { get; set; }
        public long? TerritoryId { get; set; }
       
        public bool? IsPricingshown { get; set; }
        public string twitterScreenName { get; set; }


        public bool? CanUserEditProfile { get; set; }
    
    }
}