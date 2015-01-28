using System;
using System.Collections.Generic;
using System.Linq;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System.Data.Entity;
using Microsoft.Practices.Unity;

namespace MPC.Repository.Repositories
{
    class MachineRepository : BaseRepository<Machine>, IMachineRepository
    {
         #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MachineRepository(IUnityContainer container)
            : base(container)
        {

        }
        #endregion
        public MachineResponseModel GetAllMachine(MachineRequestModel request)
        {
           
            return new MachineResponseModel
            {
                RowCount = DbSet.Count(),
                MachineList =  DbSet.ToList()
            };    


               
        }
        public Machine GetMachineByID(long MachineID)
        {
            return DbSet.Where(g=>g.MachineId==MachineID).FirstOrDefault();
        }
        protected override IDbSet<Machine> DbSet
        {
            get
            {
                return db.Machines;
            }
        }

    }
}
