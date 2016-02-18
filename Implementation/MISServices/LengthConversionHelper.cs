using System;
using System.Security.Cryptography.X509Certificates;
using MPC.Models.Common;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Length Conversion Helper
    /// </summary>
    public static class LengthConversionHelper
    {

        #region Private

        /// <summary>
        /// Converts Point to MM Unit
        /// </summary>
        public static double PointToMm(double val)
        {
            return val / 2.834645669;
        }

        /// <summary>
        /// Converts Mm to Point
        /// </summary>
        public static double MmToPoint(double val)
        {
            return val * 2.834645669;
        }

        /// <summary>
        /// Converts Point to Pixel
        /// </summary>
        public static double PointToPixel(double val)
        {
            return val * 96 / 72;
        }

        /// <summary>
        /// Converts Pixel To Point
        /// </summary>
        public static double PixelToPoint(double val)
        {
            return val/96*72;



        }

        

        public static double InchtoPoint(double val)
        {
            return val*25.4*2.834645669;

        }

        public static double PointToInch(double val)
        {
            return val / (25.4 * 2.834645669);

        }

        #endregion

        #region Public

        /// <summary>
        /// Converts Input unit to Specified Output Unit
        /// </summary>
        public static double ConvertLength(double input, LengthUnit outputUnit, Models.DomainModels.LengthUnit systemUnit)
        {
            
            double conversionUnit = 0;
            if (systemUnit != null)
            {
                switch (outputUnit)
                {
                    case LengthUnit.Cm:
                        conversionUnit = systemUnit.CM.HasValue ? Math.Round(input * (double)systemUnit.CM, 3) : 0;
                        break;
                    case LengthUnit.Inch:
                        conversionUnit = systemUnit.Inch.HasValue ? Math.Round(input * (double)systemUnit.Inch, 3) : 0;
                        break;
                    case LengthUnit.Mm:
                        conversionUnit = systemUnit.MM.HasValue ? (double)systemUnit.MM : 0;
                        conversionUnit = input*conversionUnit;
                        break;
                }

            }
            else
            {
                return 0;
            }

            return conversionUnit;
        }

       
        #endregion
    }
}
