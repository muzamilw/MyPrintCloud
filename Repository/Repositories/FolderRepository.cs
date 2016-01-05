using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
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
            db.Configuration.LazyLoadingEnabled = false;

            return db.Folders.Where(i => i.CompanyId == CompanyID && i.OrganisationId == OrganisationID && (i.ParentFolderId == null || i.ParentFolderId == 0)).ToList();
        }
        public List<Folder> GetAllFolders(long CompanyID, long OrganisationID)
        {
            db.Configuration.LazyLoadingEnabled = false;

            return db.Folders.Where(i => i.CompanyId == CompanyID && i.OrganisationId == OrganisationID).ToList();
        }
        public List<Folder> GetChildFolders(long ParentFolderId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Folders.Where(i => i.ParentFolderId == ParentFolderId ).ToList();
        }

        public long AddFolder(Folder NewFolder)
        {
            long folderID = 0;
            Folder folder=new Folder();
            folder.FolderName = NewFolder.FolderName;
            folder.ImagePath = NewFolder.ImagePath;
            folder.Description = NewFolder.Description;
            folder.ParentFolderId = NewFolder.ParentFolderId;
            folder.CompanyId = NewFolder.CompanyId;
            folder.OrganisationId = NewFolder.OrganisationId;
            db.Folders.Add(folder);
            if (db.SaveChanges() > 0)
            {
                folderID = folder.FolderId;
            }
            return folderID;
        }
        public bool UpdateImage(Folder folder)
        {
            bool result = false;
            Folder GetFolder = db.Folders.Where(i => i.FolderId == folder.FolderId).FirstOrDefault();
            GetFolder.ImagePath = folder.ImagePath;

            db.Folders.Attach(GetFolder);
            db.Entry(GetFolder).State = EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
                result = true;
            }
            return result;
        }
        public Folder GetFolderByFolderId(long FolderID)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Folders.Where(i => i.FolderId == FolderID).FirstOrDefault();
            
        }

        public List<TreeViewNodeVM> GetTreeVeiwList(long CompanyId,long OrganisationId)
        {
            List<TreeViewNodeVM> rootNode = (from e1 in db.Folders
                                       where e1.CompanyId == CompanyId && e1.OrganisationId == OrganisationId && (e1.ParentFolderId == null || e1.ParentFolderId == 0)
                                       select new TreeViewNodeVM()
                                       {
                                           FolderName = e1.FolderName,
                                           ParentFolderId = e1.ParentFolderId,
                                           FolderId = e1.FolderId
                                       }).ToList();
            foreach (var i in rootNode)
            {
                BuildChildNode(i);
                
            }
            return rootNode;
        }
        public void BuildChildNode(TreeViewNodeVM rootNode)
        {
            if (rootNode != null)
            {
                List<TreeViewNodeVM> chidNode = (from e1 in db.Folders
                                                 where e1.ParentFolderId == rootNode.FolderId
                                                 select new TreeViewNodeVM()
                                                 {
                                                     FolderName = e1.FolderName,
                                                     ParentFolderId = e1.ParentFolderId,
                                                     FolderId=e1.FolderId
                                                 }).ToList<TreeViewNodeVM>();
                if (chidNode.Count > 0)
                {
                    foreach (var childRootNode in chidNode)
                    {
                        BuildChildNode(childRootNode);
                        rootNode.ChildNode.Add(childRootNode);
                    }
                }
            }
        }

        public void UpdateFolder(Folder Ufolder)
        {
            Folder model = db.Folders.Where(i => i.FolderId == Ufolder.FolderId).FirstOrDefault();
            model.FolderName = Ufolder.FolderName;
            model.Description =Ufolder.Description;
            model.FolderId = Ufolder.FolderId;
           // model.ParentFolderId = Ufolder.ParentFolderId;
            if (Ufolder.ImagePath != null && Ufolder.ImagePath != string.Empty)
            {
                model.ImagePath = Ufolder.ImagePath;
            }
            db.Folders.Attach(model);
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        //public TreeViewNodeVM FoldersTree(long ComapnyId, long OrganisationId)
        //{
        //    return GetTreeVeiwList(ComapnyId, OrganisationId);
        //}
        public void DeleteFolder(long folderID)
        {
            List<Asset> Assets = db.Assets.Where(i => i.FolderId == folderID).ToList();
            foreach (var Asset in Assets)
            {
                List<AssetItem> listitem = db.AssetItems.Where(i => i.Asset.AssetId == Asset.AssetId).ToList();
                foreach (var i in listitem)
                {
                    db.AssetItems.Remove(i);
                }

                db.Assets.Remove(Asset);
            }
            Folder folder = db.Folders.Where(i => i.FolderId == folderID).FirstOrDefault();

            db.Folders.Remove(folder);
            db.SaveChanges();
        }
    }
   
}
