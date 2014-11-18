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
        public MPCException(string message, int domainKey) : base(message)
        {
            DomainKey = domainKey;
        }

        /// <summary>
        /// Initializes a new instance of Cares Exception
        /// </summary>
        public MPCException(string message, int domainKey, Exception innerException)
            : base(message, innerException)
        {
            DomainKey = domainKey;
        }

        public int DomainKey { get; set; }
    }
}
