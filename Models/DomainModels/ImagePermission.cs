namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Image Permission Domain Model
    /// </summary>
    public class ImagePermission
    {
        public long Id { get; set; }
        public long? TerritoryID { get; set; }
        public long? ImageId { get; set; }

        public virtual TemplateBackgroundImage TemplateBackgroundImage { get; set; }
    }
}
