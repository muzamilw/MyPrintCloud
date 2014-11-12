
namespace MPC.ExceptionHandling
{
    /// <summary>
    /// Cares Exception Contents
    /// </summary>
    public sealed class MPCExceptionContent
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Cares Exception Type
        /// </summary>
        public string ExceptionType { get { return MPCExceptionTypes.CaresGeneralException; } }

    }
}
