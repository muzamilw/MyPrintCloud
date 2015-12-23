using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using AutoMapper;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Phrase Field Repository
    /// </summary>
    public class PhraseFieldRepository : BaseRepository<PhraseField>, IPhraseFieldRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PhraseFieldRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PhraseField> DbSet
        {
            get
            {
                return db.PhraseFields;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get All Phrases
        /// </summary>
        public override IEnumerable<PhraseField> GetAll()
        {
            return DbSet.ToList();
        }

        /// <summary>
        /// Get Phrase Field By Section Id
        /// </summary>
        public IEnumerable<PhraseField> GetPhraseFieldsBySectionId(long sectionId)
        {
            return DbSet.Where(p => p.SectionId == sectionId).ToList();
        }

        public List<PhraseField> GetPhraseFieldsByOrganisationID(long OID)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;

                db.Configuration.ProxyCreationEnabled = false;

                Mapper.CreateMap<PhraseField, PhraseField>()
               .ForMember(x => x.Section, opt => opt.Ignore());

                Mapper.CreateMap<Phrase, Phrase>()
             .ForMember(x => x.PhraseField, opt => opt.Ignore());
        

                List<PhraseField> PFs =  db.PhraseFields.Include("Phrases").Where(p => p.OrganisationId == OID).ToList();


                List<PhraseField> oOutputPF = new List<PhraseField>();

                if (PFs != null && PFs.Count > 0)
                {
                    foreach (var item in PFs)
                    {
                        var omappedItem = Mapper.Map<PhraseField, PhraseField>(item);
                        oOutputPF.Add(omappedItem);
                    }
                }
                return oOutputPF;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
