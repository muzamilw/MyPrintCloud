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

namespace MPC.Repository.Repositories
{
    class MachineRepository : BaseRepository<Machine>, IMachineRepository
    {


        #region Private
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
        public MachineRepository(IUnityContainer container)
            : base(container)
        {

        }
        #endregion
        public MachineListResponseModel GetAllMachine(MachineRequestModel request)
        {

            //var result = from t in db.Machines
            //             join x in db.LookupMethods on t.MachineId equals x.MethodId
            //             select t;
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<Machine, bool>> query;
            if (request.isGuillotineList)
            {
                 query = machine => (machine.IsDisabled == false && machine.MachineCatId==4);
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
        public MachineResponseModel GetMachineByID(long MachineID)
        {
           // Machine ms = DbSet.Where(g => g.MachineId == MachineID).FirstOrDefault();
            return new MachineResponseModel
            {
                machine = DbSet.Where(g => g.MachineId == MachineID).SingleOrDefault(),
                lookupMethods = GetAllLookupMethodList(),
                Markups = GetAllMarkupList(),
                StockItemforInk = GetAllStockItemforInk(),
                MachineResources= GetAllMachineResources()

            };

            
        }


        protected override IDbSet<Machine> DbSet
        {
            get
            {
                return db.Machines;
            }
        }
        public IEnumerable<LookupMethod> GetAllLookupMethodList()
        {
            return db.LookupMethods;
        }
        public IEnumerable<Markup> GetAllMarkupList()
        {
            return db.Markups;
        }
        public IEnumerable<StockItem> GetAllStockItemforInk()
        {
            return db.StockItems.Where(g => g.CategoryId == 2).ToList();
        }
        public IEnumerable<MachineResource> GetAllMachineResources()
        {
            return db.MachineResources;
        }

      
    }
}