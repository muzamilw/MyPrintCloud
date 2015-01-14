namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Favorite Design Domain Model
    /// </summary>
    public class FavoriteDesign
    {
        public int FavoriteDesignId { get; set; }
        public int TemplateId { get; set; }
        public long ItemId { get; set; }
        public long ContactUserId { get; set; }
        public bool IsFavorite { get; set; }
        public int CategoryId { get; set; }

        public virtual Item Item { get; set; }
        public virtual CompanyContact CompanyContact { get; set; }
    }
}
