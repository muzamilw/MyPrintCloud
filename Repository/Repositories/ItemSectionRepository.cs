using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System.Linq;
using MPC.Models.Common;
using System;
using System.Collections.Generic;
using MPC.Models.ResponseModels;
using System.Drawing;
using System.Web;
using lengthunit = MPC.Models.Common.LengthUnit;
using System.Drawing.Text;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Item Section Repository
    /// </summary>
    public class ItemSectionRepository : BaseRepository<ItemSection>, IItemSectionRepository
    {
        #region private
        private readonly IOrganisationRepository organisationRepository;
        private readonly ICostCentreRepository costCenterRepository;
        private readonly IMachineRepository machineRepository;
        private readonly IStockItemRepository stockItemRepository;
        private readonly ILookupMethodRepository lookupMethodRepository;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemSectionRepository(IUnityContainer container, IOrganisationRepository _organisationRepository, ICostCentreRepository _costCenterRepository, IMachineRepository _machineRepository, IStockItemRepository _stockItemRepository, ILookupMethodRepository _lookupMethodRepository)
            : base(container)
        {
            this.organisationRepository = _organisationRepository;
            this.costCenterRepository = _costCenterRepository;
            this.machineRepository = _machineRepository;
            this.stockItemRepository = _stockItemRepository;
            this.lookupMethodRepository = _lookupMethodRepository;
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemSection> DbSet
        {
            get
            {
                return db.ItemSections;
            }
        }

        #endregion

        #region Estimation Quiries

        public Machine GetPressById(int PressId)
        {
           return machineRepository.Find(PressId);
        }
        public CostCentre GetCostCenterBySystemType(int systemType)
        {
            return db.CostCentres.FirstOrDefault(c => c.SystemTypeId == systemType && c.SystemSiteId == 1 && c.OrganisationId == this.OrganisationId);
        }
        public StockItem GetStockById(long StockItemId)
        {
            return stockItemRepository.Find(StockItemId); 
        }
        public LookupMethod GetLookupMethodById(long MethodId)
        {
            return lookupMethodRepository.Find(MethodId);
        }

        public MachineClickChargeLookup GetMachineClickChargeById(long Id)
        {
            return  db.MachineClickChargeLookups.Where(l => l.MethodId == Id).FirstOrDefault();
        }
        public MachineSpeedWeightLookup GetMachineSpeedWeightLookup(long Id)
        {
            return db.MachineSpeedWeightLookups.Where(l => l.MethodId == Id).FirstOrDefault();
        }
        public MachineMeterPerHourLookup GetMachinemeterPerHourLookup(long id)
        {
            return db.MachineMeterPerHourLookups.FirstOrDefault(l => l.MethodId == id);
        }
        public MachinePerHourLookup GetMachinePerHourLookup(long Id)
        {
            return db.MachinePerHourLookups.Where(p => p.MethodId == Id).FirstOrDefault();
        }
        public MachineClickChargeZone GetMachineClickChargeZone(long Id)
        {
            return db.MachineClickChargeZones.Where(z => z.MethodId == Id).FirstOrDefault();
        }
        public Markup GetMarkupById(int Id)
        {
            return db.Markups.Where(m => m.MarkUpId == Id).FirstOrDefault();
        }
        public List<StockCostAndPrice> GetStockPricingByStockId(int StockId, bool isPrice)
        {
            if(isPrice)
                return db.StockCostAndPrices.Where(s => s.ItemId == StockId && s.CostOrPriceIdentifier == -1).ToList();
            else
                return db.StockCostAndPrices.Where(s => s.ItemId == StockId && s.CostOrPriceIdentifier == 0).ToList();
        }
        public MachineSpoilage GetMachineSpoilageByMachineIdAndColors(int machineId, int Inks)
        {
            return db.MachineSpoilages.Where(s => s.MachineId == machineId && s.NoOfColors == Inks).FirstOrDefault();
        }
        public InkCoverageGroup GetInkCoverageById(int Id)
        {
            return db.InkCoverageGroups.Where(c => c.CoverageGroupId == Id).FirstOrDefault();
        }
        public PaperSize GetPaperSizeById(int Id)
        {
           return db.PaperSizes.Where(s => s.PaperSizeId == Id).FirstOrDefault();
        }
        public MachineGuillotineCalc GetGuillotineCalcMethod(long iMethodId)
        {
           return db.MachineGuillotineCalcs.Where(g => g.MethodId == iMethodId).FirstOrDefault();
        }
        public MachineGuilotinePtv GetGuillotinePtv(long guillotineId, int orderPtv)
        {
           return db.MachineGuilotinePtvs.Where(p => p.GuilotineId == guillotineId && p.NoofUps == orderPtv).FirstOrDefault();
        }

        public string GetWeightUnitName(int UnitID)
        {
            return db.WeightUnits.Where(o => o.Id == UnitID).FirstOrDefault().UnitName;
        }

        public string GetLengthUnitName(int UnitID)
        {
            return db.LengthUnits.Where(o => o.Id == UnitID).FirstOrDefault().UnitName;
        }
        

        public MPC.Models.DomainModels.LengthUnit GetLengthUnitByInput(int InputUnit)
        {
            return db.LengthUnits.Where(o => o.Id == InputUnit).FirstOrDefault();
        }

        public MPC.Models.DomainModels.WeightUnit GetWeightUnitByInput(int InputUnit)
        {
            return db.WeightUnits.Where(o => o.Id == InputUnit).FirstOrDefault();
        }
        public JobPreference GetJobPreferences(int SystemSiteID)
        {
            return db.JobPreferences.Where(g => g.SystemSiteId == SystemSiteID).Single();
        }
        public long GetSystemCostCentreID(SystemCostCenterTypes SystemType)
        {
            return db.CostCentres.Where(g => g.SystemTypeId == (int)SystemType).Select(g => g.CostCentreId).Single();
        }
        public List<Machine> GetEnabledPresses(double sectionHeight, double sectionWidth)
        {
            List<Machine> EnablePresses = db.Machines.Where(m => m.MachineCatId != (int)MachineCategories.Guillotin && m.minimumsheetheight <= sectionHeight && m.minimumsheetwidth <= sectionWidth && m.OrganisationId == this.OrganisationId && (m.IsDisabled != true)).ToList();
            return EnablePresses; 
        }
        public List<MachineSpoilage> GetSpoilageByPressId(int PressId)
        {
            return db.MachineSpoilages.Where(m => m.MachineId == PressId).ToList();
        }

        public List<CostCentre> GetUserDefinedCostCentres()
        {
            return costCenterRepository.GetAllNonSystemCostCentres().ToList();
        }
        
        #endregion

        #region public

           /// <summary>
        /// get item section no 1
        /// </summary>
        /// <param name="StockOptionID"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public ItemSection GetFirstSectionOfItem(long ItemId)
        {
            try
            {

                return db.ItemSections.Where(i => i.ItemId == ItemId && i.SectionNo == 1).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ItemSection GetSectionByItemId(long ItemId)
        {
            try
            {

                return db.ItemSections.Where(i => i.ItemId == ItemId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ItemSection GetItemSectionById(long itemSectionId)
        {
            return DbSet.FirstOrDefault(o => o.ItemSectionId == itemSectionId);
        }
        #endregion
    }
}
