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
    /// Registration Question Repository
    /// </summary>
    public class RegistrationQuestionRepository : BaseRepository<RegistrationQuestion>, IRegistrationQuestionRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RegistrationQuestionRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<RegistrationQuestion> DbSet
        {
            get
            {
                return db.RegistrationQuestions;
            }
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All Registration Questions
        /// </summary>
        public override IEnumerable<RegistrationQuestion> GetAll()
        {
            return DbSet.ToList();
        }
        public RegistrationQuestion GetSecretQuestionByID(int QuestionID)
        {
            return db.RegistrationQuestions.Where(i => i.QuestionId == QuestionID).FirstOrDefault();
        }

        #endregion
    }
}
