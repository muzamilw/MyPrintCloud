using System;
using System.Collections.Generic;
using System.Globalization;
using MPC.ExceptionHandling;
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
        public List<double> ConvertLengthToPoints(List<double> inputs, LengthUnit inputUnit, LengthUnit outputUnit)
        {
            Organisation organisation = organisationRepository.GetOrganizatiobByID();

            if (organisation == null)
            {
                throw new MPCException(string.Format(CultureInfo.InvariantCulture, "Organisation with Id {0} not found", organisationRepository.OrganisationId), 
                    organisationRepository.OrganisationId);
            }

            if (organisation.LengthUnit == null)
            {
                throw new MPCException(string.Format(CultureInfo.InvariantCulture, "Length Unit not defined for Current Organisation"),
                    organisationRepository.OrganisationId);
            }

            if (inputs == null)
            {
// ReSharper disable LocalizableElement
                throw new ArgumentException("Input list can't be null", "inputs");
// ReSharper restore LocalizableElement
            }

          // ReSharper disable SuggestUseVarKeywordEvident
            List<double> outputValues = new List<double>();
// ReSharper restore SuggestUseVarKeywordEvident
            foreach (double input in inputs)
            {
                double outputvalue;
                
                if (organisation.LengthUnit.Id == (int)LengthUnit.Inch)
                {
                    outputvalue = LengthConversionHelper.ConvertLength(input, outputUnit, organisation.LengthUnit);
                    outputvalue = LengthConversionHelper.MmToPoint(outputvalue);
                    outputValues.Add(outputvalue);
                }
                else if (organisation.LengthUnit.Id == (int)LengthUnit.Cm)
                {
                    outputvalue = LengthConversionHelper.ConvertLength(input, outputUnit, organisation.LengthUnit);
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
        public List<double> ConvertLengthFromPoints(List<double> inputs, LengthUnit inputUnit, LengthUnit outputUnit)
        {
            Organisation organisation = organisationRepository.GetOrganizatiobByID();

            if (organisation == null)
            {
                throw new MPCException(string.Format(CultureInfo.InvariantCulture, "Organisation with Id {0} not found", organisationRepository.OrganisationId),
                    organisationRepository.OrganisationId);
            }

            if (organisation.LengthUnit == null)
            {
                throw new MPCException(string.Format(CultureInfo.InvariantCulture, "Length Unit not defined for Current Organisation"),
                    organisationRepository.OrganisationId);
            }

            if (inputs == null)
            {
                // ReSharper disable LocalizableElement
                throw new ArgumentException("Input list can't be null", "inputs");
                // ReSharper restore LocalizableElement
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
                    outputvalue = LengthConversionHelper.ConvertLength(outputvalue, outputUnit, organisation.LengthUnit);
                    outputValues.Add(outputvalue);
                }
                else if (organisation.LengthUnit.Id == (int)LengthUnit.Cm)
                {
                    outputvalue = LengthConversionHelper.PointToMm(input);
                    outputvalue = LengthConversionHelper.ConvertLength(outputvalue, outputUnit, organisation.LengthUnit);
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
