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

        /// <summary>
        /// Get All Machines
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Machine> GetAll()
        {
            return DbSet.Where(machine => machine.OrganisationId == OrganisationId && machine.MachineCatId != (int)MachineCategories.Guillotin && machine.IsDisabled != true).OrderBy(machine => machine.MachineName).ToList();
            
        }

        public MachineListResponseModel GetAllMachine(MachineRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<Machine, bool>> query;
            if (request.isGuillotineList)
            {
                query = machine => (machine.IsDisabled == false && machine.MachineCatId == 4 && machine.OrganisationId == this.OrganisationId);
            }
            else
            {
                query = machine => (machine.IsDisabled == false && machine.MachineCatId != 4 && machine.OrganisationId == this.OrganisationId);
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
                if (SI != null)
                    return SI.ItemName;
                else
                    return string.Empty;
            }
            return "";
        }

        public MachineResponseModel GetMachineByID(long MachineID)
        {
            Machine omachine = DbSet.Where(g => g.MachineId == MachineID).SingleOrDefault();
           
            
           List<MachineGuilotinePtv> optv = db.MachineGuilotinePtvs.Where(c => c.GuilotineId == omachine.MachineId).ToList();

           
            
           
            bool IsGuillotine = false;
            if (omachine.MachineCatId == 4)
            {
                IsGuillotine = true;
            }
            Organisation organisation = organisationRepository.GetOrganizatiobByID();
            return new MachineResponseModel
            {

                machine = omachine,
                GuilotinePtv = optv,
              //  lookupMethods = GetAllLookupMethodList(IsGuillotine),
                Markups = null,
               // StockItemforInk = GetAllStockItemforInk(),
               // MachineSpoilageItems = GetMachineSpoilageItems(MachineID),
               // MachineLookupMethods = GetMachineLookupMethods(MachineID),
                deFaultPaperSizeName = GetStockItemName(omachine.DefaultPaperId),
                deFaultPlatesName = GetStockItemName(omachine.DefaultPlateId),
               // InkCoveragItems = GetInkCoveragItems(),
                CurrencySymbol = organisation == null ? null : organisation.Currency.CurrencySymbol,
                WeightUnit = organisation == null ? null : organisation.WeightUnit.UnitName,
                LengthUnit = organisation == null ? null : organisation.LengthUnit.UnitName
               
            };


        }

        public MachineResponseModel CreateMachineByType(bool IsGuillotine)
        {

            Organisation organisation = organisationRepository.GetOrganizatiobByID();
            Machine machine = new Machine {MachineName = "New Machine", SetupSpoilage = 20, RunningSpoilage = 3, Passes = 1, IsSpotColor = true};
            return new MachineResponseModel
            {
                machine = machine,
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

        public long AddMachine(Machine machine,MachineClickChargeZone ClickChargeZone,MachineMeterPerHourLookup MeterPerHour,MachineGuillotineCalc GuillotineCalc, IEnumerable<MachineGuilotinePtv> GuillotinePtv,int oType)
        {
            try
            {
                long NewLookupID = 0;
               //  int Type = 0;
                LookupMethod oLookupMethod = new LookupMethod();

                if (oType == 5)
                {
                    oLookupMethod.Name = "Click Charge Zone";
                    oLookupMethod.Type = 5;
                   // Type = 5;
                }
                else if (oType == 8)
                {
                    oLookupMethod.Name = "Meter Per Hour Lookup";
                    oLookupMethod.Type = 8;
                    //Type = 8;
                }
                else if (oType == 6)
                {
                    oLookupMethod.Name = "Guillotine Calculation Lookup";
                    oLookupMethod.Type = 6;
                  //  Type = 6;
                }

                oLookupMethod.OrganisationId = Convert.ToInt32(OrganisationId);
                db.LookupMethods.Add(oLookupMethod);
                db.SaveChanges();

                NewLookupID = oLookupMethod.MethodId;


                long NewMachineID = 0;
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
                omachine.Priority = 10000;
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
                omachine.LookupMethodId = NewLookupID;
                omachine.OrganisationId = OrganisationId;
                omachine.RunningSpoilage = machine.RunningSpoilage;
                omachine.SetupSpoilage = machine.SetupSpoilage;
                omachine.CoverageHigh = machine.CoverageHigh;
                omachine.CoverageLow = machine.CoverageLow;
                omachine.CoverageMedium = machine.CoverageMedium;
                omachine.isSheetFed = machine.isSheetFed;
                omachine.Passes = machine.Passes;
                omachine.IsSpotColor = machine.IsSpotColor;
                db.Machines.Add(omachine);
              //  db.SaveChanges();

               

               // NewMachineID = omachine.MachineId;


               
                    switch (oType)
                    {


                        case 5:
                            MachineClickChargeZone ClickChargeZoneLookup = new MachineClickChargeZone();
                            ClickChargeZoneLookup.MethodId = NewLookupID;
                            if (ClickChargeZone != null)
                            {
                                ClickChargeZoneLookup.From1 = ClickChargeZone.From1;
                                ClickChargeZoneLookup.To1 = ClickChargeZone.To1;
                                ClickChargeZoneLookup.Sheets1 = ClickChargeZone.Sheets1;
                                ClickChargeZoneLookup.SheetCost1 = ClickChargeZone.SheetCost1;
                                ClickChargeZoneLookup.SheetPrice1 = ClickChargeZone.SheetPrice1;
                                ClickChargeZoneLookup.From2 = ClickChargeZone.From2;
                                ClickChargeZoneLookup.To2 = ClickChargeZone.To2;
                                ClickChargeZoneLookup.Sheets2 = ClickChargeZone.Sheets2;
                                ClickChargeZoneLookup.SheetCost2 = ClickChargeZone.SheetCost2;
                                ClickChargeZoneLookup.SheetPrice2 = ClickChargeZone.SheetPrice2;
                                ClickChargeZoneLookup.From3 = ClickChargeZone.From3;
                                ClickChargeZoneLookup.To3 = ClickChargeZone.To3;
                                ClickChargeZoneLookup.Sheets3 = ClickChargeZone.Sheets3;
                                ClickChargeZoneLookup.SheetCost3 = ClickChargeZone.SheetCost3;
                                ClickChargeZoneLookup.SheetPrice3 = ClickChargeZone.SheetPrice3;
                                ClickChargeZoneLookup.From4 = ClickChargeZone.From4;
                                ClickChargeZoneLookup.To4 = ClickChargeZone.To4;
                                ClickChargeZoneLookup.Sheets4 = ClickChargeZone.Sheets4;
                                ClickChargeZoneLookup.SheetCost4 = ClickChargeZone.SheetCost4;
                                ClickChargeZoneLookup.SheetPrice4 = ClickChargeZone.SheetPrice4;
                                ClickChargeZoneLookup.From5 = ClickChargeZone.From5;
                                ClickChargeZoneLookup.To5 = ClickChargeZone.To5;
                                ClickChargeZoneLookup.Sheets5 = ClickChargeZone.Sheets5;
                                ClickChargeZoneLookup.SheetCost5 = ClickChargeZone.SheetCost5;
                                ClickChargeZoneLookup.SheetPrice5 = ClickChargeZone.SheetPrice5;
                                ClickChargeZoneLookup.From6 = ClickChargeZone.From6;
                                ClickChargeZoneLookup.To6 = ClickChargeZone.To6;
                                ClickChargeZoneLookup.Sheets6 = ClickChargeZone.Sheets6;
                                ClickChargeZoneLookup.SheetCost6 = ClickChargeZone.SheetCost6;
                                ClickChargeZoneLookup.SheetPrice6 = ClickChargeZone.SheetPrice6;
                                ClickChargeZoneLookup.From7 = ClickChargeZone.From7;
                                ClickChargeZoneLookup.To7 = ClickChargeZone.To7;
                                ClickChargeZoneLookup.Sheets7 = ClickChargeZone.Sheets7;
                                ClickChargeZoneLookup.SheetCost7 = ClickChargeZone.SheetCost7;
                                ClickChargeZoneLookup.SheetPrice7 = ClickChargeZone.SheetPrice7;
                                ClickChargeZoneLookup.From8 = ClickChargeZone.From8;
                                ClickChargeZoneLookup.To8 = ClickChargeZone.To8;
                                ClickChargeZoneLookup.Sheets8 = ClickChargeZone.Sheets8;
                                ClickChargeZoneLookup.SheetCost8 = ClickChargeZone.SheetCost8;
                                ClickChargeZoneLookup.SheetPrice8 = ClickChargeZone.SheetPrice8;
                                ClickChargeZoneLookup.From9 = ClickChargeZone.From9;
                                ClickChargeZoneLookup.To9 = ClickChargeZone.To9;
                                ClickChargeZoneLookup.Sheets9 = ClickChargeZone.Sheets9;
                                ClickChargeZoneLookup.SheetCost9 = ClickChargeZone.SheetCost9;
                                ClickChargeZoneLookup.SheetPrice9 = ClickChargeZone.SheetPrice9;
                                ClickChargeZoneLookup.From10 = ClickChargeZone.From10;
                                ClickChargeZoneLookup.To10 = ClickChargeZone.To10;
                                ClickChargeZoneLookup.Sheets10 = ClickChargeZone.Sheets10;
                                ClickChargeZoneLookup.SheetCost10 = ClickChargeZone.SheetCost10;
                                ClickChargeZoneLookup.SheetPrice10 = ClickChargeZone.SheetPrice10;
                                ClickChargeZoneLookup.From11 = ClickChargeZone.From11;
                                ClickChargeZoneLookup.To11 = ClickChargeZone.To11;
                                ClickChargeZoneLookup.Sheets11 = ClickChargeZone.Sheets11;
                                ClickChargeZoneLookup.SheetCost11 = ClickChargeZone.SheetCost11;
                                ClickChargeZoneLookup.SheetPrice11 = ClickChargeZone.SheetPrice11;
                                ClickChargeZoneLookup.From12 = ClickChargeZone.From12;
                                ClickChargeZoneLookup.To12 = ClickChargeZone.To12;
                                ClickChargeZoneLookup.Sheets12 = ClickChargeZone.Sheets12;
                                ClickChargeZoneLookup.SheetCost12 = ClickChargeZone.SheetCost12;
                                ClickChargeZoneLookup.SheetPrice12 = ClickChargeZone.SheetPrice12;
                                ClickChargeZoneLookup.From13 = ClickChargeZone.From13;
                                ClickChargeZoneLookup.To13 = ClickChargeZone.To13;
                                ClickChargeZoneLookup.Sheets13 = ClickChargeZone.Sheets13;
                                ClickChargeZoneLookup.SheetCost13 = ClickChargeZone.SheetCost13;
                                ClickChargeZoneLookup.SheetPrice13 = ClickChargeZone.SheetPrice13;
                                ClickChargeZoneLookup.From14 = ClickChargeZone.From14;
                                ClickChargeZoneLookup.To14 = ClickChargeZone.To14;
                                ClickChargeZoneLookup.Sheets14 = ClickChargeZone.Sheets14;
                                ClickChargeZoneLookup.SheetCost14 = ClickChargeZone.SheetCost14;
                                ClickChargeZoneLookup.SheetPrice14 = ClickChargeZone.SheetPrice14;
                                ClickChargeZoneLookup.From15 = ClickChargeZone.From15;
                                ClickChargeZoneLookup.To15 = ClickChargeZone.To15;
                                ClickChargeZoneLookup.Sheets15 = ClickChargeZone.Sheets15;
                                ClickChargeZoneLookup.SheetCost15 = ClickChargeZone.SheetCost15;
                                ClickChargeZoneLookup.SheetPrice15 = ClickChargeZone.SheetPrice15;
                                ClickChargeZoneLookup.isaccumulativecharge = ClickChargeZone.isaccumulativecharge;
                                ClickChargeZoneLookup.IsRoundUp = ClickChargeZone.IsRoundUp;
                                ClickChargeZoneLookup.TimePerHour = ClickChargeZone.TimePerHour;
                                db.MachineClickChargeZones.Add(ClickChargeZoneLookup);
                            }
                          
                            
                            break;
                        case 6:
                            MachineGuillotineCalc GuillotineCalcu = new MachineGuillotineCalc();
                            GuillotineCalcu.MethodId = oLookupMethod.MethodId;
                            if (GuillotineCalc != null)
                            {
                                GuillotineCalcu.PaperWeight1 = GuillotineCalc.PaperWeight1;
                                GuillotineCalcu.PaperThroatQty1 = GuillotineCalc.PaperThroatQty1;
                                GuillotineCalcu.PaperWeight2 = GuillotineCalc.PaperWeight2;
                                GuillotineCalcu.PaperThroatQty2 = GuillotineCalc.PaperThroatQty2;
                                GuillotineCalcu.PaperWeight3 = GuillotineCalc.PaperWeight3;
                                GuillotineCalcu.PaperThroatQty3 = GuillotineCalc.PaperThroatQty3;
                                GuillotineCalcu.PaperWeight4 = GuillotineCalc.PaperWeight4;
                                GuillotineCalcu.PaperThroatQty4 = GuillotineCalc.PaperThroatQty4;
                                GuillotineCalcu.PaperWeight5 = GuillotineCalc.PaperWeight5;
                                GuillotineCalcu.PaperThroatQty5 = GuillotineCalc.PaperThroatQty5;
                                db.MachineGuillotineCalcs.Add(GuillotineCalcu);
                                if (db.SaveChanges() > 0)
                                {
                                    foreach (MachineGuilotinePtv item in GuillotinePtv)
                                    {
                                        MachineGuilotinePtv oMachineGuilotinePtv = new MachineGuilotinePtv();
                                        oMachineGuilotinePtv.GuilotineId = Convert.ToInt32(omachine.MachineId);
                                        oMachineGuilotinePtv.NoofSections = item.NoofSections;
                                        oMachineGuilotinePtv.NoofUps = item.NoofUps;
                                        oMachineGuilotinePtv.Noofcutswithoutgutters = item.Noofcutswithoutgutters;
                                        oMachineGuilotinePtv.Noofcutswithgutters = item.Noofcutswithgutters;
                                        db.MachineGuilotinePtvs.Add(oMachineGuilotinePtv);


                                    }
                                }
                            }
                            
                            break;
                        case 8:
                            MachineMeterPerHourLookup oMeterPerHourLookup = new MachineMeterPerHourLookup();
                            oMeterPerHourLookup.MethodId = oLookupMethod.MethodId;
                            if(MeterPerHour != null)
                            {
                                oMeterPerHourLookup.SheetsQty1 = MeterPerHour.SheetsQty1;
                                oMeterPerHourLookup.SheetsQty2 = MeterPerHour.SheetsQty2;
                                oMeterPerHourLookup.SheetsQty3 = MeterPerHour.SheetsQty3;
                                oMeterPerHourLookup.SheetsQty4 = MeterPerHour.SheetsQty4;
                                oMeterPerHourLookup.SheetsQty5 = MeterPerHour.SheetsQty5;
                                oMeterPerHourLookup.SheetWeight1 = MeterPerHour.SheetWeight1;
                                oMeterPerHourLookup.speedqty11 = MeterPerHour.speedqty11;
                                oMeterPerHourLookup.speedqty12 = MeterPerHour.speedqty12;
                                oMeterPerHourLookup.speedqty13 = MeterPerHour.speedqty13;
                                oMeterPerHourLookup.speedqty14 = MeterPerHour.speedqty14;
                                oMeterPerHourLookup.speedqty15 = MeterPerHour.speedqty15;
                                oMeterPerHourLookup.SheetWeight2 = MeterPerHour.SheetWeight2;
                                oMeterPerHourLookup.speedqty21 = MeterPerHour.speedqty21;
                                oMeterPerHourLookup.speedqty22 = MeterPerHour.speedqty22;
                                oMeterPerHourLookup.speedqty23 = MeterPerHour.speedqty23;
                                oMeterPerHourLookup.speedqty24 = MeterPerHour.speedqty24;
                                oMeterPerHourLookup.speedqty25 = MeterPerHour.speedqty25;
                                oMeterPerHourLookup.SheetWeight3 = MeterPerHour.SheetWeight3;
                                oMeterPerHourLookup.speedqty31 = MeterPerHour.speedqty31;
                                oMeterPerHourLookup.speedqty32 = MeterPerHour.speedqty32;
                                oMeterPerHourLookup.speedqty33 = MeterPerHour.speedqty33;
                                oMeterPerHourLookup.speedqty34 = MeterPerHour.speedqty34;
                                oMeterPerHourLookup.speedqty35 = MeterPerHour.speedqty35;
                                oMeterPerHourLookup.hourlyCost = MeterPerHour.hourlyCost;
                                oMeterPerHourLookup.hourlyPrice = MeterPerHour.hourlyPrice;
                                db.MachineMeterPerHourLookups.Add(oMeterPerHourLookup);
                            }
                           
                            break;
                        default:
                            return 0;


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
        public bool UpdateMachine(Machine machine, MachineClickChargeZone ClickChargeZone, MachineMeterPerHourLookup MeterPerHour, MachineGuillotineCalc GuillotineCalc, IEnumerable<MachineGuilotinePtv> GuillotinePtv, int type)
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
                omachine.Priority = 10000;
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
                omachine.RunningSpoilage = machine.RunningSpoilage;
                omachine.SetupSpoilage = machine.SetupSpoilage;
                omachine.CoverageHigh = machine.CoverageHigh;
                omachine.CoverageLow = machine.CoverageLow;
                omachine.CoverageMedium = machine.CoverageMedium;
                omachine.Passes = machine.Passes;
                omachine.IsSpotColor = machine.IsSpotColor;
               // omachine.LookupMethod.MachineClickChargeZones.ToList().ForEach(a => a = ClickCharge);

                if (type == 0)
                {
                    foreach (var ClickChargeZoneLookup in omachine.LookupMethod.MachineClickChargeZones)
                    {


                        ClickChargeZoneLookup.From1 = ClickChargeZone.From1;
                        ClickChargeZoneLookup.To1 = ClickChargeZone.To1;
                        ClickChargeZoneLookup.Sheets1 = ClickChargeZone.Sheets1;
                        ClickChargeZoneLookup.SheetCost1 = ClickChargeZone.SheetCost1;
                        ClickChargeZoneLookup.SheetPrice1 = ClickChargeZone.SheetPrice1;
                        ClickChargeZoneLookup.From2 = ClickChargeZone.From2;
                        ClickChargeZoneLookup.To2 = ClickChargeZone.To2;
                        ClickChargeZoneLookup.Sheets2 = ClickChargeZone.Sheets2;
                        ClickChargeZoneLookup.SheetCost2 = ClickChargeZone.SheetCost2;
                        ClickChargeZoneLookup.SheetPrice2 = ClickChargeZone.SheetPrice2;
                        ClickChargeZoneLookup.From3 = ClickChargeZone.From3;
                        ClickChargeZoneLookup.To3 = ClickChargeZone.To3;
                        ClickChargeZoneLookup.Sheets3 = ClickChargeZone.Sheets3;
                        ClickChargeZoneLookup.SheetCost3 = ClickChargeZone.SheetCost3;
                        ClickChargeZoneLookup.SheetPrice3 = ClickChargeZone.SheetPrice3;
                        ClickChargeZoneLookup.From4 = ClickChargeZone.From4;
                        ClickChargeZoneLookup.To4 = ClickChargeZone.To4;
                        ClickChargeZoneLookup.Sheets4 = ClickChargeZone.Sheets4;
                        ClickChargeZoneLookup.SheetCost4 = ClickChargeZone.SheetCost4;
                        ClickChargeZoneLookup.SheetPrice4 = ClickChargeZone.SheetPrice4;
                        ClickChargeZoneLookup.From5 = ClickChargeZone.From5;
                        ClickChargeZoneLookup.To5 = ClickChargeZone.To5;
                        ClickChargeZoneLookup.Sheets5 = ClickChargeZone.Sheets5;
                        ClickChargeZoneLookup.SheetCost5 = ClickChargeZone.SheetCost5;
                        ClickChargeZoneLookup.SheetPrice5 = ClickChargeZone.SheetPrice5;
                        ClickChargeZoneLookup.From6 = ClickChargeZone.From6;
                        ClickChargeZoneLookup.To6 = ClickChargeZone.To6;
                        ClickChargeZoneLookup.Sheets6 = ClickChargeZone.Sheets6;
                        ClickChargeZoneLookup.SheetCost6 = ClickChargeZone.SheetCost6;
                        ClickChargeZoneLookup.SheetPrice6 = ClickChargeZone.SheetPrice6;
                        ClickChargeZoneLookup.From7 = ClickChargeZone.From7;
                        ClickChargeZoneLookup.To7 = ClickChargeZone.To7;
                        ClickChargeZoneLookup.Sheets7 = ClickChargeZone.Sheets7;
                        ClickChargeZoneLookup.SheetCost7 = ClickChargeZone.SheetCost7;
                        ClickChargeZoneLookup.SheetPrice7 = ClickChargeZone.SheetPrice7;
                        ClickChargeZoneLookup.From8 = ClickChargeZone.From8;
                        ClickChargeZoneLookup.To8 = ClickChargeZone.To8;
                        ClickChargeZoneLookup.Sheets8 = ClickChargeZone.Sheets8;
                        ClickChargeZoneLookup.SheetCost8 = ClickChargeZone.SheetCost8;
                        ClickChargeZoneLookup.SheetPrice8 = ClickChargeZone.SheetPrice8;
                        ClickChargeZoneLookup.From9 = ClickChargeZone.From9;
                        ClickChargeZoneLookup.To9 = ClickChargeZone.To9;
                        ClickChargeZoneLookup.Sheets9 = ClickChargeZone.Sheets9;
                        ClickChargeZoneLookup.SheetCost9 = ClickChargeZone.SheetCost9;
                        ClickChargeZoneLookup.SheetPrice9 = ClickChargeZone.SheetPrice9;
                        ClickChargeZoneLookup.From10 = ClickChargeZone.From10;
                        ClickChargeZoneLookup.To10 = ClickChargeZone.To10;
                        ClickChargeZoneLookup.Sheets10 = ClickChargeZone.Sheets10;
                        ClickChargeZoneLookup.SheetCost10 = ClickChargeZone.SheetCost10;
                        ClickChargeZoneLookup.SheetPrice10 = ClickChargeZone.SheetPrice10;
                        ClickChargeZoneLookup.From11 = ClickChargeZone.From11;
                        ClickChargeZoneLookup.To11 = ClickChargeZone.To11;
                        ClickChargeZoneLookup.Sheets11 = ClickChargeZone.Sheets11;
                        ClickChargeZoneLookup.SheetCost11 = ClickChargeZone.SheetCost11;
                        ClickChargeZoneLookup.SheetPrice11 = ClickChargeZone.SheetPrice11;
                        ClickChargeZoneLookup.From12 = ClickChargeZone.From12;
                        ClickChargeZoneLookup.To12 = ClickChargeZone.To12;
                        ClickChargeZoneLookup.Sheets12 = ClickChargeZone.Sheets12;
                        ClickChargeZoneLookup.SheetCost12 = ClickChargeZone.SheetCost12;
                        ClickChargeZoneLookup.SheetPrice12 = ClickChargeZone.SheetPrice12;
                        ClickChargeZoneLookup.From13 = ClickChargeZone.From13;
                        ClickChargeZoneLookup.To13 = ClickChargeZone.To13;
                        ClickChargeZoneLookup.Sheets13 = ClickChargeZone.Sheets13;
                        ClickChargeZoneLookup.SheetCost13 = ClickChargeZone.SheetCost13;
                        ClickChargeZoneLookup.SheetPrice13 = ClickChargeZone.SheetPrice13;
                        ClickChargeZoneLookup.From14 = ClickChargeZone.From14;
                        ClickChargeZoneLookup.To14 = ClickChargeZone.To14;
                        ClickChargeZoneLookup.Sheets14 = ClickChargeZone.Sheets14;
                        ClickChargeZoneLookup.SheetCost14 = ClickChargeZone.SheetCost14;
                        ClickChargeZoneLookup.SheetPrice14 = ClickChargeZone.SheetPrice14;
                        ClickChargeZoneLookup.From15 = ClickChargeZone.From15;
                        ClickChargeZoneLookup.To15 = ClickChargeZone.To15;
                        ClickChargeZoneLookup.Sheets15 = ClickChargeZone.Sheets15;
                        ClickChargeZoneLookup.SheetCost15 = ClickChargeZone.SheetCost15;
                        ClickChargeZoneLookup.SheetPrice15 = ClickChargeZone.SheetPrice15;
                        ClickChargeZoneLookup.isaccumulativecharge = ClickChargeZone.isaccumulativecharge;
                        ClickChargeZoneLookup.IsRoundUp = ClickChargeZone.IsRoundUp;
                        ClickChargeZoneLookup.TimePerHour = ClickChargeZone.TimePerHour;
                    }

                }
                else if(type == 1)
                {
                    foreach (var oMeterPerHourLookup in omachine.LookupMethod.MachineMeterPerHourLookups)
                    {

                        oMeterPerHourLookup.SheetsQty1 = MeterPerHour.SheetsQty1;
                        oMeterPerHourLookup.SheetsQty2 = MeterPerHour.SheetsQty2;
                        oMeterPerHourLookup.SheetsQty3 = MeterPerHour.SheetsQty3;
                        oMeterPerHourLookup.SheetsQty4 = MeterPerHour.SheetsQty4;
                        oMeterPerHourLookup.SheetsQty5 = MeterPerHour.SheetsQty5;
                        oMeterPerHourLookup.SheetWeight1 = MeterPerHour.SheetWeight1;
                        oMeterPerHourLookup.speedqty11 = MeterPerHour.speedqty11;
                        oMeterPerHourLookup.speedqty12 = MeterPerHour.speedqty12;
                        oMeterPerHourLookup.speedqty13 = MeterPerHour.speedqty13;
                        oMeterPerHourLookup.speedqty14 = MeterPerHour.speedqty14;
                        oMeterPerHourLookup.speedqty15 = MeterPerHour.speedqty15;
                        oMeterPerHourLookup.SheetWeight2 = MeterPerHour.SheetWeight2;
                        oMeterPerHourLookup.speedqty21 = MeterPerHour.speedqty21;
                        oMeterPerHourLookup.speedqty22 = MeterPerHour.speedqty22;
                        oMeterPerHourLookup.speedqty23 = MeterPerHour.speedqty23;
                        oMeterPerHourLookup.speedqty24 = MeterPerHour.speedqty24;
                        oMeterPerHourLookup.speedqty25 = MeterPerHour.speedqty25;
                        oMeterPerHourLookup.SheetWeight3 = MeterPerHour.SheetWeight3;
                        oMeterPerHourLookup.speedqty31 = MeterPerHour.speedqty31;
                        oMeterPerHourLookup.speedqty32 = MeterPerHour.speedqty32;
                        oMeterPerHourLookup.speedqty33 = MeterPerHour.speedqty33;
                        oMeterPerHourLookup.speedqty34 = MeterPerHour.speedqty34;
                        oMeterPerHourLookup.speedqty35 = MeterPerHour.speedqty35;
                        oMeterPerHourLookup.hourlyCost = MeterPerHour.hourlyCost;
                        oMeterPerHourLookup.hourlyPrice = MeterPerHour.hourlyPrice;
                    
                    
                    
                    
                    }

                }
                else if(type == 2)
                {
                    foreach (var GuillotineCalcu in omachine.LookupMethod.MachineGuillotineCalcs)
                    {
                        GuillotineCalcu.PaperWeight1 = GuillotineCalc.PaperWeight1;
                        GuillotineCalcu.PaperThroatQty1 = GuillotineCalc.PaperThroatQty1;
                        GuillotineCalcu.PaperWeight2 = GuillotineCalc.PaperWeight2;
                        GuillotineCalcu.PaperThroatQty2 = GuillotineCalc.PaperThroatQty2;
                        GuillotineCalcu.PaperWeight3 = GuillotineCalc.PaperWeight3;
                        GuillotineCalcu.PaperThroatQty3 = GuillotineCalc.PaperThroatQty3;
                        GuillotineCalcu.PaperWeight4 = GuillotineCalc.PaperWeight4;
                        GuillotineCalcu.PaperThroatQty4 = GuillotineCalc.PaperThroatQty4;
                        GuillotineCalcu.PaperWeight5 = GuillotineCalc.PaperWeight5;
                        GuillotineCalcu.PaperThroatQty5 = GuillotineCalc.PaperThroatQty5;
                      //.  db.MachineGuillotineCalcs.Add(GuillotineCalcu);
                       
                        
                            foreach (MachineGuilotinePtv item in GuillotinePtv)
                            {
                                if (item.Id > 0)
                                {
                                    MachineGuilotinePtv oMachineGuilotinePtv = db.MachineGuilotinePtvs.Where(g => g.Id == item.Id).SingleOrDefault();
                                    oMachineGuilotinePtv.NoofSections = item.NoofSections;
                                    oMachineGuilotinePtv.NoofUps = item.NoofUps;
                                    oMachineGuilotinePtv.Noofcutswithoutgutters = item.Noofcutswithoutgutters;
                                    oMachineGuilotinePtv.Noofcutswithgutters = item.Noofcutswithgutters;
                                }
                                else
                                {
                                    MachineGuilotinePtv oMachineGuilotinePtv = new MachineGuilotinePtv();
                                    oMachineGuilotinePtv.GuilotineId = Convert.ToInt32(omachine.MachineId);
                                    oMachineGuilotinePtv.NoofSections = item.NoofSections;
                                    oMachineGuilotinePtv.NoofUps = item.NoofUps;
                                    oMachineGuilotinePtv.Noofcutswithoutgutters = item.Noofcutswithoutgutters;
                                    oMachineGuilotinePtv.Noofcutswithgutters = item.Noofcutswithgutters;
                                    db.MachineGuilotinePtvs.Add(oMachineGuilotinePtv);
                                }
                            }
                        
                    }
                }
               
               // omachine.LookupMethod.MachineClickChargeZones.FirstOrDefault() = ClickCharge;
                //foreach (var item in machine.MachineInkCoverages)
                //{
                //    MachineInkCoverage obj = db.MachineInkCoverages.Where(g => g.Id == item.Id).SingleOrDefault();
                //    obj.SideInkOrder = item.SideInkOrder;
                //    obj.SideInkOrderCoverage = item.SideInkOrderCoverage;
                //}

                //foreach (var item in MachineSpoilages)
                //{
                //    MachineSpoilage obj = db.MachineSpoilages.Where(g => g.MachineSpoilageId == item.MachineSpoilageId).SingleOrDefault();
                //    obj.RunningSpoilage = item.RunningSpoilage;
                //    obj.SetupSpoilage = item.SetupSpoilage;

                //}

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

        public IEnumerable<MachineLookupMethod> GetMachineLookupMethods(long machineId)
        {
            return db.MachineLookupMethods.Where(g => g.MachineId == machineId).ToList();
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
               .ForMember(x => x.ItemSections, opt => opt.Ignore())
               .ForMember(x => x.ItemSectionsSide2, opt => opt.Ignore());


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
                    .ForMember(x => x.Machines, opt => opt.Ignore());

                Mapper.CreateMap<MachineClickChargeLookup, MachineClickChargeLookup>()
              .ForMember(x => x.LookupMethod, opt => opt.Ignore());

                Mapper.CreateMap<MachineClickChargeZone, MachineClickChargeZone>()
            .ForMember(x => x.LookupMethod, opt => opt.Ignore());

                Mapper.CreateMap<MachineGuillotineCalc, MachineGuillotineCalc>()
          .ForMember(x => x.LookupMethod, opt => opt.Ignore());

                Mapper.CreateMap<MachinePerHourLookup, MachinePerHourLookup>()
         .ForMember(x => x.LookupMethod, opt => opt.Ignore());


                Mapper.CreateMap<MachineSpeedWeightLookup, MachineSpeedWeightLookup>()
         .ForMember(x => x.LookupMethod, opt => opt.Ignore());


                Mapper.CreateMap<MachineMeterPerHourLookup, MachineMeterPerHourLookup>()
         .ForMember(x => x.LookupMethod, opt => opt.Ignore());

                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;

                List<LookupMethod> methods = db.LookupMethods.Include("MachineClickChargeLookups").Include("MachineClickChargeZones").Include("MachineGuillotineCalcs").Include("MachinePerHourLookups").Include("MachineSpeedWeightLookups").Include("MachineMeterPerHourLookups").Where(o => o.OrganisationId == OID).ToList();

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

        public List<MachineGuilotinePtv> getGuilotinePtv(long GuilotineId)
        {
            try
            {
                return db.MachineGuilotinePtvs.Where(c => c.GuilotineId == GuilotineId).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public string GetInkPlatesSidesByInkID(long InkID)
        {
          return  db.InkPlateSides.Where(x => x.PlateInkId == InkID).Select(a => a.InkTitle).FirstOrDefault();
        }

        public string GetMachineByID(int MachineID)
        {
            return db.Machines.Where(c => c.MachineId == MachineID).Select(c => c.MachineName).FirstOrDefault();
        }

        public Machine GetDefaultGuillotine()
        {
            return DbSet.FirstOrDefault(m => m.MachineCatId == (int)MachineCategories.Guillotin && m.OrganisationId == OrganisationId && (m.IsDisabled == false || m.IsDisabled == null));
        }

    }
}