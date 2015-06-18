namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// System Variable For List View
    /// </summary>
    public class SystemVariableForListView
    {
        public long VariableId { get; set; }
        public string VariableName { get; set; }
        public string ScopeName { get; set; }
        public string VariableTag { get; set; }
        public string TypeName { get; set; }
    }
}