namespace MPC.Models.LoggerModels
{
    /// <summary>
    /// Category Log class for database logging
    /// </summary>
    public class CategoryLog
    {
        /// <summary>
        /// Category Log Id
        /// </summary>
        public int CategoryLogId { get; set; }

        /// <summary>
        /// LogCategory Id
        /// </summary>
        public int LogCategoryId { get; set; }

        /// <summary>
        /// Log Id
        /// </summary>
        public int LogId { get; set; }

        /// <summary>
        /// Log Category
        /// </summary>
        public virtual LogCategory LogCategory { get; set; }

        /// <summary>
        /// Log 
        /// </summary>
        public virtual Log Log { get; set; }

    }
}
