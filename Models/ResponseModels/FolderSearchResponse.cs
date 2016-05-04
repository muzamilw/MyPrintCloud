using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.Common;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class FolderSearchResponse
    {
       public List<Folder> Folders { get; set; }
        public List<Asset> Assets { get; set; }
        public List<TreeViewNodeVM> TreeView { get; set; }
    }
}
