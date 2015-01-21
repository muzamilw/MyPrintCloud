﻿using System.Collections.Generic;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    public class PaperSheetService : IPaperSheetService
    {
        #region Private

        private readonly IPaperSheetRepository paperSheetRepository;
        private readonly IOrganisationRepository organisationRepository;
        
        #endregion

        #region Constructor

        public PaperSheetService(IPaperSheetRepository paperSheetRepository, IOrganisationRepository organisationRepository)
        {
            this.paperSheetRepository = paperSheetRepository;
            this.organisationRepository = organisationRepository;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All Paper Sheets as requested
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PaperSheetResponse GetAll(PaperSheetRequestModel request)
        {
            return paperSheetRepository.SearchPaperSheet(request);
        }
        /// <summary>
        /// Add Paper Sheet
        /// </summary>
        /// <param name="paperSize"></param>
        /// <returns></returns>
        public PaperSize Add(PaperSize paperSize)
        {
            paperSheetRepository.Add(paperSize);
            paperSheetRepository.SaveChanges();
            return paperSize;
        }
        /// <summary>
        /// Update Paper Sheet
        /// </summary>
        /// <param name="paperSize"></param>
        /// <returns></returns>
        public PaperSize Update(PaperSize paperSize)
        {
            paperSheetRepository.Update(paperSize);
            paperSheetRepository.SaveChanges();
            return paperSize;
        }
        /// <summary>
        /// Delete Paper Sheet
        /// </summary>
        /// <param name="paperSheetId"></param>
        /// <returns></returns>
        public bool Delete(int paperSheetId)
        {
            paperSheetRepository.Delete(GetPaperSheetById(paperSheetId));
            paperSheetRepository.SaveChanges();
            return true;
        }

        public PaperSize GetPaperSheetById(int id)
        {
            return paperSheetRepository.Find(id);
        }

        public int? GetBaseData()
        {
            //var organisation = organisationRepository.GetOrganizatiobByID(paperSheetRepository.OrganisationId);
            //    if (organisation != null)
            //{
            //    if (organisation.SystemLengthUnit != null) return (int) organisation.SystemLengthUnit.Value;
            //}
            return null;
        }
        #endregion

    }
}
