using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    public class GlobalLanguage
    {
        public long LanguageId { get; set; }
        public string FriendlyName { get; set; }
        public string uiCulture { get; set; }
        public string culture { get; set; }

        public virtual ICollection<Organisation> Organisations { get; set; }
    }
}
