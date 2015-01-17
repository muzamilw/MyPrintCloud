using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Phrase Respository
    /// </summary>
    public class PhraseRespository : BaseRepository<Phrase>, IPhraseRespository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PhraseRespository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Phrase> DbSet
        {
            get
            {
                return db.Phrases;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get All Phrases
        /// </summary>
        public override IEnumerable<Phrase> GetAll()
        {
            return DbSet.ToList();
        }

        /// <summary>
        /// Get Phrases By Phrase Filed Id
        /// </summary>
        public IEnumerable<Phrase> GetPhrasesByPhraseFiledId(long phraseFieldId)
        {
            return DbSet.Where(p => p.FieldId == phraseFieldId && p.OrganisationId == OrganisationId).ToList();
        }

        #endregion
    }
}
