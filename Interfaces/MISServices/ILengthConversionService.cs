using System.Collections.Generic;
using MPC.Models.Common;

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
        List<double> ConvertLengthToPoints(List<double> inputs, LengthUnit inputUnit, LengthUnit outputUnit);

        /// <summary>
        /// Converts input length unit to specified output unit
        /// </summary>
        List<double> ConvertLengthFromPoints(List<double> inputs, LengthUnit inputUnit, LengthUnit outputUnit);
    }
}
