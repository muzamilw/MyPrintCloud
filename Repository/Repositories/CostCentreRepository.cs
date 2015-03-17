using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System;
using System.Data;
using MPC.Models.Common;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using System.Linq.Expressions;
using AutoMapper;

namespace MPC.Repository.Repositories
{
	/// <summary>
	/// CostCentre Repository
	/// </summary>
	public class CostCentreRepository : BaseRepository<CostCentre>, ICostCentreRepository
	{
		#region privte
		#region Private
		private readonly Dictionary<CostCentersColumns, Func<CostCentre, object>> OrderByClause = new Dictionary<CostCentersColumns, Func<CostCentre, object>>
					{
						{CostCentersColumns.Name, d => d.Name},
						{CostCentersColumns.Type, d => d.Type},
						
					};
		#endregion
		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public CostCentreRepository(IUnityContainer container)
			: base(container)
		{

		}

		/// <summary>
		/// Primary database set
		/// </summary>
		protected override IDbSet<CostCentre> DbSet
		{
			get
			{
				return db.CostCentres;
			}
		}

		#endregion

		#region public

		/// <summary>
		/// Get All Cost Centres that are not system defined
		/// </summary>
		public IEnumerable<CostCentre> GetAllNonSystemCostCentres()
		{
			return DbSet.Where(costcentre => costcentre.OrganisationId == OrganisationId && costcentre.Type != 1)
                .OrderBy(costcentre => costcentre.Name).ToList();
		}

		public bool Delete(long CostCentreID)
		{
			try
			{
				db.CostCentres.Remove(db.CostCentres.Where(g => g.CostCentreId == CostCentreID).SingleOrDefault());
				db.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("Delete", ex);
			}
		}


