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
        private readonly IOrganisationRepository organisationRepository;
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
        public CostCentreRepository(IUnityContainer container, IOrganisationRepository organisationRepository)
			: base(container)
		{
            this.organisationRepository = organisationRepository;
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
            return DbSet.Where(costcentre => costcentre.OrganisationId == OrganisationId && costcentre.Type != (int)CostCenterTypes.SystemCostCentres && costcentre.IsDisabled != true && costcentre.Type != (int)CostCenterTypes.Delivery && costcentre.Type != (int)CostCenterTypes.WebOrder)
                .OrderBy(costcentre => costcentre.Name).ToList();
		}
		/// <summary>
		/// Get All Cost Centres that are not system defined
		/// </summary>
        public CostCentreResponse GetAllNonSystemCostCentresForProduct(GetCostCentresRequest request)
		{
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchString);
            Expression<Func<CostCentre, bool>> query =
                s =>
                    (isSearchFilterSpecified && (s.Name.Contains(request.SearchString)) ||
                     (s.HeaderCode.Contains(request.SearchString)) ||
                     !isSearchFilterSpecified && (s.Type != 1) && (s.Type != 11) && (s.Type != 29)) &&
                     (!request.Type.HasValue || s.Type == request.Type.Value) &&
                     s.OrganisationId == OrganisationId;

            int rowCount = DbSet.Count(query);
            // ReSharper disable once ConditionalTernaryEqualBranch
            IEnumerable<CostCentre> costCentres = request.IsAsc
                ? DbSet.Where(query)
                    .OrderBy(x => x.Name)
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Where(query)
                    .OrderBy(x => x.Name)
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();
            return new CostCentreResponse
            {
                RowCount = rowCount,
                CostCentresForproducts = costCentres
            };
		}

        public CostCentre GetFirstCostCentreByOrganisationId(long organisationId)
        {
            return db.CostCentres.Where(c => c.OrganisationId == organisationId & c.Type != (int)CostCenterTypes.Delivery).FirstOrDefault();

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
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;
                    return db.CostCentres.Include("CostcentreInstructions").Include("CostcentreInstructions.CostcentreWorkInstructionsChoices").Where(c => c.CostCentreId == CostCentreID).SingleOrDefault();

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

        public long GetCostCentreIdByName(string costCenterName)
        {

            try
            {

                try
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.Configuration.LazyLoadingEnabled = false;
                    return db.CostCentres.Where(c => c.Name == costCenterName).Select(c => c.CostCentreId).SingleOrDefault();

                }
                catch (Exception ex)
                {
                    throw new Exception("GetCostCentreByName", ex);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("GetCostCentreByName", ex);
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
                oCostCentre.OrganisationId = this.OrganisationId;
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
                if (oCostCentre.ImageBytes != null)
                {
                    result.ThumbnailImageURL = oCostCentre.ThumbnailImageURL;
                    result.ImageBytes = null;
                }
                List<CostcentreInstruction> oCostcentreInstructions = db.CostcentreInstructions.Where(g => g.CostCentreId == oCostCentre.CostCentreId).ToList();
                
                if(oCostCentre.CostcentreInstructions != null)
                {
                    foreach (var item in oCostcentreInstructions)
                    {
                        CostcentreInstruction oCCInstruction = oCostCentre.CostcentreInstructions.Where(g=>g.InstructionId==item.InstructionId).FirstOrDefault();
                        if (oCCInstruction == null)
                        {
                            List<CostcentreWorkInstructionsChoice> choicesList = db.CostcentreWorkInstructionsChoices.Where(g => g.InstructionId == item.InstructionId).ToList();
                            db.CostcentreWorkInstructionsChoices.RemoveRange(choicesList);
                            db.CostcentreInstructions.Remove(item);
                        }
                        
                    }

                    foreach (var inst in oCostCentre.CostcentreInstructions)
                    {
                        if (inst.InstructionId > 0)
                        {
                            List<CostcentreWorkInstructionsChoice> choList = db.CostcentreWorkInstructionsChoices.Where(g => g.InstructionId == inst.InstructionId).ToList();
                            if (inst.CostcentreWorkInstructionsChoices == null)
                            {
                                db.CostcentreWorkInstructionsChoices.RemoveRange(choList);
                            }
                            else
                            {
                                foreach(var ch in choList){
                                    CostcentreWorkInstructionsChoice chi = inst.CostcentreWorkInstructionsChoices.Where(g => g.Id == ch.Id).FirstOrDefault();
                                    if (chi == null)
                                    {
                                        db.CostcentreWorkInstructionsChoices.Remove(ch);
                                    }
                                }
                                
                            }

                            CostcentreInstruction obj = db.CostcentreInstructions.Where(i => i.InstructionId == inst.InstructionId).SingleOrDefault();
                            obj.Instruction = inst.Instruction;

                            if (inst.CostcentreWorkInstructionsChoices != null)
                            {
                                foreach (var ch in inst.CostcentreWorkInstructionsChoices)
                                {
                                    if (ch.Id > 0)
                                    {
                                        CostcentreWorkInstructionsChoice objChoice = db.CostcentreWorkInstructionsChoices.Where(i => i.Id == ch.Id).SingleOrDefault();
                                        objChoice.Choice = ch.Choice;
                                    }
                                    else
                                    {
                                        CostcentreWorkInstructionsChoice objChoice = new CostcentreWorkInstructionsChoice();
                                        objChoice.Choice = ch.Choice;
                                        objChoice.InstructionId = obj.InstructionId;
                                        db.CostcentreWorkInstructionsChoices.Add(objChoice);
                                    }
                                   
                                }
                            }
                        }
                        else
                        {
                            CostcentreInstruction obj = new CostcentreInstruction();
                            obj.Instruction = inst.Instruction;
                            obj.CostCentreId = oCostCentre.CostCentreId;
                            db.CostcentreInstructions.Add(obj);
                            db.SaveChanges();
                            if (inst.CostcentreWorkInstructionsChoices != null)
                            {
                                foreach (var ch in inst.CostcentreWorkInstructionsChoices)
                                {
                                    CostcentreWorkInstructionsChoice objChoice = new CostcentreWorkInstructionsChoice();
                                    objChoice.Choice = ch.Choice;
                                    objChoice.InstructionId = obj.InstructionId;
                                    db.CostcentreWorkInstructionsChoices.Add(objChoice);
                                }
                            }
                        }
                        
                    }
                }
                else if (oCostcentreInstructions.Count > 0)
                {
                    foreach (var item in oCostcentreInstructions)
                    {
                        List<CostcentreWorkInstructionsChoice> choicesList = db.CostcentreWorkInstructionsChoices.Where(g => g.InstructionId == item.InstructionId).ToList();
                        db.CostcentreWorkInstructionsChoices.RemoveRange(choicesList);
                        db.CostcentreInstructions.Remove(item);
                    }
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


        /// <summary>
        /// Get Code for CostCentre For a Company
        /// </summary>
        /// <returns></returns>
        public List<CostCentre> GetAllCostCentresForRecompiling(long OrganisationId)
        {
            try
            {
                var query = (from ccType in db.CostCentreTypes
                             join cc in db.CostCentres on ccType.TypeId equals cc.Type
                             join Org in db.Organisations on cc.OrganisationId equals Org.OrganisationId
                             where ((ccType.IsExternal == 1 && ccType.IsSystem == 0)
                             && Org.OrganisationId == OrganisationId)
                             select cc);

                return query.ToList<CostCentre>();

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
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
            bool isStringSpecified = !string.IsNullOrEmpty(request.SearchString);
            Expression<Func<CostCentre, bool>> query;
            if (request.CostCenterType != 0)
            {
                query = oCostCenter => (!isStringSpecified || oCostCenter.Name.Contains(request.SearchString) || oCostCenter.WebStoreDesc.Contains(request.SearchString)) && oCostCenter.Type == request.CostCenterType && oCostCenter.OrganisationId == OrganisationId;
            }
            else
            {
                query = oCostCenter => (!isStringSpecified || oCostCenter.Name.Contains(request.SearchString)|| oCostCenter.WebStoreDesc.Contains(request.SearchString)) && oCostCenter.Type != 1 && oCostCenter.OrganisationId == OrganisationId;
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
		public IEnumerable<CostCentre> GetAllCompanyCentersForOrderItem()
		{
			return DbSet.Where(x => x.OrganisationId == OrganisationId && x.isPublished == true && (x.Type == 29 || x.Type == 139)).ToList();
		}

        public IEnumerable<CostCentre> GetAllDeliveryCostCentersForStore()
        {
            return DbSet.Where(x => x.OrganisationId == OrganisationId && x.isPublished == true && x.Type == (int)CostCenterTypes.Delivery)
                .OrderBy(x => x.Name).ToList();
        }
        public CostCenterVariablesResponseModel GetCostCenterVariablesTree(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            CostCenterVariablesResponseModel oResponse = new CostCenterVariablesResponseModel();
            if(id == 1) //Cost Centers
            {
                List<CostCentreType> ccTypes = db.CostCentreTypes.Where(c => c.IsSystem != (short)1 && c.OrganisationId == this.OrganisationId).ToList();
                if (ccTypes != null)
                {
                    foreach (var cc in ccTypes)
                    {
                        cc.CostCentres = db.CostCentres.Where(cv => cv.Type == cc.TypeId && cv.OrganisationId == this.OrganisationId && cv.IsDisabled != true).ToList();
                    }
                    oResponse.CostCenterVariables = ccTypes;
                }
            }
            else if(id == 2)//Variables
            {
                List<CostCentreVariableType> vTypes = db.CostCentreVariableTypes.ToList();
                if (vTypes != null)
                {
                    foreach (var v in vTypes)
                    {
                        v.VariablesList = db.CostCentreVariables.Where(cv => cv.CategoryId == v.CategoryId).ToList();
                    }
                    oResponse.VariableVariables = vTypes;
                }
            }
            else if(id == 3)//Resources
            {
                oResponse.ResourceVariables = db.SystemUsers.Where(u => u.OrganizationId == this.OrganisationId).ToList();
            }
            else if (id == 4)//Questions
            {
                oResponse.QuestionVariables = db.CostCentreQuestions.ToList();
            }
            else if (id == 5)//Matrices
            {
                oResponse.MatricesVariables = db.CostCentreMatrices.Where(c => c.OrganisationId == this.OrganisationId).ToList();
            }
            else if (id == 6)//Lookups
            {
                oResponse.LookupVariables = db.LookupMethods.Where(c => c.OrganisationId == this.OrganisationId && (c.Type != 0 || c.Type != null)).ToList();
            }

            return oResponse;
        }

        public CostCenterBaseResponse GetBaseData()
        {
            Organisation organisation = organisationRepository.GetOrganizatiobByID();
            List<Currency> list = db.Currencies.ToList();
            db.Configuration.LazyLoadingEnabled = false;
            var types = db.CostCentreTypes.Where(c => c.TypeId == 2 || c.TypeId ==3).ToList();
            var resources = db.SystemUsers.Where(u => u.OrganizationId == this.OrganisationId).ToList();
            var nominalCodes = db.ChartOfAccounts.Where(u => u.SystemSiteId == this.OrganisationId).ToList();
            var ccVariables = db.CostCentreVariables.OrderBy(v => v.Name).ToList();   //Where(c => c.SystemSiteId == this.OrganisationId)   Commented by Muzzammil on 12th may 2015 as this is not needed.
            var carriers = db.DeliveryCarriers.ToList();
            var markups = db.Markups.Where(m => m.OrganisationId == this.OrganisationId).ToList();
            return new CostCenterBaseResponse
            {
                CostCenterCategories = types,
                CostCenterResources = resources,
                NominalCodes = nominalCodes,
                Markups = markups,
                CostCentreVariables = ccVariables,
                DeliveryCarriers = carriers,
                CurrencySymbol = organisation == null ? null : organisation.Currency==null? null: organisation.Currency.CurrencySymbol
            };
        }

        public CostCentre GetGlobalWebOrderCostCentre(long OrganisationId) 
        {
            return db.CostCentres.Where(g => g.Type == 29 && g.OrganisationId == OrganisationId).SingleOrDefault();
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
						where tblCostCenter.Type == (int)CostCenterTypes.Delivery && tblCostCenter.isPublished == true && tblCostCenter.IsDisabled == false
						orderby tblCostCenter.MinimumCost
						select tblCostCenter;


				return query.ToList();
			

		}

        /// <summary>
        /// Get web order cost centre
        /// </summary>
        public CostCentre GetWebOrderCostCentre(long OrganisationId)
        {
            return db.CostCentres.Where(c => c.Type == (int)CostCenterTypes.WebOrder && c.OrganisationId == OrganisationId).FirstOrDefault();
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


        public List<CostCentreType> GetCostCentreTypeByOrganisationID(long OID)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                return db.CostCentreTypes.Where(c => c.OrganisationId == OID).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<CostCentre> GetCostCentresforxml(List<long> CostCenterIDs)
        {
           

            //List<CostCentre> ProductData = from CC in db.CostCentres
            //                  where CostCenterIDs.Contains(CC.CostCentreId) select CC;

            return db.CostCentres.Where(c => CostCenterIDs.Contains(c.CostCentreId)).ToList();


           
        }
		
		#endregion



	}
}
