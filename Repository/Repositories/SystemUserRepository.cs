﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class SystemUserRepository : BaseRepository<SystemUser>, ISystemUserRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SystemUserRepository(IUnityContainer container)
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

        #endregion
        #region Public
        /// <summary>
        /// Get All System Users of Organisation
        /// </summary>
        public override IEnumerable<SystemUser> GetAll()
        {
            
            return DbSet.Where(systemUser => systemUser.OrganizationId == OrganisationId && systemUser.IsAccountDisabled == 0).ToList();
        }
        public SystemUser GetUserrById(System.Guid SytemUserId)
        {
            return db.SystemUsers.Where(g => g.SystemUserId == SytemUserId).SingleOrDefault();
            //db.SystemUsers.Where(s => s.SystemUserId == SytemUserId).FirstOrDefault();
        }
        public bool Add(System.Guid Id, string Email, string FullName, int OrganizationId)
        {
            SystemUser user = new SystemUser();
            user.Email = Email;
            user.SystemUserId = Id;
            user.FullName = FullName;
            user.UserName = Email;
            user.OrganizationId = OrganizationId;
            user.IsAccountDisabled = 0;
            db.SystemUsers.Add(user);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Update(System.Guid Id, string Email, string FullName, int status, string EmailSignature, string EstimateHeadNotes, string EstimateFootNotes)
        {
            //System.Guid SystemId = Id;

            SystemUser user = db.SystemUsers.Where(g => g.SystemUserId == Id).FirstOrDefault();
            user.Email = Email;
            user.FullName = FullName;
            user.UserName = Email;
            user.EmailSignature = EmailSignature;
            user.EstimateHeadNotes = EstimateHeadNotes;
            user.EstimateFootNotes = EstimateFootNotes;


            if (status == 0)
                user.IsAccountDisabled = 1;
            else if ( status == 1)
                user.IsAccountDisabled = 0;
            else
                user.IsAccountDisabled = 1;


                if(db.SaveChanges()>0){
                    return true;
                }else{
                    return false;
                }

            
        }
        public List<SystemUser> GetSystemUSersByOrganisationID(long OrganisationID)
        {
            return db.SystemUsers.Where(s => s.OrganizationId == OrganisationID).ToList();
            //db.SystemUsers.Where(s => s.SystemUserId == SytemUserId).FirstOrDefault();
        }
       
        public SystemUser GetFirstSystemUser()
        {
            return db.SystemUsers.Where(c => c.OrganizationId == OrganisationId).FirstOrDefault();
        }
        public long OrganisationThroughSystemUserEmail(string Email)
        {
            long OrgID = 0;
           SystemUser SUser= db.SystemUsers.Where(i => i.Email.Equals(Email)).FirstOrDefault();
           if (SUser != null)
           {
               OrgID = (long)SUser.OrganizationId;
           }
            return OrgID;
        }
        public void UpdateEmailSignature(string signature)
        {
            SystemUser currSystemUser = GetUserrById(LoggedInUserId);
            if (currSystemUser != null)
            {
                currSystemUser.EmailSignature = signature;
                SaveChanges();
            }
        }
        public SystemUser GetSystemUserById(Guid userId)
        {
            return DbSet.FirstOrDefault(u => u.SystemUserId == userId);
        }
       
        #endregion
    }
}
