using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using System;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Length Conversion Service
    /// </summary>
    public class LengthConversionService : ILengthConversionService
    {
        #region Private
        private readonly IItemSectionRepository itemsectionRepository;
        #endregion

        #region Constructor
        public LengthConversionService(IItemSectionRepository itemsectionRepository)
        {
            if (itemsectionRepository == null)
            {
                throw new ArgumentNullException("itemsectionRepository");
            }
            this.itemsectionRepository = itemsectionRepository;
            
         }

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
                //outputvalue = LengthConversionHelper.ConvertLength(input, LengthUnit.Mm, systemUnit);
                //outputvalue = LengthConversionHelper.MmToPoint(outputvalue);
                outputvalue = LengthConversionHelper.InchtoPoint(input);
            }
            else if (systemUnit.Id == (int)LengthUnit.Cm)
            {
                outputvalue = LengthConversionHelper.ConvertLength(input, LengthUnit.Mm, systemUnit);
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
                //outputvalue = LengthConversionHelper.PointToMm(input);
                //outputvalue = ConvertLength(outputvalue, LengthUnit.Mm, LengthUnit.Inch);
                outputvalue = LengthConversionHelper.PointToInch(input);
            }
            else if (systemUnit.Id == (int)LengthUnit.Cm)
            {
                outputvalue = LengthConversionHelper.PointToMm(input);
                outputvalue = ConvertLength(outputvalue, LengthUnit.Mm, LengthUnit.Cm);
            }
            else
            {
                outputvalue = LengthConversionHelper.PointToMm(input);
            }

            return outputvalue;
        }
        // convert length 
        //  
        public double ConvertLength(double Input, LengthUnit InputUnit, LengthUnit OutputUnit)
        {
            double ConversionUnit = 0;
            MPC.Models.DomainModels.LengthUnit oRows = itemsectionRepository.GetLengthUnitByInput((int)InputUnit);
            if (oRows != null)
            {
                switch (OutputUnit)
                {
                    case LengthUnit.Cm:
                        ConversionUnit = (double)oRows.CM;
                        break;
                    case LengthUnit.Inch:
                        ConversionUnit = (double)oRows.Inch;
                        break;
                    case LengthUnit.Mm:
                        ConversionUnit = (double)oRows.MM;
                        break;
                }
            }

            return Input * Math.Round(ConversionUnit, 2);
        }
        #endregion
    }
}
