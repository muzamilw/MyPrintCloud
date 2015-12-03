namespace MPC.MIS.Areas.Api.Models
{
    public class StockSubCategory
    {
        public long SubCategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Fixed { get; set; }
        public long CategoryId { get; set; }
        public long? OrganisationId { get; set; }
    }
}