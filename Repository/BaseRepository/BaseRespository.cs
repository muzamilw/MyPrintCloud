﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using MPC.Common;
using MPC.Interfaces.Repository;
using MPC.Models.Common;

namespace MPC.Repository.BaseRepository
{
    /// <summary>
    /// Base Repository
    /// </summary>
    /// 
    [Serializable]
    public abstract class BaseRepository<TDomainClass> : IBaseRepository<TDomainClass, long>
       where TDomainClass : class
    {
        #region Private

        // ReSharper disable once InconsistentNaming
        private readonly IUnityContainer container;
        #endregion
        #region Protected
        /// <summary>
        /// Primary database set
        /// </summary>
        protected abstract IDbSet<TDomainClass> DbSet { get; }

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseRepository(IUnityContainer container)
        {

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
            string connectionString = ConfigurationManager.ConnectionStrings["BaseDbContext"].ConnectionString;
            db = (BaseDbContext)container.Resolve(typeof(BaseDbContext), new ResolverOverride[] { new ParameterOverride("connectionString", connectionString) });
           
        }

        #endregion
        #region Public
        /// <summary>
        /// base Db Context
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public BaseDbContext db;

        /// <summary>
        /// Create object instance
        /// </summary>
        public virtual TDomainClass Create()
        {
// ReSharper disable SuggestUseVarKeywordEvident
            TDomainClass result = container.Resolve<TDomainClass>();
// ReSharper restore SuggestUseVarKeywordEvident
            return result;
        }
        /// <summary>
        /// Find entry by key
        /// </summary>
        public virtual IQueryable<TDomainClass> Find(TDomainClass instance)
        {
            return DbSet.Find(instance) as IQueryable<TDomainClass>;
        }
        /// <summary>
        /// Find Entity by Id
        /// </summary>
        public virtual TDomainClass Find(long id)
        {
            return DbSet.Find(id);
        }
        /// <summary>
        /// Get All Entites 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TDomainClass> GetAll()
        {
            return DbSet;
        }
        /// <summary>
        /// Save Changes in the entities
        /// </summary>
        public void SaveChanges()
        {
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = new List<string>();
                foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
                {
                    var entityName = validationResult.Entry.Entity.GetType().Name;
                    errorMessages.AddRange(validationResult.ValidationErrors.Select(error => entityName + "." + error.PropertyName + ": " + error.ErrorMessage));
                }
            }
        }
        /// <summary>
        /// Delete an entry
        /// </summary>
        public virtual void Delete(TDomainClass instance)
        {
            DbSet.Remove(instance);
        }
        /// <summary>
        /// Add an entry
        /// </summary>
        public virtual void Add(TDomainClass instance)
        {
            DbSet.Add(instance);
        }
        /// <summary>
        /// Add an entry
        /// </summary>
        public virtual void Update(TDomainClass instance)
        {
            DbSet.AddOrUpdate(instance);
        }

        /// <summary>
        /// Eager load property
        /// </summary>
        public void LoadProperty<T>(object entity, Expression<Func<T>> propertyExpression, bool isCollection = false)
        {
            db.LoadProperty(entity, propertyExpression, isCollection);
        }

        /// <summary>that specifies the User's domain on the system
        /// User Domain key 
        /// </summary>        
        public long OrganisationId
        {
            get
            {
                IEnumerable<OrganisationClaimValue> organisationClaimValues = ClaimHelper.GetClaimsByType<OrganisationClaimValue>(MpcClaimTypes.Organisation);
                return organisationClaimValues != null && organisationClaimValues.Any() ? organisationClaimValues.ElementAt(0).OrganisationId : 0;
                
            }
        }
        
        /// <summary>
        /// Logged in User Identity
        /// Name of Logged In User
        /// </summary>
        public string LoggedInUserIdentity
        {
            get
            {
                IEnumerable<NameClaimValue> nameClaimValues = ClaimHelper.GetClaimsByType<NameClaimValue>(MpcClaimTypes.MisUser);
                return nameClaimValues != null && nameClaimValues.Any()
                    ? nameClaimValues.ElementAt(0).Name
                    : "N/A";
            }
        }

        /// <summary>
        /// Logged in User Id
        /// </summary>
        public Guid LoggedInUserId
        {
            get
            {
                IEnumerable<SystemUserClaimValue> systemUserClaimValues = ClaimHelper.GetClaimsByType<SystemUserClaimValue>(MpcClaimTypes.SystemUser);
                return systemUserClaimValues != null && systemUserClaimValues.Any()
                    ? systemUserClaimValues.ElementAt(0).SystemUserId
                    : Guid.Empty;
            }
        }

        #endregion

    }
}