using System;
namespace MPC.Models.DomainModels
{
    [Serializable()]
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
