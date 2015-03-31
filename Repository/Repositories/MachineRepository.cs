using System.Linq;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System.Data.Entity;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using MPC.Models.Common;
using System;
using System.Linq.Expressions;
using AutoMapper;

namespace MPC.Repository.Repositories
{
    class MachineRepository : BaseRepository<Machine>, IMachineRepository
    {


        #region Private
        private readonly IOrganisationRepository organisationRepository;
        private readonly Dictionary<MachineListColumns, Func<Machine, object>> OrderByClause = new Dictionary<MachineListColumns, Func<Machine, object>>
                    {
                        {MachineListColumns.MachineName, d => d.MachineName},
                        {MachineListColumns.CalculationMethod, d => d.MachineCatId},
                        
                    };
        private readonly Dictionary<MachineByColumn, Func<Machine, object>> machineOrderByClause =
          new Dictionary<MachineByColumn, Func<Machine, object>>
                    {
                         {MachineByColumn.Name, c => c.MachineName}
                    };
        #endregion


        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MachineRepository(IUnityContainer container, IOrganisationRepository organisationRepository)
            : base(container)
        {
            this.organisationRepository = organisationRepository;
        }
        #endregion
        public MachineListResponseModel GetAllMachine(MachineRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<Machine, bool>> query;
            if (request.isGuillotineList)
            {
                query = machine => (machine.IsDisabled == false && machine.MachineCatId == 4);
            }
            else
            {
                query = machine => (machine.IsDisabled == false && machine.MachineCatId != 4);
            }


            var machineList = request.IsAsc
                ? DbSet.Where(query)
                .OrderBy(OrderByClause[request.MachineOrderBy])
                .Skip(fromRow)
                .Take(toRow)
                .ToList()
                : DbSet.OrderByDescending(OrderByClause[request.MachineOrderBy])
                .Skip(fromRow)
                .Take(toRow)
                .ToList();

            return new MachineListResponseModel
            {
                RowCount = DbSet.Count(query),
                MachineList = machineList,
                lookupMethod = db.LookupMethods

            };



        }
        public MachineSearchResponse GetMachinesForProduct(MachineSearchRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<Machine, bool>> query =
                machine =>
                    (string.IsNullOrEmpty(request.SearchString) || machine.MachineName.Contains(request.SearchString));

            IEnumerable<Machine> machines = request.IsAsc
               ? DbSet.Where(query)
                   .OrderBy(machineOrderByClause[request.MachineOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList()
               : DbSet.Where(query)
                   .OrderByDescending(machineOrderByClause[request.MachineOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList();

            return new MachineSearchResponse { Machines = machines, TotalCount = DbSet.Count(query) };
        }


        public Machine Find(int id)
        {
            return DbSet.Find(id);
        }
        public bool archiveMachine(long id)
        {
            try
            {
                Machine machine = db.Machines.Where(g => g.MachineId == id).SingleOrDefault();
                machine.IsDisabled = true;
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public string GetStockItemName(int? itemId)
        {
            if (itemId != null && itemId > 0)
            {
                StockItem SI = db.StockItems.Where(g => g.StockItemId == itemId).SingleOrDefault();
                return SI.ItemName;
            }
            return "";
        }

        public MachineResponseModel GetMachineByID(long MachineID)
        {
            Machine omachine = DbSet.Where(g => g.MachineId == MachineID).SingleOrDefault();
            bool IsGuillotine = false;
            if (omachine.MachineCatId == 4)
            {
                IsGuillotine = true;
            }
            Organisation organisation = organisationRepository.GetOrganizatiobByID();
            return new MachineResponseModel
            {

                machine = omachine,
                lookupMethods = GetAllLookupMethodList(IsGuillotine),
                Markups = null,
                StockItemforInk = GetAllStockItemforInk(),
                MachineSpoilageItems = GetMachineSpoilageItems(MachineID),
                deFaultPaperSizeName = GetStockItemName(omachine.DefaultPaperId),
                deFaultPlatesName = GetStockItemName(omachine.DefaultPlateId),
                InkCoveragItems = GetInkCoveragItems(),
                CurrencySymbol = organisation == null ? null : organisation.Currency.CurrencySymbol,
                WeightUnit = organisation == null ? null : organisation.WeightUnit.UnitName,
                LengthUnit = organisation == null ? null : organisation.LengthUnit.UnitName

            };


        }

        public MachineResponseModel CreateMachineByType(bool IsGuillotine)
        {

            Organisation organisation = organisationRepository.GetOrganizatiobByID();
            
            return new MachineResponseModel
            {
                machine = null,
                lookupMethods = GetAllLookupMethodList(IsGuillotine),
                Markups = null,
                StockItemforInk = GetAllStockItemforInk(),
                MachineSpoilageItems = null,
                deFaultPaperSizeName = null,
                deFaultPlatesName = null,
                InkCoveragItems = GetInkCoveragItems(),
                CurrencySymbol = organisation == null ? null : organisation.Currency.CurrencySymbol,
                WeightUnit = organisation == null ? null : organisation.WeightUnit.UnitName,
                LengthUnit = organisation == null ? null : organisation.LengthUnit.UnitName

            };


        }

        public long AddMachine(Machine machine, IEnumerable<MachineSpoilage> MachineSpoilages)
        {
            try
            {
                StockItem stockItem = db.StockItems.Where(g => g.CategoryId == 1 && g.OrganisationId == OrganisationId && g.ItemName.Contains("100")).FirstOrDefault();
                Machine omachine = new Machine();
                omachine.MachineName = machine.MachineName;
                omachine.MachineCatId = machine.MachineCatId;
                omachine.ColourHeads = machine.ColourHeads;
                omachine.isPerfecting = machine.isPerfecting;
                omachine.SetupCharge = machine.SetupCharge;
                omachine.WashupPrice = machine.WashupPrice;
                omachine.WashupCost = machine.WashupCost;
                omachine.MinInkDuctqty = 0;
                omachine.worknturncharge = machine.worknturncharge;
                omachine.MakeReadyCost = machine.MakeReadyCost;
                omachine.DefaultFilmId = machine.DefaultFilmId;
                omachine.DefaultPlateId = machine.DefaultPlateId;
                if (stockItem != null)
                {
                    omachine.DefaultPaperId = Convert.ToInt32(stockItem.StockItemId);
                }

                omachine.isfilmused = machine.isfilmused;
                omachine.isplateused = machine.isplateused;
                omachine.ismakereadyused = machine.ismakereadyused;
                omachine.iswashupused = machine.iswashupused;
                omachine.maximumsheetweight = machine.maximumsheetweight;
                omachine.maximumsheetheight = machine.maximumsheetheight;
                omachine.maximumsheetwidth = machine.maximumsheetwidth;
                omachine.minimumsheetheight = 50;
                omachine.minimumsheetwidth = 50;
                omachine.gripdepth = machine.gripdepth;
                omachine.gripsideorientaion = machine.gripsideorientaion;
                omachine.gutterdepth = machine.gutterdepth;
                omachine.headdepth = machine.headdepth;
                omachine.MarkupId = machine.MarkupId;
                omachine.PressSizeRatio = machine.PressSizeRatio;
                omachine.Description = machine.Description;
                omachine.Priority = machine.Priority;
                omachine.DirectCost = machine.DirectCost;
                omachine.Image = machine.Image;
                omachine.MinimumCharge = machine.MinimumCharge;
                omachine.CostPerCut = machine.CostPerCut;
                omachine.PricePerCut = machine.PricePerCut;
                omachine.IsAdditionalOption = machine.IsAdditionalOption;
                omachine.IsDisabled = false;
                omachine.LockedBy = machine.LockedBy;
                omachine.CylinderSizeId = machine.CylinderSizeId;
                omachine.MaxItemAcrossCylinder = machine.MaxItemAcrossCylinder;
                omachine.Web1MRCost = machine.Web1MRCost;
                omachine.Web1MRPrice = machine.Web1MRPrice;
                omachine.Web2MRCost = machine.Web2MRCost;
                omachine.Web2MRPrice = machine.Web2MRPrice;
                omachine.ReelMRCost = machine.ReelMRCost;
                omachine.ReelMRPrice = machine.ReelMRPrice;
                omachine.IsMaxColorLimit = false;
                omachine.PressUtilization = machine.PressUtilization;
                omachine.MakeReadyPrice = machine.MakeReadyPrice;
                omachine.InkChargeForUniqueColors = machine.InkChargeForUniqueColors;
                omachine.CompanyId = machine.CompanyId;
                omachine.FlagId = machine.FlagId;
                omachine.IsScheduleable = machine.IsScheduleable;
                omachine.SystemSiteId = machine.SystemSiteId;
                omachine.SpoilageType = machine.SpoilageType;
                omachine.SetupTime = machine.SetupTime;
                omachine.TimePerCut = machine.TimePerCut;
                omachine.MakeReadyTime = machine.MakeReadyTime;
                omachine.WashupTime = machine.WashupTime;
                omachine.ReelMakereadyTime = machine.ReelMakereadyTime;
                omachine.LookupMethodId = machine.LookupMethodId;
                omachine.OrganisationId = OrganisationId;
                db.Machines.Add(omachine);
                db.SaveChanges();

                foreach (var item in machine.MachineInkCoverages)
                {
                    MachineInkCoverage obj = new MachineInkCoverage();
                    obj.MachineId = omachine.MachineId;
                    obj.SideInkOrder = item.SideInkOrder;
                    obj.SideInkOrderCoverage = item.SideInkOrderCoverage;
                    db.MachineInkCoverages.Add(obj);
                }

                foreach (var item in MachineSpoilages)
                {
                    MachineSpoilage obj = new MachineSpoilage();
                    obj.MachineId = omachine.MachineId;
                    obj.RunningSpoilage = item.RunningSpoilage;
                    obj.SetupSpoilage = item.SetupSpoilage;
                    obj.NoOfColors = item.NoOfColors;
                    db.MachineSpoilages.Add(obj);

                }

                if (db.SaveChanges() > 0)
                {

                    return omachine.MachineId;

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool UpdateMachine(Machine machine, IEnumerable<MachineSpoilage> MachineSpoilages)
        {
            try
            {
                Machine omachine = db.Machines.Where(s => s.MachineId == machine.MachineId).SingleOrDefault();
                omachine.MachineId = machine.MachineId;
                omachine.MachineName = machine.MachineName;
                omachine.MachineCatId = machine.MachineCatId;
                omachine.ColourHeads = machine.ColourHeads;
                omachine.isPerfecting = machine.isPerfecting;
                omachine.SetupCharge = machine.SetupCharge;
                omachine.WashupPrice = machine.WashupPrice;
                omachine.WashupCost = machine.WashupCost;
                //omachine.MinInkDuctqty = machine.MinInkDuctqty;
                omachine.worknturncharge = machine.worknturncharge;
                omachine.MakeReadyCost = machine.MakeReadyCost;
                omachine.DefaultFilmId = machine.DefaultFilmId;
                omachine.DefaultPlateId = machine.DefaultPlateId;
                // omachine.DefaultPaperId = machine.DefaultPaperId;
                omachine.isfilmused = machine.isfilmused;
                omachine.isplateused = machine.isplateused;
                omachine.ismakereadyused = machine.ismakereadyused;
                omachine.iswashupused = machine.iswashupused;
                omachine.maximumsheetweight = machine.maximumsheetweight;
                omachine.maximumsheetheight = machine.maximumsheetheight;
                omachine.maximumsheetwidth = machine.maximumsheetwidth;
                // omachine.minimumsheetheight = machine.minimumsheetheight;
                // omachine.minimumsheetwidth = machine.minimumsheetwidth;
                omachine.gripdepth = machine.gripdepth;
                omachine.gripsideorientaion = machine.gripsideorientaion;
                omachine.gutterdepth = machine.gutterdepth;
                omachine.headdepth = machine.headdepth;
                omachine.MarkupId = machine.MarkupId;
                omachine.PressSizeRatio = machine.PressSizeRatio;
                omachine.Description = machine.Description;
                omachine.Priority = machine.Priority;
                omachine.DirectCost = machine.DirectCost;
                omachine.Image = machine.Image;
                omachine.MinimumCharge = machine.MinimumCharge;
                omachine.CostPerCut = machine.CostPerCut;
                omachine.PricePerCut = machine.PricePerCut;
                omachine.IsAdditionalOption = machine.IsAdditionalOption;
                // omachine.IsDisabled = machine.IsDisabled;
                omachine.LockedBy = machine.LockedBy;
                omachine.CylinderSizeId = machine.CylinderSizeId;
                omachine.MaxItemAcrossCylinder = machine.MaxItemAcrossCylinder;
                omachine.Web1MRCost = machine.Web1MRCost;
                omachine.Web1MRPrice = machine.Web1MRPrice;
                omachine.Web2MRCost = machine.Web2MRCost;
                omachine.Web2MRPrice = machine.Web2MRPrice;
                omachine.ReelMRCost = machine.ReelMRCost;
                omachine.ReelMRPrice = machine.ReelMRPrice;
                //omachine.IsMaxColorLimit = machine.IsMaxColorLimit;
                omachine.PressUtilization = machine.PressUtilization;
                omachine.MakeReadyPrice = machine.MakeReadyPrice;
                omachine.InkChargeForUniqueColors = machine.InkChargeForUniqueColors;
                omachine.CompanyId = machine.CompanyId;
                omachine.FlagId = machine.FlagId;
                omachine.IsScheduleable = machine.IsScheduleable;
                omachine.SystemSiteId = machine.SystemSiteId;
                omachine.SpoilageType = machine.SpoilageType;
                omachine.SetupTime = machine.SetupTime;
                omachine.TimePerCut = machine.TimePerCut;
                omachine.MakeReadyTime = machine.MakeReadyTime;
                omachine.WashupTime = machine.WashupTime;
                omachine.ReelMakereadyTime = machine.ReelMakereadyTime;
                omachine.LookupMethodId = machine.LookupMethodId;
                // omachine.OrganisationId = machine.OrganisationId;


                foreach (var item in machine.MachineInkCoverages)
                {
                    MachineInkCoverage obj = db.MachineInkCoverages.Where(g => g.Id == item.Id).SingleOrDefault();
                    obj.SideInkOrder = item.SideInkOrder;
                    obj.SideInkOrderCoverage = item.SideInkOrderCoverage;
                }

                foreach (var item in MachineSpoilages)
                {
                    MachineSpoilage obj = db.MachineSpoilages.Where(g => g.MachineSpoilageId == item.MachineSpoilageId).SingleOrDefault();
                    obj.RunningSpoilage = item.RunningSpoilage;
                    obj.SetupSpoilage = item.SetupSpoilage;

                }

                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public IEnumerable<MachineSpoilage> GetMachineSpoilageItems(long machineId)
        {
            return db.MachineSpoilages.Where(g => g.MachineId == machineId).ToList();
        }

        public IEnumerable<InkCoverageGroup> GetInkCoveragItems()
        {
            return db.InkCoverageGroups;
        }
        protected override IDbSet<Machine> DbSet
        {
            get
            {
                return db.Machines;
            }
        }
        public IEnumerable<LookupMethod> GetAllLookupMethodList(bool IsGuillotine)
        {
            if (IsGuillotine)
            {
                return db.LookupMethods.Where(g => g.MethodId == 6 && g.OrganisationId == 0 || g.MethodId == 6 && g.OrganisationId == OrganisationId || g.Type == 6 && g.OrganisationId == 0 || g.Type == 6 && g.OrganisationId == OrganisationId).ToList();
            }
            else
            {
                return db.LookupMethods.Where(g => g.MethodId != 6 && g.Type != 6 && g.OrganisationId == 0 || g.MethodId != 6 && g.Type != 6 && g.OrganisationId == OrganisationId).ToList();
            }

        }
        public IEnumerable<Markup> GetAllMarkupList()
        {
            return db.Markups;
        }
        public IEnumerable<StockItem> GetAllStockItemforInk()
        {
            return db.StockItems.Join(db.StockCategories, SI => SI.CategoryId, SC => SC.CategoryId, (SI, SC) => new { SI, SC }).Where(IC => IC.SC.Code == "INK").Select(IC => IC.SI).ToList();

            //return (from SI in db.StockItems
            //      join CI in db.StockCategories on SI.CategoryId equals CI.CategoryId
            //      where CI.Code == "INK"
            //      select SI).ToList();
        }
        public IEnumerable<MachineResource> GetAllMachineResources()
        {
            return db.MachineResources;
        }

        public List<Machine> GetMachinesByOrganisationID(long OID)
        {
            try
            {
                Mapper.CreateMap<Machine, Machine>()
               .ForMember(x => x.ItemSections, opt => opt.Ignore());

                 Mapper.CreateMap<MachineInkCoverage, MachineInkCoverage>()
               .ForMember(x => x.Machine, opt => opt.Ignore());

                 Mapper.CreateMap<MachineResource, MachineResource>()
               .ForMember(x => x.Machine, opt => opt.Ignore());



                 List<Machine> machines = db.Machines.Include("MachineResources").Include("MachineInkCoverages").Where(o => o.OrganisationId == OID).ToList();

                List<Machine> oOutputMachine = new List<Machine>();

                if (machines != null && machines.Count > 0)
                {
                    foreach (var item in machines)
                    {
                        var omappedItem = Mapper.Map<Machine, Machine>(item);
                        oOutputMachine.Add(omappedItem);
                    }
                }

                return oOutputMachine;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<LookupMethod> getLookupmethodsbyOrganisationID(long OID)
        {
            try
            {
                Mapper.CreateMap<LookupMethod, LookupMethod>()
               .ForMember(x => x.MachineClickChargeLookups, opt => opt.Ignore())
                 .ForMember(x => x.MachineClickChargeZones, opt => opt.Ignore())
                 .ForMember(x => x.MachineGuillotineCalcs, opt => opt.Ignore())
                 .ForMember(x => x.MachinePerHourLookups, opt => opt.Ignore())
                 .ForMember(x => x.MachineSpeedWeightLookups, opt => opt.Ignore());

                List<LookupMethod> methods = db.LookupMethods.Where(o => o.OrganisationId == OID).ToList();

                List<LookupMethod> oOutputLookup = new List<LookupMethod>();

                if (methods != null && methods.Count > 0)
                {
                    foreach (var item in methods)
                    {
                        var omappedItem = Mapper.Map<LookupMethod, LookupMethod>(item);
                        oOutputLookup.Add(omappedItem);
                    }
                }


                return oOutputLookup;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}