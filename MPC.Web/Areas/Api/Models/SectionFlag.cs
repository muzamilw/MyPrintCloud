namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Section Flag WebApi Model
    /// </summary>
    public class SectionFlag
    {
        /// <summary>
        /// Section Id
        /// </summary>
        public int SectionFlagId { get; set; }

        /// <summary>
        /// Section Id
        /// </summary>
        public int? SectionId { get; set; }

        /// <summary>
        /// Flag Name
        /// </summary>
        public string FlagName { get; set; }

        /// <summary>
        /// Flag Color
        /// </summary>
        public string FlagColor { get; set; }

        /// <summary>
        /// Flag Description
        /// </summary>
        public string FlagDescription { get; set; }

        /// <summary>
        /// Company Id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Flag Column
        /// </summary>
        public string FlagColumn { get; set; }

        /// <summary>
        /// Is Default
        /// </summary>
        public bool? IsDefault { get; set; }
    }
}