		public long GetMaxCostCentreID()
		{
			try
			{

				var maxId = db.CostCentres.Max(g => g.CostCentreId);

				maxId += 1;


				return maxId;

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool UpdateSystemCostCentre(long CostCentreID, int ProfitMarginID, int NominalCodeId, double MinCost, int UserID, string Description, bool DirectCost, bool IsScheduleable)
		{
			try
			{

				var costCentre = db.CostCentres.Where(g => g.CostCentreId == CostCentreID).SingleOrDefault();

				if (costCentre != null)
				{
					costCentre.DefaultVA = ProfitMarginID;
					costCentre.nominalCode = NominalCodeId;
					costCentre.MinimumCost = MinCost;
					costCentre.LastModifiedBy = UserID.ToString();
					costCentre.Description = Description;
					costCentre.IsDirectCost = Convert.ToInt16(DirectCost);
					costCentre.IsScheduleable = Convert.ToInt16(IsScheduleable);
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
				throw new Exception("UpdateSystemCostCentre", ex);
			}
		}

		/// <summary>
		/// returns the costcentre object with summary information
		/// </summary>
		/// <param name="CostCentreID"></param>
		/// <returns></returns>
		public CostCentre GetCostCentreByID(long CostCentreID)
		{

			try
			{

				try
				{
                    return db.CostCentres.Include("CostcentreInstructions").Where(c => c.CostCentreId == CostCentreID).SingleOrDefault();

				}
				catch (Exception ex)
				{
					throw new Exception("GetCostCentreSummaryByID", ex);
				}

			}
			catch (Exception ex)
			{
				throw new Exception("GetCostCentreSummaryByID", ex);
			}

		}

		/// <summary>
		/// returns the costcentre resources as datatable
		/// </summary>
		/// <param name="CostcentreID"></param>
		/// <returns></returns>
		public CostcentreResource GetCostCentreResources(long CostcentreID)
		{
			try
			{

				return db.CostcentreResources.Where(rr => rr.CostCentreId == CostcentreID).SingleOrDefault();

			}
			catch (Exception ex)
			{
				throw new Exception("GetCostCentreResources", ex);
			}
		}

		public List<CostCentreResource> GetCostCentreResourcesWithNames(long CostcentreID)
		{
			try
			{
				var query = from sysUser in db.SystemUsers
							//join resource in db.CostcentreResources on sysUser.SystemUserId equals resource.ResourceId
							//where resource.CostCentreId == CostcentreID
							select new CostCentreResource()
							{
								//ResourceId = resource.ResourceId ?? 0,
								UserName = sysUser.UserName
							};


				return query.ToList<CostCentreResource>();

			}
			catch (Exception ex)
			{
				throw new Exception("GetCostCentreResources", ex);
			}
		}


		public List<CostCentreType> GetCostCentreTypes(TypeReturnMode ReturnMode)
		{
			try
			{

				if (ReturnMode == TypeReturnMode.All)
				{
					return db.CostCentreTypes.ToList();// "Select * from tbl_costcentreTypes";
				}
				else if (ReturnMode == TypeReturnMode.System)
				{
					return db.CostCentreTypes.Where(c => c.IsSystem == 1).ToList(); //"Select * from tbl_costcentreTypes where IsSystem=1";
				}
				else if (ReturnMode == TypeReturnMode.UserDefined)
				{
					return db.CostCentreTypes.Where(c => c.IsExternal == 1 && c.TypeId != 1).ToList(); //"Select * from tbl_costcentreTypes where IsExternal=1 and typeid <> 1";
				}
				else
					return null;
			}
			catch (Exception ex)
			{
				throw new Exception("GetCostCentreTypes", ex);
			}
		}

		public List<CostcentreSystemType> GetCostCentreSystemTypes()
		{
			try
			{
				return db.CostcentreSystemTypes.ToList();

			}
			catch (Exception ex)
			{
				throw new Exception("GetCostCentreTypes", ex);
				return null;
			}
		}


		public CostcentreInstruction GetCostCentreWorkInstruction(long CostcentreID)
		{
			try
			{
				return db.CostcentreInstructions.Where(i => i.CostCentreId == CostcentreID).FirstOrDefault();

			}
			catch (Exception ex)
			{
				throw new Exception("GetCostCentreWorkInstruction", ex);
			}
		}

		public List<CostCentreType> ReturnCostCentreCategories()
		{
			try
			{

				return db.CostCentreTypes.Where(c => c.IsSystem == 0 && c.IsExternal == 1).ToList();
				//"Select * from tbl_CostCentreTypes where IsSystem=0 and IsExternal=1";

			}
			catch (Exception ex)
			{
				throw new Exception("ReturnCostCentreCategories", ex);
			}
		}

		public List<CostCentre> GetCostCentreList()
		{
			try
			{
				return db.CostCentres.ToList();
			  
			}
			catch (Exception ex)
			{
				throw new Exception("GetCostCentreList", ex);
			}
		}

		public bool CheckCostCentreName(long CostCentreID, string CostCentreName, long OrganisationId)
		{

			try
			{
				long CostCentreId = db.CostCentres.Where(c => c.CostCentreId != CostCentreID && c.Name == CostCentreName && c.OrganisationId == OrganisationId).SingleOrDefault().CostCentreId;

				if (CostCentreId > 0)
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
				throw new Exception("CheckCostCentreName", ex);
			}
		}


	  
		public long InsertWorkInstruction(CostcentreInstruction oInstruction)
		{
			try
			{
				CostcentreInstruction oInstructionObj = new CostcentreInstruction();

				oInstructionObj.Instruction = oInstruction.Instruction;
				oInstructionObj.CostCentreId = oInstruction.CostCentreId;

				db.CostcentreInstructions.Add(oInstructionObj);
				if (db.SaveChanges() > 0)
				{
					return oInstructionObj.InstructionId;
				}
				else
				{
					return 0;
				}

			}
			catch (Exception ex)
			{
				throw new Exception("", ex);
			}
		}

		public long UpdateWorkInstruction(CostcentreInstruction oInstruction)
		{
			try
			{
				CostcentreInstruction oInstructionObj = db.CostcentreInstructions.Where(w => w.InstructionId == oInstruction.InstructionId).FirstOrDefault();

				oInstructionObj.Instruction = oInstruction.Instruction;
				oInstructionObj.CostCentreId = oInstruction.CostCentreId;

				if (db.SaveChanges() > 0)
				{
					return oInstructionObj.InstructionId;
				}
				else
				{
					return 0;
				}

			}
			catch (Exception ex)
			{
				throw new Exception("", ex);
			}
		}

		public bool DeleteWorkInstruction(long InstructionID)
		{
			try
			{
				CostcentreInstruction oInstructionObj = db.CostcentreInstructions.Where(w => w.InstructionId == InstructionID).FirstOrDefault();

				db.CostcentreInstructions.Remove(oInstructionObj);

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
				throw new Exception("", ex);
			}
		}

		public long InsertChoice(CostcentreWorkInstructionsChoice ochoice)
		{

			try
			{
				CostcentreWorkInstructionsChoice oChoiceObj = new CostcentreWorkInstructionsChoice();

				oChoiceObj.Choice = ochoice.Choice;
				oChoiceObj.InstructionId = ochoice.InstructionId;

				db.CostcentreWorkInstructionsChoices.Add(oChoiceObj);
				if (db.SaveChanges() > 0)
				{
					return oChoiceObj.Id;
				}
				else
				{
					return 0;
				}

			}
			catch (Exception ex)
			{
				throw new Exception("", ex);
			}

		}

		public long UpdateChoice(CostcentreWorkInstructionsChoice ochoice)
		{
			try
			{
				CostcentreWorkInstructionsChoice oChoiceObj = db.CostcentreWorkInstructionsChoices.Where(w => w.Id == ochoice.Id).FirstOrDefault();

				oChoiceObj.Choice = ochoice.Choice;
				oChoiceObj.InstructionId = ochoice.InstructionId;

				if (db.SaveChanges() > 0)
				{
					return oChoiceObj.Id;
				}
				else
				{
					return 0;
				}

			}
			catch (Exception ex)
			{
				throw new Exception("", ex);
			}
		}

		public bool DeleteChoice(long ChoiceID)
		{

			try
			{
				CostcentreWorkInstructionsChoice oChoiceObj = db.CostcentreWorkInstructionsChoices.Where(w => w.Id == ChoiceID).FirstOrDefault();

				db.CostcentreWorkInstructionsChoices.Remove(oChoiceObj);

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
				throw new Exception("", ex);
			}
		}

		public long InsertCostCentre(CostCentre oCostCentre)
		{
			try
			{

				db.CostCentres.Add(oCostCentre);
				if (db.SaveChanges() > 0)
				{
					return oCostCentre.CostCentreId;
				}
				else
				{
					return 0;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("InsertUserDefinedCostCentre", ex);
			}
		}

		public bool UpdateCostCentre(CostCentre oCostCentre)
		{
			try
			{

				CostCentre result = db.CostCentres.Where(c => c.CostCentreId == oCostCentre.CostCentreId).FirstOrDefault();

				result = oCostCentre;

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
				throw new Exception("InsertUserDefinedCostCentre", ex);
			}
		}

		public CostCentre GetCostCentreSummary(long CostCentreID)
		{
			try
			{
				//returning the object
				return db.CostCentres.Where(c => c.CostCentreId == CostCentreID).FirstOrDefault();

			}
			catch (Exception ex)
			{
				throw new Exception("GetCostCentreSummaryByID", ex);
			}

		}

	   
		/// <summary>
		/// Get Code for CostCentre For a Company
		/// </summary>
		/// <returns></returns>
		public List<CostCentre> GetCompleteCodeofAllCostCentres(long OrganisationId)
		{
			try
			{
                var query = (from ccType in db.CostCentreTypes
                             join cc in db.CostCentres on ccType.TypeId equals cc.Type
                             join Org in db.Organisations on cc.OrganisationId equals Org.OrganisationId
                             where ((ccType.IsExternal == 1 && ccType.IsSystem == 0 && cc.CompleteCode != null)
                             && Org.OrganisationId == OrganisationId)
                             select cc);

                return query.ToList<CostCentre>();

				return null;
			}
			catch (Exception ex)
			{
				throw  ex;
			}
		}


		public bool ChangeFlag(int FlagID, long CostCentreID)
		{
			try
			{

				CostCentre oCC = db.CostCentres.Where(c => c.CostCentreId == CostCentreID).FirstOrDefault();
				oCC.FlagId = FlagID;
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
				throw new Exception("ChangeFlag", ex);
			}
		}


		public CostCentre GetSystemCostCentre(long SystemTypeID, long OrganisationID)
		{

			try
			{
				return db.CostCentres.Where(c => c.SystemTypeId == SystemTypeID && c.OrganisationId == OrganisationID).FirstOrDefault();

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		public List<CostCentreType> GetCostCentreCategories(long OrganisationId)
		{
			try
			{
                return db.CostCentreTypes.Where(t => t.OrganisationId == OrganisationId && t.IsSystem == 0).ToList();

			}
			catch (Exception ex)
			{
				throw new Exception("GetCostCentreCategories", ex);
			}
		}


		public bool IsCostCentreAvailable(int CategoryID)
		{
			try
			{
				if (db.CostCentres.Where(c => c.Type == CategoryID).Count() > 0)
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
				throw new Exception("IsCostCentreAvailable", ex);
			}
		}

		public CostCentersResponse GetUserDefinedCostCenters(CostCenterRequestModel request)
		{
			int fromRow = (request.PageNo - 1) * request.PageSize;
			int toRow = request.PageSize;
            Expression<Func<CostCentre, bool>> query;
            if (request.CostCenterType != 0)
            {
                query = oCostCenter => oCostCenter.Type == request.CostCenterType && oCostCenter.IsDisabled == 0 && oCostCenter.OrganisationId == OrganisationId;
            }
            else
            {
                query = oCostCenter => oCostCenter.Type != 1 && oCostCenter.IsDisabled == 0 && oCostCenter.OrganisationId == OrganisationId;
            }
			var rowCount = DbSet.Count(query);
			var costCenters = request.IsAsc
				? DbSet.Where(query)
					.OrderBy(OrderByClause[request.CostCenterOrderBy])
					.Skip(fromRow)
					.Take(toRow)
					.ToList()
				: DbSet.Where(query)
					.OrderByDescending(OrderByClause[request.CostCenterOrderBy])
					.Skip(fromRow)
					.Take(toRow)
					.ToList();
			return new CostCentersResponse
			{
				RowCount = rowCount,
				CostCenters = costCenters
                
			};
		}
		public CostCentre GetCostCentersByID(long costCenterID)
		{


			var query = from tblCostCenter in db.CostCentres
						where tblCostCenter.CostCentreId == costCenterID && tblCostCenter.isPublished == true
						select tblCostCenter;

				return query.ToList().FirstOrDefault();
			

		}
		public IEnumerable<CostCentre> GetAllCompanyCentersByOrganisationId()
		{
			return DbSet.Where(x => x.OrganisationId == OrganisationId && x.isPublished == true).ToList();
		}
		#endregion

		#region "CostCentre Template"
		public CostCentreTemplate LoadCostCentreTemplate(int TemplateID)
		{
			try
			{

				return db.CostCentreTemplates.Where(t => t.Id == TemplateID).FirstOrDefault();


			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public double ExecUserVariable(CostCentreVariable oVariable)
		{
			string sSqlString = null;
			double dResult = 0;
			try
			{
			   
				if (Convert.ToBoolean( oVariable.IsCriteriaUsed) == true)
				{
					sSqlString = db.Database.SqlQuery<string>("select top 1 cast(" + oVariable.RefFieldName + " as varchar(100)) from " + oVariable.RefTableName + " where " + oVariable.CriteriaFieldName + "= " + oVariable.Criteria + "", "").FirstOrDefault();
				   
				   
				}
				else
				{
					sSqlString = db.Database.SqlQuery<string>("select top 1 cast(" + oVariable.RefFieldName + " as varchar(100)) from " + oVariable.RefTableName + "", "").FirstOrDefault();
				}

			   
				//we have received a propper result, continue else raise exception
				if ((sSqlString != null))
				{
					dResult = Convert.ToDouble(sSqlString);
				}
				else
				{
					throw new Exception("Unable to retreive System Variable Value");
				}

				return dResult;
			}
			catch (Exception ex)
			{
				throw new Exception("ExecUserVariable", ex);
			}
		}

		public double ExecuteUserResource(long ResourceID, ResourceReturnType oCostPerHour)
		{
			try
			{
				if (oCostPerHour == ResourceReturnType.CostPerHour)
				{
					return Convert.ToDouble(db.SystemUsers.Select(c => c.CostPerHour).FirstOrDefault());
					// db.SystemUsers.Where(s => s.SystemUserId == ResourceID)
				}
				else
					return 0;

			}
			catch (Exception ex)
			{
				throw new Exception("ExecuteUserResource", ex);
			}
		}

		public double ExecuteUserStockItem(int StockID, StockPriceType StockPriceType, out double Price, out double PerQtyQty)
		{
			try
			{
				ObjectParameter paramPrice = new ObjectParameter("Price", typeof(float));
				ObjectParameter paramQty = new ObjectParameter("PerQtyQty", typeof(float));
				
				db.sp_CostCentreExecution_get_StockPriceByCalculationType(StockID, (int)StockPriceType, paramPrice, paramQty);
				Price = Convert.ToDouble(paramPrice);
				PerQtyQty = Convert.ToDouble(paramQty);
				return Price;
			}
			catch (Exception ex)
			{
				throw new Exception("ExecuteUserStockItem", ex);
			}
		}

        
		#endregion

		#region AddressSelect
		public List<CostCentre> GetCorporateDeliveryCostCentersList(long CompanyID)
		{

            var query = (from tblCostCenter in db.CostCentres
                         join CorpCostCenter in db.CompanyCostCentres on tblCostCenter.CostCentreId equals (long)CorpCostCenter.CostCentreId
                         where tblCostCenter.Type == (int)CostCenterTypes.Delivery && tblCostCenter.isPublished == true
                         && CorpCostCenter.CompanyId == CompanyID
                         orderby tblCostCenter.MinimumCost
                         select tblCostCenter).ToList();
                            //select new CostCentre()
                            //{

                            //    CostCentreId = tblCostCenter.CostCentreId,
                            //    CompletionTime = tblCostCenter.CompletionTime,
                            //    MinimumCost = tblCostCenter.MinimumCost,
                            //    Description = tblCostCenter.Description,
                            //    Name = tblCostCenter.Name,
                            //    SetupCost = tblCostCenter.DeliveryCharges ?? 0,
                            //    EstimateProductionTime = tblCostCenter.EstimateProductionTime
                            //};


				return query.ToList();

		}
		public List<CostCentre> GetDeliveryCostCentersList()
		{


			var query = from tblCostCenter in db.CostCentres
						where tblCostCenter.Type == (int)CostCenterTypes.Delivery && tblCostCenter.isPublished == true && tblCostCenter.IsDisabled == 0
						orderby tblCostCenter.MinimumCost
						select tblCostCenter;


				return query.ToList();
			

		}
		#endregion
		 #region "Compile Binaries"
		#endregion

		#region exportOrgFunctions

		public List<CostCentre> GetCostCentersByOrganisationID(long OrganisationID,out List<CostCenterChoice> CostCentreChoices)
		{
			try
			{
				List<CostCentre> CostCentres = new List<CostCentre>();
				db.Configuration.LazyLoadingEnabled = false;
				db.Configuration.ProxyCreationEnabled = false;
				List<CostCenterChoice> choices = new  List<CostCenterChoice>();
				List<CostCenterChoice> Lstchoices = new List<CostCenterChoice>();

                Mapper.CreateMap<CostCentre, CostCentre>()
                .ForMember(x => x.CompanyCostCentres, opt => opt.Ignore())
                .ForMember(x => x.CostCentreType, opt => opt.Ignore());

                Mapper.CreateMap<CostcentreInstruction, CostcentreInstruction>()
               .ForMember(x => x.CostCentre, opt => opt.Ignore());

                Mapper.CreateMap<CostcentreResource, CostcentreResource>()
                .ForMember(x => x.CostCentre, opt => opt.Ignore());

                Mapper.CreateMap<CostcentreWorkInstructionsChoice, CostcentreWorkInstructionsChoice>()
                .ForMember(x => x.CostcentreInstruction, opt => opt.Ignore());

              


				CostCentres = db.CostCentres.Include("CostcentreInstructions").Include("CostcentreResources").Include("CostcentreInstructions.CostcentreWorkInstructionsChoices").Where(c => c.OrganisationId == OrganisationID).ToList();

                List<CostCentre> oOutputCostCentre = new List<CostCentre>();

                if (CostCentres != null && CostCentres.Count > 0)
                {
                    foreach (var cost in CostCentres)
                    {
                        var omappedCost = Mapper.Map<CostCentre, CostCentre>(cost);
                        oOutputCostCentre.Add(omappedCost);

                        if (cost.CostCentreId != null)
                        {
                            choices = db.CostCenterChoices.Where(c => c.CostCenterId == cost.CostCentreId).ToList();
                            if (choices != null && choices.Count > 0)
                            {
                                foreach (var ch in choices)
                                {
                                    Lstchoices.Add(ch);
                                }

                            }
                    }
                }

				}


                CostCentreChoices = Lstchoices;
                return oOutputCostCentre;

				
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}



		
		#endregion
	}
}
