using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System.Collections.Generic;
using System.Linq;
using MPC.Models.Common;
using System;
using System.Linq.Expressions;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Machine Repository
    /// </summary>
    public class MachineRepository : BaseRepository<Machine>, IMachineRepository
    {
        #region privte

        /// <summary>
        /// Machine Orderby clause
        /// </summary>
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

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Machine> DbSet
        {
            get
            {
                return db.Machines;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Find Machine
        /// </summary>
        public Machine Find(int id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Get Machines For Product
        /// </summary>
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

        #endregion

    }
}
