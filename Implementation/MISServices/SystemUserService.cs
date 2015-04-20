﻿using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.MISServices
{
    public class SystemUserService : ISystemUserService
    {
        private readonly ISystemUserRepository systemUserRepository;
        public SystemUserService(ISystemUserRepository systemUserRepository)
        {
            this.systemUserRepository = systemUserRepository;
        }

        public bool Update(System.Guid Id, string Email, string FullName)
        {
            return systemUserRepository.Update(Id, Email, FullName);
        }
        public bool Add(System.Guid Id, string Email, string FullName, int OrganizationId)
        {
            return systemUserRepository.Add(Id, Email, FullName, OrganizationId);
        }


    }
}
