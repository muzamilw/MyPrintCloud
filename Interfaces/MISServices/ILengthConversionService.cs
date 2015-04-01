using System.Collections.Generic;

namespace MPC.Interfaces.MISServices
{
    /// <summary>
    /// Length Conversion Service Interface
    /// </summary>
    public interface ILengthConversionService
    {
        /// <summary>
        /// Converts input length unit to specified output unit
        /// </summary>
        List<double> ConvertLengthFromSystemUnitToPoints(List<double> inputs);

        /// <summary>
        /// Converts input length unit to specified output unit
        /// </summary>
        List<double> ConvertLengthFromPointsToSystemUnit(List<double> inputs);
    }
}
