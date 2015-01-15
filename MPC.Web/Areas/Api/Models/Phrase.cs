namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Phrase Api Model
    /// </summary>
    public class Phrase
    {
        public int PhraseId { get; set; }
        public string Phrase1 { get; set; }
        public int? FieldId { get; set; }
        public bool IsDeleted { get; set; }
    }
}