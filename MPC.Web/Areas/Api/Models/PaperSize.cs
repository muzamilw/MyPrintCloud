namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Paper Size DropDown
    /// </summary>
    public class PaperSizeDropDown
    {
        /// <summary>
        /// Paper Size Id
        /// </summary>
        public int PaperSizeId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Height
        /// </summary>
        public double? Height { get; set; }

        /// <summary>
        /// Width
        /// </summary>
        public double? Width { get; set; }

        /// <summary>
        /// Area
        /// </summary>
        public double? Area { get; set; }
    }
}