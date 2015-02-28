using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MigrationUtility.Preview;
using AutoMapper;
using AutoMapper.Mappers;
using System.Reflection;

namespace MigrationUtility
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 递归创建类型间的映射关系 (Recursively create mappings between types)
        ///created by cqwang
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        public void CreateNestedMappers(Type sourceType, Type destinationType)
        {
            try
            {


                PropertyInfo[] sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo[] destinationProperties = destinationType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var destinationProperty in destinationProperties)
                {
                    Type destinationPropertyType = destinationProperty.PropertyType;
                    if (Filter(destinationPropertyType))
                        continue;

                    PropertyInfo sourceProperty = sourceProperties.FirstOrDefault(prop => NameMatches(prop.Name, destinationProperty.Name));
                    if (sourceProperty == null)
                        continue;

                    Type sourcePropertyType = sourceProperty.PropertyType;
                    if (destinationPropertyType.IsGenericType)
                    {
                        Type destinationGenericType = destinationPropertyType.GetGenericArguments()[0];
                        if (Filter(destinationGenericType))
                            continue;

                        Type sourceGenericType = sourcePropertyType.GetGenericArguments()[0];
                        CreateNestedMappers(sourceGenericType, destinationGenericType);
                    }
                    else
                    {
                        CreateNestedMappers(sourcePropertyType, destinationPropertyType);
                    }
                }

                Mapper.CreateMap(sourceType, destinationType);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        /// <summary>
        /// 过滤 (Filter)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        static bool Filter(Type type)
        {
            return type.IsPrimitive || NoPrimitiveTypes.Contains(type.Name);
        }

        static readonly HashSet<string> NoPrimitiveTypes = new HashSet<string>() { "String", "DateTime", "Decimal" };

        private static bool NameMatches(string memberName, string nameToMatch)
        {
            return String.Compare(memberName, nameToMatch, StringComparison.OrdinalIgnoreCase) == 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
              
                //Mapper.CreateMap<tbl_costcentres, CostCentre>();



                output.Text += "Start;"+ Environment.NewLine;

                CreateNestedMappers(typeof(tbl_papersize), typeof(PaperSize));

               // CreateNestedMappers(typeof(tbl_stockitems), typeof(StockItem));


                

                Mapper.CreateMap<tbl_stockcategories, StockCategory>()
                    .ForMember(x => x.OrganisationId, opt => opt.Ignore())
                    .ForMember(x => x.OrganisationId, opt => opt.Ignore())
                    .ForMember(x => x.StockSubCategories, opt => opt.MapFrom(src => src.tbl_stocksubcategories))
                    .ForMember(x => x.StockItems, opt => opt.Ignore());

                
                Mapper.CreateMap<tbl_stocksubcategories, StockSubCategory>();

                Mapper.CreateMap<tbl_stockitems, StockItem>()
                    .ForMember(x => x.OrganisationId, opt => opt.Ignore())
                    .ForMember(x => x.ThresholdLevel, opt => opt.Ignore())
                    .ForMember(x => x.ThresholdProductionQuantity, opt => opt.Ignore())
                    .ForMember(x => x.isAllowBackOrder, opt => opt.Ignore())
                    .ForMember(x => x.Company, opt => opt.Ignore())
                    .ForMember(x => x.ItemSections, opt => opt.Ignore())
                    .ForMember(x => x.ItemStockOptions, opt => opt.Ignore())
                    .ForMember(x => x.SectionCostCentreDetails, opt => opt.Ignore())
                    .ForMember(x => x.StockCategory, opt => opt.Ignore())
                    .ForMember(x => x.StockCostAndPrices, opt => opt.MapFrom(src => src.tbl_stock_cost_and_price))
                    .ForMember(x => x.StockItemsColors, opt => opt.MapFrom(src => src.tbl_stockitems_colors))
                    .ForMember(x => x.StockSubCategory, opt => opt.Ignore());

                Mapper.CreateMap<tbl_stock_cost_and_price, StockCostAndPrice>();
                Mapper.CreateMap<tbl_stockitems_colors, StockItemsColor>();


                Mapper.CreateMap<tbl_costcentres, CostCentre>()
                    .ForMember(x => x.OrganisationId, opt => opt.Ignore())
                    .ForMember(x => x.DeliveryType, opt => opt.Ignore())
                    .ForMember(x => x.DeliveryServiceType, opt => opt.Ignore())
                    .ForMember(x => x.CompanyCostCentres, opt => opt.Ignore())
                    .ForMember(x => x.CostcentreResources, opt => opt.MapFrom(src => src.tbl_costcentre_resources))
                    .ForMember(x => x.ItemAddonCostCentres, opt => opt.Ignore())
                    .ForMember(x => x.CostCentreType, opt => opt.Ignore())
                    .ForMember(x => x.SectionCostcentres, opt => opt.Ignore())
                    .ForMember(x => x.CostcentreInstructions, opt => opt.MapFrom(src => src.tbl_costcentre_instructions))
                     .ForMember(x => x.CostcentreResources, opt => opt.Ignore());

                //Mapper.CreateMap<tbl_costcentre_resources, CostcentreResource>();
                Mapper.CreateMap<tbl_costcentre_instructions, CostcentreInstruction>();

                Mapper.CreateMap<tbl_costcentretypes, CostCentreType>();



                Mapper.CreateMap<tbl_contactcompanies, Company>().ForMember(x => x.AccountManagerId, opt => opt.Ignore());

                Mapper.CreateMap<tbl_addresses, Address>();
                Mapper.CreateMap<tbl_contacts, CompanyContact>();
                Mapper.CreateMap<tbl_ContactCompanyTerritories, tbl_ContactCompanyTerritories>();


                Mapper.CreateMap<tbl_report_notes, ReportNote>();
                Mapper.CreateMap<tbl_prefixes, prefix>()
                      .ForMember(x => x.OrganisationId, opt => opt.Ignore())
                    .ForMember(x => x.Markup, opt => opt.Ignore());

                Mapper.CreateMap<tbl_lookup_methods, LookupMethod>()
                    .ForMember(x => x.MachineClickChargeLookups, opt => opt.MapFrom(src => src.tbl_machine_clickchargelookup))
                      .ForMember(x => x.MachineClickChargeZones, opt => opt.MapFrom(src => src.tbl_machine_clickchargezone))
                        .ForMember(x => x.MachineGuillotineCalcs, opt => opt.MapFrom(src => src.tbl_machine_guillotinecalc))
                          .ForMember(x => x.MachineMeterPerHourLookups, opt => opt.MapFrom(src => src.tbl_machine_meterperhourlookup))
                            .ForMember(x => x.MachinePerHourLookups, opt => opt.MapFrom(src => src.tbl_machine_perhourlookup))
                          .ForMember(x => x.MachineSpeedWeightLookups, opt => opt.MapFrom(src => src.tbl_machine_speedweightlookup));


                Mapper.CreateMap<tbl_machine_clickchargelookup, MachineClickChargeLookup>()
                    .ForMember(x => x.LookupMethod, opt => opt.Ignore());
                  Mapper.CreateMap<tbl_machine_clickchargezone, MachineClickChargeZone>()
                .ForMember(x => x.LookupMethod, opt => opt.Ignore());
                  Mapper.CreateMap<tbl_machine_guillotinecalc, MachineGuillotineCalc>()
                .ForMember(x => x.LookupMethod, opt => opt.Ignore());
                  Mapper.CreateMap<tbl_machine_meterperhourlookup, MachineMeterPerHourLookup>()
                .ForMember(x => x.LookupMethod, opt => opt.Ignore());
                  Mapper.CreateMap<tbl_machine_perhourlookup, MachinePerHourLookup>()
                .ForMember(x => x.LookupMethod, opt => opt.Ignore());
                 Mapper.CreateMap<tbl_machine_speedweightlookup, MachineSpeedWeightLookup>()
                .ForMember(x => x.LookupMethod, opt => opt.Ignore());


                 Mapper.CreateMap<tbl_machines, Machine>()
                  .ForMember(x => x.LookupMethodId, opt => opt.Ignore())
                    .ForMember(x => x.MachineCostCentreGroups, opt => opt.Ignore())
                        .ForMember(x => x.MarkupId, opt => opt.Ignore())
                          .ForMember(x => x.OrganisationId, opt => opt.Ignore())
                            .ForMember(x => x.MachineInkCoverages, opt => opt.MapFrom(src => src.tbl_machine_ink_coverage))
                             
                          .ForMember(x => x.ItemSections, opt => opt.Ignore())
                           .ForMember(x => x.MachineLookupMethods, opt => opt.Ignore())
                 .ForMember(x => x.MachinePaginationProfiles, opt => opt.Ignore())
                 .ForMember(x => x.MachineResources, opt => opt.Ignore());

                      
                           Mapper.CreateMap<tbl_machine_guilotine_ptv, MachineGuilotinePtv>();
                                Mapper.CreateMap<tbl_machine_ink_coverage, MachineInkCoverage>();
                                     Mapper.CreateMap<tbl_machine_spoilage, MachineSpoilage>();

                                     Mapper.CreateMap<tbl_phrase, Phrase>()
                                                            .ForMember(x => x.OrganisationId, opt => opt.Ignore());
                                     //.ForMember(x => x.PhraseField, opt => opt.MapFrom(src => src.));

                                     Mapper.CreateMap<tbl_phrase_fields, PhraseField>()
                                     .ForMember(x => x.OrganisationId, opt => opt.Ignore());
                //.ForMember(x => x.Phrases, opt => opt.Ignore());

                                     Mapper.CreateMap<tbl_ProductCategory, ProductCategory>()
                                         .ForMember(x => x.CompanyId, opt => opt.Ignore())                                         .ForMember(x => x.OrganisationId, opt => opt.Ignore())                                         .ForMember(x => x.Company, opt => opt.Ignore())                                         .ForMember(x => x.ProductCategoryItems, opt => opt.Ignore())                                         .ForMember(x => x.ProductCategoryFoldLines, opt => opt.Ignore());                                         
                                             
                                             


                 output.Text += "Mapping done" + Environment.NewLine;



                //Mapper.AssertConfigurationIsValid();


                //return;

            long OrganizationId = 1;
            string MPCContentBasePath = @"E:\mpc-content\";

            string PinkCardsStoredImagesBasePath = @"E:\StoredImages\";

            //D:\GitHub\Usman\MyPrintCloud\MyPrintCloud\MPC.Web\MPC_Content\Organisations\Organisation1\Organisation1_infinity-vehicle.jpg.jpeg
            //ensure directory created

            if (! Directory.Exists (MPCContentBasePath + "Organisations" ))
            {
                Directory.CreateDirectory(MPCContentBasePath + "Organisations");
            }


              if (! Directory.Exists (MPCContentBasePath + @"Organisations\Organisation" + OrganizationId.ToString() ))
            {
                Directory.CreateDirectory(MPCContentBasePath + @"Organisations\Organisation" + OrganizationId.ToString());
            }


            using (pinkcardsEntities PCContext = new pinkcardsEntities())
            {
                PCContext.Configuration.LazyLoadingEnabled = false;
                using (MPCPreviewEntities1 MPCContext = new MPCPreviewEntities1())
                {
                    MPCContext.Configuration.LazyLoadingEnabled = false;



                    
                    //copy the countries first


                    foreach (var item in PCContext.tbl_country.ToList())
                    {
                        Preview.Country ocountr = new Country();
                        ocountr.CountryID = item.CountryID;
                        ocountr.CountryName = item.CountryName;
                        ocountr.CountryCode = item.CountryCode;

                        MPCContext.Countries.Add(ocountr);
                    }

                    MPCContext.SaveChanges();

                    output.Text += "Country imported" + Environment.NewLine;

                    //state sync

                    foreach (var item in PCContext.tbl_state.ToList())
                    {
                        Preview.State oState = new State();
                        oState.StateId = item.StateID;
                        oState.CountryId = item.CountryID;
                        oState.StateCode = item.StateCode;
                        oState.StateName = item.StateName;


                        MPCContext.States.Add(oState);
                    }

                    MPCContext.SaveChanges();


                    output.Text += "Country imported" + Environment.NewLine;

                    tbl_company_sites PCCompany = PCContext.tbl_company_sites.FirstOrDefault();


                Organisation MPCOrg = new Organisation();
                MPCOrg.OrganisationId = OrganizationId;
                    MPCOrg.OrganisationName = PCCompany.CompanySiteName;
                MPCOrg.Address1 = PCCompany.Address1;
                MPCOrg.Address2 = PCCompany.Address2;
                MPCOrg.Address3 = PCCompany.Address3;
                MPCOrg.BleedAreaSize = 5;
                MPCOrg.City = PCCompany.City;
                    MPCOrg.StateId =   MPCContext.States.Where( g=> g.StateName ==    PCCompany.State).Single().StateId;

                MPCOrg.CmsSkinPageWidgets = null;///???

                MPCOrg.CountryId = 213;// MPCContext.Countries.Where(g => g.CountryName == PCCompany.Country).Single().CountryID;
                    MPCOrg.ZipCode = PCCompany.ZipCode;
                    MPCOrg.CurrencyId = 1;
                    MPCOrg.CustomerAccountNumber = null;
                    MPCOrg.Email = PCCompany.Email;
                    MPCOrg.Fax = PCCompany.Fax;
                    MPCOrg.Tel = PCCompany.Tel;

                    MPCOrg.Mobile = PCCompany.Mobile;
                    MPCOrg.URL = PCCompany.URL;

                   

                    string logoname = Path.GetFileName(PCCompany.MISLogo);
                    if (File.Exists( PinkCardsStoredImagesBasePath + PCCompany.MISLogo))
                        File.Copy(PinkCardsStoredImagesBasePath + PCCompany.MISLogo, MPCContentBasePath + @"Organisations\Organisation\" + logoname);
                    MPCOrg.MISLogo = MPCContentBasePath + @"Organisations\Organisation\" + logoname;

                    MPCOrg.TaxRegistrationNo = PCCompany.VATRegNumber;
                    MPCOrg.VATRegNumber = PCCompany.VATRegNumber;
                    
                    MPCOrg.SmtpPassword = "p@ssw0rd";
                    MPCOrg.SmtpServer = "smtp.sendgrid.net";
                    MPCOrg.SmtpUserName = "myprintcloud.com";

                    MPCOrg.SystemLengthUnit = 1;
                    MPCOrg.SystemWeightUnit = 2;
                    MPCOrg.CurrencyId = 1;
                    MPCOrg.LanguageId = 1;

                    

                    MPCContext.Organisations.Add(MPCOrg);

                    MPCContext.SaveChanges();


                    output.Text += "Organization imported" + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////paper size

                    List<tbl_papersize> otbl_papersize =  PCContext.tbl_papersize.ToList();
                    foreach (var item in otbl_papersize)
                    {
                        Preview.PaperSize oPapersize = Mapper.Map<tbl_papersize, PaperSize>(item);
                        oPapersize.OrganisationId = OrganizationId;
                        MPCContext.PaperSizes.Add(oPapersize);

                    }

                    MPCContext.SaveChanges();

                    output.Text += "Papersize" + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////stock category
                   
                    List<tbl_stockcategories> otbl_stockcategories = PCContext.tbl_stockcategories.Include("tbl_stocksubcategories").ToList();
                    foreach (var item in otbl_stockcategories)
                    {
                        Preview.StockCategory oStockCategory = Mapper.Map<tbl_stockcategories, StockCategory>(item);
                        oStockCategory.OrganisationId = OrganizationId;
                        MPCContext.StockCategories.Add(oStockCategory);

                    }

                    MPCContext.SaveChanges();

                    output.Text += "Stock Category" + Environment.NewLine;


                    ///////////////////////////////////////////////////////////////supplier
                    //CreateNestedMappers(typeof(tbl_contactcompanies), typeof(Company));

               
                    List<tbl_contactcompanies> otbl_contactcompanies = PCContext.tbl_contactcompanies .Where ( g=> g.IsCustomer == 2).ToList();
                    foreach (var item in otbl_contactcompanies)
                    {
                        Preview.Company oCompany = Mapper.Map<tbl_contactcompanies, Company>(item);
                        oCompany.OrganisationId = OrganizationId;
                        MPCContext.Companies.Add(oCompany);

                    }

                    MPCContext.SaveChanges();

                    output.Text += "suppliers" + Environment.NewLine;

                    

                    ///////////////////////////////////////////////////////////////Stock
                    //CreateNestedMappers(typeof(tbl_stockitems), typeof(StockItem));
                    List<tbl_stockitems> otbl_stockitems = PCContext.tbl_stockitems.Include("tbl_stock_cost_and_price").Include("tbl_stockitems_colors").ToList();
                    foreach (var item in otbl_stockitems)
                    {
                        Preview.StockItem oStockItem = Mapper.Map<tbl_stockitems, StockItem>(item);
                        string suppliername = PCContext.tbl_contactcompanies.Where ( g=> g.ContactCompanyID == item.SupplierID).Single().Name;
                        oStockItem.SupplierId = MPCContext.Companies.Where(g => g.Name == suppliername).Single().CompanyId;

                        var ocat =  PCContext.tbl_stockcategories.Where(g => g.CategoryID == item.CategoryID).Single();
                        string categoryname = ocat.Name;
                        int catid = ocat.CategoryID;
                        var newcat = MPCContext.StockCategories.Where(g => g.Name == categoryname).Single();
                        oStockItem.CategoryId = newcat.CategoryId;
                        long newcatid = newcat.CategoryId;


                        string subcategoryname = PCContext.tbl_stocksubcategories.Where(g => g.SubCategoryID == item.SubCategoryID).First().Name;
                        string subcategorycode = PCContext.tbl_stocksubcategories.Where(g => g.SubCategoryID == item.SubCategoryID).First().Code;
                        oStockItem.SubCategoryId = MPCContext.StockSubCategories.Where(g => g.Name == subcategoryname && g.Code == subcategorycode && g.CategoryId == newcatid).First().SubCategoryId;
                        
                        oStockItem.OrganisationId = OrganizationId;
                        MPCContext.StockItems.Add(oStockItem);

                    }

                    MPCContext.SaveChanges();
                    output.Text += "Stock Items" + Environment.NewLine;
                    
                    /////////////////////////////////////////////////////////////////costcentres matrix

                    List<tbl_costcentrematrices> otbl_costcentrematrices = PCContext.tbl_costcentrematrices.ToList();
                    List<tbl_costcentrematrixdetails> otbl_costcentrematrixdetails =  PCContext.tbl_costcentrematrixdetails.ToList();

                    foreach (var matrix in otbl_costcentrematrices)
                    {
                        CostCentreMatrix oMatrix = new CostCentreMatrix();
                        oMatrix.ColumnsCount = matrix.ColumnsCount;
                        oMatrix.CompanyId = Convert.ToInt32(OrganizationId);
                        oMatrix.Description = matrix.Description;
                        oMatrix.Name = matrix.Name;
                        oMatrix.RowsCount = matrix.RowsCount;
                        

                        MPCContext.CostCentreMatrices.Add(oMatrix);

                        MPCContext.SaveChanges();

                        foreach (var item in otbl_costcentrematrixdetails.Where(g => g.MatrixID == matrix.MatrixID))
                        {
                            CostCentreMatrixDetail oDetail = new CostCentreMatrixDetail();
                            oDetail.MatrixId = oMatrix.MatrixId;
                            oDetail.Value = item.Value;
                            MPCContext.CostCentreMatrixDetails.Add(oDetail);
                        }

                        MPCContext.SaveChanges();

                    }

                    output.Text += "CostCentre Matrix" + Environment.NewLine;

                    //select * from pinkcards.dbo.tbl_CostCentreVariableTypes
                    /////////////////////////////////////////////////////////costcentre variables
                    List<tbl_costcentrevariabletypes> otbl_CostCentreVariableTypes = PCContext.tbl_costcentrevariabletypes.ToList();

                    foreach (var item in otbl_CostCentreVariableTypes)
                    {
                        Preview.CostCentreVariableType oVar = new CostCentreVariableType();
                        oVar.CategoryId = item.CategoryID;
                        oVar.Name = item.Name;

                        MPCContext.CostCentreVariableTypes.Add( oVar);

                    }
                    MPCContext.SaveChanges();

                    output.Text += "CostCentre Variables" + Environment.NewLine;

                    ///////////////////////////////////////////////////////////costcentre variables
                    List<tbl_costcentrevariables> otbl_CostCentreVariables = PCContext.tbl_costcentrevariables.ToList();

                    foreach (var item in otbl_CostCentreVariables)
                    {
                        Preview.CostCentreVariable oVar = new CostCentreVariable();
                        oVar.CategoryId = item.CategoryID;
                        oVar.Criteria = item.Criteria;
                        oVar.CriteriaFieldName = item.CriteriaFieldName;
                        oVar.IsCriteriaUsed = item.IsCriteriaUsed;
                        oVar.Name = item.Name;
                        oVar.PropertyType = item.PropertyType;
                        oVar.RefFieldName = item.RefFieldName;
                        oVar.RefTableName = item.RefTableName;
                        oVar.SystemSiteId = Convert.ToInt32( OrganizationId);
                        oVar.Type = item.Type;
                        oVar.VariableDescription = item.VariableDescription;
                        oVar.VariableValue = item.VariableValue;
                        oVar.VarId = item.VarID;

                        MPCContext.CostCentreVariables.Add(oVar);

                    }
                    MPCContext.SaveChanges();
                    output.Text += "Costcentre Variables" + Environment.NewLine;


                    /////////////////////////////////////////////////////////////costcentre type

                    List<tbl_costcentretypes> otbl_costcentretypes = PCContext.tbl_costcentretypes.ToList();

                    foreach (var item in otbl_costcentretypes)
                    {
                        Preview.CostCentreType oCostCentreType = Mapper.Map<tbl_costcentretypes, CostCentreType>(item);
                        oCostCentreType.CompanyId =Convert.ToInt32(  OrganizationId);
                        MPCContext.CostCentreTypes.Add(oCostCentreType);
                    }

                    MPCContext.SaveChanges();

                    output.Text += "costCentretypes" + Environment.NewLine;

                    ////////////////////////////////////////////////////////////////// costcentres
                   // AutoMapperConfiguration.Configure();

               
                    List<tbl_costcentres> otbl_costcentres = PCContext.tbl_costcentres.Include("tbl_costcentre_instructions").Include("tbl_costcentre_instructions.tbl_costcentre_workinstructions_choices").ToList();
                    foreach (var item in otbl_costcentres)
                    {
                        Preview.CostCentre oCostCentre = Mapper.Map<tbl_costcentres, CostCentre>(item);
                        oCostCentre.OrganisationId = OrganizationId;
                       string typename = PCContext.tbl_costcentretypes.Where(g => g.TypeID == item.Type).Single().TypeName;

                       oCostCentre.Type = MPCContext.CostCentreTypes.Where(g => g.TypeName == typename).Single().TypeId;

                        MPCContext.CostCentres.Add(oCostCentre);
                                                                       
                    }


                    MPCContext.SaveChanges();

                    output.Text += "Costcentres" + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////report notes
                    List<tbl_report_notes> otbl_report_notes = PCContext.tbl_report_notes.ToList();

                    foreach (var item in otbl_report_notes)
                    {
                        Preview.ReportNote oReportNote = Mapper.Map<tbl_report_notes, ReportNote>(item);
                        oReportNote.SystemSiteId = Convert.ToInt32( OrganizationId);

                        MPCContext.ReportNotes.Add(oReportNote);

                    }
                    MPCContext.SaveChanges();
                    output.Text += "Report notes" + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////prefixes
                    List<tbl_prefixes> otbl_prefixes = PCContext.tbl_prefixes.Where(g=> g.SystemSiteID == 1).ToList();

                    foreach (var item in otbl_prefixes)
                    {
                        Preview.prefix oprefix = Mapper.Map<tbl_prefixes, prefix>(item);
                        oprefix.OrganisationId = Convert.ToInt32(OrganizationId);
                       
                        MPCContext.prefixes.Add(oprefix);

                    }
                    MPCContext.SaveChanges();
                    output.Text += "Prefixes" + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////lookup methods
                    
                    List<tbl_lookup_methods> otbl_lookup_methods = PCContext.tbl_lookup_methods.Include("tbl_machine_clickchargelookup").Include("tbl_machine_clickchargezone").Include("tbl_machine_guillotinecalc").Include("tbl_machine_meterperhourlookup").Include("tbl_machine_perhourlookup").Include("tbl_machine_speedweightlookup").ToList();
                    foreach (var item in otbl_lookup_methods)
                    {

                        Preview.LookupMethod oLookupMethod = Mapper.Map<tbl_lookup_methods, LookupMethod>(item);
                        oLookupMethod.CompanyId = Convert.ToInt32(OrganizationId);
                        MPCContext.LookupMethods.Add(oLookupMethod);

                    }
                    MPCContext.SaveChanges();
                    output.Text += "Lookup Methods" + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////machines
                    List<tbl_machines> otbl_machines = PCContext.tbl_machines.Include("tbl_machine_guilotine_ptv").Include("tbl_machine_ink_coverage").Include("tbl_machine_spoilage").ToList();
                    foreach (var item in otbl_machines)
                    {

                        Preview.Machine oMachine = Mapper.Map<tbl_machines, Machine>(item);
                        oMachine.OrganisationId = OrganizationId;

                        var oldMethodID = PCContext.tbl_machine_lookup_methods.Where ( g=> g.MachineID == item.MachineID && g.DefaultMethod == true).Single().MethodID;
                        tbl_lookup_methods oOldMethod  = PCContext.tbl_lookup_methods.Where ( g=> g.MethodID == oldMethodID).Single();

                        oMachine.LookupMethodId = MPCContext.LookupMethods.Where(g => g.Name == oOldMethod.Name).Single().MethodId;

                        MPCContext.Machines.Add(oMachine);

                    }
                    MPCContext.SaveChanges();
                    output.Text += "machines" + Environment.NewLine;


                    ///////////////////////////////////////////////////////////////////tbl_phrase_fields
                    List<tbl_phrase_fields> otbl_phrase_fields = PCContext.tbl_phrase_fields.ToList();
                    foreach (var item in otbl_phrase_fields)
                    {
                        Preview.PhraseField oPhraseField = Mapper.Map<tbl_phrase_fields, PhraseField>(item);

                        oPhraseField.OrganisationId = OrganizationId;


                        List<tbl_phrase> otbl_phrase = PCContext.tbl_phrase.Where(g=> g.FieldID == item.FieldID).ToList();

                        foreach (var citem in otbl_phrase)
                        {
                            Preview.Phrase oPhrase = Mapper.Map<tbl_phrase, Phrase>(citem);
                            oPhrase.Phrase1 = citem.Phrase;
                            oPhrase.OrganisationId = OrganizationId;
                            oPhraseField.Phrases.Add(oPhrase);
                        }

                        MPCContext.PhraseFields.Add(oPhraseField);

                    }

                    MPCContext.SaveChanges();
                    output.Text += "tbl_phrase_fields" + Environment.NewLine;



                    ////////////////////////////////////////////////////// retail store
                    Preview.Company oRetailStore = new Company();
                    oRetailStore.Name = "Retail Store";
                    oRetailStore.WebAccessCode = "retail";
                    oRetailStore.IsCustomer = 4;
                    oRetailStore.TypeId = 52;

                    CompanyDomain oDomain = new CompanyDomain();
                    oDomain.Domain = "preview.myprintcloud.com/store/retail";

                    oRetailStore.CompanyDomains.Add(oDomain);

                    MPCContext.Companies.Add(oRetailStore);

                    MPCContext.SaveChanges();
                    long RetailStoreId = oRetailStore.CompanyId;


                    output.Text += "Retail Store" + Environment.NewLine;
                    /////////////////////////////////////////// CATS





                    if (Directory.Exists(MPCContentBasePath + @"\categories\"))
                    {
                        Directory.CreateDirectory(MPCContentBasePath + @"\categories\");
                    }

                    if (!Directory.Exists(MPCContentBasePath + @"\categories\" + OrganizationId.ToString()))
                    {
                        Directory.CreateDirectory(MPCContentBasePath + @"\categories\" + OrganizationId.ToString());
                    }

                    if (!Directory.Exists(MPCContentBasePath + @"\categories\" + OrganizationId.ToString() + "\\" + RetailStoreId.ToString()))
                    {
                        Directory.CreateDirectory(MPCContentBasePath + @"\categories\" + OrganizationId.ToString() + "\\" + RetailStoreId.ToString());
                    }

                    string targetCatBasePath = MPCContentBasePath + @"\categories\" + OrganizationId.ToString() + "\\" + RetailStoreId.ToString()+ "\\";


                    var cats = PCContext.sp_PublicCategoryTree().OrderBy(g => g.ParentCategoryID);
                    List<tbl_ProductCategory> oCategory = new List<tbl_ProductCategory>();
                    foreach (var item in cats)
                    {
                        tbl_ProductCategory pcCategory = PCContext.tbl_ProductCategory.Where(g => g.ProductCategoryID == item.ProductCategoryID).Single();

                        Preview.ProductCategory oProductCategory = Mapper.Map<tbl_ProductCategory, ProductCategory>(pcCategory);


                        oProductCategory.OrganisationId = OrganizationId;
                        oProductCategory.CompanyId = RetailStoreId;
                        oProductCategory.ContentType = item.ProductCategoryID.ToString();
                        oProductCategory.Description2 = item.ParentCategoryID.ToString();
                        oProductCategory.ParentCategoryId = null;
                        MPCContext.ProductCategories.Add(oProductCategory);
                        MPCContext.SaveChanges();

                        //\mpc_content\Categories\2\2205

                        //StoredImages/ProductCategoryImages/XXStationery_193_catDetail.png
                        if (pcCategory.ImagePath != null)
                        {
                            if (File.Exists(System.IO.Path.GetFullPath(PinkCardsStoredImagesBasePath + pcCategory.ImagePath.Replace("/StoredImages/", ""))))
                            {
                                string sourcePath = System.IO.Path.GetFullPath(PinkCardsStoredImagesBasePath + pcCategory.ImagePath.Replace("/StoredImages/", ""));
                                string targetPath = targetCatBasePath + pcCategory.ImagePath.Replace(item.ProductCategoryID.ToString(), oProductCategory.ProductCategoryId.ToString());
                                targetPath = targetPath.Replace("/StoredImages/ProductCategoryImages/", "");
                                File.Copy(sourcePath, targetPath);


                            }
                            oProductCategory.ImagePath = pcCategory.ImagePath.Replace(item.ProductCategoryID.ToString(), oProductCategory.ProductCategoryId.ToString());
                            oProductCategory.ImagePath = oProductCategory.ImagePath.Replace("/StoredImages/ProductCategoryImages/", "");
                            
                        }



                        if (pcCategory.ThumbnailPath != null)
                        {
                            if (File.Exists(System.IO.Path.GetFullPath(PinkCardsStoredImagesBasePath + pcCategory.ThumbnailPath.Replace("/StoredImages/", ""))))
                            {
                                string sourcePath = System.IO.Path.GetFullPath(PinkCardsStoredImagesBasePath + pcCategory.ThumbnailPath.Replace("/StoredImages/", ""));
                                string targetPath = targetCatBasePath + pcCategory.ThumbnailPath.Replace(item.ProductCategoryID.ToString(), oProductCategory.ProductCategoryId.ToString());
                                targetPath = targetPath.Replace("/StoredImages/ProductCategoryImages/", "");
                                File.Copy(sourcePath, targetPath);


                            }
                            oProductCategory.ThumbnailPath = pcCategory.ThumbnailPath.Replace(item.ProductCategoryID.ToString(), oProductCategory.ProductCategoryId.ToString());
                            oProductCategory.ThumbnailPath = oProductCategory.ThumbnailPath.Replace("/StoredImages/ProductCategoryImages/", "");

                        }

                        MPCContext.SaveChanges();

                    }


                    foreach (var item in MPCContext.ProductCategories.Where(g => g.OrganisationId == OrganizationId).ToList())
                    {
                        if (item.Description2.Trim() != "0")
                        {
                            string scat = item.Description2;
                            var pCat = MPCContext.ProductCategories.Where(g => g.ContentType.Contains(scat)).SingleOrDefault();
                            if (pCat != null)
                            {
                                item.ParentCategoryId = Convert.ToInt32(pCat.ProductCategoryId);
                                MPCContext.SaveChanges();
                            }
                        }
                    }


                    output.Text += "Retail Store Categories" + Environment.NewLine;



                    MessageBox.Show("test done");

                    return;



                   

                    MessageBox.Show("done");

                    //costcentres

                    //PCContext.tbl_costcentres.Include("CostcentreInstructions").Include("CostcentreResources").Include("CostcentreInstructions.CostcentreWorkInstructionsChoices").ToList();

                }
            }



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

      
    }
}
