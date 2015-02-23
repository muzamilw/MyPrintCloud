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

           
            

            long OrganizationId = 1;
            string MPCContentBasePath = @"E:\mpc-content\";

            string PinkCardsStoredImagesBasePath = @"E:\StoredImagesPinkCards\";

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


                    //costcentres matrix

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

                    //select * from pinkcards.dbo.tbl_CostCentreVariableTypes
                    //costcentre variables
                    List<tbl_costcentrevariabletypes> otbl_CostCentreVariableTypes = PCContext.tbl_costcentrevariabletypes.ToList();

                    foreach (var item in otbl_CostCentreVariableTypes)
                    {
                        Preview.CostCentreVariableType oVar = new CostCentreVariableType();
                        oVar.CategoryId = item.CategoryID;
                        oVar.Name = item.Name;

                        MPCContext.CostCentreVariableTypes.Add( oVar);

                    }
                    MPCContext.SaveChanges();



                    //costcentre variables
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

                    }


                    // costcentres
                   // AutoMapperConfiguration.Configure();

                    Mapper.AssertConfigurationIsValid();
                    //Mapper.CreateMap<tbl_costcentres, CostCentre>();

                    CreateNestedMappers(typeof( tbl_costcentres),  typeof(CostCentre));
                    List<tbl_costcentres> otbl_costcentres = PCContext.tbl_costcentres.Include("tbl_costcentre_instructions").Include("tbl_costcentre_instructions.tbl_costcentre_workinstructions_choices").Include("tbl_costcentre_resources").ToList();
                    foreach (var item in otbl_costcentres)
                    {
                        Preview.CostCentre oCostCentre = Mapper.Map<tbl_costcentres, CostCentre>(item);

                        MPCContext.CostCentres.Add(oCostCentre);
                                                                       
                    }

                    MPCContext.SaveChanges();
                       

