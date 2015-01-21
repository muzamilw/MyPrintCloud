namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Cost Centre Matrix Domain Model
    /// </summary>
    public partial class CostCentreMatrix
    {
        public int MatrixId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RowsCount { get; set; }
        public int ColumnsCount { get; set; }
        public int CompanyId { get; set; }
        public int SystemSiteId { get; set; }
    }
}
