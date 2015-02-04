using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class QuickTextData
    {
        public string AddressLine1 { get; set; }
        public string CompanyName { get; set; }
        public string CompanyMessage { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public string Website { get; set; }

        public QuickTextData(string AddressLine1, string CompanyName, string CompanyMessage, string Email, string Fax, string Name, string Phone, string Title, string Website)
        {
            this.AddressLine1 = AddressLine1;
            this.CompanyMessage = CompanyMessage;
            this.CompanyName = CompanyName;
            this.Email = Email;
            this.Fax = Fax;
            this.Name = Name;
            this.Phone = Phone;
            this.Title = Title;
            this.Website = Website;
        }
    }
}