//                        //oCostCentre.CostCentreId = 
//oCostCentre.Name = item.Name;
//oCostCentre.Description = item.Description;
//oCostCentre.Type = item.Type;
//oCostCentre.CreatedBy = item.CreatedBy;
//oCostCentre.LockedBy = item.LockedBy;
//oCostCentre.LastModifiedBy = item.LastModifiedBy;
//oCostCentre.MinimumCost = item.MinimumCost;
//oCostCentre.SetupCost = item.SetupCost;
//oCostCentre.SetupTime = item.SetupTime;
//oCostCentre.DefaultVA = item.DefaultVA ;
//oCostCentre.DefaultVAId = item.DefaultVAId;
//oCostCentre.OverHeadRate = item.OverHeadRate;
//oCostCentre.HourlyCharge = item.HourlyCharge;
//oCostCentre.CostPerThousand = item.CostPerThousand;
//oCostCentre.CreationDate = item.CreationDate;
//oCostCentre.LastModifiedDate = item.LastModifiedDate;
//oCostCentre.PreferredSupplierId = item.PreferredSupplierID;
//oCostCentre.CodeFileName = item.CodeFileName;
//oCostCentre.nominalCode = item.nominalCode;
//oCostCentre.CompletionTime = item.CompletionTime;
//oCostCentre.HeaderCode = item.HeaderCode;
//oCostCentre.MiddleCode = item.MiddleCode;
//oCostCentre.FooterCode = item.FooterCode;
//oCostCentre.strCostPlantParsed = item.strCostPlantParsed;
//oCostCentre.strCostPlantUnParsed = item.strCostPlantUnParsed;
//oCostCentre.strCostLabourParsed  = strCostLabourParsed
//oCostCentre.strCostLabourUnParsed = strCostLabourUnParsed;
//oCostCentre.strCostMaterialParsed = strCostMaterialParsed;
//oCostCentre.strCostMaterialUnParsed = strCostMaterialUnParsed;
//oCostCentre.strPricePlantParsed = strPricePlantParsed;
//oCostCentre.strPricePlantUnParsed = strPricePlantUnParsed;
//oCostCentre.strPriceLabourParsed = strPriceLabourParsed;
//oCostCentre.strPriceLabourUnParsed = strPriceLabourUnParsed;
//oCostCentre.strPriceMaterialParsed = strPriceMaterialParsed;
//oCostCentre.strPriceMaterialUnParsed=strPriceMaterialUnParsed;
//oCostCentre.strActualCostPlantParsed = strActualCostPlantParsed;
//oCostCentre.strActualCostPlantUnParsed=strActualCostPlantUnParsed;
//oCostCentre.strActualCostLabourParsed=strActualCostLabourParsed;
//oCostCentre.strActualCostLabourUnParsed=strActualCostLabourUnParsed;
//oCostCentre.strActualCostMaterialParsed=strActualCostMaterialParsed;
//oCostCentre.strActualCostMaterialUnParsed=strActualCostMaterialUnParsed;
//oCostCentre.strTimeParsed=strTimeParsed;
//oCostCentre.strTimeUnParsed=strTimeUnParsed;
//oCostCentre.IsDisabled=IsDisabled;
//oCostCentre.IsDirectCost=IsDirectCost;
//oCostCentre.SetupSpoilage=SetupSpoilage;
//oCostCentre.RunningSpoilage=RunningSpoilage;
//oCostCentre.CalculationMethodType=CalculationMethodType;
//oCostCentre.NoOfHours = NoOfHours;
//oCostCentre.PerHourCost = PerHourCost;
//oCostCentre.PerHourPrice=PerHourPrice;
//oCostCentre.UnitQuantity=UnitQuantity;
//oCostCentre.QuantitySourceType=QuantitySourceType;
//oCostCentre.QuantityVariableId=QuantityVariableId;
//oCostCentre.QuantityQuestionString=QuantityQuestionString;
//oCostCentre.QuantityQuestionDefaultValue=QuantityQuestionDefaultValue;
//oCostCentre.QuantityCalculationString=QuantityCalculationString;
//oCostCentre.CostPerUnitQuantity=CostPerUnitQuantity;
//oCostCentre.PricePerUnitQuantity=PricePerUnitQuantity;
//oCostCentre.TimePerUnitQuantity=TimePerUnitQuantity;
//oCostCentre.TimeRunSpeed=TimeRunSpeed;
//oCostCentre.TimeNoOfPasses=TimeNoOfPasses;
//oCostCentre.TimeSourceType=TimeSourceType;
//oCostCentre.TimeVariableId=TimeVariableId;
//oCostCentre.TimeQuestionString=TimeQuestionString;
//oCostCentre.TimeQuestionDefaultValue=TimeQuestionDefaultValue;
//oCostCentre.TimeCalculationString=TimeCalculationString;
//oCostCentre.Priority=Priority;
//oCostCentre.CostQuestionString=CostQuestionString;
//oCostCentre.CostDefaultValue=CostDefaultValue;
//oCostCentre.PriceQuestionString=PriceQuestionString
//oCostCentre.PriceDefaultValue=PriceDefaultValue;
//oCostCentre.EstimatedTimeQuestionString=EstimatedTimeQuestionString;
//oCostCentre.EstimatedTimeDefaultValue=EstimatedTimeDefaultValue;
//oCostCentre.Sequence
//oCostCentre.CompleteCode
//oCostCentre.ItemDescription
//oCostCentre.SystemTypeId
//oCostCentre.FlagId
//oCostCentre.IsScheduleable
//oCostCentre.SystemSiteId
//oCostCentre.IsPrintOnJobCard
//oCostCentre.WebStoreDesc
//oCostCentre.isPublished
//oCostCentre.EstimateProductionTime
//oCostCentre.MainImageURL
//oCostCentre.ThumbnailImageURL
//oCostCentre.isOption1
//oCostCentre.isOption2
//oCostCentre.isOption3
//oCostCentre.TextOption1
//oCostCentre.TextOption2
//oCostCentre.TextOption3
//oCostCentre.CCIDOption1
//oCostCentre.CCIDOption2
//oCostCentre.CCIDOption3
//oCostCentre.SetupCharge2
//oCostCentre.SetupCharge3
//oCostCentre.MinimumCost2
//oCostCentre.MinimumCost3
//oCostCentre.PricePerUnitQuantity2
//oCostCentre.PricePerUnitQuantity3
//oCostCentre.QuantityVariableID2
//oCostCentre.QuantityVariableID3
//oCostCentre.QuantitySourceType2
//oCostCentre.QuantitySourceType3
//oCostCentre.QuantityQuestionString2
//oCostCentre.QuantityQuestionString3
//oCostCentre.QuantityQuestionDefaultValue2
//oCostCentre.QuantityQuestionDefaultValue3
//oCostCentre.DefaultVAId2
//oCostCentre.DefaultVAId3
//oCostCentre.IsPrintOnJobCard2
//oCostCentre.IsPrintOnJobCard3
//oCostCentre.IsDirectCost2
//oCostCentre.IsDirectCost3
//oCostCentre.PreferredSupplierID2
//oCostCentre.PreferredSupplierID3
//oCostCentre.EstimateProductionTime2
//oCostCentre.EstimateProductionTime3
//oCostCentre.DeliveryCharges
//oCostCentre.isFromMIS
//oCostCentre.XeroAccessCode
//oCostCentre.OrganisationId
//oCostCentre.DeliveryType
//oCostCentre.DeliveryServiceType

//                    }



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
