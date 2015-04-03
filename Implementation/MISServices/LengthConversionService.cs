using MPC.Interfaces.MISServices;
using MPC.Models.Common;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Length Conversion Service
    /// </summary>
    public class LengthConversionService : ILengthConversionService
    {
        #region Private

        #endregion

        #region Constructor

        #endregion

        #region Public

        /// <summary>
        /// Converts length to a specified output unit
        /// </summary>
        public double ConvertLengthFromSystemUnitToPoints(double input, Models.DomainModels.LengthUnit systemUnit)
        {

            double outputvalue;

            if (systemUnit.Id == (int)LengthUnit.Inch)
            {
                outputvalue = LengthConversionHelper.ConvertLength(input, LengthUnit.Inch, systemUnit);
                outputvalue = LengthConversionHelper.MmToPoint(outputvalue);
            }
            else if (systemUnit.Id == (int)LengthUnit.Cm)
            {
                outputvalue = LengthConversionHelper.ConvertLength(input, LengthUnit.Cm, systemUnit);
                outputvalue = LengthConversionHelper.MmToPoint(outputvalue);
            }
            else
            {
                outputvalue = LengthConversionHelper.MmToPoint(input);
            }

            return outputvalue;
        }

        /// <summary>
        /// Converts length to a specified output unit
        /// </summary>
        public double ConvertLengthFromPointsToSystemUnit(double input, Models.DomainModels.LengthUnit systemUnit)
        {

            double outputvalue;

            if (systemUnit.Id == (int)LengthUnit.Inch)
            {
                outputvalue = LengthConversionHelper.PointToMm(input);
                outputvalue = LengthConversionHelper.ConvertLength(outputvalue, LengthUnit.Inch, systemUnit);
            }
            else if (systemUnit.Id == (int)LengthUnit.Cm)
            {
                outputvalue = LengthConversionHelper.PointToMm(input);
                outputvalue = LengthConversionHelper.ConvertLength(outputvalue, LengthUnit.Cm, systemUnit);
            }
            else
            {
                outputvalue = LengthConversionHelper.PointToMm(input);
            }

            return outputvalue;
        }

        #endregion
    }
}
