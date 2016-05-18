using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    public class SystemUserService : ISystemUserService
    {
        private readonly ISystemUserRepository systemUserRepository;
        public SystemUserService(ISystemUserRepository systemUserRepository)
        {
            this.systemUserRepository = systemUserRepository;
        }

        public bool Update(System.Guid Id, string Email, string FullName, int OrganizationId, int status, string EmailSignature, string EstimateHeadNotes, string EstimateFootNotes)
        {

            if (systemUserRepository.GetUserrById(Id) != null)
            {
                return systemUserRepository.Update(Id, Email, FullName, status, EmailSignature, EstimateHeadNotes, EstimateFootNotes);
            }
            else
            {
                return systemUserRepository.Add(Id, Email, FullName, OrganizationId);
            }
        }
        public bool Add(System.Guid Id, string Email, string FullName, int OrganizationId)
        {
            return systemUserRepository.Add(Id, Email, FullName, OrganizationId);
        }

        public string GetEmailSignature()
        {
            var user = systemUserRepository.GetUserrById(systemUserRepository.LoggedInUserId);
                return user != null ? user.EmailSignature : string.Empty;
        }

        public void UpdateEmailSignature(string signature)
        {
            systemUserRepository.UpdateEmailSignature(signature);
        }
        public SystemUser UpdateSystemUser(SystemUser user)
        {
            var dbUser = systemUserRepository.GetSystemUserById(user.SystemUserId);
            if (dbUser != null)
            {
                dbUser.FullName = user.FullName;
                dbUser.RoleId = user.RoleId;
                systemUserRepository.SaveChanges();
            }
            return dbUser;
        }

        public List<SystemUser> GetAllUserByOrganisation()
        {
            return systemUserRepository.GetAll().ToList();
        }
        
    }
}
