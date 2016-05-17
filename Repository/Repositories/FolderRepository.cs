using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
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

            return db.Folders.Where(i => i.CompanyId == CompanyID && i.OrganisationId == OrganisationID && (i.ParentFolderId == null || i.ParentFolderId == 0)).OrderBy(a => a.FolderName).ToList();
        }

        public List<Folder> GetFoldersByCompanyTerritory(long companyId, long organisationId, long territoryId)
        {
            db.Configuration.LazyLoadingEnabled = false;
           
            var qry = from folder in DbSet
                join folderterritory in db.FolderTerritories on folder.FolderId equals folderterritory.FolderId
                where
                    folderterritory.TerritoryId == territoryId && folder.CompanyId == companyId &&
                    folder.OrganisationId == organisationId && (folder.ParentFolderId == null || folder.ParentFolderId == 0)
                select folder;


            return qry.OrderBy(a => a.FolderName).ToList();
        }

        public List<Folder> GetAllFoldersByCompanyTerritory(long companyId, long organisationId, long territoryId)
        {
            db.Configuration.LazyLoadingEnabled = false;

            var qry = from folder in DbSet
                      join folderterritory in db.FolderTerritories on folder.FolderId equals folderterritory.FolderId
                      where
                          folderterritory.TerritoryId == territoryId && folder.CompanyId == companyId &&
                          folder.OrganisationId == organisationId
                      select folder;


            return qry.OrderBy(a => a.FolderName).ToList();
        }

        /// <summary>
        /// used for DAM Designer to search and show the items by levels.
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="companyId"></param>
        /// <param name="organisationId"></param>
        /// <param name="territoryId"></param>
        /// <param name="ParentFolderId"></param>
        /// <returns></returns>
        public List<Folder> GetFoldersBySearchTextCompanyTerritoryParentFolder(string searchText,long companyId, long organisationId, long territoryId, Nullable<long> ParentFolderId )
        {
            db.Configuration.LazyLoadingEnabled = false;

            IQueryable<Folder> result = null;

            if (ParentFolderId.HasValue)
            {
                // search
                if (!string.IsNullOrEmpty(searchText))
                {

                    result = from folder in DbSet
                             join folderterritory in db.FolderTerritories on folder.FolderId equals folderterritory.FolderId
                             where
                                 folderterritory.TerritoryId == territoryId && folder.CompanyId == companyId &&
                                 folder.OrganisationId == organisationId
                                 && folder.ParentFolderId == ParentFolderId.Value
                                 && folder.FolderName.Contains(searchText)
                             select folder;
                }
                else //no search
                {
                    result = from folder in DbSet
                             join folderterritory in db.FolderTerritories on folder.FolderId equals folderterritory.FolderId
                             where
                                 folderterritory.TerritoryId == territoryId && folder.CompanyId == companyId &&
                                 folder.OrganisationId == organisationId
                                 && folder.ParentFolderId == ParentFolderId.Value
                             select folder;
                }

            }
            else
            {
                
                // search
                if (!string.IsNullOrEmpty(searchText))
                {

                    result = from folder in DbSet
                             join folderterritory in db.FolderTerritories on folder.FolderId equals folderterritory.FolderId
                             where
                                 folderterritory.TerritoryId == territoryId && folder.CompanyId == companyId &&
                                 folder.OrganisationId == organisationId
                                  && folder.FolderName.Contains(searchText)
                             select folder;
                }
                else // no search
                {
                    result = from folder in DbSet
                             join folderterritory in db.FolderTerritories on folder.FolderId equals folderterritory.FolderId
                             where
                                 folderterritory.TerritoryId == territoryId && folder.CompanyId == companyId &&
                                 folder.OrganisationId == organisationId

                             select folder;
                }

                
            }

            return result.OrderBy(a => a.FolderName).ToList();
        }


        /// <summary>
        /// used for DAM Designer to search and show the items by levels.
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="companyId"></param>
        /// <param name="organisationId"></param>
        
        /// <param name="ParentFolderId"></param>
        /// <returns></returns>
        public List<Folder> GetFoldersBySearchTextCompanyTerritoryParentFolder(string searchText, long companyId, long organisationId, Nullable<long> ParentFolderId)
        {
            db.Configuration.LazyLoadingEnabled = false;

            IQueryable<Folder> result = null;

            if (ParentFolderId.HasValue)
            {
                // search
                if (!string.IsNullOrEmpty(searchText))
                {

                    result = from folder in DbSet
                           
                             where
                               folder.CompanyId == companyId &&
                                 folder.OrganisationId == organisationId
                                 && folder.ParentFolderId == ParentFolderId.Value
                                 && folder.FolderName.Contains(searchText)
                             select folder;
                }
                else //no search
                {
                    result = from folder in DbSet
                            
                             where
                                folder.CompanyId == companyId &&
                                 folder.OrganisationId == organisationId
                                 && folder.ParentFolderId == ParentFolderId.Value
                             select folder;
                }

            }
            else
            {

                // search
                if (!string.IsNullOrEmpty(searchText))
                {

                    result = from folder in DbSet
                             
                             where
                                folder.CompanyId == companyId &&
                                 folder.OrganisationId == organisationId
                                  && folder.FolderName.Contains(searchText)
                             select folder;
                }
                else // no search
                {
                    result = from folder in DbSet
                            
                             where
                                folder.CompanyId == companyId &&
                                 folder.OrganisationId == organisationId

                             select folder;
                }


            }

            return result.OrderBy(a => a.FolderName).ToList();
        }


        public List<Folder> GetAllFolders(long CompanyID, long OrganisationID)
        {
            db.Configuration.LazyLoadingEnabled = false;

            return db.Folders.Where(i => i.CompanyId == CompanyID && i.OrganisationId == OrganisationID).OrderBy(a => a.FolderName).ToList();
        }
        public List<Folder> GetChildFolders(long ParentFolderId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Folders.Where(i => i.ParentFolderId == ParentFolderId ).OrderBy(i => i.FolderName).ToList();
        }

        public List<Folder> GetChildFoldersByTerritory(long parentFolderId, long territoryId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var qry = from folder in DbSet
                      join folderterritory in db.FolderTerritories on folder.FolderId equals folderterritory.FolderId
                      where
                          folderterritory.TerritoryId == territoryId && folder.ParentFolderId == parentFolderId
                      select folder;
            return qry.OrderBy(a => a.FolderName).ToList();
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

            if (NewFolder.FolderTerritories != null)
            {
               folder.FolderTerritories = new Collection<FolderTerritory>();
                foreach (var terr in NewFolder.FolderTerritories)
                {
                    var folderTerritory = new FolderTerritory
                    {
                        FolderTerritoryId = 0,
                        TerritoryId = terr.TerritoryId,
                        CompanyId = NewFolder.CompanyId,
                        OrganisationId = NewFolder.OrganisationId
                    };
                    folder.FolderTerritories.Add(folderTerritory);
                }
            }
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
            return db.Folders.Include(c => c.FolderTerritories).Where(i => i.FolderId == FolderID).FirstOrDefault();
            
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
                                       }).OrderBy(a => a.FolderName).ToList();
            foreach (var i in rootNode)
            {
                BuildChildNode(i);
                
            }
            return rootNode;
        }
        public List<TreeViewNodeVM> GetTreeVeiwListByTerritory(long companyId, long organisationId, long territoryId)
        {
            List<TreeViewNodeVM> rootNode = (from e1 in db.Folders
                                             join folderTerritory in db.FolderTerritories on e1.FolderId equals folderTerritory.FolderId
                                             where folderTerritory.TerritoryId == territoryId && e1.CompanyId == companyId && e1.OrganisationId == organisationId && (e1.ParentFolderId == null || e1.ParentFolderId == 0)
                                             select new TreeViewNodeVM()
                                             {
                                                 FolderName = e1.FolderName,
                                                 ParentFolderId = e1.ParentFolderId,
                                                 FolderId = e1.FolderId
                                             }).OrderBy(a => a.FolderName).ToList();
            foreach (var i in rootNode)
            {
                BuildChildNode(i, territoryId);

            }
            return rootNode;
        }
        public void BuildChildNode(TreeViewNodeVM rootNode, long territoryId = 0)
        {
            if (rootNode != null)
            {
                List<TreeViewNodeVM> chidNode = new List<TreeViewNodeVM>();
                if (territoryId > 0)
                {
                    chidNode = (from e1 in db.Folders
                                join folderTerritory in db.FolderTerritories on e1.FolderId equals folderTerritory.FolderId
                                where folderTerritory.TerritoryId == territoryId && e1.ParentFolderId == rootNode.FolderId
                                select new TreeViewNodeVM()
                                {
                                    FolderName = e1.FolderName,
                                    ParentFolderId = e1.ParentFolderId,
                                    FolderId = e1.FolderId
                                }).OrderBy(a => a.FolderName).ToList<TreeViewNodeVM>();
                }
                else
                {
                    chidNode = (from e1 in db.Folders
                                where e1.ParentFolderId == rootNode.FolderId
                                select new TreeViewNodeVM()
                                {
                                    FolderName = e1.FolderName,
                                    ParentFolderId = e1.ParentFolderId,
                                    FolderId = e1.FolderId
                                }).OrderBy(a => a.FolderName).ToList<TreeViewNodeVM>();
                }
                
                if (chidNode.Count > 0)
                {
                    foreach (var childRootNode in chidNode)
                    {
                        BuildChildNode(childRootNode, territoryId > 0? territoryId : 0);
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
            model.ParentFolderId = Ufolder.ParentFolderId;
           // model.ParentFolderId = Ufolder.ParentFolderId;
            if (Ufolder.ImagePath != null && Ufolder.ImagePath != string.Empty)
            {
                model.ImagePath = Ufolder.ImagePath;
            }

            //Naveed Added below code lines
            if (Ufolder.FolderTerritories != null)
            {
                foreach (var terr in Ufolder.FolderTerritories)
                {
                    var folderTerritory = model.FolderTerritories.FirstOrDefault(f => f.TerritoryId == terr.TerritoryId);
                    if (folderTerritory == null)
                    {
                        folderTerritory = new FolderTerritory
                        {
                            FolderTerritoryId = 0,
                            TerritoryId = terr.TerritoryId,
                            CompanyId = model.CompanyId,
                            OrganisationId = model.OrganisationId
                        };
                        model.FolderTerritories.Add(folderTerritory);
                    }
                }
                List<FolderTerritory> linesToBeRemoved = model.FolderTerritories.Where(
                  ft => !IsNewFolderTerritory(ft) && Ufolder.FolderTerritories.All(sourceft => sourceft.TerritoryId != ft.TerritoryId))
                    .ToList();
                linesToBeRemoved.ForEach(line => db.FolderTerritories.Remove(line));
            }
            

            //End



            db.Folders.Attach(model);
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }
        private static bool IsNewFolderTerritory(FolderTerritory sourceItem)
        {
            return sourceItem.FolderTerritoryId <= 0;
        }
        //public TreeViewNodeVM FoldersTree(long ComapnyId, long OrganisationId)
        //{
        //    return GetTreeVeiwList(ComapnyId, OrganisationId);
        //}
        public void DeleteFolder(long folderID)
        {
            List<long?> folderChilds = db.usp_GetChildFoldersById(folderID).ToList();
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
            //removingchildfolders
            //List<Folder> ChildFolders = db.Folders.Where(i => i.ParentFolderId == folderID).ToList();
            List<Folder> ChildFolders = db.Folders.Where(i => folderChilds.Contains(i.FolderId)).ToList();
            foreach (var child in ChildFolders)
            {
                List<Asset> Assetss = db.Assets.Where(i => i.FolderId == child.FolderId).ToList();
                foreach (var Asset in Assetss)
                {
                    List<AssetItem> listitem = db.AssetItems.Where(i => i.Asset.AssetId == Asset.AssetId).ToList();
                    foreach (var i in listitem)
                    {
                        db.AssetItems.Remove(i);
                    }

                    db.Assets.Remove(Asset);
                }
                db.Folders.Remove(child);
            }
            List<FolderTerritory> folderTerritories =
                db.FolderTerritories.Where(t => folderChilds.Contains(t.FolderId)).ToList();
            if(folderTerritories.Count > 0)
                folderTerritories.ForEach(t => db.FolderTerritories.Remove(t));
            //Folder folder = db.Folders.Where(i => i.FolderId == folderID).FirstOrDefault();

            //db.Folders.Remove(folder);
            db.SaveChanges();
        }

        

        
    }
   
}
