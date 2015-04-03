using LengthUnit = MPC.Models.DomainModels.LengthUnit;

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
        double ConvertLengthFromSystemUnitToPoints(double input, LengthUnit systemUnit);

        /// <summary>
        /// Converts input length unit to specified output unit
        /// </summary>
        double ConvertLengthFromPointsToSystemUnit(double input, LengthUnit systemUnit);
    }
}
