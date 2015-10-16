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
    /// Company Contact Variable Repository
    /// </summary>
    public class ScopeVariableRepository : BaseRepository<ScopeVariable>, IScopeVariableRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ScopeVariableRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ScopeVariable> DbSet
        {
            get
            {
                return db.ScopeVariables;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Scope Variable By Contact ID Anb Scope Type
        /// </summary>
        public IEnumerable<ScopeVariable> GetContactVariableByContactId(long contactId, int scope)
        {
            return DbSet.Where(cv => cv.Id == contactId && cv.Scope == scope);

        }

        public void AddScopeVariables(long ContactId, long CompanyId) 
        {
            // add scope variables

            List<FieldVariable> listOfCompanyVariables = db.FieldVariables.Where(f => f.CompanyId == CompanyId && f.Scope == 2).ToList();
            List<ScopeVariable> listOfUserScopeVariables = new List<ScopeVariable>();
            ScopeVariable UserScopeVariable = null;
            foreach (FieldVariable vari in listOfCompanyVariables)
            {
                UserScopeVariable = new ScopeVariable();
                UserScopeVariable.VariableId = vari.VariableId;
                UserScopeVariable.Id = ContactId;
                UserScopeVariable.Value = vari.DefaultValue;
                UserScopeVariable.Scope = 2;
                listOfUserScopeVariables.Add(UserScopeVariable);
            }

            if (listOfUserScopeVariables.Count() > 0)
            {
                db.ScopeVariables.AddRange(listOfUserScopeVariables);
            }

            db.SaveChanges();
        }
        #endregion
    }
}
