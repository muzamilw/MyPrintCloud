namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Phrase Domain Model
    /// </summary>
    public class Phrase
    {
        public int PhraseId { get; set; }
        public string Phrase1 { get; set; }
        public int? FieldId { get; set; }
        public long? CompanyId { get; set; }
        public long? OrganisationId { get; set; }

        public virtual PhraseField PhraseField { get; set; }
    }
}
