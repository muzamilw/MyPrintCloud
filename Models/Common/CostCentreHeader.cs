using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class CostCentreHeader
    {
        long ID {get; set;}
        string Name {get; set;}
        string Description {get; set;}
        int Type {get; set;}
        string CodeFileName {get; set;}
    }
}
