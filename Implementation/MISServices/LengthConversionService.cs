using System;
using System.Collections.Generic;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using LengthUnit = MPC.Models.Common.LengthUnit;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Length Conversion Service
    /// </summary>
    public class LengthConversionService : ILengthConversionService
    {
        #region Private
        
        private readonly IOrganisationRepository organisationRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public LengthConversionService(IOrganisationRepository organisationRepository)
        {
            if (organisationRepository == null)
            {
                throw new ArgumentNullException("organisationRepository");
            }

            this.organisationRepository = organisationRepository;
        }

        #endregion
        
        #region Public

        /// <summary>
        /// Converts length to a specified output unit
        /// </summary>
        public List<double> ConvertLengthFromSystemUnitToPoints(List<double> inputs)
        {
            Organisation organisation = organisationRepository.GetOrganizatiobByID();

            if (organisation == null)
            {
                return new List<double>();
            }

            if (organisation.LengthUnit == null)
            {
                return new List<double>();
            }

            if (inputs == null)
            {
                return new List<double>();
            }

          // ReSharper disable SuggestUseVarKeywordEvident
            List<double> outputValues = new List<double>();
// ReSharper restore SuggestUseVarKeywordEvident
            foreach (double input in inputs)
            {
                double outputvalue;
                
                if (organisation.LengthUnit.Id == (int)LengthUnit.Inch)
                {
                    outputvalue = LengthConversionHelper.ConvertLength(input, LengthUnit.Inch, organisation.LengthUnit);
                    outputvalue = LengthConversionHelper.MmToPoint(outputvalue);
                    outputValues.Add(outputvalue);
                }
                else if (organisation.LengthUnit.Id == (int)LengthUnit.Cm)
                {
                    outputvalue = LengthConversionHelper.ConvertLength(input, LengthUnit.Cm, organisation.LengthUnit);
                    outputvalue = LengthConversionHelper.MmToPoint(outputvalue);
                    outputValues.Add(outputvalue);
                }
                else
                {
                    outputvalue = LengthConversionHelper.MmToPoint(input);
                    outputValues.Add(outputvalue);
                }
            }

            return outputValues;
        }

        /// <summary>
        /// Converts length to a specified output unit
        /// </summary>
        public List<double> ConvertLengthFromPointsToSystemUnit(List<double> inputs)
        {
            Organisation organisation = organisationRepository.GetOrganizatiobByID();

            if (organisation == null)
            {
                return new List<double>();
            }

            if (organisation.LengthUnit == null)
            {
                return new List<double>();
            }

            if (inputs == null)
            {
                return new List<double>();
            }

            // ReSharper disable SuggestUseVarKeywordEvident
            List<double> outputValues = new List<double>();
            // ReSharper restore SuggestUseVarKeywordEvident
            foreach (double input in inputs)
            {
                double outputvalue;

                if (organisation.LengthUnit.Id == (int)LengthUnit.Inch)
                {
                    outputvalue = LengthConversionHelper.PointToMm(input);
                    outputvalue = LengthConversionHelper.ConvertLength(outputvalue, LengthUnit.Inch, organisation.LengthUnit);
                    outputValues.Add(outputvalue);
                }
                else if (organisation.LengthUnit.Id == (int)LengthUnit.Cm)
                {
                    outputvalue = LengthConversionHelper.PointToMm(input);
                    outputvalue = LengthConversionHelper.ConvertLength(outputvalue, LengthUnit.Cm, organisation.LengthUnit);
                    outputValues.Add(outputvalue);
                }
                else
                {
                    outputvalue = LengthConversionHelper.PointToMm(input);
                    outputValues.Add(outputvalue);
                }
            }

            return outputValues;
        }

        #endregion
    }
}
