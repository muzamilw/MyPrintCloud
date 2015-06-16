using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.ResponseModels;

namespace MPC.Repository.Repositories
{
    public class LookupMethodRepository : BaseRepository<LookupMethod>, ILookupMethodRepository
    {
        #region Constructor
        private readonly IOrganisationRepository organisationRepository;
        public LookupMethodRepository(IUnityContainer container, IOrganisationRepository organisationRepository)
            : base(container)
        {
            this.organisationRepository = organisationRepository;
        }
        protected override IDbSet<LookupMethod> DbSet
        {
            get
            {
                return db.LookupMethods;
            }
        }

       

        public LookupMethodListResponse GetAll()
        {
            Organisation organisation = organisationRepository.GetOrganizatiobByID();
            return new LookupMethodListResponse {
                LookupMethods = DbSet.Where(g => g.OrganisationId == OrganisationId || g.OrganisationId == 0).ToList(),
                CurrencySymbol = organisation == null ? null : organisation.Currency.CurrencySymbol,
                WeightUnit = organisation == null ? null : organisation.WeightUnit.UnitName,
                LengthUnit = organisation == null ? null : organisation.LengthUnit.UnitName
            };

                
        }

        public bool DeleteMachineLookup(long id)
        {
            IEnumerable<Machine> machines = db.Machines.Where(g => g.LookupMethodId == id).ToList();
            if (machines.Count() > 0)
            {
                return false;
            }
            else
            {
                LookupMethod olookup = db.LookupMethods.Where(g => g.MethodId == id).SingleOrDefault();
                switch (olookup.Type)
                {
                    case 1:
                        MachineClickChargeLookup ClickChargeLookup = db.MachineClickChargeLookups.Where(g => g.MethodId == olookup.MethodId).SingleOrDefault();
                        db.MachineClickChargeLookups.Remove(ClickChargeLookup);
                        break;
                    case 3:
                        MachineSpeedWeightLookup SpeedWeightLookup = db.MachineSpeedWeightLookups.Where(g => g.MethodId == olookup.MethodId).SingleOrDefault();

                        db.MachineSpeedWeightLookups.Remove(SpeedWeightLookup);
                        break;
                    case 4:
                        MachinePerHourLookup PerHourLookup = db.MachinePerHourLookups.Where(g => g.MethodId == olookup.MethodId).SingleOrDefault();
                        db.MachinePerHourLookups.Remove(PerHourLookup);
                        break;
                    case 5:
                        MachineClickChargeZone ClickChargeZoneLookup = db.MachineClickChargeZones.Where(g => g.MethodId == olookup.MethodId).SingleOrDefault();
                        db.MachineClickChargeZones.Remove(ClickChargeZoneLookup);
                       break;
                    case 6:
                        MachineGuillotineCalc GuillotineCalc = db.MachineGuillotineCalcs.Where(g => g.MethodId == olookup.MethodId).SingleOrDefault();
                        IEnumerable<MachineGuilotinePtv> MachineGuilotinePtvs = db.MachineGuilotinePtvs.Where(g => g.GuilotineId == GuillotineCalc.Id).ToList();
                        db.MachineGuilotinePtvs.RemoveRange(MachineGuilotinePtvs);
                        db.MachineGuillotineCalcs.Remove(GuillotineCalc);
                        break;
                    case 8:
                        MachineMeterPerHourLookup oMeterPerHourLookup = db.MachineMeterPerHourLookups.Where(g => g.MethodId == olookup.MethodId).SingleOrDefault();
                        db.MachineMeterPerHourLookups.Remove(oMeterPerHourLookup);

                        break;
                    default:
                        return false;

                }

                db.LookupMethods.Remove(olookup);
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }
        public bool DeleteGuillotinePTVId(long id)
        {
            MachineGuilotinePtv GuilotinePtv = db.MachineGuilotinePtvs.Where(g => g.Id == id).FirstOrDefault();
            if (GuilotinePtv != null)
            {

                db.MachineGuilotinePtvs.Remove(GuilotinePtv);
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }


        }
        public LookupMethod AddLookup(LookupMethodResponse response)
        {

            int Type = 0; 
            LookupMethod oLookupMethod = new LookupMethod();
            oLookupMethod.Name = response.LookupMethod.Name;
            if(response.ClickChargeZone != null)
            {
                oLookupMethod.Name = "Click Charge Zone";
                oLookupMethod.Type = 5;
                Type = 5;
            }
            else if(response.MeterPerHourLookup != null)
            {
                oLookupMethod.Name = "Meter Per Hour Lookup";
                oLookupMethod.Type = 8;
                Type = 8;
            }
            else if (response.GuillotineCalc != null)
            {
                oLookupMethod.Name = "Guillotine Calculation Lookup";
                oLookupMethod.Type = 6;
                Type = 6;
            }
            
            oLookupMethod.OrganisationId = Convert.ToInt32(OrganisationId);
            db.LookupMethods.Add(oLookupMethod);
            if (db.SaveChanges() > 0)
            {

                switch (Type)
                {
                    case 1:
                        MachineClickChargeLookup ClickChargeLookup = new MachineClickChargeLookup();
                        ClickChargeLookup.MethodId = oLookupMethod.MethodId;
                        ClickChargeLookup.SheetCost = response.ClickChargeLookup.SheetCost;
                        ClickChargeLookup.Sheets = response.ClickChargeLookup.Sheets;
                        ClickChargeLookup.SheetPrice = response.ClickChargeLookup.SheetPrice;
                        db.MachineClickChargeLookups.Add(ClickChargeLookup);
                        break;
                    case 3:
                        MachineSpeedWeightLookup SpeedWeightLookup = new MachineSpeedWeightLookup();
                        SpeedWeightLookup.MethodId = oLookupMethod.MethodId;
                        SpeedWeightLookup.SheetsQty1 = response.SpeedWeightLookup.SheetsQty1;
                        SpeedWeightLookup.SheetsQty2 = response.SpeedWeightLookup.SheetsQty2;
                        SpeedWeightLookup.SheetsQty3 = response.SpeedWeightLookup.SheetsQty3;
                        SpeedWeightLookup.SheetsQty4 = response.SpeedWeightLookup.SheetsQty4;
                        SpeedWeightLookup.SheetsQty5 = response.SpeedWeightLookup.SheetsQty5;
                        SpeedWeightLookup.SheetWeight1 = response.SpeedWeightLookup.SheetWeight1;
                        SpeedWeightLookup.speedqty11 = response.SpeedWeightLookup.speedqty11;
                        SpeedWeightLookup.speedqty12 = response.SpeedWeightLookup.speedqty12;
                        SpeedWeightLookup.speedqty13 = response.SpeedWeightLookup.speedqty13;
                        SpeedWeightLookup.speedqty14 = response.SpeedWeightLookup.speedqty14;
                        SpeedWeightLookup.speedqty15 = response.SpeedWeightLookup.speedqty15;
                        SpeedWeightLookup.SheetWeight2 = response.SpeedWeightLookup.SheetWeight2;
                        SpeedWeightLookup.speedqty21 = response.SpeedWeightLookup.speedqty21;
                        SpeedWeightLookup.speedqty22 = response.SpeedWeightLookup.speedqty22;
                        SpeedWeightLookup.speedqty23 = response.SpeedWeightLookup.speedqty23;
                        SpeedWeightLookup.speedqty24 = response.SpeedWeightLookup.speedqty24;
                        SpeedWeightLookup.speedqty25 = response.SpeedWeightLookup.speedqty25;
                        SpeedWeightLookup.SheetWeight3 = response.SpeedWeightLookup.SheetWeight3;
                        SpeedWeightLookup.speedqty31 = response.SpeedWeightLookup.speedqty31;
                        SpeedWeightLookup.speedqty32 = response.SpeedWeightLookup.speedqty32;
                        SpeedWeightLookup.speedqty33 = response.SpeedWeightLookup.speedqty33;
                        SpeedWeightLookup.speedqty34 = response.SpeedWeightLookup.speedqty34;
                        SpeedWeightLookup.speedqty35 = response.SpeedWeightLookup.speedqty35;
                        SpeedWeightLookup.hourlyCost = response.SpeedWeightLookup.hourlyCost;
                        SpeedWeightLookup.hourlyPrice = response.SpeedWeightLookup.hourlyPrice;
                        db.MachineSpeedWeightLookups.Add(SpeedWeightLookup);
                        break;
                    case 4:
                        MachinePerHourLookup PerHourLookup = new MachinePerHourLookup();
                        PerHourLookup.MethodId = oLookupMethod.MethodId;
                        PerHourLookup.SpeedCost = response.PerHourLookup.SpeedCost;
                        PerHourLookup.Speed = response.PerHourLookup.Speed;
                        PerHourLookup.SpeedPrice = response.PerHourLookup.SpeedPrice;
                        db.MachinePerHourLookups.Add(PerHourLookup);
                        break;
                    case 5:
                        MachineClickChargeZone ClickChargeZoneLookup = new MachineClickChargeZone();
                        ClickChargeZoneLookup.MethodId = oLookupMethod.MethodId;
                        ClickChargeZoneLookup.From1 = response.ClickChargeZone.From1;
                        ClickChargeZoneLookup.To1 = response.ClickChargeZone.To1;
                        ClickChargeZoneLookup.Sheets1 = response.ClickChargeZone.Sheets1;
                        ClickChargeZoneLookup.SheetCost1 = response.ClickChargeZone.SheetCost1;
                        ClickChargeZoneLookup.SheetPrice1 = response.ClickChargeZone.SheetPrice1;
                        ClickChargeZoneLookup.From2 = response.ClickChargeZone.From2;
                        ClickChargeZoneLookup.To2 = response.ClickChargeZone.To2;
                        ClickChargeZoneLookup.Sheets2 = response.ClickChargeZone.Sheets2;
                        ClickChargeZoneLookup.SheetCost2 = response.ClickChargeZone.SheetCost2;
                        ClickChargeZoneLookup.SheetPrice2 = response.ClickChargeZone.SheetPrice2;
                        ClickChargeZoneLookup.From3 = response.ClickChargeZone.From3;
                        ClickChargeZoneLookup.To3 = response.ClickChargeZone.To3;
                        ClickChargeZoneLookup.Sheets3 = response.ClickChargeZone.Sheets3;
                        ClickChargeZoneLookup.SheetCost3 = response.ClickChargeZone.SheetCost3;
                        ClickChargeZoneLookup.SheetPrice3 = response.ClickChargeZone.SheetPrice3;
                        ClickChargeZoneLookup.From4 = response.ClickChargeZone.From4;
                        ClickChargeZoneLookup.To4 = response.ClickChargeZone.To4;
                        ClickChargeZoneLookup.Sheets4 = response.ClickChargeZone.Sheets4;
                        ClickChargeZoneLookup.SheetCost4 = response.ClickChargeZone.SheetCost4;
                        ClickChargeZoneLookup.SheetPrice4 = response.ClickChargeZone.SheetPrice4;
                        ClickChargeZoneLookup.From5 = response.ClickChargeZone.From5;
                        ClickChargeZoneLookup.To5 = response.ClickChargeZone.To5;
                        ClickChargeZoneLookup.Sheets5 = response.ClickChargeZone.Sheets5;
                        ClickChargeZoneLookup.SheetCost5 = response.ClickChargeZone.SheetCost5;
                        ClickChargeZoneLookup.SheetPrice5 = response.ClickChargeZone.SheetPrice5;
                        ClickChargeZoneLookup.From6 = response.ClickChargeZone.From6;
                        ClickChargeZoneLookup.To6 = response.ClickChargeZone.To6;
                        ClickChargeZoneLookup.Sheets6 = response.ClickChargeZone.Sheets6;
                        ClickChargeZoneLookup.SheetCost6 = response.ClickChargeZone.SheetCost6;
                        ClickChargeZoneLookup.SheetPrice6 = response.ClickChargeZone.SheetPrice6;
                        ClickChargeZoneLookup.From7 = response.ClickChargeZone.From7;
                        ClickChargeZoneLookup.To7 = response.ClickChargeZone.To7;
                        ClickChargeZoneLookup.Sheets7 = response.ClickChargeZone.Sheets7;
                        ClickChargeZoneLookup.SheetCost7 = response.ClickChargeZone.SheetCost7;
                        ClickChargeZoneLookup.SheetPrice7 = response.ClickChargeZone.SheetPrice7;
                        ClickChargeZoneLookup.From8 = response.ClickChargeZone.From8;
                        ClickChargeZoneLookup.To8 = response.ClickChargeZone.To8;
                        ClickChargeZoneLookup.Sheets8 = response.ClickChargeZone.Sheets8;
                        ClickChargeZoneLookup.SheetCost8 = response.ClickChargeZone.SheetCost8;
                        ClickChargeZoneLookup.SheetPrice8 = response.ClickChargeZone.SheetPrice8;
                        ClickChargeZoneLookup.From9 = response.ClickChargeZone.From9;
                        ClickChargeZoneLookup.To9 = response.ClickChargeZone.To9;
                        ClickChargeZoneLookup.Sheets9 = response.ClickChargeZone.Sheets9;
                        ClickChargeZoneLookup.SheetCost9 = response.ClickChargeZone.SheetCost9;
                        ClickChargeZoneLookup.SheetPrice9 = response.ClickChargeZone.SheetPrice9;
                        ClickChargeZoneLookup.From10 = response.ClickChargeZone.From10;
                        ClickChargeZoneLookup.To10 = response.ClickChargeZone.To10;
                        ClickChargeZoneLookup.Sheets10 = response.ClickChargeZone.Sheets10;
                        ClickChargeZoneLookup.SheetCost10 = response.ClickChargeZone.SheetCost10;
                        ClickChargeZoneLookup.SheetPrice10 = response.ClickChargeZone.SheetPrice10;
                        ClickChargeZoneLookup.From11 = response.ClickChargeZone.From11;
                        ClickChargeZoneLookup.To11 = response.ClickChargeZone.To11;
                        ClickChargeZoneLookup.Sheets11 = response.ClickChargeZone.Sheets11;
                        ClickChargeZoneLookup.SheetCost11 = response.ClickChargeZone.SheetCost11;
                        ClickChargeZoneLookup.SheetPrice11 = response.ClickChargeZone.SheetPrice11;
                        ClickChargeZoneLookup.From12 = response.ClickChargeZone.From12;
                        ClickChargeZoneLookup.To12 = response.ClickChargeZone.To12;
                        ClickChargeZoneLookup.Sheets12 = response.ClickChargeZone.Sheets12;
                        ClickChargeZoneLookup.SheetCost12 = response.ClickChargeZone.SheetCost12;
                        ClickChargeZoneLookup.SheetPrice12 = response.ClickChargeZone.SheetPrice12;
                        ClickChargeZoneLookup.From13 = response.ClickChargeZone.From13;
                        ClickChargeZoneLookup.To13 = response.ClickChargeZone.To13;
                        ClickChargeZoneLookup.Sheets13 = response.ClickChargeZone.Sheets13;
                        ClickChargeZoneLookup.SheetCost13 = response.ClickChargeZone.SheetCost13;
                        ClickChargeZoneLookup.SheetPrice13 = response.ClickChargeZone.SheetPrice13;
                        ClickChargeZoneLookup.From14 = response.ClickChargeZone.From14;
                        ClickChargeZoneLookup.To14 = response.ClickChargeZone.To14;
                        ClickChargeZoneLookup.Sheets14 = response.ClickChargeZone.Sheets14;
                        ClickChargeZoneLookup.SheetCost14 = response.ClickChargeZone.SheetCost14;
                        ClickChargeZoneLookup.SheetPrice14 = response.ClickChargeZone.SheetPrice14;
                        ClickChargeZoneLookup.From15 = response.ClickChargeZone.From15;
                        ClickChargeZoneLookup.To15 = response.ClickChargeZone.To15;
                        ClickChargeZoneLookup.Sheets15 = response.ClickChargeZone.Sheets15;
                        ClickChargeZoneLookup.SheetCost15 = response.ClickChargeZone.SheetCost15;
                        ClickChargeZoneLookup.SheetPrice15 = response.ClickChargeZone.SheetPrice15;
                        ClickChargeZoneLookup.isaccumulativecharge = response.ClickChargeZone.isaccumulativecharge;
                        ClickChargeZoneLookup.IsRoundUp = response.ClickChargeZone.IsRoundUp;
                        ClickChargeZoneLookup.TimePerHour = response.ClickChargeZone.TimePerHour;
                        db.MachineClickChargeZones.Add(ClickChargeZoneLookup);
                        break;
                    case 6:
                        MachineGuillotineCalc GuillotineCalc = new MachineGuillotineCalc();
                        GuillotineCalc.MethodId = oLookupMethod.MethodId;
                        GuillotineCalc.PaperWeight1 = response.GuillotineCalc.PaperWeight1;
                        GuillotineCalc.PaperThroatQty1 = response.GuillotineCalc.PaperThroatQty1;
                        GuillotineCalc.PaperWeight2 = response.GuillotineCalc.PaperWeight2;
                        GuillotineCalc.PaperThroatQty2 = response.GuillotineCalc.PaperThroatQty2;
                        GuillotineCalc.PaperWeight3 = response.GuillotineCalc.PaperWeight3;
                        GuillotineCalc.PaperThroatQty3 = response.GuillotineCalc.PaperThroatQty3;
                        GuillotineCalc.PaperWeight4 = response.GuillotineCalc.PaperWeight4;
                        GuillotineCalc.PaperThroatQty4 = response.GuillotineCalc.PaperThroatQty4;
                        GuillotineCalc.PaperWeight5 = response.GuillotineCalc.PaperWeight5;
                        GuillotineCalc.PaperThroatQty5 = response.GuillotineCalc.PaperThroatQty5;
                        db.MachineGuillotineCalcs.Add(GuillotineCalc);
                        if (db.SaveChanges() > 0)
                        {
                            foreach (MachineGuilotinePtv item in response.GuilotinePtv)
                            {
                                MachineGuilotinePtv oMachineGuilotinePtv = new MachineGuilotinePtv();
                                oMachineGuilotinePtv.GuilotineId = Convert.ToInt32(GuillotineCalc.Id);
                                oMachineGuilotinePtv.NoofSections = item.NoofSections;
                                oMachineGuilotinePtv.NoofUps = item.NoofUps;
                                oMachineGuilotinePtv.Noofcutswithoutgutters = item.Noofcutswithoutgutters;
                                oMachineGuilotinePtv.Noofcutswithgutters = item.Noofcutswithgutters;
                                db.MachineGuilotinePtvs.Add(oMachineGuilotinePtv);


                            }
                        }
                        break;
                    case 8:
                        MachineMeterPerHourLookup oMeterPerHourLookup = new MachineMeterPerHourLookup();
                        oMeterPerHourLookup.MethodId = oLookupMethod.MethodId;
                        oMeterPerHourLookup.SheetsQty1 = response.MeterPerHourLookup.SheetsQty1;
                        oMeterPerHourLookup.SheetsQty2 = response.MeterPerHourLookup.SheetsQty2;
                        oMeterPerHourLookup.SheetsQty3 = response.MeterPerHourLookup.SheetsQty3;
                        oMeterPerHourLookup.SheetsQty4 = response.MeterPerHourLookup.SheetsQty4;
                        oMeterPerHourLookup.SheetsQty5 = response.MeterPerHourLookup.SheetsQty5;
                        oMeterPerHourLookup.SheetWeight1 = response.MeterPerHourLookup.SheetWeight1;
                        oMeterPerHourLookup.speedqty11 = response.MeterPerHourLookup.speedqty11;
                        oMeterPerHourLookup.speedqty12 = response.MeterPerHourLookup.speedqty12;
                        oMeterPerHourLookup.speedqty13 = response.MeterPerHourLookup.speedqty13;
                        oMeterPerHourLookup.speedqty14 = response.MeterPerHourLookup.speedqty14;
                        oMeterPerHourLookup.speedqty15 = response.MeterPerHourLookup.speedqty15;
                        oMeterPerHourLookup.SheetWeight2 = response.MeterPerHourLookup.SheetWeight2;
                        oMeterPerHourLookup.speedqty21 = response.MeterPerHourLookup.speedqty21;
                        oMeterPerHourLookup.speedqty22 = response.MeterPerHourLookup.speedqty22;
                        oMeterPerHourLookup.speedqty23 = response.MeterPerHourLookup.speedqty23;
                        oMeterPerHourLookup.speedqty24 = response.MeterPerHourLookup.speedqty24;
                        oMeterPerHourLookup.speedqty25 = response.MeterPerHourLookup.speedqty25;
                        oMeterPerHourLookup.SheetWeight3 = response.MeterPerHourLookup.SheetWeight3;
                        oMeterPerHourLookup.speedqty31 = response.MeterPerHourLookup.speedqty31;
                        oMeterPerHourLookup.speedqty32 = response.MeterPerHourLookup.speedqty32;
                        oMeterPerHourLookup.speedqty33 = response.MeterPerHourLookup.speedqty33;
                        oMeterPerHourLookup.speedqty34 = response.MeterPerHourLookup.speedqty34;
                        oMeterPerHourLookup.speedqty35 = response.MeterPerHourLookup.speedqty35;
                        oMeterPerHourLookup.hourlyCost = response.MeterPerHourLookup.hourlyCost;
                        oMeterPerHourLookup.hourlyPrice = response.MeterPerHourLookup.hourlyPrice;
                        db.MachineMeterPerHourLookups.Add(oMeterPerHourLookup);
                        break;
                    default:
                        return null;

                }
            }

            if (db.SaveChanges() > 0)
            {
                return oLookupMethod;
            }
            else
            {
                return null;
            }
        }


        public bool UpdateLookup(LookupMethodResponse response)
        {

            LookupMethod oLookupMethod = DbSet.Where(g => g.MethodId == response.LookupMethodId).SingleOrDefault();
            oLookupMethod.Name = oLookupMethod.Name;
            switch (oLookupMethod.Type)
            {
                case 1:
                    MachineClickChargeLookup ClickChargeLookup = db.MachineClickChargeLookups.Where(g => g.MethodId == response.LookupMethod.MethodId).SingleOrDefault();
                    ClickChargeLookup.SheetCost = response.ClickChargeLookup.SheetCost;
                    ClickChargeLookup.Sheets = response.ClickChargeLookup.Sheets;
                    ClickChargeLookup.SheetPrice = response.ClickChargeLookup.SheetPrice;
                    break;
                case 3:
                    MachineSpeedWeightLookup SpeedWeightLookup = db.MachineSpeedWeightLookups.Where(g => g.MethodId == response.LookupMethodId).SingleOrDefault();
                    SpeedWeightLookup.Id = response.SpeedWeightLookup.Id;
                    SpeedWeightLookup.MethodId = response.SpeedWeightLookup.MethodId;
                    SpeedWeightLookup.SheetsQty1 = response.SpeedWeightLookup.SheetsQty1;
                    SpeedWeightLookup.SheetsQty2 = response.SpeedWeightLookup.SheetsQty2;
                    SpeedWeightLookup.SheetsQty3 = response.SpeedWeightLookup.SheetsQty3;
                    SpeedWeightLookup.SheetsQty4 = response.SpeedWeightLookup.SheetsQty4;
                    SpeedWeightLookup.SheetsQty5 = response.SpeedWeightLookup.SheetsQty5;
                    SpeedWeightLookup.SheetWeight1 = response.SpeedWeightLookup.SheetWeight1;
                    SpeedWeightLookup.speedqty11 = response.SpeedWeightLookup.speedqty11;
                    SpeedWeightLookup.speedqty12 = response.SpeedWeightLookup.speedqty12;
                    SpeedWeightLookup.speedqty13 = response.SpeedWeightLookup.speedqty13;
                    SpeedWeightLookup.speedqty14 = response.SpeedWeightLookup.speedqty14;
                    SpeedWeightLookup.speedqty15 = response.SpeedWeightLookup.speedqty15;
                    SpeedWeightLookup.SheetWeight2 = response.SpeedWeightLookup.SheetWeight2;
                    SpeedWeightLookup.speedqty21 = response.SpeedWeightLookup.speedqty21;
                    SpeedWeightLookup.speedqty22 = response.SpeedWeightLookup.speedqty22;
                    SpeedWeightLookup.speedqty23 = response.SpeedWeightLookup.speedqty23;
                    SpeedWeightLookup.speedqty24 = response.SpeedWeightLookup.speedqty24;
                    SpeedWeightLookup.speedqty25 = response.SpeedWeightLookup.speedqty25;
                    SpeedWeightLookup.SheetWeight3 = response.SpeedWeightLookup.SheetWeight3;
                    SpeedWeightLookup.speedqty31 = response.SpeedWeightLookup.speedqty31;
                    SpeedWeightLookup.speedqty32 = response.SpeedWeightLookup.speedqty32;
                    SpeedWeightLookup.speedqty33 = response.SpeedWeightLookup.speedqty33;
                    SpeedWeightLookup.speedqty34 = response.SpeedWeightLookup.speedqty34;
                    SpeedWeightLookup.speedqty35 = response.SpeedWeightLookup.speedqty35;
                    SpeedWeightLookup.hourlyCost = response.SpeedWeightLookup.hourlyCost;
                    SpeedWeightLookup.hourlyPrice = response.SpeedWeightLookup.hourlyPrice;

                    break;
                case 4:
                    MachinePerHourLookup PerHourLookup = db.MachinePerHourLookups.Where(g => g.MethodId == response.LookupMethod.MethodId).SingleOrDefault();
                    PerHourLookup.SpeedCost = response.PerHourLookup.SpeedCost;
                    PerHourLookup.Speed = response.PerHourLookup.Speed;
                    PerHourLookup.SpeedPrice = response.PerHourLookup.SpeedPrice;
                    break;
                case 5:
                    MachineClickChargeZone ClickChargeZoneLookup = db.MachineClickChargeZones.Where(g => g.MethodId == response.LookupMethodId).SingleOrDefault();
                    ClickChargeZoneLookup.From1 = response.ClickChargeZone.From1;
                    ClickChargeZoneLookup.To1 = response.ClickChargeZone.To1;
                    ClickChargeZoneLookup.Sheets1 = response.ClickChargeZone.Sheets1;
                    ClickChargeZoneLookup.SheetCost1 = response.ClickChargeZone.SheetCost1;
                    ClickChargeZoneLookup.SheetPrice1 = response.ClickChargeZone.SheetPrice1;
                    ClickChargeZoneLookup.From2 = response.ClickChargeZone.From2;
                    ClickChargeZoneLookup.To2 = response.ClickChargeZone.To2;
                    ClickChargeZoneLookup.Sheets2 = response.ClickChargeZone.Sheets2;
                    ClickChargeZoneLookup.SheetCost2 = response.ClickChargeZone.SheetCost2;
                    ClickChargeZoneLookup.SheetPrice2 = response.ClickChargeZone.SheetPrice2;
                    ClickChargeZoneLookup.From3 = response.ClickChargeZone.From3;
                    ClickChargeZoneLookup.To3 = response.ClickChargeZone.To3;
                    ClickChargeZoneLookup.Sheets3 = response.ClickChargeZone.Sheets3;
                    ClickChargeZoneLookup.SheetCost3 = response.ClickChargeZone.SheetCost3;
                    ClickChargeZoneLookup.SheetPrice3 = response.ClickChargeZone.SheetPrice3;
                    ClickChargeZoneLookup.From4 = response.ClickChargeZone.From4;
                    ClickChargeZoneLookup.To4 = response.ClickChargeZone.To4;
                    ClickChargeZoneLookup.Sheets4 = response.ClickChargeZone.Sheets4;
                    ClickChargeZoneLookup.SheetCost4 = response.ClickChargeZone.SheetCost4;
                    ClickChargeZoneLookup.SheetPrice4 = response.ClickChargeZone.SheetPrice4;
                    ClickChargeZoneLookup.From5 = response.ClickChargeZone.From5;
                    ClickChargeZoneLookup.To5 = response.ClickChargeZone.To5;
                    ClickChargeZoneLookup.Sheets5 = response.ClickChargeZone.Sheets5;
                    ClickChargeZoneLookup.SheetCost5 = response.ClickChargeZone.SheetCost5;
                    ClickChargeZoneLookup.SheetPrice5 = response.ClickChargeZone.SheetPrice5;
                    ClickChargeZoneLookup.From6 = response.ClickChargeZone.From6;
                    ClickChargeZoneLookup.To6 = response.ClickChargeZone.To6;
                    ClickChargeZoneLookup.Sheets6 = response.ClickChargeZone.Sheets6;
                    ClickChargeZoneLookup.SheetCost6 = response.ClickChargeZone.SheetCost6;
                    ClickChargeZoneLookup.SheetPrice6 = response.ClickChargeZone.SheetPrice6;
                    ClickChargeZoneLookup.From7 = response.ClickChargeZone.From7;
                    ClickChargeZoneLookup.To7 = response.ClickChargeZone.To7;
                    ClickChargeZoneLookup.Sheets7 = response.ClickChargeZone.Sheets7;
                    ClickChargeZoneLookup.SheetCost7 = response.ClickChargeZone.SheetCost7;
                    ClickChargeZoneLookup.SheetPrice7 = response.ClickChargeZone.SheetPrice7;
                    ClickChargeZoneLookup.From8 = response.ClickChargeZone.From8;
                    ClickChargeZoneLookup.To8 = response.ClickChargeZone.To8;
                    ClickChargeZoneLookup.Sheets8 = response.ClickChargeZone.Sheets8;
                    ClickChargeZoneLookup.SheetCost8 = response.ClickChargeZone.SheetCost8;
                    ClickChargeZoneLookup.SheetPrice8 = response.ClickChargeZone.SheetPrice8;
                    ClickChargeZoneLookup.From9 = response.ClickChargeZone.From9;
                    ClickChargeZoneLookup.To9 = response.ClickChargeZone.To9;
                    ClickChargeZoneLookup.Sheets9 = response.ClickChargeZone.Sheets9;
                    ClickChargeZoneLookup.SheetCost9 = response.ClickChargeZone.SheetCost9;
                    ClickChargeZoneLookup.SheetPrice9 = response.ClickChargeZone.SheetPrice9;
                    ClickChargeZoneLookup.From10 = response.ClickChargeZone.From10;
                    ClickChargeZoneLookup.To10 = response.ClickChargeZone.To10;
                    ClickChargeZoneLookup.Sheets10 = response.ClickChargeZone.Sheets10;
                    ClickChargeZoneLookup.SheetCost10 = response.ClickChargeZone.SheetCost10;
                    ClickChargeZoneLookup.SheetPrice10 = response.ClickChargeZone.SheetPrice10;
                    ClickChargeZoneLookup.From11 = response.ClickChargeZone.From11;
                    ClickChargeZoneLookup.To11 = response.ClickChargeZone.To11;
                    ClickChargeZoneLookup.Sheets11 = response.ClickChargeZone.Sheets11;
                    ClickChargeZoneLookup.SheetCost11 = response.ClickChargeZone.SheetCost11;
                    ClickChargeZoneLookup.SheetPrice11 = response.ClickChargeZone.SheetPrice11;
                    ClickChargeZoneLookup.From12 = response.ClickChargeZone.From12;
                    ClickChargeZoneLookup.To12 = response.ClickChargeZone.To12;
                    ClickChargeZoneLookup.Sheets12 = response.ClickChargeZone.Sheets12;
                    ClickChargeZoneLookup.SheetCost12 = response.ClickChargeZone.SheetCost12;
                    ClickChargeZoneLookup.SheetPrice12 = response.ClickChargeZone.SheetPrice12;
                    ClickChargeZoneLookup.From13 = response.ClickChargeZone.From13;
                    ClickChargeZoneLookup.To13 = response.ClickChargeZone.To13;
                    ClickChargeZoneLookup.Sheets13 = response.ClickChargeZone.Sheets13;
                    ClickChargeZoneLookup.SheetCost13 = response.ClickChargeZone.SheetCost13;
                    ClickChargeZoneLookup.SheetPrice13 = response.ClickChargeZone.SheetPrice13;
                    ClickChargeZoneLookup.From14 = response.ClickChargeZone.From14;
                    ClickChargeZoneLookup.To14 = response.ClickChargeZone.To14;
                    ClickChargeZoneLookup.Sheets14 = response.ClickChargeZone.Sheets14;
                    ClickChargeZoneLookup.SheetCost14 = response.ClickChargeZone.SheetCost14;
                    ClickChargeZoneLookup.SheetPrice14 = response.ClickChargeZone.SheetPrice14;
                    ClickChargeZoneLookup.From15 = response.ClickChargeZone.From15;
                    ClickChargeZoneLookup.To15 = response.ClickChargeZone.To15;
                    ClickChargeZoneLookup.Sheets15 = response.ClickChargeZone.Sheets15;
                    ClickChargeZoneLookup.SheetCost15 = response.ClickChargeZone.SheetCost15;
                    ClickChargeZoneLookup.SheetPrice15 = response.ClickChargeZone.SheetPrice15;
                    ClickChargeZoneLookup.isaccumulativecharge = response.ClickChargeZone.isaccumulativecharge;
                    ClickChargeZoneLookup.IsRoundUp = response.ClickChargeZone.IsRoundUp;
                    ClickChargeZoneLookup.TimePerHour = response.ClickChargeZone.TimePerHour;

                    break;
                case 6:
                    MachineGuillotineCalc GuillotineCalc = db.MachineGuillotineCalcs.Where(g => g.MethodId == response.LookupMethodId).SingleOrDefault();
                    GuillotineCalc.PaperWeight1 = response.GuillotineCalc.PaperWeight1;
                    GuillotineCalc.PaperThroatQty1 = response.GuillotineCalc.PaperThroatQty1;
                    GuillotineCalc.PaperWeight2 = response.GuillotineCalc.PaperWeight2;
                    GuillotineCalc.PaperThroatQty2 = response.GuillotineCalc.PaperThroatQty2;
                    GuillotineCalc.PaperWeight3 = response.GuillotineCalc.PaperWeight3;
                    GuillotineCalc.PaperThroatQty3 = response.GuillotineCalc.PaperThroatQty3;
                    GuillotineCalc.PaperWeight4 = response.GuillotineCalc.PaperWeight4;
                    GuillotineCalc.PaperThroatQty4 = response.GuillotineCalc.PaperThroatQty4;
                    GuillotineCalc.PaperWeight5 = response.GuillotineCalc.PaperWeight5;
                    GuillotineCalc.PaperThroatQty5 = response.GuillotineCalc.PaperThroatQty5;
                    foreach (MachineGuilotinePtv item in response.GuilotinePtv)
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
                            oMachineGuilotinePtv.GuilotineId = Convert.ToInt32(GuillotineCalc.Id);
                            oMachineGuilotinePtv.NoofSections = item.NoofSections;
                            oMachineGuilotinePtv.NoofUps = item.NoofUps;
                            oMachineGuilotinePtv.Noofcutswithoutgutters = item.Noofcutswithoutgutters;
                            oMachineGuilotinePtv.Noofcutswithgutters = item.Noofcutswithgutters;
                            db.MachineGuilotinePtvs.Add(oMachineGuilotinePtv);
                        }
                    }

                    break;
                case 8:
                    MachineMeterPerHourLookup oMeterPerHourLookup = db.MachineMeterPerHourLookups.Where(g => g.MethodId == response.LookupMethodId).SingleOrDefault();
                    oMeterPerHourLookup.SheetsQty1 = response.MeterPerHourLookup.SheetsQty1;
                    oMeterPerHourLookup.SheetsQty2 = response.MeterPerHourLookup.SheetsQty2;
                    oMeterPerHourLookup.SheetsQty3 = response.MeterPerHourLookup.SheetsQty3;
                    oMeterPerHourLookup.SheetsQty4 = response.MeterPerHourLookup.SheetsQty4;
                    oMeterPerHourLookup.SheetsQty5 = response.MeterPerHourLookup.SheetsQty5;
                    oMeterPerHourLookup.SheetWeight1 = response.MeterPerHourLookup.SheetWeight1;
                    oMeterPerHourLookup.speedqty11 = response.MeterPerHourLookup.speedqty11;
                    oMeterPerHourLookup.speedqty12 = response.MeterPerHourLookup.speedqty12;
                    oMeterPerHourLookup.speedqty13 = response.MeterPerHourLookup.speedqty13;
                    oMeterPerHourLookup.speedqty14 = response.MeterPerHourLookup.speedqty14;
                    oMeterPerHourLookup.speedqty15 = response.MeterPerHourLookup.speedqty15;
                    oMeterPerHourLookup.SheetWeight2 = response.MeterPerHourLookup.SheetWeight2;
                    oMeterPerHourLookup.speedqty21 = response.MeterPerHourLookup.speedqty21;
                    oMeterPerHourLookup.speedqty22 = response.MeterPerHourLookup.speedqty22;
                    oMeterPerHourLookup.speedqty23 = response.MeterPerHourLookup.speedqty23;
                    oMeterPerHourLookup.speedqty24 = response.MeterPerHourLookup.speedqty24;
                    oMeterPerHourLookup.speedqty25 = response.MeterPerHourLookup.speedqty25;
                    oMeterPerHourLookup.SheetWeight3 = response.MeterPerHourLookup.SheetWeight3;
                    oMeterPerHourLookup.speedqty31 = response.MeterPerHourLookup.speedqty31;
                    oMeterPerHourLookup.speedqty32 = response.MeterPerHourLookup.speedqty32;
                    oMeterPerHourLookup.speedqty33 = response.MeterPerHourLookup.speedqty33;
                    oMeterPerHourLookup.speedqty34 = response.MeterPerHourLookup.speedqty34;
                    oMeterPerHourLookup.speedqty35 = response.MeterPerHourLookup.speedqty35;
                    oMeterPerHourLookup.hourlyCost = response.MeterPerHourLookup.hourlyCost;
                    oMeterPerHourLookup.hourlyPrice = response.MeterPerHourLookup.hourlyPrice;

                    break;
                default:
                    return false;

            }

