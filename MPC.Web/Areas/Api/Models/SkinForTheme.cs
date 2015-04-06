namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Skin For Theme
    /// </summary>
    public class SkinForTheme
    {
        public long SkinId { get; set; }
        public string Name { get; set; }
        public int? Type { get; set; }
        public string FullZipPath { get; set; }
        public string Thumbnail { get; set; }
    }
}