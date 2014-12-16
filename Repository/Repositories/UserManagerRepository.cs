﻿using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Repository.Repositories
{
    public class UserManagerRepository : BaseRepository<SystemUser>, IUserManagerRepository
    {
        public UserManagerRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<SystemUser> DbSet
        {
            get
            {
                return db.SystemUsers;
            }
        }
        public SystemUser GetSalesManagerDataByID(int ManagerId)
        {
          
            SystemUser rec = db.SystemUsers.Where(u => u.SystemUserId == ManagerId).FirstOrDefault();
            return rec;
        }

      
    }
}