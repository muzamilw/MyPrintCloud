using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class Folder
    {
        public long FolderId { get; set; }
        public string FolderName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<long> ParentFolderId { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public Nullable<long> OrganisationId { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
        public virtual Company Company { get; set; }


        public virtual ICollection<FolderTerritory> FolderTerritories { get; set; }

    }
}
