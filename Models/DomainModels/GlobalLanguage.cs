namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Global Language Domain Model
    /// </summary>
    public class GlobalLanguage
    {
        public long LanguageId { get; set; }
        public string FriendlyName { get; set; }
        public string uiCulture { get; set; }
        public string culture { get; set; }
    }
}
