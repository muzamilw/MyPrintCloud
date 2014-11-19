using System;
using System.Data.Entity;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using MPC.Models.DomainModels;

namespace MPC.Repository.BaseRepository
{
    /// <summary>
    /// Base Db Context. Implements Identity Db Context over Application User
    /// </summary>
    public sealed class BaseDbContext : DbContext
    {
        #region Private
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once NotAccessedField.Local
        private IUnityContainer container;
        #endregion

        #region Protected


        #endregion

        #region Constructor
        public BaseDbContext()
        {
        }
        /// <summary>
        /// Eager load property
        /// </summary>
        public void LoadProperty(object entity, string propertyName, bool isCollection = false)
        {
            if (!isCollection)
            {
                Entry(entity).Reference(propertyName).Load();
            }
            else
            {
                Entry(entity).Collection(propertyName).Load();
            }
        }
        /// <summary>
        /// Eager load property
        /// </summary>
        public void LoadProperty<T>(object entity, Expression<Func<T>> propertyExpression, bool isCollection = false)
        {
            string propertyName = PropertyReference.GetPropertyName(propertyExpression);
            LoadProperty(entity, propertyName, isCollection);
        }

        #endregion

        #region Public

        public BaseDbContext(IUnityContainer container, string connectionString)
            : base(connectionString)
        {
            this.container = container;
        }
        /// <summary>
        /// Organisation Db Set
        /// </summary>
        public DbSet<Organisation> Organisations { get; set; }
        /// <summary>
        /// MarkUp Db Set
        /// </summary>
        public DbSet<MarkUp> MarkUps { get; set; }
        /// <summary>
        /// Tax Rate Db Set
        /// </summary>
        public DbSet<TaxRate> TaxRates { get; set; }
        /// <summary>
        /// Chart Of Account Db Set
        /// </summary>
        public DbSet<ChartOfAccount> ChartOfAccounts { get; set; }
        #endregion
    }
}
