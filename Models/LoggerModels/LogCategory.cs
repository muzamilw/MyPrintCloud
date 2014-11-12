using System.Collections.Generic;

namespace MPC.Models.LoggerModels
{
    /// <summary>
    /// Log Category Class for database logging
    /// </summary>
    public class LogCategory
    {
        /// <summary>
        /// LogCategory Id
        /// </summary>
        public int LogCategoryId { get; set; }

        /// <summary>
        /// Category Name
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Category Logs
        /// </summary>
        public virtual ICollection<CategoryLog> CategoryLogs { get; set; } 
    }
}
