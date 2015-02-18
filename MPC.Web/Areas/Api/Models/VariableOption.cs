namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Variable Option API Model
    /// </summary>
    public class VariableOption
    {
        public long VariableOptionId { get; set; }
        public long? FakeId { get; set; }
        public long? VariableId { get; set; }
        public string Value { get; set; }
        public int? SortOrder { get; set; }
    }
}