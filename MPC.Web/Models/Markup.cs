namespace MPC.MIS.Models
{
    /// <summary>
    /// Markup Web Model
    /// </summary>
    public class Markup
    {
        /// <summary>
        /// Markup id
        /// </summary>
        public long MarkUpId { get; set; }

        /// <summary>
        /// Markup Name
        /// </summary>
        public string MarkUpName { get; set; }

        /// <summary>
        /// Markup Rate
        /// </summary>
        public double? MarkUpRate { get; set; }
    }
}