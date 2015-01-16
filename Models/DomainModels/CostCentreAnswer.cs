namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Cost Centre Answer
    /// </summary>
    public class CostCentreAnswer
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public double? AnswerString { get; set; }
    }
}
