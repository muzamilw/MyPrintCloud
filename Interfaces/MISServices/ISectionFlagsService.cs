using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.MISServices
{
    public interface ISectionFlagsService
    {
        IEnumerable<SectionFlag> GetAllSectionFlag();
    }
}
