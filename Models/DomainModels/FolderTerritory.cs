using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class FolderTerritory
    {
        public long FolderTerritoryId { get; set; }
        public Nullable<long> FolderId { get; set; }
        public Nullable<long> TerritoryId { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public Nullable<long> OrganisationId { get; set; }

        public virtual Folder Folder { get; set; }
    }
}
