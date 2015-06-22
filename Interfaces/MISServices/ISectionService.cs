using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.MISServices
{
    public interface ISectionService
    {
        IEnumerable<Section> GetSectionsForPhraseLibrary();
        IEnumerable<SectionFlag> GetSectionFlagBySectionId(long sectionId);

        bool SaveSectionFlags(IEnumerable<SectionFlag> flags);
        IEnumerable<Section> GetSectionsForSectionFlags();
    }
}
