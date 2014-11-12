using System;

namespace MPC.ExceptionHandling
{
    /// <summary>
    /// Cares Exception
    /// </summary>
    public sealed class MPCException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of MPC Exception
        /// </summary>
        public MPCException(string message): base(message)
        {            
        }
        /// <summary>
        /// Initializes a new instance of Cares Exception
        /// </summary>
        public MPCException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
