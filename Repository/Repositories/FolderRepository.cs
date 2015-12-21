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
    public class FolderRepository : BaseRepository<Folder>, IFolderRepository 
    {
        public FolderRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Folder> DbSet
        {
            get
            {
                return db.Folders;
            }
        }
        public List<Folder> GetFoldersByCompanyId(long CompanyID, long OrganisationID)
        {
            return db.Folders.Where(i => i.CompanyId == CompanyID && i.OrganisationId == OrganisationID).ToList();
        }
    }
}
