using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class GlobalLanguage
    {
        public long LanguageId { get; set; }
        public string FriendlyName { get; set; }
        public string uiCulture { get; set; }
        public string culture { get; set; }
    }
}