            db.SaveChanges();
            return true;
        }

        public LookupMethodResponse GetlookupById(long MethodId)
        {
            LookupMethod olookupMethod = DbSet.Where(g => g.MethodId == MethodId).SingleOrDefault();
            MachineGuillotineCalc oGuillotineCalc = olookupMethod.Type == 6 ? db.MachineGuillotineCalcs.Where(g => g.MethodId == MethodId).SingleOrDefault() : null;
            
            return new LookupMethodResponse
            {
               // ClickChargeLookup = olookupMethod.Type == 1 ? db.MachineClickChargeLookups.Where(g => g.MethodId == MethodId).SingleOrDefault() : null,
                ClickChargeZone = olookupMethod.Type == 5 ? db.MachineClickChargeZones.Where(g => g.MethodId == MethodId).SingleOrDefault() : null,
                GuillotineCalc = oGuillotineCalc,
                GuilotinePtv = oGuillotineCalc != null ? db.MachineGuilotinePtvs.Where(g => g.GuilotineId == oGuillotineCalc.Id).ToList() : null,
                MeterPerHourLookup = olookupMethod.Type == 8 ? db.MachineMeterPerHourLookups.Where(g => g.MethodId == MethodId).SingleOrDefault() : null,
             //   PerHourLookup = olookupMethod.Type == 4 ? db.MachinePerHourLookups.Where(g => g.MethodId == MethodId).SingleOrDefault() : null,
                //SpeedWeightLookup = olookupMethod.Type == 3 ? db.MachineSpeedWeightLookups.Where(g => g.MethodId == MethodId).SingleOrDefault() : null,
               
            };
        }

        #endregion
    }
}
