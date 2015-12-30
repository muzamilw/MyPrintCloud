using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
  public class TreeViewNodeVM
    {
        
        
            public TreeViewNodeVM()
            {
                ChildNode = new List<TreeViewNodeVM>();
            }

            public string FolderName { get; set; }
            public long? ParentFolderId { get; set; }
            public long FolderId { get; set; }
            public string NodeName
            {
                get { return GetNodeName(); }
            }
            public IList<TreeViewNodeVM> ChildNode { get; set; }

            public string GetNodeName()
            {
                //  string temp = ChildNode.Count > 0 ? "    This employee manages " +
                //  ChildNode.Count : "    This employee is working without westing time in managing.";
                // this.FolderName; 
                return FolderName;
            }
        }

    
}
