using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface IFolderRepository : IBaseRepository<Folder, long>
    {
        List<Folder> GetFoldersByCompanyId(long CompanyID, long OrganisationID);
    }
}
