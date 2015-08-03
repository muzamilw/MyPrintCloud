using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System.Collections.Generic;
using System;
using System.Linq;
namespace MPC.Repository.Repositories
{
    /// <summary>
    /// SectionCostCentre Repository
    /// </summary>
    public class SectionCostCentreRepository : BaseRepository<SectionCostcentre>, ISectionCostCentreRepository
    {
        #region private
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SectionCostCentreRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<SectionCostcentre> DbSet
        {
            get
            {
                return db.SectionCostcentres;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Find Section Cost Centre by id
        /// </summary>
        public SectionCostcentre Find(int id)
        {
            return base.Find(id);
        }
        /// <summary>
        /// get item section cost centres
        /// </summary>
        /// <param name="StockOptionID"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public List<SectionCostcentre> GetAllSectionCostCentres(long ItemSetionId)
        {
            try
            {
                return db.SectionCostcentres.Where(s => s.ItemSectionId == ItemSetionId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RemoveCostCentreOfFirstSection(long ItemSetionId)
        {
            try
            {
                db.SectionCostcentres.Where(
                            c => c.ItemSectionId == ItemSetionId && c.IsOptionalExtra == 1)
                            .ToList()
                            .ForEach(sc =>
                            {
                                db.SectionCostcentres.Remove(sc);

                            });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
