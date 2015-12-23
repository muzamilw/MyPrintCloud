using MPC.Models.Common;
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
        long AddFolder(Folder NewFolder);
        List<Folder> GetChildFolders(long ParentFolderId);
        List<Folder> GetFoldersByCompanyId(long CompanyID, long OrganisationID);
        Folder GetFolderByFolderId(long FolderID);
        bool UpdateImage(Folder folder);
       
        void BuildChildNode(TreeViewNodeVM rootNode);
        
        List<TreeViewNodeVM> GetTreeVeiwList(long CompanyId, long OrganisationId);
        List<Folder> GetAllFolders(long CompanyID, long OrganisationID);
    }
    

}
